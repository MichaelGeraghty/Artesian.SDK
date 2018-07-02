using NodaTime;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Net.Http;
using Artesian.SDK.API.Configuration;
using Artesian.SDK.API;
using Artesian.SDK.ArtesianService.Clients;
using Artesian.SDK.Common;
using Artesian.SDK.Common.Dto.Api.V2;
using Artesian.SDK.API.Dto;
using Artesian.SDK.Common.Dto.Search;
using Artesian.SDK.API.Dto.Api.V2.CurveData;
using Artesian.SDK.API.ArtesianService;
using Artesian.SDK.API.DTO;
using Artesian.SDK.Dependencies;
using Artesian.SDK.Dependencies.MarketTools.MarketProducts;
using Artesian.SDK.Common.Dto.TimeTransform.Dto;
using Artesian.SDK.API.Dto.PagedResult;
using Artesian.SDK.API.Dto.Auth;
using Artesian.SDK.API.Dto.Auth.Dto;
using Artesian.SDK.Common.Dto.Api.V2.Operations;

namespace Artesian.SDK.ArtesianService
{
    internal static class ArtesianService
    {
        public class Latest : IArtesianService.Latest, IDisposable
        {
            private static Auth0Client _client;

            public Latest(ArtesianServiceConfig config, Func<HttpMessageHandler> httpMessageHandler)
            {
                _client = new Auth0Client(config, httpMessageHandler, $"{ArtesianConstants.ApiVersions.Last()}");
            }

            public Task<MarketDataEntity.V2.Output> RegisterMarketDataAsync(MarketDataEntity.V2.Input metadata, CancellationToken ctk = default(CancellationToken))
            {
                return _client.Exec<MarketDataEntity.V2.Output, MarketDataEntity.V2.Input>(HttpMethod.Post, "/marketdata/entity", metadata, ctk: ctk);
            }

            public Task<MarketDataEntity.V2.Output> UpdateMarketDataAsync(MarketDataEntity.V2.Input metadata, CancellationToken ctk = default(CancellationToken))
            {
                return _client.Exec<MarketDataEntity.V2.Output, MarketDataEntity.V2.Input>(HttpMethod.Put, $"/marketdata/entity/{metadata.MarketDataId}", metadata, ctk: ctk);
            }

            public Task DeleteMarketDataAsync(int id, CancellationToken ctk = default(CancellationToken))
            {
                return _client.Exec(HttpMethod.Delete, $"/marketdata/entity/{id}", ctk: ctk);
            }

            public Task<MarketDataEntity.V2.Output> ReadMarketDataRegistryAsync(MarketDataIdentifier id, CancellationToken ctk = default(CancellationToken))
            {
                var url = new UrlComposer("/marketdata/entity")
                    .AddQueryParam("provider", id.Provider)
                    .AddQueryParam("curveName", id.Name)
                    ;

                return _client.Exec<MarketDataEntity.V2.Output>(HttpMethod.Get, url, ctk: ctk);
            }

            public Task<MarketDataEntity.V2.Output> ReadMarketDataRegistryAsync(int id, CancellationToken ctk = default(CancellationToken))
            {
                return _client.Exec<MarketDataEntity.V2.Output>(HttpMethod.Get, $"/marketdata/entity/{id}", ctk: ctk);
            }

            public Task<ArtesianSearchResults> SearchFacetAsync(ArtesianSearchFilter filter, CancellationToken ctk = default(CancellationToken))
            {
                var url = new UrlComposer("/marketdata/searchfacet")
                    .AddQueryParam("pageSize", $"{filter.PageSize}")
                    .AddQueryParam("page", $"{filter.Page}")
                    .AddQueryParam("searchText", filter.SearchText)
                    .AddQueryParam("filters", filter.Filters?.SelectMany(s => s.Value.Select(x => $@"{s.Key}:{x}")))
                    .AddQueryParam("sorts", filter.Sorts)
                    ;

                return _client.Exec<ArtesianSearchResults>(HttpMethod.Get, url);
            }

