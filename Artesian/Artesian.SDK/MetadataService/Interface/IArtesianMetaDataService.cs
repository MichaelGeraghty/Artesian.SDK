using Artesian.SDK.Common.Dto.Search;
using Artesian.SDK.Dependencies.Common.Dto.PagedResult;
using Artesian.SDK.Dependencies.Common.Dto.Search;
using Artesian.SDK.Dependencies.Common.Dto.TimeTransform.Dto;
using System.Threading;
using System.Threading.Tasks;

namespace Artesian.SDK.MetadataService.Interface
{
    interface IArtesianMetaDataService
    {
        Task<TimeTransformBase> ReadTimeTransformBaseAsync(int timeTransformId, CancellationToken ctk = default(CancellationToken));
        Task<PagedResult<TimeTransformBase>> ReadTimeTransformsAsync(int page, int pageSize, bool userDefined, CancellationToken ctk = default(CancellationToken));
        Task<ArtesianSearchResults> SearchFacetAsync(ArtesianSearchFilter filter, CancellationToken ctk = default(CancellationToken));
    }
}
