using Artesian.SDK.API.Dto;
using Artesian.SDK.API.Dto.Api.V2.CurveData;
using Artesian.SDK.API.DTO;
using Artesian.SDK.API.Exceptions.Client;
using Artesian.SDK.API.MarketData.Exception;
using Artesian.SDK.Common;
using Artesian.SDK.Common.Dto.Api.V2;
using Artesian.SDK.Dependencies.TimeTools;
using EnsureThat;
using NodaTime;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Artesian.SDK.API.MarketData
{
    public class VersionedTimeSerie
    {
        private readonly IArtesianService.Latest _ArtesianService;
        private MarketDataEntity.V2.Output _entity;

        public int MarketDataId { get { return _entity == null ? 0 : _entity.MarketDataId; } }
        public MarketDataIdentifier VersionedTimeSerieID { get; protected set; }
        public LocalDateTime? SelectedVersion { get; protected set; }
        public string DataTimezone
        {
            get
            {
                if (_entity?.OriginalGranularity.IsTimeGranularity() == true)
                    return "UTC";
                else
                    return _entity?.OriginalTimezone;
            }
        }
        private Dictionary<LocalDateTime, double?> _values = new Dictionary<LocalDateTime, double?>();
        public IReadOnlyDictionary<LocalDateTime, double?> Values { get; private set; }
        public MarketDataTypeV2 Type { get { return MarketDataTypeV2.VersionedTimeSerie; } }
        public Dictionary<string, List<string>> Tags { get { return _entity == null ? null : _entity.Tags; } }
        public Granularity Granularity { get { return _entity == null ? default(Granularity) : _entity.OriginalGranularity; } }
        public string Timezone => _entity?.OriginalTimezone;

        public VersionedTimeSerie(IArtesianService.Latest ArtesianService)
        {
            EnsureArg.IsNotNull(ArtesianService, nameof(ArtesianService));

            _ArtesianService = ArtesianService;
            Values = new ReadOnlyDictionary<LocalDateTime, double?>(_values);
        }


        internal Task Create(string provider, string name)
        {
            return Create(new MarketDataIdentifier(provider, name));
        }

        internal async Task Create(MarketDataIdentifier id)
        {
            VersionedTimeSerieID = id;
            _entity = await _ArtesianService.ReadMarketDataRegistryAsync(VersionedTimeSerieID);
        }

        public void SetSelectedVersion(LocalDateTime version)
        {
            if ((SelectedVersion.HasValue) && (Values.Count != 0))
                throw new VersionedTimeSerieException("SelectedVersion can't be changed if curve contains values. Current Version is {0}", SelectedVersion.Value);

            SelectedVersion = version;
        }


        public void ClearData()
        {
            _values = new Dictionary<LocalDateTime, double?>();
            Values = new ReadOnlyDictionary<LocalDateTime, double?>(_values);
        }


        public AddTimeSerieValueOperationResult AddValue(LocalDate date, double? value)
        {
            Ensure.Any.IsNotNull(_entity);

            if (_entity.OriginalGranularity.IsTimeGranularity())
                throw new ArtesianSdkClientException("This MarketData has Time granularity. Use AddValue(Instant time, double? value)");

            var localTime = date.AtMidnight();

            return _add(localTime, value);
        }
        public AddTimeSerieValueOperationResult AddValue(Instant time, double? value)
        {
            Ensure.Any.IsNotNull(_entity);

            if (!_entity.OriginalGranularity.IsTimeGranularity())
                throw new ArtesianSdkClientException("This MarketData has Date granularity. Use AddValue(LocalDate date, double? value)");

            var localTime = time.InUtc().LocalDateTime;

            return _add(localTime, value);
        }

        private AddTimeSerieValueOperationResult _add(LocalDateTime localTime, double? value)
        {
            if (_values.ContainsKey(localTime))
                return AddTimeSerieValueOperationResult.TimeAlreadyPresent;

            if (_entity.OriginalGranularity.IsTimeGranularity())
            {
                var period = ArtesianUtils.MapTimePeriod(_entity.OriginalGranularity);
                if (!TimeInterval.IsStartOfInterval(localTime, period))
                    throw new ArtesianSdkClientException("Trying to insert Time {0} with wrong format to serie {1}. Should be of period {2}", localTime, VersionedTimeSerieID, period);
            }
            else
            {
                var period = ArtesianUtils.MapDatePeriod(_entity.OriginalGranularity);
                if (!Dependencies.TimeTools.DateInterval.IsStartOfInterval(localTime, period))
                    throw new ArtesianSdkClientException("Trying to insert Time {0} with wrong format to serie {1}. Should be of period {2}", localTime, VersionedTimeSerieID, period);
            }

            _values.Add(localTime, value);
            return AddTimeSerieValueOperationResult.ValueAdded;
        }

        public async Task Register(MarketDataEntity.V2.Input metadata)
        {
            EnsureArg.IsNotNull(metadata, nameof(metadata));
            EnsureArg.IsTrue(metadata.ProviderName == null || metadata.ProviderName == this.VersionedTimeSerieID.Provider);
            EnsureArg.IsTrue(metadata.ProviderName == null || metadata.ProviderName == this.VersionedTimeSerieID.Provider);
            EnsureArg.IsTrue(metadata.MarketDataName == null || metadata.MarketDataName == this.VersionedTimeSerieID.Name);
            EnsureArg.IsNotNullOrWhiteSpace(metadata.OriginalTimezone);

            metadata.ProviderName = this.VersionedTimeSerieID.Provider;
            metadata.MarketDataName = this.VersionedTimeSerieID.Name;

            if (_entity != null)
                throw new VersionedTimeSerieException("Versioned Time Serie is already registered with ID {0}", _entity.MarketDataId);
            _entity = await _ArtesianService.RegisterMarketDataAsync(metadata);
        }

        public async Task<bool> IsRegistered()
        {
            if (_entity == null)
                _entity = await _ArtesianService.ReadMarketDataRegistryAsync(VersionedTimeSerieID);

            if (_entity != null)
                return true;

            return false;
        }

        public async Task Save(Instant downloadedAt, bool deferCommandExecution = false, bool deferDataGeneration = true)
        {
            Ensure.Bool.IsTrue(_entity != null, "Market Data is not registred in Artesian.");

            if (!SelectedVersion.HasValue)
                throw new VersionedTimeSerieException("No Version Has been selected to save Data");

            if (Values.Any())
            {
                var data = new UpsertCurveData(VersionedTimeSerieID, SelectedVersion.Value)
                {
                    Timezone = _entity.OriginalGranularity.IsTimeGranularity() ? "UTC" : _entity.OriginalTimezone,
                    DownloadedAt = downloadedAt,
                    Rows = _values,
                    DeferCommandExecution = deferCommandExecution,
                    DeferDataGeneration = deferDataGeneration
                };

                await _ArtesianService.UpsertCurveDataAsync(data);
            }
            else
            {
                //_logger.Warn("No Data to be saved.");
            }
        }

    }
}