            public Task<List<MarketDataEntity.V2.Output>> PerformOperationsAsync(Operations operations, CancellationToken ctk = default(CancellationToken))
            {
                return _client.Exec<List<MarketDataEntity.V2.Output>, Operations>(HttpMethod.Post, "/marketdata/operations", operations, ctk: ctk);
            }

            public Task UpsertCurveDataAsync(UpsertCurveData data, CancellationToken ctk = default(CancellationToken))
            {
                return _client.Exec(HttpMethod.Post, "/marketdata/upsertdata", data, ctk: ctk);
            }

            public Task<IEnumerable<TimeSerieRow.Actual.V2>> GetActualTimeSeries(IEnumerable<int> ids, Granularity granularity, LocalDateRange range, CancellationToken ctk = default(CancellationToken))
            {
                var resource = $"/marketdata/extractdata/ts/{granularity}/{UrlComposer.ToUrlParam(range)}/{string.Join(",", ids)}";
                return _client.Exec<IEnumerable<TimeSerieRow.Actual.V2>>(HttpMethod.Get, resource, ctk: ctk);
            }

            public Task<IEnumerable<TimeSerieRow.Actual.V2>> GetActualTimeSeries(IEnumerable<int> ids, Granularity granularity, RelativeInterval interval, LocalDate? analysisDate = null, CancellationToken ctk = default(CancellationToken))
            {
                var resource = new UrlComposer($"/marketdata/extractdata/ts/{granularity}/{interval}/{string.Join(",", ids)}")
                    .AddQueryParam("analysisDate", analysisDate);

                return _client.Exec<IEnumerable<TimeSerieRow.Actual.V2>>(HttpMethod.Get, resource, ctk: ctk);
            }

            public Task<IEnumerable<TimeSerieRow.Versioned.V2>> GetVersionedTimeSeriesLastOfDay(IEnumerable<int> ids, Granularity granularity, LocalDateRange versionRange, LocalDateRange range, CancellationToken ctk = default(CancellationToken))
            {
                var resource = $"/marketdata/extractdata/vts/{granularity}/{UrlComposer.ToUrlParam(range)}/LastOfDays/{UrlComposer.ToUrlParam(versionRange)}/{string.Join(",", ids)}";

                return _client.Exec<IEnumerable<TimeSerieRow.Versioned.V2>>(HttpMethod.Get, resource, ctk: ctk);
            }

            public Task<IEnumerable<TimeSerieRow.Versioned.V2>> GetVersionedTimeSeriesLastOfDay(IEnumerable<int> ids, Granularity granularity, LocalDateRange versionRange, RelativeInterval interval, LocalDate? analysisDate = null, CancellationToken ctk = default(CancellationToken))
            {
                var resource = new UrlComposer($"/marketdata/extractdata/vts/{granularity}/{interval}/LastOfDays/{UrlComposer.ToUrlParam(versionRange)}/{string.Join(",", ids)}")
                    .AddQueryParam("analysisDate", analysisDate);

                return _client.Exec<IEnumerable<TimeSerieRow.Versioned.V2>>(HttpMethod.Get, resource, ctk: ctk);
            }

            public Task<IEnumerable<TimeSerieRow.Versioned.V2>> GetVersionedTimeSeriesLastOfMonth(IEnumerable<int> ids, Granularity granularity, LocalDateRange versionRange, LocalDateRange range, CancellationToken ctk = default(CancellationToken))
            {
                var resource = $"/marketdata/extractdata/vts/{granularity}/{UrlComposer.ToUrlParam(range)}/LastOfMonths/{UrlComposer.ToUrlParam(versionRange)}/{string.Join(",", ids)}";

                return _client.Exec<IEnumerable<TimeSerieRow.Versioned.V2>>(HttpMethod.Get, resource, ctk: ctk);
            }

