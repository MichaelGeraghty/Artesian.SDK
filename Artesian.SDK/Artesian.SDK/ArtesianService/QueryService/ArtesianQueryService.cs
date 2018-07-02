
using NodaTime;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Artesian.SDK.Dependencies.MarketTools.MarketProducts;
using System.Net.Http;
using Artesian.SDK.Common;
using Artesian.SDK.API.Configuration;
using Artesian.SDK.ArtesianService.Clients;
using Artesian.SDK.API.ArtesianService;
using Artesian.SDK.Dependencies;
using Artesian.SDK.API.DTO;

namespace Artesian.SDK.ArtesianService.QueryService
{
    internal static class ArtesianQueryService
    {
        public class Latest : IArtesianQueryService.Latest, IDisposable
        {
            private readonly Auth0Client _client;

            public Latest(ArtesianServiceConfig config, Func<HttpMessageHandler> httpMessageHandler)
            {
                this._client = new Auth0Client(config, httpMessageHandler, $"query/{ArtesianConstants.QueryVersions.Last()}");
            }

            public Task<IEnumerable<AssessmentRow.V2>> GetMarketAssessments(IEnumerable<int> ids, IEnumerable<IMarketProduct> products, LocalDateRange range, string tz = null, CancellationToken ctk = default)
            {
                var url = new UrlComposer($"/mas/{UrlComposer.ToUrlParam(range)}")
                    .AddQueryParam("id", ids)
                    .AddQueryParam("p", products)
                    .AddQueryParam("tz", tz)
                    ;

                return _client.Exec<IEnumerable<AssessmentRow.V2>>(HttpMethod.Get, url, ctk: ctk);
            }
            public Task<IEnumerable<AssessmentRow.V2>> GetMarketAssessments(IEnumerable<int> ids, IEnumerable<IMarketProduct> products, RelativeInterval interval, LocalDate? analysisDate = null, string tz = null, CancellationToken ctk = default)
            {
                var url = new UrlComposer($"/mas/{interval}")
                    .AddQueryParam("id", ids)
                    .AddQueryParam("p", products)
                    .AddQueryParam("tz", tz)
                    .AddQueryParam("ad", analysisDate)
                    ;

                return _client.Exec<IEnumerable<AssessmentRow.V2>>(HttpMethod.Get, url, ctk: ctk);
            }
            public Task<IEnumerable<AssessmentRow.V2>> GetMarketAssessments(IEnumerable<int> ids, IEnumerable<IMarketProduct> products, Period period, LocalDate? analysisDate = null, string tz = null, CancellationToken ctk = default)
            {
                var url = new UrlComposer($"/mas/{period}")
                    .AddQueryParam("id", ids)
                    .AddQueryParam("p", products)
                    .AddQueryParam("tz", tz)
                    .AddQueryParam("ad", analysisDate)
                    ;

                return _client.Exec<IEnumerable<AssessmentRow.V2>>(HttpMethod.Get, url, ctk: ctk);
            }
            public Task<IEnumerable<AssessmentRow.V2>> GetMarketAssessments(IEnumerable<int> ids, IEnumerable<IMarketProduct> products, Period periodFrom, Period periodTo, LocalDate? analysisDate = null, string tz = null, CancellationToken ctk = default)
            {
                var url = new UrlComposer($"/mas/{periodFrom}/{periodTo}")
                    .AddQueryParam("id", ids)
                    .AddQueryParam("p", products)
                    .AddQueryParam("tz", tz)
                    .AddQueryParam("ad", analysisDate)
                    ;

                return _client.Exec<IEnumerable<AssessmentRow.V2>>(HttpMethod.Get, url, ctk: ctk);
            }

