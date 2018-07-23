namespace Artesian.SDK.Service
{
    interface IQueryService
    {
        ActualQuery CreateActual();
        VersionedQuery CreateVersioned();
        MasQuery CreateMarketAssessment();
    }
}