            public Task<IEnumerable<TimeSerieRow.Versioned.V2>> GetVersionedTimeSeriesLastOfMonth(IEnumerable<int> ids, Granularity granularity, LocalDateRange versionRange, RelativeInterval interval, LocalDate? analysisDate = null, CancellationToken ctk = default(CancellationToken))
            {
                var resource = new UrlComposer($"/marketdata/extractdata/vts/{granularity}/{interval}/LastOfMonths/{UrlComposer.ToUrlParam(versionRange)}/{string.Join(",", ids)}")
                    .AddQueryParam("analysisDate", analysisDate);

                return _client.Exec<IEnumerable<TimeSerieRow.Versioned.V2>>(HttpMethod.Get, resource, ctk: ctk);
            }

            public Task<IEnumerable<TimeSerieRow.Versioned.V2>> GetVersionedTimeSeriesLastN(IEnumerable<int> ids, Granularity granularity, int versionCount, LocalDateRange range, CancellationToken ctk = default(CancellationToken))
            {
                var resource = $"/marketdata/extractdata/vts/{granularity}/{UrlComposer.ToUrlParam(range)}/Last{versionCount}/{string.Join(",", ids)}";

                return _client.Exec<IEnumerable<TimeSerieRow.Versioned.V2>>(HttpMethod.Get, resource, ctk: ctk);
            }

            public Task<IEnumerable<TimeSerieRow.Versioned.V2>> GetVersionedTimeSeriesLastN(IEnumerable<int> ids, Granularity granularity, int versionCount, RelativeInterval interval, LocalDate? analysisDate = null, CancellationToken ctk = default(CancellationToken))
            {
                var resource = new UrlComposer($"/marketdata/extractdata/vts/{granularity}/{interval}/Last{versionCount}/{string.Join(",", ids)}")
                    .AddQueryParam("analysisDate", analysisDate);

                return _client.Exec<IEnumerable<TimeSerieRow.Versioned.V2>>(HttpMethod.Get, resource, ctk: ctk);
            }

            public Task<IEnumerable<TimeSerieRow.Versioned.V2>> GetVersionedTimeSeriesMostUpdatedVersion(IEnumerable<int> ids, Granularity granularity, LocalDateRange range, CancellationToken ctk = default(CancellationToken))
            {
                var resource = $"/marketdata/extractdata/vts/{granularity}/{UrlComposer.ToUrlParam(range)}/MUV/{string.Join(",", ids)}";

                return _client.Exec<IEnumerable<TimeSerieRow.Versioned.V2>>(HttpMethod.Get, resource, ctk: ctk);
            }

            public Task<IEnumerable<TimeSerieRow.Versioned.V2>> GetVersionedTimeSeriesMostUpdatedVersion(IEnumerable<int> ids, Granularity granularity, RelativeInterval interval, LocalDate? analysisDate = null, CancellationToken ctk = default(CancellationToken))
            {
                var resource = new UrlComposer($"/marketdata/extractdata/vts/{granularity}/{interval}/MUV/{string.Join(",", ids)}")
                    .AddQueryParam("analysisDate", analysisDate);

                return _client.Exec<IEnumerable<TimeSerieRow.Versioned.V2>>(HttpMethod.Get, resource, ctk: ctk);
            }

            public Task<IEnumerable<AssessmentRow.V2>> GetAssessmentSeries(IEnumerable<int> ids, IEnumerable<string> products, LocalDateRange range, CancellationToken ctk = default(CancellationToken))
            {
                return GetAssessmentSeries(ids, products.Select(x => MarketProductBuilder.Parse(x)), range, ctk: ctk);
            }

            public Task<IEnumerable<AssessmentRow.V2>> GetAssessmentSeries(IEnumerable<int> ids, IEnumerable<string> products, RelativeInterval interval, LocalDate? analysisDate = null, CancellationToken ctk = default(CancellationToken))
            {
                return GetAssessmentSeries(ids, products.Select(x => MarketProductBuilder.Parse(x)), interval, analysisDate, ctk: ctk);
            }

