using System;
using Artesian.SDK.Dto;
using Artesian.SDK.Service;
using NUnit.Framework;

namespace Artesian.SDK.Tests
{
    [TestFixture]
    public class MetaDataQueries
    {
        private ArtesianServiceConfig _cfg = new ArtesianServiceConfig()
        {
          
        };

        [Test]
        public void ReadTimeTransformBaseWithTimeTransformID()
        {
            {
                var mds = new MetaDataService(_cfg);

                var mdq = mds.ReadTimeTransformBaseAsync(1).ConfigureAwait(true).GetAwaiter().GetResult();
            }
        }

        [Test]
        public void ReadTimeTransform()
        {
            {
                var mds = new MetaDataService(_cfg);

                var mdq = mds.ReadTimeTransformsAsync(1, 1, true).ConfigureAwait(true).GetAwaiter().GetResult();
            }
        }

        [Test]
        public void ReadMarketDataByProviderCurveName()
        {
            {
                var mds = new MetaDataService(_cfg);

                var mdq = mds.ReadMarketDataRegistryAsync(new MarketDataIdentifier("GME", "MGPPrezzi_NORD")).ConfigureAwait(true).GetAwaiter().GetResult();
            }
        }

        [Test]
        public void ReadMarketDataByCurveRange()
        {
            {
                var mds = new MetaDataService(_cfg);

                var mdq = mds.ReadCurveRangeAsync(100001250,1,1).ConfigureAwait(true).GetAwaiter().GetResult();
            }
        }

        [Test]
        public void ReadMarketDataRegistryAsync()
        {
            {
                var mds = new MetaDataService(_cfg);

                var mdq = mds.ReadMarketDataRegistryAsync(100001250).ConfigureAwait(true).GetAwaiter().GetResult();
            }
        }

        [Test]
        public void SearchFacetAsync()
        {
            {
                var mds = new MetaDataService(_cfg);
                var filter = new ArtesianSearchFilter();
                filter.SearchText = "gme";
                filter.Page = 1;
                filter.PageSize = 1;
                var mdq = mds.SearchFacetAsync(filter).ConfigureAwait(true).GetAwaiter().GetResult(); ;
            }
        }

    }
}
