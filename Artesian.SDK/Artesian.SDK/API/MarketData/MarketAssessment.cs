using NodaTime;
using System;
using System.Collections.Generic;
using EnsureThat;
using System.Linq;
using System.Threading.Tasks;
using Artesian.SDK.Common.Dto.Api.V2;
using Artesian.SDK.API.Dto;
using Artesian.SDK.API.DTO;
using Artesian.SDK.API.Exceptions.Client;
using Artesian.SDK.Common.Dto;
using Artesian.SDK.Dependencies.MarketTools.MarketProducts;
using Artesian.SDK.Common;
using Artesian.SDK.API.MarketData.Exception;
using Artesian.SDK.Dependencies.TimeTools;
using Artesian.SDK.API.Dto.Api.V2.CurveData;

namespace Artesian.SDK.API.MarketData
{
    public class MarketAssessment
    {
        private readonly IArtesianService.Latest _ArtesianService;
        private MarketDataEntity.V2.Output _entity;

        public int MarketDataId { get { return _entity == null ? 0 : _entity.MarketDataId; } }
        public MarketDataIdentifier MarketAssessmentID { get; protected set; }
        public MarketDataTypeV2 Type { get { return MarketDataTypeV2.MarketAssessment; } }
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
        public List<AssessmentElement> Assessments { get; protected set; }
        public Dictionary<string, List<string>> Tags { get { return _entity?.Tags; } }
        public Granularity Granularity { get { return _entity == null ? default(Granularity) : _entity.OriginalGranularity; } }
        public string Timezone => _entity?.OriginalTimezone;

        public MarketAssessment(IArtesianService.Latest ArtesianService)
        {
            EnsureArg.IsNotNull(ArtesianService, nameof(ArtesianService));

            _ArtesianService = ArtesianService;
            Assessments = new List<AssessmentElement>();
        }


        internal Task Create(string provider, string name)
        {
            return Create(new MarketDataIdentifier(provider, name));
        }
        internal async Task Create(MarketDataIdentifier id)
        {
            MarketAssessmentID = id;
            _entity = await _ArtesianService.ReadMarketDataRegistryAsync(MarketAssessmentID);

        }

        public void ClearData()
        {
            Assessments.Clear();
        }

        public AddAssessmentOperationResult AddAssessment(LocalDate reportDate, string product, MarketAssessmentValue value)
        {
            if (_entity.OriginalGranularity.IsTimeGranularity())
                throw new ArtesianSdkClientException("This MarketData has Time granularity. Use AddAssessment(Instant time...)");

            return _addAssessment(reportDate.AtMidnight(), product, value);
        }
        public AddAssessmentOperationResult AddAssessment(LocalDate reportDate, IMarketProduct product, MarketAssessmentValue value)
        {
            if (_entity.OriginalGranularity.IsTimeGranularity())
                throw new ArtesianSdkClientException("This MarketData has Time granularity. Use AddAssessment(Instant time...)");

            return _addAssessment(reportDate.AtMidnight(), product, value);
        }
        public AddAssessmentOperationResult AddAssessment(Instant reportTime, string product, MarketAssessmentValue value)
        {
            if (!_entity.OriginalGranularity.IsTimeGranularity())
                throw new ArtesianSdkClientException("This MarketData has Date granularity. Use AddAssessment(LocalDate date...)");

            return _addAssessment(reportTime.InUtc().LocalDateTime, product, value);
        }
        public AddAssessmentOperationResult AddAssessment(Instant reportTime, IMarketProduct product, MarketAssessmentValue value)
        {
            if (!_entity.OriginalGranularity.IsTimeGranularity())
                throw new ArtesianSdkClientException("This MarketData has Date granularity. Use AddAssessment(LocalDate date...)");

            return _addAssessment(reportTime.InUtc().LocalDateTime, product, value);
        }

        private AddAssessmentOperationResult _addAssessment(LocalDateTime reportTime, string product, MarketAssessmentValue value)
        {
            IMarketProduct parsedProduct = default(IMarketProduct);
            if (!MarketProductBuilder.TryParse(product, out parsedProduct))
                throw new MarketAssessmentException("Given Product <{0}> is invalid and cannot be added", product);

            return _addAssessment(reportTime, parsedProduct, value);
        }

        private AddAssessmentOperationResult _addAssessment(LocalDateTime reportTime, IMarketProduct product, MarketAssessmentValue value)
        {
            switch (product.Type)
            {
                case MarketProductType.Absolute:
                    return _addAssessment(reportTime, (ProductAbsolute)product, value);
                case MarketProductType.Special:
                    return _addAssessment(reportTime, (ProductSpecial)product, value);
                case MarketProductType.Relative:
                    throw new NotSupportedException("Relative Products are not supported");
            }

            throw new NotSupportedException("Invalid Product Type");
        }