            public Task<IEnumerable<AssessmentRow.V2>> GetAssessmentSeries(IEnumerable<int> ids, IEnumerable<IMarketProduct> products, LocalDateRange range, CancellationToken ctk = default(CancellationToken))
            {
                var resource = $"/marketdata/extractdata/mas/{UrlComposer.ToUrlParam(range)}/{string.Join(",", ids)}/{string.Join(",", products)}";

                return _client.Exec<IEnumerable<AssessmentRow.V2>>(HttpMethod.Get, resource, ctk: ctk);
            }

            public Task<IEnumerable<AssessmentRow.V2>> GetAssessmentSeries(IEnumerable<int> ids, IEnumerable<IMarketProduct> products, RelativeInterval interval, LocalDate? analysisDate = null, CancellationToken ctk = default(CancellationToken))
            {
                var resource = new UrlComposer($"/marketdata/extractdata/mas/{interval}/{string.Join(",", ids)}/{string.Join(",", products)}")
                    .AddQueryParam("analysisDate", analysisDate);

                return _client.Exec<IEnumerable<AssessmentRow.V2>>(HttpMethod.Get, resource, ctk: ctk);
            }

            public Task<TimeTransformBase> RegisterTimeTransformBaseAsync(TimeTransformBase timeTransform, CancellationToken ctk = default(CancellationToken))
            {
                return _client.Exec<TimeTransformBase, TimeTransformBase>(HttpMethod.Post, "/timeTransform/entity", timeTransform, ctk: ctk);
            }

            public Task<TimeTransformBase> ReadTimeTransformBaseAsync(int timeTransformId, CancellationToken ctk = default(CancellationToken))
            {
                return _client.Exec<TimeTransformBase>(HttpMethod.Get, $@"/timeTransform/entity/{timeTransformId}", ctk: ctk);
            }

            public Task<TimeTransformBase> UpdateTimeTransformBaseAsync(TimeTransformBase timeTransform, CancellationToken ctk = default(CancellationToken))
            {
                return _client.Exec<TimeTransformBase, TimeTransformBase>(HttpMethod.Put, $@"/timeTransform/entity/{timeTransform.ID}", timeTransform, ctk: ctk);
            }

            public Task DeleteTimeTransformSimpleShiftAsync(int timeTransformID, CancellationToken ctk = default(CancellationToken))
            {
                return _client.Exec(HttpMethod.Delete, $@"/timeTransform/entity/{timeTransformID}", ctk: ctk);

            }
            public Task<PagedResult<TimeTransformBase>> ReadTimeTransformsAsync(int page, int pageSize, bool userDefined, CancellationToken ctk = default(CancellationToken))
            {
                var url = new UrlComposer("/timeTransform/entity")
                    .AddQueryParam("pageSize", $"{pageSize}")
                    .AddQueryParam("page", $"{page}")
                    .AddQueryParam("userDefined", userDefined)
                    ;

                return _client.Exec<PagedResult<TimeTransformBase>>(HttpMethod.Get, url, ctk: ctk);
            }

            public Task<PagedResult<CurveRangeV2>> ReadCurveRange(int id, int page, int pageSize, string product = null, LocalDateTime? versionFrom = null, LocalDateTime? versionTo = null, CancellationToken ctk = default)
            {
                var url = new UrlComposer($"/marketdata/entity/{id}/curves")
                    .AddQueryParam("page", page)
                    .AddQueryParam("pageSize", pageSize)
                    .AddQueryParam("product", product)
                    .AddQueryParam("versionFrom", versionFrom)
                    .AddQueryParam("versionTo", versionTo);

                return _client.Exec<PagedResult<CurveRangeV2>>(HttpMethod.Get, url, ctk: ctk);
            }

            public Task<AuthGroup> CreateAuthGroup(AuthGroup group, CancellationToken ctk = default(CancellationToken))
            {
                return _client.Exec<AuthGroup, AuthGroup>(HttpMethod.Post, $@"/group", group);
            }

            public Task<AuthGroup> UpdateAuthGroup(int groupID, AuthGroup group, CancellationToken ctk = default(CancellationToken))
            {
                return _client.Exec<AuthGroup, AuthGroup>(HttpMethod.Put, $@"/group/{groupID}", group);
            }

