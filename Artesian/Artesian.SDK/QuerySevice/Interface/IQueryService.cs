using Artesian.SDK.QueryService.Queries;
using Artesian.SDK.Dependencies.Common.DTO;

namespace Artesian.SDK.QueryService.Interface
{
    interface IQueryService
    {
        ActualQuery CreateActual(int[] ids, Granularity granularity);
        VersionedQuery CreateVersioned(int[] ids, Granularity granularity);
        MasQuery CreateMarketAssessment(int[] ids);
    }
}
