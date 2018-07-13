using Artesian.SDK.Dto;
using Artesian.SDK.QueryService.Queries;

namespace Artesian.SDK.QueryService.Interface
{
    interface IQueryService
    {
        ActualQuery CreateActual();
        VersionedQuery CreateVersioned();
        MasQuery CreateMarketAssessment();
    }
}