            public Task<AuthGroup> ReadAuthGroup(int groupID, CancellationToken ctk = default(CancellationToken))
            {
                return _client.Exec<AuthGroup>(HttpMethod.Get, $@"/group/{groupID}");
            }

            public Task<List<Principals>> ReadUserPrincipals(string user, CancellationToken ctk = default(CancellationToken))
            {
                var url = new UrlComposer($@"/user/principals")
                            .AddQueryParam("user", $"{user}");

                return _client.Exec<List<Principals>>(HttpMethod.Get, url);
            }

            public Task RemoveAuthGroup(int groupID, CancellationToken ctk = default(CancellationToken))
            {
                return _client.Exec(HttpMethod.Delete, $@"/group/{groupID}");
            }

            public Task<PagedResult<AuthGroup>> ReadAuthGroups(int page, int pageSize, CancellationToken ctk = default(CancellationToken))
            {
                var url = new UrlComposer($@"/group")
                        .AddQueryParam("pageSize", $"{pageSize}")
                        .AddQueryParam("page", $"{page}");

                return _client.Exec<PagedResult<AuthGroup>>(HttpMethod.Get, url);
            }

            public Task<AuthGroup> AddUsersToGroup(int groupID, List<string> users, Guid? ifMatch = null, CancellationToken ctk = default(CancellationToken))
            {
                return _client.Exec<AuthGroup, List<string>>(HttpMethod.Post, $@"/group/{groupID}/users", users);
            }

            public Task<AuthGroup> SetGroupUsers(int groupID, List<string> users, Guid? ifMatch = null, CancellationToken ctk = default(CancellationToken))
            {
                return _client.Exec<AuthGroup, List<string>>(HttpMethod.Put, $@"/group/{groupID}/users", users);
            }

            public Task<AuthGroup> RemoveUsersFromGroup(int groupID, List<string> users, Guid? ifMatch = null, CancellationToken ctk = default(CancellationToken))
            {
                return _client.Exec<AuthGroup, List<string>>(HttpMethod.Delete, $@"/group/{groupID}/users", users);
            }

            public Task<IEnumerable<AuthorizationPath.Output>> ReadRolesByPath(PathString path, CancellationToken ctk = default)
            {
                var url = new UrlComposer($@"/acl/me")
                        .AddQueryParam("path", $"{path}");

                return _client.Exec<IEnumerable<AuthorizationPath.Output>>(HttpMethod.Get, url);
            }

            public Task<PagedResult<AuthorizationPath.Output>> GetRoles(int page, int pageSize, string[] principalIds, LocalDateTime? asOf = null, CancellationToken ctk = default)
            {
                var url = new UrlComposer($@"/acl")
                        .AddQueryParam("pageSize", $"{pageSize}")
                        .AddQueryParam("page", $"{page}")
                        .AddQueryParam("principalIds", principalIds)
                        .AddQueryParam("asOf", asOf);

                return _client.Exec<PagedResult<AuthorizationPath.Output>>(HttpMethod.Get, url);
            }

            public Task UpsertRoles(AuthorizationPath.Input upsert, CancellationToken ctk = default)
            {
                return _client.Exec<AuthorizationPath.Input>(HttpMethod.Post, $@"/acl", upsert);
            }

            public Task AddRoles(AuthorizationPath.Input add, CancellationToken ctk = default)
            {
                return _client.Exec<AuthorizationPath.Input>(HttpMethod.Post, $@"/acl/roles", add);
            }

            public Task RemoveRoles(AuthorizationPath.Input remove, CancellationToken ctk = default)
            {
                return _client.Exec<AuthorizationPath.Input>(HttpMethod.Delete, $@"/acl/roles", remove);
            }

            public void Dispose()
            {
                _client.Dispose();
            }

            public Task<MarketDataEntity.V2.Output> ReadMarketDataRegistryAsync(MarketDataIdentifier actualTimeSerieID)
            {
                throw new NotImplementedException();
            }

