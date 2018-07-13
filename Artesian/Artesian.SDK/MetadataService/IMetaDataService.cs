using Artesian.SDK.Dto;
using Artesian.SDK.Dto.PagedResult;
using Artesian.SDK.Dto.Search;
using System.Threading;
using System.Threading.Tasks;

namespace Artesian.SDK.MetadataService.Interface
{
    interface IMetaDataService
    {
        Task<TimeTransform> ReadTimeTransformBaseAsync(int timeTransformId, CancellationToken ctk = default(CancellationToken));
        Task<PagedResult<TimeTransform>> ReadTimeTransformsAsync(int page, int pageSize, bool userDefined, CancellationToken ctk = default(CancellationToken));
        Task<ArtesianSearchResults> SearchFacetAsync(ArtesianSearchFilter filter, CancellationToken ctk = default(CancellationToken));
    }
}
