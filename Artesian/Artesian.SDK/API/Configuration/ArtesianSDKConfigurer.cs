using SimpleInjector;
using EnsureThat;
using System;
using System.Net.Http;
using Artesian.SDK.API.MarketData;
using Artesian.SDK.ArtesianService.QueryService;
using Artesian.SDK.API.ArtesianService;

namespace Artesian.SDK.API.Configuration
{
    public class ArtesianSdkConfigurer
    {
        private readonly Container _container;

        public static ArtesianSdkConfigurer Into(Container container)
        {
            return new ArtesianSdkConfigurer(container);
        }

        internal ArtesianSdkConfigurer(Container container)
        {
            _container = container;
            _container.RegisterSingleton<MarketDataFactory>();
        }

        public ArtesianSdkConfigurer WithDefaultArtesianService(ArtesianServiceConfig config)
        {
            EnsureArg.IsNotNull(config);

            _container.RegisterInstance(config);
            _container.RegisterSingleton<Func<HttpMessageHandler>>(() => () => new HttpClientHandler()
            {
                AutomaticDecompression = System.Net.DecompressionMethods.GZip | System.Net.DecompressionMethods.Deflate
            });

            var regArtesianService = Lifestyle.Singleton.CreateRegistration<Artesian.SDK.ArtesianService.ArtesianService.Latest>(_container);
            var regArtesianServiceDeprecated = Lifestyle.Singleton.CreateRegistration<Artesian.SDK.ArtesianService.ArtesianService.Deprecated>(_container);

            _container.AddRegistration<IArtesianService.Latest>(regArtesianService);
            _container.AddRegistration<IArtesianService.V2_1>(regArtesianService);

            _container.AddRegistration<IArtesianService.Deprecated>(regArtesianServiceDeprecated);
            _container.AddRegistration<IArtesianService.V2_0>(regArtesianServiceDeprecated);


            var regQueryService = Lifestyle.Singleton.CreateRegistration<ArtesianQueryService.Latest>(_container);

            _container.AddRegistration<IArtesianQueryService.Latest>(regQueryService);
            _container.AddRegistration<IArtesianQueryService.V1_0>(regQueryService);

            return this;
        }
    }
}