            public Task<MarketDataEntity.V2.Output> RegisterMarketDataAsync(MarketDataEntity.V2.Input metadata)
            {
                throw new NotImplementedException();
            }
        }

        public class Deprecated : IArtesianService.Deprecated, IDisposable
        {
            private static Auth0Client _client;

            public Deprecated(ArtesianServiceConfig config, Func<HttpMessageHandler> httpMessageHandler)
            {
                _client = new Auth0Client(config, httpMessageHandler, $"v2.0");
            }

            public Task<MarketDataEntity.V2.Output> RegisterMarketDataAsync(MarketDataEntity.V2.Input metadata, CancellationToken ctk = default(CancellationToken))
            {
                return _client.Exec<MarketDataEntity.V2.Output, MarketDataEntity.V2.Input>(HttpMethod.Post, "/marketdata/entity", metadata, ctk: ctk);
            }

            public Task<MarketDataEntity.V2.Output> UpdateMarketDataAsync(MarketDataEntity.V2.Input metadata, CancellationToken ctk = default(CancellationToken))
            {
                return _client.Exec<MarketDataEntity.V2.Output, MarketDataEntity.V2.Input>(HttpMethod.Put, $"/marketdata/entity/{metadata.MarketDataId}", metadata, ctk: ctk);
            }

            public Task DeleteMarketDataAsync(int id, CancellationToken ctk = default(CancellationToken))
            {
                return _client.Exec(HttpMethod.Delete, $"/marketdata/entity/{id}", ctk: ctk);
            }

            public Task<MarketDataEntity.V2.Output> ReadMarketDataRegistryAsync(MarketDataIdentifier id, CancellationToken ctk = default(CancellationToken))
            {
                var url = new UrlComposer("/marketdata/entity")
                    .AddQueryParam("provider", id.Provider)
                    .AddQueryParam("curveName", id.Name)
                    ;

                return _client.Exec<MarketDataEntity.V2.Output>(HttpMethod.Get, url, ctk: ctk);
            }

            public Task<MarketDataEntity.V2.Output> ReadMarketDataRegistryAsync(int id, CancellationToken ctk = default(CancellationToken))
            {
                return _client.Exec<MarketDataEntity.V2.Output>(HttpMethod.Get, $"/marketdata/entity/{id}", ctk: ctk);
            }

            public Task<ArtesianSearchResults> SearchFacetAsync(ArtesianSearchFilter filter, CancellationToken ctk = default(CancellationToken))
            {
                var url = new UrlComposer("/marketdata/searchfacet")
                    .AddQueryParam("pageSize", $"{filter.PageSize}")
                    .AddQueryParam("page", $"{filter.Page}")
                    .AddQueryParam("searchText", filter.SearchText)
                    .AddQueryParam("filters", filter.Filters?.SelectMany(s => s.Value.Select(x => $@"{s.Key}:{x}")))
                    .AddQueryParam("sorts", filter.Sorts)
                    ;

                return _client.Exec<ArtesianSearchResults>(HttpMethod.Get, url);
            }

            public Task<List<MarketDataEntity.V2.Output>> PerformOperationsAsync(Operations operations, CancellationToken ctk = default(CancellationToken))
            {
                return _client.Exec<List<MarketDataEntity.V2.Output>, Operations>(HttpMethod.Post, "/marketdata/operations", operations, ctk: ctk);
            }

            public Task UpsertCurveDataAsync(UpsertCurveData data, CancellationToken ctk = default(CancellationToken))
            {
                return _client.Exec(HttpMethod.Post, "/marketdata/upsertdata", data, ctk: ctk);
            }

            public Task<IEnumerable<TimeSerieRow.Actual.V2>> GetActualTimeSeries(IEnumerable<int> ids, Granularity granularity, LocalDateRange range, CancellationToken ctk = default(CancellationToken))
            {
                var resource = $"/marketdata/extractdata/ts/{granularity}/{UrlComposer.ToUrlParam(range)}/{string.Join(",", ids)}";
                return _client.Exec<IEnumerable<TimeSerieRow.Actual.V2>>(HttpMethod.Get, resource, ctk: ctk);
            }

