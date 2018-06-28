using NLog;
using EnsureThat;
using System.Threading;
using System.Threading.Tasks;
using Artesian.SDK.API.Dto;

namespace Artesian.SDK.API.MarketData
{
    public class MarketDataFactory
    {

        private readonly IArtesianService.Latest _ArtesianService;
        private static Logger _logger = LogManager.GetCurrentClassLogger();


        public MarketDataFactory(
            IArtesianService.Latest ArtesianService)
        {
            EnsureArg.IsNotNull(ArtesianService);

            _ArtesianService = ArtesianService;

        }

        #region Actual Time Serie
        public async Task<ActualTimeSerie> CreateActualTimeSerie(MarketDataIdentifier id, CancellationToken ctk = default(CancellationToken))
        {
            ActualTimeSerie actual = new ActualTimeSerie(_ArtesianService);
            await actual.Create(id);
            return actual;
        }
        public async Task<ActualTimeSerie> CreateActualTimeSerie(string provider, string name, CancellationToken ctk = default(CancellationToken))
        {
            ActualTimeSerie actual = new ActualTimeSerie(_ArtesianService);
            await actual.Create(provider, name);
            return actual;
        }

        #endregion Actual Time Serie

        #region Versioned Time Serie
        public async Task<VersionedTimeSerie> CreateVersionedTimeSerie(MarketDataIdentifier id, CancellationToken ctk = default(CancellationToken))
        {
            VersionedTimeSerie actual = new VersionedTimeSerie(_ArtesianService);
            await actual.Create(id);
            return actual;
        }
        public async Task<VersionedTimeSerie> CreateVersionedTimeSerie(string provider, string name, CancellationToken ctk = default(CancellationToken))
        {
            VersionedTimeSerie actual = new VersionedTimeSerie(_ArtesianService);
            await actual.Create(provider, name);
            return actual;
        }

        #endregion Versioned Time Serie

        #region Market  Assessment
        public async Task<MarketAssessment> CreateMarketAssessment(MarketDataIdentifier id, CancellationToken ctk = default(CancellationToken))
        {
            MarketAssessment mas = new MarketAssessment(_ArtesianService);
            await mas.Create(id);
            return mas;
        }
        public async Task<MarketAssessment> CreateMarketAssessment(string provider, string name, CancellationToken ctk = default(CancellationToken))
        {
            MarketAssessment mas = new MarketAssessment(_ArtesianService);
            await mas.Create(provider, name);
            return mas;
        }


        #endregion Market  Assessment



    }
}