            public Task<IEnumerable<TimeSerieRow.Actual.V1_0>> GetActualTimeSeries(IEnumerable<int> ids, Granularity granularity, LocalDateRange range, string tz = null, int? transformid = null, CancellationToken ctk = default)
            {
                var url = new UrlComposer($"/ts/{granularity}/{UrlComposer.ToUrlParam(range)}")
                    .AddQueryParam("id", ids)
                    .AddQueryParam("tz", tz)
                    .AddQueryParam("tr", transformid)
                    ;

                return _client.Exec<IEnumerable<TimeSerieRow.Actual.V1_0>>(HttpMethod.Get, url, ctk: ctk);
            }
            public Task<IEnumerable<TimeSerieRow.Actual.V1_0>> GetActualTimeSeries(IEnumerable<int> ids, Granularity granularity, RelativeInterval interval, LocalDate? analysisDate = null, string tz = null, int? transformid = null, CancellationToken ctk = default)
            {
                var url = new UrlComposer($"/ts/{granularity}/{interval}")
                    .AddQueryParam("id", ids)
                    .AddQueryParam("tz", tz)
                    .AddQueryParam("tr", transformid)
                    .AddQueryParam("ad", analysisDate)
                    ;

                return _client.Exec<IEnumerable<TimeSerieRow.Actual.V1_0>>(HttpMethod.Get, url, ctk: ctk);
            }
            public Task<IEnumerable<TimeSerieRow.Actual.V1_0>> GetActualTimeSeries(IEnumerable<int> ids, Granularity granularity, Period period, LocalDate? analysisDate = null, string tz = null, int? transformid = null, CancellationToken ctk = default(CancellationToken))
            {
                var url = new UrlComposer($"/ts/{granularity}/{period}")
                    .AddQueryParam("id", ids)
                    .AddQueryParam("tz", tz)
                    .AddQueryParam("tr", transformid)
                    .AddQueryParam("ad", analysisDate)
    ;
                return _client.Exec<IEnumerable<TimeSerieRow.Actual.V1_0>>(HttpMethod.Get, url, ctk: ctk);
            }
            public Task<IEnumerable<TimeSerieRow.Actual.V1_0>> GetActualTimeSeries(IEnumerable<int> ids, Granularity granularity, Period periodFrom, Period periodTo, LocalDate? analysisDate = null, string tz = null, int? transformid = null, CancellationToken ctk = default(CancellationToken))
            {
                var url = new UrlComposer($"/ts/{granularity}/{periodFrom}/{periodTo}")
                    .AddQueryParam("id", ids)
                    .AddQueryParam("tz", tz)
                    .AddQueryParam("tr", transformid)
                    .AddQueryParam("ad", analysisDate)
                ;
                return _client.Exec<IEnumerable<TimeSerieRow.Actual.V1_0>>(HttpMethod.Get, url, ctk: ctk);
            }

