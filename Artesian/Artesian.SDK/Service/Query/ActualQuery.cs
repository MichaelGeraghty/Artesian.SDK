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
    /// Actual Time Serie Query Class
    /// </summary>
    public class ActualQuery : Query, IActualQuery<ActualQuery>
    {
        protected Granularity? _granularity;
        private Client _client;
        protected int? _tr;
        private string _routePrefix = "ts";

        internal ActualQuery(Client client)
        {
            _client = client;
        }

        #region facade methods
        /// <summary>
        /// Set of marketdata ID's to be queried
        /// </summary>
        /// <param name="ids">An Int array</param>
        /// <returns>ActualQuery</returns>
        public ActualQuery ForMarketData(int[] ids)
        {
            _ids = ids;
            return this;
        }
        /// <summary>
        /// Marketdata ID to be queried
        /// </summary>
        /// <param name="id">An Int</param>
        /// <returns>ActualQuery</returns>
        public ActualQuery ForMarketData(int id)
        {
            _ids = new int[] { id };
            return this;
        }
        /// <summary>
        /// Timezone of extracted marketdata. Defaults to UTC
        /// </summary>
        /// <param name="tz">String</param>
        /// <returns>ActualQuery</returns>
        public ActualQuery InTimezone(string tz)
        {
            _inTimezone(tz);
            return this;
        }
        /// <summary>
        /// Date range to be queried
        /// </summary>
        /// <param name="start">LocalDate</param>
        /// <param name="end">LocalDate</param>
        /// <returns>ActualQuery</returns>
        public ActualQuery InAbsoluteDateRange(LocalDate start, LocalDate end)
        {
            _inAbsoluteDateRange(start, end);
            return this;
        }
        /// <summary>
        /// Period Range to be queried
        /// </summary>
        /// <param name="from">Period</param>
        /// <param name="to">Period</param>
        /// <returns>ActualQuery</returns>
        public ActualQuery InRelativePeriodRange(Period from, Period to)
        {
            _inRelativePeriodRange(from, to);
            return this;
        }
        /// <summary>
        /// Relative period from today to be queried
        /// </summary>
        /// <param name="extractionPeriod">Period</param>
        /// <returns>ActualQuery</returns>
        public ActualQuery InRelativePeriod(Period extractionPeriod)
        {
            _inRelativePeriod(extractionPeriod);
            return this;
        }
        /// <summary>
        /// Interval to be queried
        /// </summary>
        /// <param name="relativeInterval">RelativeInterval</param>
        /// <returns>ActualQuery</returns>
        public ActualQuery InRelativeInterval(RelativeInterval relativeInterval)
        {
            _inRelativeInterval(relativeInterval);
            return this;
        }
        /// <summary>
        /// Time Transform to be applied to query
        /// </summary>
        /// <param name="tr">An Int</param>
        /// <returns>ActualQuery</returns>
        public ActualQuery WithTimeTransform(int tr)
        {
            _tr = tr;
            return this;
        }
        /// <summary>
        /// Time Transform to be applied to query
        /// </summary>
        /// <param name="tr">SystemTimeTransform</param>
        /// <returns>ActualQuery</returns>
        public ActualQuery WithTimeTransform(SystemTimeTransform tr)
        {
            _tr = (int)tr;
            return this;
        }
        #endregion

        #region actual query methods
        /// <summary>
        /// Granularity of the extracted marketdata
        /// </summary>
        /// <param name="granularity">Granularity</param>
        /// <returns>ActualQuery</returns>
        public ActualQuery InGranularity(Granularity granularity)
        {
            _granularity = granularity;
            return this;
        }
        /// <summary>
        /// Execute ActualQuery
        /// </summary>
        /// <param name="ctk">CancellationToken</param>
        /// <returns>client.Exec() <see cref="Client.Exec{TResult}(HttpMethod, string, CancellationToken)"/></returns>
        public async Task<IEnumerable<TimeSerieRow.Actual>> ExecuteAsync(CancellationToken ctk = default)
        {
            return await _client.Exec<IEnumerable<TimeSerieRow.Actual>>(HttpMethod.Get, _buildRequest(), ctk: ctk);
        }

        #region private
        string _buildRequest()
        {
            _validateQuery();

            var url = $"/{_routePrefix}/{_granularity}/{_buildExtractionRangeRoute()}"
                .SetQueryParam("id", _ids)
                .SetQueryParam("tz", _tz)
                .SetQueryParam("tr", _tr);

            return url.ToString();
        }

        protected sealed override void _validateQuery()
        {
            base._validateQuery();

            if (_granularity == null)
                throw new ApplicationException("Extraction granularity must be provided");
        } 
        #endregion
        #endregion
    }
}