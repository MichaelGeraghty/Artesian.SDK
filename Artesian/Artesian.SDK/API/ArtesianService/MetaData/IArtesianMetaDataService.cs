using NodaTime;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Artesian.SDK.API.Dto.PagedResult;
using Artesian.SDK.API.Dto.Auth;
using Artesian.SDK.API.Dto.Auth.Dto;
using Artesian.SDK.Common;
using Artesian.SDK.Common.Dto.TimeTransform.Dto;
using Artesian.SDK.Common.Dto.Api.V2;
using Artesian.SDK.API.Dto;
using Artesian.SDK.Common.Dto.Search;
using Artesian.SDK.Common.Dto.Api.V2.Operations;

namespace Artesian.SDK.API.ArtesianService.MetaData
{
    public static class IArtesianMetaDataService
    {
        public interface V2_1 : V2_0
        {
            Task<AuthGroup> CreateAuthGroup(AuthGroup group, CancellationToken ctk = default(CancellationToken));
            Task<AuthGroup> UpdateAuthGroup(int groupID, AuthGroup group, CancellationToken ctk = default(CancellationToken));
            Task<AuthGroup> ReadAuthGroup(int groupID, CancellationToken ctk = default(CancellationToken));
            Task RemoveAuthGroup(int groupID, CancellationToken ctk = default(CancellationToken));
            Task<PagedResult<AuthGroup>> ReadAuthGroups(int page, int pageSize, CancellationToken ctk = default(CancellationToken));
            Task<AuthGroup> AddUsersToGroup(int groupID, List<string> users, Guid? ifMatch = null, CancellationToken ctk = default(CancellationToken));
            Task<AuthGroup> SetGroupUsers(int groupID, List<string> users, Guid? ifMatch = null, CancellationToken ctk = default(CancellationToken));
            Task<AuthGroup> RemoveUsersFromGroup(int groupID, List<string> users, Guid? ifMatch = null, CancellationToken ctk = default(CancellationToken));
            Task<List<Principals>> ReadUserPrincipals(string user, CancellationToken ctk = default(CancellationToken));

            Task<IEnumerable<AuthorizationPath.Output>> ReadRolesByPath(PathString path, CancellationToken ctk = default(CancellationToken));
            Task<PagedResult<AuthorizationPath.Output>> GetRoles(int page, int pageSize, string[] principalIds, LocalDateTime? asOf = null, CancellationToken ctk = default(CancellationToken));
            Task UpsertRoles(AuthorizationPath.Input upsert, CancellationToken ctk = default(CancellationToken));
            Task AddRoles(AuthorizationPath.Input add, CancellationToken ctk = default(CancellationToken));
            Task RemoveRoles(AuthorizationPath.Input remove, CancellationToken ctk = default(CancellationToken));

            // TimeTransform
            Task<TimeTransformBase> RegisterTimeTransformBaseAsync(TimeTransformBase timeTransformSimpleShift, CancellationToken ctk = default(CancellationToken));
            Task<TimeTransformBase> ReadTimeTransformBaseAsync(int timeTransformId, CancellationToken ctk = default(CancellationToken));
            Task<TimeTransformBase> UpdateTimeTransformBaseAsync(TimeTransformBase timeTransformSimpleShift, CancellationToken ctk = default(CancellationToken));
            Task DeleteTimeTransformSimpleShiftAsync(int timeTransformSimpleShiftID, CancellationToken ctk = default(CancellationToken));
            Task<PagedResult<TimeTransformBase>> ReadTimeTransformsAsync(int page, int pageSize, bool userDefined, CancellationToken ctk = default(CancellationToken));

            //ReadAPI
            Task<PagedResult<CurveRangeV2>> ReadCurveRange(int id, int page, int pageSize, string product = null, LocalDateTime? versionFrom = null, LocalDateTime? versionTo = null, CancellationToken ctk = default(CancellationToken));
        }

        public interface V2_0
        {
            // WriteAPI
            Task<MarketDataEntity.V2.Output> RegisterMarketDataAsync(MarketDataEntity.V2.Input metadata, CancellationToken ctk = default(CancellationToken));
            Task<MarketDataEntity.V2.Output> UpdateMarketDataAsync(MarketDataEntity.V2.Input metadata, CancellationToken ctk = default(CancellationToken));
            Task DeleteMarketDataAsync(int id, CancellationToken ctk = default(CancellationToken));

            // ReadAPI
            Task<MarketDataEntity.V2.Output> ReadMarketDataRegistryAsync(MarketDataIdentifier id, CancellationToken ctk = default(CancellationToken));
            Task<MarketDataEntity.V2.Output> ReadMarketDataRegistryAsync(int id, CancellationToken ctk = default(CancellationToken));
            Task<ArtesianSearchResults> SearchFacetAsync(ArtesianSearchFilter filter, CancellationToken ctk = default(CancellationToken));

            // OperationApi
            Task<List<MarketDataEntity.V2.Output>> PerformOperationsAsync(Operations operations, CancellationToken ctk = default(CancellationToken));
        }
    }
}