        private AddAssessmentOperationResult _addAssessment(LocalDateTime reportTime, ProductAbsolute product, MarketAssessmentValue value)
        {
            if (_entity.OriginalGranularity.IsTimeGranularity())
            {
                var period = ArtesianUtils.MapTimePeriod(_entity.OriginalGranularity);
                if (!TimeInterval.IsStartOfInterval(reportTime, period))
                    throw new ArtesianSdkClientException("Trying to insert Report Time {0} with wrong format to Assessment {1}. Should be of period {2}", reportTime, MarketAssessmentID, period);
            }
            else
            {
                var period = ArtesianUtils.MapDatePeriod(_entity.OriginalGranularity);
                if (!Dependencies.TimeTools.DateInterval.IsStartOfInterval(reportTime, period))
                    throw new ArtesianSdkClientException("Trying to insert Report Time {0} with wrong format to Assessment {1}. Should be of period {2}", reportTime, MarketAssessmentID, period);
            }


            if (reportTime.Date >= product.ReferenceDate)
                return AddAssessmentOperationResult.IllegalReferenceDate;

            if (Assessments
                    .Any(row => row.ReportTime == reportTime && row.Product.Equals(product)))
                return AddAssessmentOperationResult.ProductAlreadyPresent;

            Assessments.Add(new AssessmentElement(reportTime, product, value));
            return AddAssessmentOperationResult.AssessmentAdded;
        }

        private AddAssessmentOperationResult _addAssessment(LocalDateTime reportTime, ProductSpecial product, MarketAssessmentValue value)
        {
            if (Assessments
                    .Any(row => row.ReportTime == reportTime && row.Product.Equals(product)))
                return AddAssessmentOperationResult.ProductAlreadyPresent;

            Assessments.Add(new AssessmentElement(reportTime, product, value));
            return AddAssessmentOperationResult.AssessmentAdded;
        }

        public async Task Register(MarketDataEntity.V2.Input metadata)
        {
            EnsureArg.IsNotNull(metadata, nameof(metadata));
            EnsureArg.IsTrue(metadata.ProviderName == null || metadata.ProviderName == this.MarketAssessmentID.Provider);
            EnsureArg.IsTrue(metadata.MarketDataName == null || metadata.MarketDataName == this.MarketAssessmentID.Name);
            EnsureArg.IsNotNullOrWhiteSpace(metadata.OriginalTimezone);
            EnsureArg.IsTrue(metadata.AggregationRule == AggregationRule.Undefined);

            metadata.ProviderName = this.MarketAssessmentID.Provider;
            metadata.MarketDataName = this.MarketAssessmentID.Name;

            if (_entity != null)
                throw new MarketAssessmentException("Market Assessment is already registered with ID {0}", _entity.MarketDataId);
            _entity = await _ArtesianService.RegisterMarketDataAsync(metadata);
        }

        public async Task<bool> IsRegistered()
        {
            if (_entity == null)
                _entity = await _ArtesianService.ReadMarketDataRegistryAsync(MarketAssessmentID);

            if (_entity != null)
                return true;

            return false;
        }

        public async Task Save(Instant lastUpdate, bool deferCommandExecution = false)
        {
            Ensure.Any.IsNotNull(_entity);

            if (Assessments.Any())
            {
                var data = new UpsertCurveData(MarketAssessmentID);
                data.Timezone = DataTimezone;
                data.DownloadedAt = lastUpdate;
                data.DeferCommandExecution = deferCommandExecution;
                data.MarketAssessment = new Dictionary<LocalDateTime, IDictionary<string, MarketAssessmentValue>>();

                foreach (var reportTime in Assessments.GroupBy(g => g.ReportTime))
                {
                    var assessments = reportTime.ToDictionary(key => key.Product.ToString(), value => value.Value);
                    data.MarketAssessment.Add(reportTime.Key, assessments);
                }

                await _ArtesianService.UpsertCurveDataAsync(data);
            }
            else
            {
                // _logger.Warn("No Data to be saved.");
            }

        }

        public class AssessmentElement
        {
            public AssessmentElement(LocalDateTime reportTime, IMarketProduct product, MarketAssessmentValue value)
            {
                ReportTime = reportTime;
                Product = product;
                Value = value;
            }

            public LocalDateTime ReportTime { get; set; }
            public IMarketProduct Product { get; set; }
            public MarketAssessmentValue Value { get; set; }
        }
    }
}