            public Task<IEnumerable<TimeSerieRow.Actual.V2>> GetActualTimeSeries(IEnumerable<int> ids, Granularity granularity, RelativeInterval interval, LocalDate? analysisDate = null, CancellationToken ctk = default(CancellationToken))
            {
                var resource = new UrlComposer($"/marketdata/extractdata/ts/{granularity}/{interval}/{string.Join(",", ids)}")
                    .AddQueryParam("analysisDate", analysisDate);

                return _client.Exec<IEnumerable<TimeSerieRow.Actual.V2>>(HttpMethod.Get, resource, ctk: ctk);
            }

            public Task<IEnumerable<TimeSerieRow.Versioned.V2>> GetVersionedTimeSeriesLastOfDay(IEnumerable<int> ids, Granularity granularity, LocalDateRange versionRange, LocalDateRange range, CancellationToken ctk = default(CancellationToken))
            {
                var resource = $"/marketdata/extractdata/vts/{granularity}/{UrlComposer.ToUrlParam(range)}/LastOfDays/{UrlComposer.ToUrlParam(versionRange)}/{string.Join(",", ids)}";

                return _client.Exec<IEnumerable<TimeSerieRow.Versioned.V2>>(HttpMethod.Get, resource, ctk: ctk);
            }

            public Task<IEnumerable<TimeSerieRow.Versioned.V2>> GetVersionedTimeSeriesLastOfDay(IEnumerable<int> ids, Granularity granularity, LocalDateRange versionRange, RelativeInterval interval, LocalDate? analysisDate = null, CancellationToken ctk = default(CancellationToken))
            {
                var resource = new UrlComposer($"/marketdata/extractdata/vts/{granularity}/{interval}/LastOfDays/{UrlComposer.ToUrlParam(versionRange)}/{string.Join(",", ids)}")
                    .AddQueryParam("analysisDate", analysisDate);

                return _client.Exec<IEnumerable<TimeSerieRow.Versioned.V2>>(HttpMethod.Get, resource, ctk: ctk);
            }

            public Task<IEnumerable<TimeSerieRow.Versioned.V2>> GetVersionedTimeSeriesLastOfMonth(IEnumerable<int> ids, Granularity granularity, LocalDateRange versionRange, LocalDateRange range, CancellationToken ctk = default(CancellationToken))
            {
                var resource = $"/marketdata/extractdata/vts/{granularity}/{UrlComposer.ToUrlParam(range)}/LastOfMonths/{UrlComposer.ToUrlParam(versionRange)}/{string.Join(",", ids)}";

                return _client.Exec<IEnumerable<TimeSerieRow.Versioned.V2>>(HttpMethod.Get, resource, ctk: ctk);
            }

            public Task<IEnumerable<TimeSerieRow.Versioned.V2>> GetVersionedTimeSeriesLastOfMonth(IEnumerable<int> ids, Granularity granularity, LocalDateRange versionRange, RelativeInterval interval, LocalDate? analysisDate = null, CancellationToken ctk = default(CancellationToken))
            {
                var resource = new UrlComposer($"/marketdata/extractdata/vts/{granularity}/{interval}/LastOfMonths/{UrlComposer.ToUrlParam(versionRange)}/{string.Join(",", ids)}")
                    .AddQueryParam("analysisDate", analysisDate);

                return _client.Exec<IEnumerable<TimeSerieRow.Versioned.V2>>(HttpMethod.Get, resource, ctk: ctk);
            }

            public Task<IEnumerable<TimeSerieRow.Versioned.V2>> GetVersionedTimeSeriesLastN(IEnumerable<int> ids, Granularity granularity, int versionCount, LocalDateRange range, CancellationToken ctk = default(CancellationToken))
            {
                var resource = $"/marketdata/extractdata/vts/{granularity}/{UrlComposer.ToUrlParam(range)}/Last{versionCount}/{string.Join(",", ids)}";

                return _client.Exec<IEnumerable<TimeSerieRow.Versioned.V2>>(HttpMethod.Get, resource, ctk: ctk);
            }