            public Task<IEnumerable<TimeSerieRow.Versioned.V1_0>> GetVersionedTimeSeriesLastOfDay(IEnumerable<int> ids, Granularity granularity, LocalDateRange versionRange, LocalDateRange range, string tz = null, int? transformid = null, CancellationToken ctk = default)
            {
                var url = new UrlComposer($"/vts/LastOfDays/{UrlComposer.ToUrlParam(versionRange)}/{granularity}/{UrlComposer.ToUrlParam(range)}")
                    .AddQueryParam("id", ids)
                    .AddQueryParam("tz", tz)
                    .AddQueryParam("tr", transformid)
                    ;

                return _client.Exec<IEnumerable<TimeSerieRow.Versioned.V1_0>>(HttpMethod.Get, url, ctk: ctk);
            }
            public Task<IEnumerable<TimeSerieRow.Versioned.V1_0>> GetVersionedTimeSeriesLastOfDay(IEnumerable<int> ids, Granularity granularity, Period vPeriod, LocalDateRange range, LocalDate? analysisDate = null, string tz = null, int? transformid = null, CancellationToken ctk = default)
            {
                var url = new UrlComposer($"/vts/LastOfDays/{vPeriod}/{granularity}/{UrlComposer.ToUrlParam(range)}")
                    .AddQueryParam("id", ids)
                    .AddQueryParam("tz", tz)
                    .AddQueryParam("tr", transformid)
                    .AddQueryParam("ad", analysisDate)
                    ;

                return _client.Exec<IEnumerable<TimeSerieRow.Versioned.V1_0>>(HttpMethod.Get, url, ctk: ctk);
            }
            public Task<IEnumerable<TimeSerieRow.Versioned.V1_0>> GetVersionedTimeSeriesLastOfDay(IEnumerable<int> ids, Granularity granularity, Period vPeriodFrom, Period vPeriodTo, LocalDateRange range, LocalDate? analysisDate = null, string tz = null, int? transformid = null, CancellationToken ctk = default)
            {
                var url = new UrlComposer($"/vts/LastOfDays/{vPeriodFrom}/{vPeriodTo}/{granularity}/{UrlComposer.ToUrlParam(range)}")
                    .AddQueryParam("id", ids)
                    .AddQueryParam("tz", tz)
                    .AddQueryParam("tr", transformid)
                    .AddQueryParam("ad", analysisDate)
                    ;

                return _client.Exec<IEnumerable<TimeSerieRow.Versioned.V1_0>>(HttpMethod.Get, url, ctk: ctk);
            }
            public Task<IEnumerable<TimeSerieRow.Versioned.V1_0>> GetVersionedTimeSeriesLastOfDay(IEnumerable<int> ids, Granularity granularity, LocalDateRange versionRange, RelativeInterval interval, LocalDate? analysisDate = null, string tz = null, int? transformid = null, CancellationToken ctk = default)
            {
                var url = new UrlComposer($"/vts/LastOfDays/{UrlComposer.ToUrlParam(versionRange)}/{granularity}/{interval}")
                    .AddQueryParam("id", ids)
                    .AddQueryParam("tz", tz)
                    .AddQueryParam("tr", transformid)
                    .AddQueryParam("ad", analysisDate)
                    ;

                return _client.Exec<IEnumerable<TimeSerieRow.Versioned.V1_0>>(HttpMethod.Get, url, ctk: ctk);
            }
            public Task<IEnumerable<TimeSerieRow.Versioned.V1_0>> GetVersionedTimeSeriesLastOfDay(IEnumerable<int> ids, Granularity granularity, Period vPeriod, RelativeInterval interval, LocalDate? analysisDate = null, string tz = null, int? transformid = null, CancellationToken ctk = default)
            {
                var url = new UrlComposer($"/vts/LastOfDays/{vPeriod}/{granularity}/{interval}")
                    .AddQueryParam("id", ids)
                    .AddQueryParam("tz", tz)
                    .AddQueryParam("tr", transformid)
                    .AddQueryParam("ad", analysisDate)
                    ;

                return _client.Exec<IEnumerable<TimeSerieRow.Versioned.V1_0>>(HttpMethod.Get, url, ctk: ctk);
            }
            public Task<IEnumerable<TimeSerieRow.Versioned.V1_0>> GetVersionedTimeSeriesLastOfDay(IEnumerable<int> ids, Granularity granularity, Period vPeriodFrom, Period vPeriodTo, RelativeInterval interval, LocalDate? analysisDate = null, string tz = null, int? transformid = null, CancellationToken ctk = default)
            {
                var url = new UrlComposer($"/vts/LastOfDays/{vPeriodFrom}/{vPeriodTo}/{granularity}/{interval}")
                    .AddQueryParam("id", ids)
                    .AddQueryParam("tz", tz)
                    .AddQueryParam("tr", transformid)
                    .AddQueryParam("ad", analysisDate)
                    ;

                return _client.Exec<IEnumerable<TimeSerieRow.Versioned.V1_0>>(HttpMethod.Get, url, ctk: ctk);
            }
            public Task<IEnumerable<TimeSerieRow.Versioned.V1_0>> GetVersionedTimeSeriesLastOfDay(IEnumerable<int> ids, Granularity granularity, LocalDateRange versionRange, Period period, LocalDate? analysisDate = null, string tz = null, int? transformid = null, CancellationToken ctk = default)
            {
                var url = new UrlComposer($"/vts/LastOfDays/{UrlComposer.ToUrlParam(versionRange)}/{granularity}/{period}")
                    .AddQueryParam("id", ids)
                    .AddQueryParam("tz", tz)
                    .AddQueryParam("tr", transformid)
                    .AddQueryParam("ad", analysisDate)
                    ;

                return _client.Exec<IEnumerable<TimeSerieRow.Versioned.V1_0>>(HttpMethod.Get, url, ctk: ctk);
            }
            public Task<IEnumerable<TimeSerieRow.Versioned.V1_0>> GetVersionedTimeSeriesLastOfDay(IEnumerable<int> ids, Granularity granularity, LocalDateRange versionRange, Period periodFrom, Period periodTo, LocalDate? analysisDate = null, string tz = null, int? transformid = null, CancellationToken ctk = default)
            {
                var url = new UrlComposer($"/vts/LastOfDays/{UrlComposer.ToUrlParam(versionRange)}/{granularity}/{periodFrom}/{periodTo}")
                    .AddQueryParam("id", ids)
                    .AddQueryParam("tz", tz)
                    .AddQueryParam("tr", transformid)
                    .AddQueryParam("ad", analysisDate)
                    ;

                return _client.Exec<IEnumerable<TimeSerieRow.Versioned.V1_0>>(HttpMethod.Get, url, ctk: ctk);
            }
            public Task<IEnumerable<TimeSerieRow.Versioned.V1_0>> GetVersionedTimeSeriesLastOfDay(IEnumerable<int> ids, Granularity granularity, Period vPeriod, Period period, LocalDate? analysisDate = null, string tz = null, int? transformid = null, CancellationToken ctk = default)
            {
                var url = new UrlComposer($"/vts/LastOfDays/{vPeriod}/{granularity}/{period}")
                    .AddQueryParam("id", ids)
                    .AddQueryParam("tz", tz)
                    .AddQueryParam("tr", transformid)
                    .AddQueryParam("ad", analysisDate)
                    ;

                return _client.Exec<IEnumerable<TimeSerieRow.Versioned.V1_0>>(HttpMethod.Get, url, ctk: ctk);
            }
            public Task<IEnumerable<TimeSerieRow.Versioned.V1_0>> GetVersionedTimeSeriesLastOfDay(IEnumerable<int> ids, Granularity granularity, Period vPeriod, Period periodFrom, Period periodTo, LocalDate? analysisDate = null, string tz = null, int? transformid = null, CancellationToken ctk = default)
            {
                var url = new UrlComposer($"/vts/LastOfDays/{vPeriod}/{granularity}/{periodFrom}/{periodTo}")
                    .AddQueryParam("id", ids)
                    .AddQueryParam("tz", tz)
                    .AddQueryParam("tr", transformid)
                    .AddQueryParam("ad", analysisDate)
                    ;

                return _client.Exec<IEnumerable<TimeSerieRow.Versioned.V1_0>>(HttpMethod.Get, url, ctk: ctk);
            }
            public Task<IEnumerable<TimeSerieRow.Versioned.V1_0>> GetVersionedTimeSeriesLastOfDayRelativeVersionRange(IEnumerable<int> ids, Granularity granularity, Period vPeriodFrom, Period vPeriodTo, Period period, LocalDate? analysisDate = null, string tz = null, int? transformid = null, CancellationToken ctk = default)
            {
                var url = new UrlComposer($"/vts/LastOfDays/{vPeriodFrom}/{vPeriodTo}/{granularity}/{period}")
                    .AddQueryParam("id", ids)
                    .AddQueryParam("tz", tz)
                    .AddQueryParam("tr", transformid)
                    .AddQueryParam("ad", analysisDate)
                    ;

                return _client.Exec<IEnumerable<TimeSerieRow.Versioned.V1_0>>(HttpMethod.Get, url, ctk: ctk);
            }
            public Task<IEnumerable<TimeSerieRow.Versioned.V1_0>> GetVersionedTimeSeriesLastOfDayRelativeVersionRange(IEnumerable<int> ids, Granularity granularity, Period vPeriodFrom, Period vPeriodTo, Period periodFrom, Period periodTo, LocalDate? analysisDate = null, string tz = null, int? transformid = null, CancellationToken ctk = default)
            {
                var url = new UrlComposer($"/vts/LastOfDays/{vPeriodFrom}/{vPeriodTo}/{granularity}/{periodFrom}/{periodTo}")
                    .AddQueryParam("id", ids)
                    .AddQueryParam("tz", tz)
                    .AddQueryParam("tr", transformid)
                    .AddQueryParam("ad", analysisDate)
                    ;

                return _client.Exec<IEnumerable<TimeSerieRow.Versioned.V1_0>>(HttpMethod.Get, url, ctk: ctk);
            }
            public Task<IEnumerable<TimeSerieRow.Versioned.V1_0>> GetVersionedTimeSeriesLastOfMonth(IEnumerable<int> ids, Granularity granularity, LocalDateRange versionRange, LocalDateRange range, string tz = null, int? transformid = null, CancellationToken ctk = default)
            {
                var url = new UrlComposer($"/vts/LastOfMonths/{UrlComposer.ToUrlParam(versionRange)}/{granularity}/{UrlComposer.ToUrlParam(range)}")
                    .AddQueryParam("id", ids)
                    .AddQueryParam("tz", tz)
                    .AddQueryParam("tr", transformid)
                    ;

                return _client.Exec<IEnumerable<TimeSerieRow.Versioned.V1_0>>(HttpMethod.Get, url, ctk: ctk);
            }
            public Task<IEnumerable<TimeSerieRow.Versioned.V1_0>> GetVersionedTimeSeriesLastOfMonth(IEnumerable<int> ids, Granularity granularity, Period vPeriod, LocalDateRange range, LocalDate? analysisDate = null, string tz = null, int? transformid = null, CancellationToken ctk = default)
            {
                var url = new UrlComposer($"/vts/LastOfMonths/{vPeriod}/{granularity}/{UrlComposer.ToUrlParam(range)}")
                    .AddQueryParam("id", ids)
                    .AddQueryParam("tz", tz)
                    .AddQueryParam("tr", transformid)
                    .AddQueryParam("ad", analysisDate)
                    ;

                return _client.Exec<IEnumerable<TimeSerieRow.Versioned.V1_0>>(HttpMethod.Get, url, ctk: ctk);
            }
            public Task<IEnumerable<TimeSerieRow.Versioned.V1_0>> GetVersionedTimeSeriesLastOfMonth(IEnumerable<int> ids, Granularity granularity, Period vPeriodFrom, Period vPeriodTo, LocalDateRange range, LocalDate? analysisDate = null, string tz = null, int? transformid = null, CancellationToken ctk = default)
            {
                var url = new UrlComposer($"/vts/LastOfMonths/{vPeriodFrom}/{vPeriodTo}/{granularity}/{UrlComposer.ToUrlParam(range)}")
                    .AddQueryParam("id", ids)
                    .AddQueryParam("tz", tz)
                    .AddQueryParam("tr", transformid)
                    .AddQueryParam("ad", analysisDate)
                    ;

                return _client.Exec<IEnumerable<TimeSerieRow.Versioned.V1_0>>(HttpMethod.Get, url, ctk: ctk);
            }
            public Task<IEnumerable<TimeSerieRow.Versioned.V1_0>> GetVersionedTimeSeriesLastOfMonth(IEnumerable<int> ids, Granularity granularity, LocalDateRange versionRange, RelativeInterval interval, LocalDate? analysisDate = null, string tz = null, int? transformid = null, CancellationToken ctk = default)
            {
                var url = new UrlComposer($"/vts/LastOfMonths/{UrlComposer.ToUrlParam(versionRange)}/{granularity}/{interval}")
                    .AddQueryParam("id", ids)
                    .AddQueryParam("tz", tz)
                    .AddQueryParam("tr", transformid)
                    .AddQueryParam("ad", analysisDate)
                    ;

                return _client.Exec<IEnumerable<TimeSerieRow.Versioned.V1_0>>(HttpMethod.Get, url, ctk: ctk);
            }
            public Task<IEnumerable<TimeSerieRow.Versioned.V1_0>> GetVersionedTimeSeriesLastOfMonth(IEnumerable<int> ids, Granularity granularity, Period vPeriod, RelativeInterval interval, LocalDate? analysisDate = null, string tz = null, int? transformid = null, CancellationToken ctk = default)
            {
                var url = new UrlComposer($"/vts/LastOfMonths/{vPeriod}/{granularity}/{interval}")
                    .AddQueryParam("id", ids)
                    .AddQueryParam("tz", tz)
                    .AddQueryParam("tr", transformid)
                    .AddQueryParam("ad", analysisDate)
                    ;

                return _client.Exec<IEnumerable<TimeSerieRow.Versioned.V1_0>>(HttpMethod.Get, url, ctk: ctk);
            }
            public Task<IEnumerable<TimeSerieRow.Versioned.V1_0>> GetVersionedTimeSeriesLastOfMonth(IEnumerable<int> ids, Granularity granularity, Period vPeriodFrom, Period vPeriodTo, RelativeInterval interval, LocalDate? analysisDate = null, string tz = null, int? transformid = null, CancellationToken ctk = default)
            {
                var url = new UrlComposer($"/vts/LastOfMonths/{vPeriodFrom}/{vPeriodTo}/{granularity}/{interval}")
                    .AddQueryParam("id", ids)
                    .AddQueryParam("tz", tz)
                    .AddQueryParam("tr", transformid)
                    .AddQueryParam("ad", analysisDate)
                    ;

                return _client.Exec<IEnumerable<TimeSerieRow.Versioned.V1_0>>(HttpMethod.Get, url, ctk: ctk);
            }
            public Task<IEnumerable<TimeSerieRow.Versioned.V1_0>> GetVersionedTimeSeriesLastOfMonth(IEnumerable<int> ids, Granularity granularity, LocalDateRange versionRange, Period period, LocalDate? analysisDate = null, string tz = null, int? transformid = null, CancellationToken ctk = default)
            {
                var url = new UrlComposer($"/vts/LastOfMonths/{UrlComposer.ToUrlParam(versionRange)}/{granularity}/{period}")
                    .AddQueryParam("id", ids)
                    .AddQueryParam("tz", tz)
                    .AddQueryParam("tr", transformid)
                    .AddQueryParam("ad", analysisDate)
                    ;

                return _client.Exec<IEnumerable<TimeSerieRow.Versioned.V1_0>>(HttpMethod.Get, url, ctk: ctk);
            }
            public Task<IEnumerable<TimeSerieRow.Versioned.V1_0>> GetVersionedTimeSeriesLastOfMonth(IEnumerable<int> ids, Granularity granularity, LocalDateRange versionRange, Period periodFrom, Period periodTo, LocalDate? analysisDate = null, string tz = null, int? transformid = null, CancellationToken ctk = default)
            {
                var url = new UrlComposer($"/vts/LastOfMonths/{UrlComposer.ToUrlParam(versionRange)}/{granularity}/{periodFrom}/{periodTo}")
                    .AddQueryParam("id", ids)
                    .AddQueryParam("tz", tz)
                    .AddQueryParam("tr", transformid)
                    .AddQueryParam("ad", analysisDate)
                    ;

                return _client.Exec<IEnumerable<TimeSerieRow.Versioned.V1_0>>(HttpMethod.Get, url, ctk: ctk);
            }
            public Task<IEnumerable<TimeSerieRow.Versioned.V1_0>> GetVersionedTimeSeriesLastOfMonth(IEnumerable<int> ids, Granularity granularity, Period vPeriod, Period period, LocalDate? analysisDate = null, string tz = null, int? transformid = null, CancellationToken ctk = default)
            {
                var url = new UrlComposer($"/vts/LastOfMonths/{vPeriod}/{granularity}/{period}")
                    .AddQueryParam("id", ids)
                    .AddQueryParam("tz", tz)
                    .AddQueryParam("tr", transformid)
                    .AddQueryParam("ad", analysisDate)
                    ;

                return _client.Exec<IEnumerable<TimeSerieRow.Versioned.V1_0>>(HttpMethod.Get, url, ctk: ctk);
            }
            public Task<IEnumerable<TimeSerieRow.Versioned.V1_0>> GetVersionedTimeSeriesLastOfMonth(IEnumerable<int> ids, Granularity granularity, Period vPeriod, Period periodFrom, Period periodTo, LocalDate? analysisDate = null, string tz = null, int? transformid = null, CancellationToken ctk = default)
            {
                var url = new UrlComposer($"/vts/LastOfMonths/{vPeriod}/{granularity}/{periodFrom}/{periodTo}")
                    .AddQueryParam("id", ids)
                    .AddQueryParam("tz", tz)
                    .AddQueryParam("tr", transformid)
                    .AddQueryParam("ad", analysisDate)
                    ;

                return _client.Exec<IEnumerable<TimeSerieRow.Versioned.V1_0>>(HttpMethod.Get, url, ctk: ctk);
            }
            public Task<IEnumerable<TimeSerieRow.Versioned.V1_0>> GetVersionedTimeSeriesLastOfMonthRelativeVersionRange(IEnumerable<int> ids, Granularity granularity, Period vPeriodFrom, Period vPeriodTo, Period period, LocalDate? analysisDate = null, string tz = null, int? transformid = null, CancellationToken ctk = default)
            {
                var url = new UrlComposer($"/vts/LastOfMonths/{vPeriodFrom}/{vPeriodTo}/{granularity}/{period}")
                    .AddQueryParam("id", ids)
                    .AddQueryParam("tz", tz)
                    .AddQueryParam("tr", transformid)
                    .AddQueryParam("ad", analysisDate)
                    ;

                return _client.Exec<IEnumerable<TimeSerieRow.Versioned.V1_0>>(HttpMethod.Get, url, ctk: ctk);
            }
            public Task<IEnumerable<TimeSerieRow.Versioned.V1_0>> GetVersionedTimeSeriesLastOfMonthRelativeVersionRange(IEnumerable<int> ids, Granularity granularity, Period vPeriodFrom, Period vPeriodTo, Period periodFrom, Period periodTo, LocalDate? analysisDate = null, string tz = null, int? transformid = null, CancellationToken ctk = default)
            {
                var url = new UrlComposer($"/vts/LastOfMonths/{vPeriodFrom}/{vPeriodTo}/{granularity}/{periodFrom}/{periodTo}")
                    .AddQueryParam("id", ids)
                    .AddQueryParam("tz", tz)
                    .AddQueryParam("tr", transformid)
                    .AddQueryParam("ad", analysisDate)
                    ;

                return _client.Exec<IEnumerable<TimeSerieRow.Versioned.V1_0>>(HttpMethod.Get, url, ctk: ctk);
            }
            public Task<IEnumerable<TimeSerieRow.Versioned.V1_0>> GetVersionedTimeSeriesLastN(IEnumerable<int> ids, Granularity granularity, int versionCount, LocalDateRange range, string tz = null, int? transformid = null, CancellationToken ctk = default)
            {
                var url = new UrlComposer($"/vts/Last{versionCount}/{granularity}/{UrlComposer.ToUrlParam(range)}")
                    .AddQueryParam("id", ids)
                    .AddQueryParam("tz", tz)
                    .AddQueryParam("tr", transformid)
                    ;

                return _client.Exec<IEnumerable<TimeSerieRow.Versioned.V1_0>>(HttpMethod.Get, url, ctk: ctk);
            }
            public Task<IEnumerable<TimeSerieRow.Versioned.V1_0>> GetVersionedTimeSeriesLastN(IEnumerable<int> ids, Granularity granularity, int versionCount, RelativeInterval interval, LocalDate? analysisDate = null, string tz = null, int? transformid = null, CancellationToken ctk = default)
            {
                var url = new UrlComposer($"/vts/Last{versionCount}/{granularity}/{interval}")
                    .AddQueryParam("id", ids)
                    .AddQueryParam("tz", tz)
                    .AddQueryParam("tr", transformid)
                    .AddQueryParam("ad", analysisDate)
                    ;

                return _client.Exec<IEnumerable<TimeSerieRow.Versioned.V1_0>>(HttpMethod.Get, url, ctk: ctk);
            }
            public Task<IEnumerable<TimeSerieRow.Versioned.V1_0>> GetVersionedTimeSeriesLastN(IEnumerable<int> ids, Granularity granularity, int versionCount, Period period, LocalDate? analysisDate = null, string tz = null, int? transformid = null, CancellationToken ctk = default)
            {
                var url = new UrlComposer($"/vts/Last{versionCount}/{granularity}/{period}")
                    .AddQueryParam("id", ids)
                    .AddQueryParam("tz", tz)
                    .AddQueryParam("tr", transformid)
                    .AddQueryParam("ad", analysisDate)
                    ;

                return _client.Exec<IEnumerable<TimeSerieRow.Versioned.V1_0>>(HttpMethod.Get, url, ctk: ctk);
            }
            public Task<IEnumerable<TimeSerieRow.Versioned.V1_0>> GetVersionedTimeSeriesLastN(IEnumerable<int> ids, Granularity granularity, int versionCount, Period periodFrom, Period periodTo, LocalDate? analysisDate = null, string tz = null, int? transformid = null, CancellationToken ctk = default)
            {
                var url = new UrlComposer($"/vts/Last{versionCount}/{granularity}/{periodFrom}/{periodTo}")
                    .AddQueryParam("id", ids)
                    .AddQueryParam("tz", tz)
                    .AddQueryParam("tr", transformid)
                    .AddQueryParam("ad", analysisDate)
                    ;

                return _client.Exec<IEnumerable<TimeSerieRow.Versioned.V1_0>>(HttpMethod.Get, url, ctk: ctk);
            }
            public Task<IEnumerable<TimeSerieRow.Versioned.V1_0>> GetVersionedTimeSeriesMostUpdatedVersion(IEnumerable<int> ids, Granularity granularity, LocalDateRange range, string tz = null, int? transformid = null, CancellationToken ctk = default)
            {
                var url = new UrlComposer($"/vts/MUV/{granularity}/{UrlComposer.ToUrlParam(range)}")
                    .AddQueryParam("id", ids)
                    .AddQueryParam("tz", tz)
                    .AddQueryParam("tr", transformid)
                    ;

                return _client.Exec<IEnumerable<TimeSerieRow.Versioned.V1_0>>(HttpMethod.Get, url, ctk: ctk);
            }
            public Task<IEnumerable<TimeSerieRow.Versioned.V1_0>> GetVersionedTimeSeriesMostUpdatedVersion(IEnumerable<int> ids, Granularity granularity, RelativeInterval interval, LocalDate? analysisDate = null, string tz = null, int? transformid = null, CancellationToken ctk = default)
            {
                var url = new UrlComposer($"/vts/MUV/{granularity}/{interval}")
                    .AddQueryParam("id", ids)
                    .AddQueryParam("tz", tz)
                    .AddQueryParam("tr", transformid)
                    .AddQueryParam("ad", analysisDate)
                    ;

                return _client.Exec<IEnumerable<TimeSerieRow.Versioned.V1_0>>(HttpMethod.Get, url, ctk: ctk);
            }
            public Task<IEnumerable<TimeSerieRow.Versioned.V1_0>> GetVersionedTimeSeriesMostUpdatedVersion(IEnumerable<int> ids, Granularity granularity, Period period, LocalDate? analysisDate = null, string tz = null, int? transformid = null, CancellationToken ctk = default)
            {
                var url = new UrlComposer($"/vts/MUV/{granularity}/{period}")
                    .AddQueryParam("id", ids)
                    .AddQueryParam("tz", tz)
                    .AddQueryParam("tr", transformid)
                    .AddQueryParam("ad", analysisDate)
                    ;

                return _client.Exec<IEnumerable<TimeSerieRow.Versioned.V1_0>>(HttpMethod.Get, url, ctk: ctk);
            }
            public Task<IEnumerable<TimeSerieRow.Versioned.V1_0>> GetVersionedTimeSeriesMostUpdatedVersion(IEnumerable<int> ids, Granularity granularity, Period periodFrom, Period periodTo, LocalDate? analysisDate = null, string tz = null, int? transformid = null, CancellationToken ctk = default)
            {
                var url = new UrlComposer($"/vts/MUV/{granularity}/{periodFrom}/{periodTo}")
                    .AddQueryParam("id", ids)
                    .AddQueryParam("tz", tz)
                    .AddQueryParam("tr", transformid)
                    .AddQueryParam("ad", analysisDate)
                    ;

                return _client.Exec<IEnumerable<TimeSerieRow.Versioned.V1_0>>(HttpMethod.Get, url, ctk: ctk);
            }
            public Task<IEnumerable<TimeSerieRow.Versioned.V1_0>> GetVersionedTimeSeries(LocalDateTime version, Granularity granularity, LocalDateRange range, IEnumerable<int> id, string tz = null, int? tr = null, CancellationToken ctk = default(CancellationToken))
            {
                var resource = new UrlComposer($"/vts/Version/{UrlComposer.ToUrlParam(version)}/{granularity}/{UrlComposer.ToUrlParam(range)}")
                        .AddQueryParam("id", id)
                        .AddQueryParam("tz", tz)
                        .AddQueryParam("tr", tr)
                        ;

                return _client.Exec<IEnumerable<TimeSerieRow.Versioned.V1_0>>(HttpMethod.Get, resource, ctk: ctk);
            }
            public Task<IEnumerable<TimeSerieRow.Versioned.V1_0>> GetVersionedTimeSeries(LocalDateTime version, Granularity granularity, RelativeInterval interval, IEnumerable<int> id, LocalDate? analysisDate = null, string tz = null, int? tr = null, CancellationToken ctk = default(CancellationToken))
            {
                var resource = new UrlComposer($"/vts/Version/{UrlComposer.ToUrlParam(version)}/{granularity}/{interval}/")
                        .AddQueryParam("id", id)
                        .AddQueryParam("tz", tz)
                        .AddQueryParam("tr", tr)
                        .AddQueryParam("ad", analysisDate);

                return _client.Exec<IEnumerable<TimeSerieRow.Versioned.V1_0>>(HttpMethod.Get, resource, ctk: ctk);
            }
            public Task<IEnumerable<TimeSerieRow.Versioned.V1_0>> GetVersionedTimeSeries(LocalDateTime version, Granularity granularity, Period period, IEnumerable<int> id, LocalDate? analysisDate = null, string tz = null, int? tr = null, CancellationToken ctk = default(CancellationToken))
            {
                var resource = new UrlComposer($"/vts/Version/{UrlComposer.ToUrlParam(version)}/{granularity}/{period}/")
                        .AddQueryParam("id", id)
                        .AddQueryParam("tz", tz)
                        .AddQueryParam("tr", tr)
                        .AddQueryParam("ad", analysisDate);

                return _client.Exec<IEnumerable<TimeSerieRow.Versioned.V1_0>>(HttpMethod.Get, resource, ctk: ctk);
            }
            public Task<IEnumerable<TimeSerieRow.Versioned.V1_0>> GetVersionedTimeSeries(LocalDateTime version, Granularity granularity, Period periodFrom, Period periodTo, IEnumerable<int> id, LocalDate? analysisDate = null, string tz = null, int? tr = null, CancellationToken ctk = default(CancellationToken))
            {
                var resource = new UrlComposer($"/vts/Version/{UrlComposer.ToUrlParam(version)}/{granularity}/{periodFrom}/{periodTo}")
                        .AddQueryParam("id", id)
                        .AddQueryParam("tz", tz)
                        .AddQueryParam("tr", tr)
                        .AddQueryParam("ad", analysisDate);

                return _client.Exec<IEnumerable<TimeSerieRow.Versioned.V1_0>>(HttpMethod.Get, resource, ctk: ctk);
            }


            public void Dispose()
            {
                _client.Dispose();
            }
        }
    }
}
