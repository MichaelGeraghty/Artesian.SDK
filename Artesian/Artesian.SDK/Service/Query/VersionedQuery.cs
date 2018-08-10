// Copyright (c) ARK LTD. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for
// license information. 
using Artesian.SDK.Dto;
using Flurl;
using NodaTime;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Artesian.SDK.Service
{
    /// <summary>
    /// Versioned Time Serie Query Class
    /// </summary>
    public class VersionedQuery : Query, IVersionedQuery<VersionedQuery>
    {
        private VersionSelectionConfig _versionSelectionCfg = new VersionSelectionConfig();
        private VersionSelectionType? _versionSelectionType = null;
        private Granularity? _granularity;
        private Client _client;
        private int? _tr;
        private string _routePrefix = "vts";

        internal VersionedQuery(Client client)
        {
            _client = client;
        }

        #region facade methods
        /// <summary>
        /// Set of marketdata ID's to be queried
        /// </summary>
        /// <param name="ids">An Int array</param>
        /// <returns>VersionedQuery</returns>
        public VersionedQuery ForMarketData(int[] ids)
        {
            _ids = ids;
            return this;
        }
        /// <summary>
        /// Marketdata ID to be queried
        /// </summary>
        /// <param name="id">An Int</param>
        /// <returns>VersionedQuery</returns>
        public VersionedQuery ForMarketData(int id)
        {
            _ids = new int[] { id };
            return this;
        }
        /// <summary>
        /// Timezone of extracted marketdata. Defaults to UTC
        /// </summary>
        /// <param name="tz">String timezone eg UTC/CET</param>
        /// <returns>VersionedQuery</returns>
        public VersionedQuery InTimezone(string tz)
        {
            _inTimezone(tz);
            return this;
        }
        /// <summary>
        /// Date range to be queried
        /// </summary>
        /// <param name="start">LocalDate start date of range</param>
        /// <param name="end">LocalDate end date of range</param>
        /// <returns>VersionedQuery</returns>
        public VersionedQuery InAbsoluteDateRange(LocalDate start, LocalDate end)
        {
            _inAbsoluteDateRange(start, end);
            return this;
        }
        /// <summary>
        /// Relative period range from today to be queried
        /// </summary>
        /// <param name="from">Period start of period range</param>
        /// <param name="to">Period end of period range</param>
        /// <returns>VersionedQuery</returns>
        public VersionedQuery InRelativePeriodRange(Period from, Period to)
        {
            _inRelativePeriodRange(from, to);
            return this;
        }
        /// <summary>
        /// Relative period from today to be queried
        /// </summary>
        /// <param name="extractionPeriod">Period</param>
        /// <returns>VersionedQuery</returns>
        public VersionedQuery InRelativePeriod(Period extractionPeriod)
        {
            _inRelativePeriod(extractionPeriod);
            return this;
        }
        /// <summary>
        /// Interval to be queried
        /// </summary>
        /// <param name="relativeInterval">RelativeInterval</param>
        /// <returns>VersionedQuery</returns>
        public VersionedQuery InRelativeInterval(RelativeInterval relativeInterval)
        {
            _inRelativeInterval(relativeInterval);
            return this;
        }
        /// <summary>
        /// Time Transform to be applied to query
        /// </summary>
        /// <param name="tr">An Int GASDAY66=1/THERMALYEAR=2</param>
        /// <returns>VersionedQuery</returns>
        public VersionedQuery WithTimeTransform(int tr)
        {
            _tr = tr;
            return this;
        }
        /// <summary>
        /// Time Transform to be applied to query
        /// </summary>
        /// <param name="tr">SystemTimeTransform GASDAY66/THERMALYEAR</param>
        /// <returns>VersionedQuery</returns>
        public VersionedQuery WithTimeTransform(SystemTimeTransform tr)
        {
            _tr = (int)tr;
            return this;
        }
        #endregion

        #region versioned query methods
        /// <summary>
        /// Granularity of the extracted marketdata
        /// </summary>
        /// <param name="granularity">Granularity <see cref="Granularity"/> for types of Granularity</param>
        /// <returns>VersionedQuery</returns>
        public VersionedQuery InGranularity(Granularity granularity)
        {
            _granularity = granularity;
            return this;
        }
        /// <summary>
        /// For LastNVersions version selection
        /// </summary>
        /// <param name="lastN">An Int</param>
        /// <returns>VersionedQuery</returns>
        public VersionedQuery ForLastNVersions(int lastN)
        {
            _versionSelectionType = VersionSelectionType.LastN;
            _versionSelectionCfg.LastN = lastN;
            return this;
        }
        /// <summary>
        /// For Most updated version selection
        /// </summary>
        /// <returns>VersionedQuery</returns>
        public VersionedQuery ForMUV()
        {
            _versionSelectionType = VersionSelectionType.MUV;
            return this;
        }
        /// <summary>
        /// For Last Of Days date range version selection
        /// </summary>
        /// <param name="start">LocalDate</param>
        /// <param name="end">LocalDate</param>
        /// <returns>VersionedQuery</returns>
        public VersionedQuery ForLastOfDays(LocalDate start, LocalDate end)
        {
            if (end <= start)
                throw new ArgumentException("End date " + end + " must be greater than start date " + start);

            _versionSelectionType = VersionSelectionType.LastOfDays;
            _versionSelectionCfg.LastOf.DateStart = start;
            _versionSelectionCfg.LastOf.DateEnd = end;

            return this;
        }
        /// <summary>
        /// For Last Of Days period range version selection
        /// </summary>
        /// <param name="from">Period</param>
        /// <param name="to">Period</param>
        /// <returns>VersionedQuery</returns>
        public VersionedQuery ForLastOfDays(Period from, Period to)
        {
            _versionSelectionType = VersionSelectionType.LastOfDays;
            _versionSelectionCfg.LastOf.PeriodFrom = from;
            _versionSelectionCfg.LastOf.PeriodTo = to;

            return this;
        }
        /// <summary>
        /// For Last Of Days period version selection
        /// </summary>
        /// <param name="lastOfPeriod">Period</param>
        /// <returns>VersionedQuery</returns>
        public VersionedQuery ForLastOfDays(Period lastOfPeriod)
        {
            _versionSelectionType = VersionSelectionType.LastOfDays;
            _versionSelectionCfg.LastOf.Period = lastOfPeriod;

            return this;
        }
        /// <summary>
        /// For Last of Months date range
        /// </summary>
        /// <param name="start">LocalDate</param>
        /// <param name="end">LocalDate</param>
        /// <returns>VersionedQuery</returns>
        public VersionedQuery ForLastOfMonths(LocalDate start, LocalDate end)
        {
            if (end <= start)
                throw new ArgumentException("End date " + end + " must be greater than start date " + start);

            _versionSelectionType = VersionSelectionType.LastOfMonths;
            _versionSelectionCfg.LastOf.DateStart = start;
            _versionSelectionCfg.LastOf.DateEnd = end;

            return this;
        }
        /// <summary>
        /// For Last of Months period
        /// </summary>
        /// <param name="lastOfPeriod">Period</param>
        /// <returns>VersionedQuery</returns>
        public VersionedQuery ForLastOfMonths(Period lastOfPeriod)
        {
            _versionSelectionType = VersionSelectionType.LastOfMonths;
            _versionSelectionCfg.LastOf.Period = lastOfPeriod;

            return this;
        }
        /// <summary>
        /// For Last of Months period range
        /// </summary>
        /// <param name="from">Period</param>
        /// <param name="to">Period</param>
        /// <returns>VersionedQuery</returns>
        public VersionedQuery ForLastOfMonths(Period from, Period to)
        {
            _versionSelectionType = VersionSelectionType.LastOfMonths;
            _versionSelectionCfg.LastOf.PeriodFrom = from;
            _versionSelectionCfg.LastOf.PeriodTo = to;

            return this;
        }
        /// <summary>
        /// For Version with LocalDateTime
        /// </summary>
        /// <param name="version">LocalDateTime</param>
        /// <returns>VersionedQuery</returns>
        public VersionedQuery ForVersion(LocalDateTime version)
        {
            _versionSelectionType = VersionSelectionType.Version;
            _versionSelectionCfg.Version = version;

            return this;
        }
        /// <summary>
        /// Execute VersionedQuery
        /// </summary>
        /// <param name="ctk"></param>
        /// <returns>client.Exec() <see cref="Client.Exec{TResult}(HttpMethod, string, CancellationToken)"/></returns>
        public async Task<IEnumerable<TimeSerieRow.Versioned>> ExecuteAsync(CancellationToken ctk = default)
        {
            return await _client.Exec<IEnumerable<TimeSerieRow.Versioned>>(HttpMethod.Get, _buildRequest(), ctk: ctk);
        }

        #region private
        protected override void _validateQuery()
        {
            base._validateQuery();

            if (_granularity == null)
                throw new ApplicationException("Extraction granularity must be provided. Use .InGranularity() argument takes a granularity type");

            if (_versionSelectionType == null)
                throw new ApplicationException("Version selection must be provided. Provide a version to query. eg .ForLastOfDays() arguments take a date range , period or period range");
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
                    subPath = $"Version/{_toUrlParam(_versionSelectionCfg.Version)}";
                    break;
                default:
                    throw new Exception("Unsupported version type");
            }

            return subPath;
        }

        private string _buildLastOfSubRoute()
        {
            string subPath;

            if (_versionSelectionCfg.LastOf.DateStart != null && _versionSelectionCfg.LastOf.DateEnd !=null)
                subPath = $"{_versionSelectionType}/{_toUrlParam(_versionSelectionCfg.LastOf.DateStart.Value,_versionSelectionCfg.LastOf.DateEnd.Value)}";
            else if (_versionSelectionCfg.LastOf.Period != null)
                subPath = $"{_versionSelectionType}/{_versionSelectionCfg.LastOf.Period}";
            else if (_versionSelectionCfg.LastOf.PeriodFrom != null && _versionSelectionCfg.LastOf.PeriodTo != null)
                subPath = $"{_versionSelectionType}/{_versionSelectionCfg.LastOf.PeriodFrom}/{_versionSelectionCfg.LastOf.PeriodTo}";
            else
                throw new ApplicationException("LastOf extraction type not defined");

            return subPath;
        }

        string _buildRequest()
        {
            _validateQuery();

            var url = $"/{_routePrefix}/{_buildVersionRoute()}/{_granularity}/{_buildExtractionRangeRoute()}"
                        .SetQueryParam("id", _ids)
                        .SetQueryParam("tz", _tz)
                        .SetQueryParam("tr", _tr);

            return url.ToString();
        } 
        #endregion

        #endregion

    }
}