            public Task<IEnumerable<TimeSerieRow.Versioned.V2>> GetVersionedTimeSeriesLastN(IEnumerable<int> ids, Granularity granularity, int versionCount, RelativeInterval interval, LocalDate? analysisDate = null, CancellationToken ctk = default(CancellationToken))
            {
                var resource = new UrlComposer($"/marketdata/extractdata/vts/{granularity}/{interval}/Last{versionCount}/{string.Join(",", ids)}")
                    .AddQueryParam("analysisDate", analysisDate);

                return _client.Exec<IEnumerable<TimeSerieRow.Versioned.V2>>(HttpMethod.Get, resource, ctk: ctk);
            }

            public Task<IEnumerable<TimeSerieRow.Actual.V2>> GetVersionedTimeSeriesMostUpdatedVersion(IEnumerable<int> ids, Granularity granularity, LocalDateRange range, CancellationToken ctk = default(CancellationToken))
            {
                var resource = $"/marketdata/extractdata/vts/{granularity}/{UrlComposer.ToUrlParam(range)}/MUV/{string.Join(",", ids)}";

                return _client.Exec<IEnumerable<TimeSerieRow.Actual.V2>>(HttpMethod.Get, resource, ctk: ctk);
            }

            public Task<IEnumerable<TimeSerieRow.Actual.V2>> GetVersionedTimeSeriesMostUpdatedVersion(IEnumerable<int> ids, Granularity granularity, RelativeInterval interval, LocalDate? analysisDate = null, CancellationToken ctk = default(CancellationToken))
            {
                var resource = new UrlComposer($"/marketdata/extractdata/vts/{granularity}/{interval}/MUV/{string.Join(",", ids)}")
                    .AddQueryParam("analysisDate", analysisDate);

                return _client.Exec<IEnumerable<TimeSerieRow.Actual.V2>>(HttpMethod.Get, resource, ctk: ctk);
            }

            public Task<IEnumerable<AssessmentRow.V2>> GetAssessmentSeries(IEnumerable<int> ids, IEnumerable<string> products, LocalDateRange range, CancellationToken ctk = default(CancellationToken))
            {
                return GetAssessmentSeries(ids, products.Select(x => MarketProductBuilder.Parse(x)), range, ctk: ctk);
            }

            public Task<IEnumerable<AssessmentRow.V2>> GetAssessmentSeries(IEnumerable<int> ids, IEnumerable<string> products, RelativeInterval interval, LocalDate? analysisDate = null, CancellationToken ctk = default(CancellationToken))
            {
                return GetAssessmentSeries(ids, products.Select(x => MarketProductBuilder.Parse(x)), interval, analysisDate, ctk: ctk);
            }

            public Task<IEnumerable<AssessmentRow.V2>> GetAssessmentSeries(IEnumerable<int> ids, IEnumerable<IMarketProduct> products, LocalDateRange range, CancellationToken ctk = default(CancellationToken))
            {
                var resource = $"/marketdata/extractdata/mas/{UrlComposer.ToUrlParam(range)}/{string.Join(",", ids)}/{string.Join(",", products)}";

                return _client.Exec<IEnumerable<AssessmentRow.V2>>(HttpMethod.Get, resource, ctk: ctk);
            }

            public Task<IEnumerable<AssessmentRow.V2>> GetAssessmentSeries(IEnumerable<int> ids, IEnumerable<IMarketProduct> products, RelativeInterval interval, LocalDate? analysisDate = null, CancellationToken ctk = default(CancellationToken))
            {
                var resource = new UrlComposer($"/marketdata/extractdata/mas/{interval}/{string.Join(",", ids)}/{string.Join(",", products)}")
                    .AddQueryParam("analysisDate", analysisDate);

                return _client.Exec<IEnumerable<AssessmentRow.V2>>(HttpMethod.Get, resource, ctk: ctk);
            }

            public void Dispose()
            {
                _client.Dispose();
            }
        }
    }
}
