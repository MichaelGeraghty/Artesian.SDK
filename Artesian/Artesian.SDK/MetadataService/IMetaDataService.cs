using Artesian.SDK.Dto.PagedResult;
using Artesian.SDK.Dto.Search;
using Artesian.SDK.Dto.TimeTransform.Dto;
using System.Threading;
using System.Threading.Tasks;

namespace Artesian.SDK.MetadataService.Interface
{
    interface IMetaDataService
    {
        Task<TimeTransformBase> ReadTimeTransformBaseAsync(int timeTransformId, CancellationToken ctk = default(CancellationToken));
        Task<PagedResult<TimeTransformBase>> ReadTimeTransformsAsync(int page, int pageSize, bool userDefined, CancellationToken ctk = default(CancellationToken));
        Task<ArtesianSearchResults> SearchFacetAsync(ArtesianSearchFilter filter, CancellationToken ctk = default(CancellationToken));
    }
}
