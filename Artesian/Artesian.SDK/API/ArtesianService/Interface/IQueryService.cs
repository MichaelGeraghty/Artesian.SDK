using Artesian.SDK.API.ArtesianService.Queries;
using Artesian.SDK.API.DTO;

namespace Artesian.SDK.API.ArtesianService.Interface
{
    interface IQueryService
    {
        ActualQuery CreateActual(int[] ids, Granularity granularity);
        VersionedQuery CreateVersioned(int[] ids, Granularity granularity);
        MasQuery CreateMarketAssessment(int[] ids);
    }
}
