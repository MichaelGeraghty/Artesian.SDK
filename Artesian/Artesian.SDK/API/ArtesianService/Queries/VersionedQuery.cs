using Artesian.SDK.API.ArtesianService.Config;
using Artesian.SDK.API.ArtesianService.Interface;
using Artesian.SDK.API.DTO;
using Artesian.SDK.ArtesianService.Clients;
using Artesian.SDK.Common;
using Artesian.SDK.Dependencies;
using NodaTime;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Artesian.SDK.API.ArtesianService.Queries
{
    public class VersionedQuery : ArkiveQuery, IVersionedQuery<VersionedQuery>
    {
        private VersionSelectionConfig _versionSelectionCfg = new VersionSelectionConfig();
        private VersionSelectionType? _versionSelectionType = null;
        private Granularity? _granularity;
        private Auth0Client _client;
        private int? _tr;
        private string _routePrefix = "vts";


        internal VersionedQuery(int[] ids, Granularity granularity, Auth0Client client)
        {
            _forMarketData(ids);
            _granularity = granularity;
            _client = client;
        }

        #region facade methods
        public VersionedQuery InTimezone(string tz)
        {
            _inTimezone(tz);
            return this;
        }

        public VersionedQuery InAbsoluteDateRange(LocalDateRange extractionDateRange)
        {

            _inAbsoluteDateRange(extractionDateRange);
            return this;
        }

        public VersionedQuery InRelativePeriodRange(PeriodRange extractionPeriodRange)
        {
            _inRelativePeriodRange(extractionPeriodRange);
            return this;
        }

        public VersionedQuery InRelativePeriod(Period extractionPeriod)
        {
            _inRelativePeriod(extractionPeriod);
            return this;
        }

        public VersionedQuery InRelativeInterval(RelativeInterval relativeInterval)
        {
            _inRelativeInterval(relativeInterval);
            return this;
        }

        public VersionedQuery WithTimeTransform(int tr)
        {
            _tr = tr;
            return this;
        }
        #endregion


        #region versioned query methods
        public VersionedQuery ForLastNVersions(int lastN)
        {
            _versionSelectionType = VersionSelectionType.LastN;
            _versionSelectionCfg.LastN = lastN;
            return this;
        }

        public VersionedQuery ForMUV()
        {
            _versionSelectionType = VersionSelectionType.MUV;
            return this;
        }

        public VersionedQuery ForLastOfDays(LocalDateRange lastOfDateRange)
        {
            _versionSelectionType = VersionSelectionType.LastOfDays;
            _versionSelectionCfg.LastOf.DateRange = lastOfDateRange;

            return this;
        }

        public VersionedQuery ForLastOfDays(Period lastOfPeriod)
        {
            _versionSelectionType = VersionSelectionType.LastOfDays;
            _versionSelectionCfg.LastOf.Period = lastOfPeriod;

            return this;
        }

        public VersionedQuery ForLastOfDays(PeriodRange lastOfPeriodRange)
        {
            _versionSelectionType = VersionSelectionType.LastOfDays;
            _versionSelectionCfg.LastOf.PeriodRange = lastOfPeriodRange;

            return this;
        }

        public VersionedQuery ForLastOfMonths(LocalDateRange lastOfDateRange)
        {
            _versionSelectionType = VersionSelectionType.LastOfMonths;
            _versionSelectionCfg.LastOf.DateRange = lastOfDateRange;

            return this;
        }

        public VersionedQuery ForLastOfMonths(Period lastOfPeriod)
        {
            _versionSelectionType = VersionSelectionType.LastOfMonths;
            _versionSelectionCfg.LastOf.Period = lastOfPeriod;

            return this;
        }

        public VersionedQuery ForLastOfMonths(PeriodRange lastOfPeriodRange)
        {
            _versionSelectionType = VersionSelectionType.LastOfMonths;
            _versionSelectionCfg.LastOf.PeriodRange = lastOfPeriodRange;

            return this;
        }

        public VersionedQuery ForVersion(LocalDateTime version)
        {
            _versionSelectionType = VersionSelectionType.Version;
            _versionSelectionCfg.Version = version;

            return this;
        }

        private string _buildVersionRoute()
        {
            string subPath;

            switch (_versionSelectionType.Value)
            {
                case VersionSelectionType.LastN:
                    subPath = $"Last{_versionSelectionCfg.LastN}";
                    break;
                case VersionSelectionType.MUV:
                    subPath = $"MUV";
                    break;
                case VersionSelectionType.LastOfDays:
                case VersionSelectionType.LastOfMonths:
                    subPath = _buildLastOfSubRoute();
                    break;
                case VersionSelectionType.Version:
                    subPath = $"Version/{UrlComposer.ToUrlParam(_versionSelectionCfg.Version)}";
                    break;
                default:
                    throw new Exception("Unsupported version type");
            }

            return subPath;
        }

        private string _buildLastOfSubRoute()
        {
            string subPath;

            if (_versionSelectionCfg.LastOf.DateRange != null)
                subPath = $"{_versionSelectionType}/{UrlComposer.ToUrlParam(_versionSelectionCfg.LastOf.DateRange)}";
            else if (_versionSelectionCfg.LastOf.Period != null)
                subPath = $"{_versionSelectionType}/{_versionSelectionCfg.LastOf.Period}";
            else if (_versionSelectionCfg.LastOf.PeriodRange != null)
                subPath = $"{_versionSelectionType}/{_versionSelectionCfg.LastOf.PeriodRange.From}/{_versionSelectionCfg.LastOf.PeriodRange.To}";
            else
                throw new ApplicationException("LastOf extraction type not defined");

            return subPath;
        }

        public string Build()
        {
            _validateQuery();

            var url = new UrlComposer($"/{_routePrefix}/{_buildVersionRoute()}/{_granularity}/{_buildExtractionRangeRoute()}")
                        .AddQueryParam("id", _ids)
                        .AddQueryParam("tz", _tz)
                        .AddQueryParam("tr", _tr);

            return url.ToString();
        }

        public async Task<IEnumerable<TimeSerieRow.Versioned.V1_0>> ExecuteAsync()
        {
            return await _client.Exec<IEnumerable<TimeSerieRow.Versioned.V1_0>>(HttpMethod.Get, Build());
        }

        protected override void _validateQuery()
        {
            base._validateQuery();

            if (_granularity == null)
                throw new ApplicationException("Extraction granularity must be provided");

            if (_versionSelectionType == null)
                throw new ApplicationException("Version selection must be provided");
        }
        #endregion

    }
}
