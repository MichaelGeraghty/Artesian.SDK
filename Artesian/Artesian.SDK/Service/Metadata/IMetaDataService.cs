// Copyright (c) ARK LTD. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for
// license information. 
using Artesian.SDK.Dto;
using NodaTime;
using System.Threading;
using System.Threading.Tasks;

namespace Artesian.SDK.Service
{
    interface IMetadataService
    {
        Task<TimeTransform> ReadTimeTransformBaseAsync(int timeTransformId, CancellationToken ctk = default(CancellationToken));
        Task<PagedResult<TimeTransform>> ReadTimeTransformsAsync(int page, int pageSize, bool userDefined, CancellationToken ctk = default(CancellationToken));
        Task<MarketDataEntity.Output> ReadMarketDataRegistryAsync(MarketDataIdentifier id, CancellationToken ctk = default(CancellationToken));
        Task<MarketDataEntity.Output> ReadMarketDataRegistryAsync(int id, CancellationToken ctk = default(CancellationToken));
        Task<PagedResult<CurveRange>> ReadCurveRangeAsync(int id, int page, int pageSize, string product = null, LocalDateTime? versionFrom = null, LocalDateTime? versionTo = null, CancellationToken ctk = default(CancellationToken));
        Task<ArtesianSearchResults> SearchFacetAsync(ArtesianSearchFilter filter, CancellationToken ctk = default(CancellationToken));
    }
}
