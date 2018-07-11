using Artesian.SDK.Dto;
using Artesian.SDK.QueryService.Queries;

namespace Artesian.SDK.QueryService.Interface
{
    interface IQueryService
    {
        ActualQuery CreateActual(int[] ids, Granularity granularity);
        VersionedQuery CreateVersioned(int[] ids, Granularity granularity);
        MasQuery CreateMarketAssessment(int[] ids);
    }
}
