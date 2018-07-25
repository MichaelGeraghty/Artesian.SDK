namespace Artesian.SDK.Service
{
    public interface IQueryService
    {
        ActualQuery CreateActual();
        VersionedQuery CreateVersioned();
        MasQuery CreateMarketAssessment();
    }
}