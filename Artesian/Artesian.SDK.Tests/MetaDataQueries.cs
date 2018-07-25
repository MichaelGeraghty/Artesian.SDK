using System;
using System.Net.Http;
using Artesian.SDK.Dto;
using Artesian.SDK.Service;
using Flurl.Http.Testing;
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
            using (var httpTest = new HttpTest())
            {
                var mds = new MetaDataService(_cfg);

                var mdq = mds.ReadTimeTransformBaseAsync(1).ConfigureAwait(true).GetAwaiter().GetResult();


                httpTest.ShouldHaveCalled($"{_cfg.BaseAddress}v2.1/timeTransform/entity/1")
                   .WithVerb(HttpMethod.Get)
                   .Times(1);
            }
        }

        [Test]
        public void ReadTimeTransform()
        {
            using (var httpTest = new HttpTest())
            {
                var mds = new MetaDataService(_cfg);

                var mdq = mds.ReadTimeTransformsAsync(1, 1, true).ConfigureAwait(true).GetAwaiter().GetResult();

                httpTest.ShouldHaveCalled($"{_cfg.BaseAddress}v2.1/timeTransform/entity")
                    .WithVerb(HttpMethod.Get)
                    .WithQueryParamValue("page", 1)
                    .WithQueryParamValue("pageSize", 1)
                    .WithQueryParamValue("userDefined", true)
                    .Times(1);
            }
        }

        [Test]
        public void ReadMarketDataByProviderCurveName()
        {
            using (var httpTest = new HttpTest())
            {
                var mds = new MetaDataService(_cfg);

                var mdq = mds.ReadMarketDataRegistryAsync(new MarketDataIdentifier("TestProvider", "TestCurveName")).ConfigureAwait(true).GetAwaiter().GetResult();

                httpTest.ShouldHaveCalled($"{_cfg.BaseAddress}v2.1/marketdata/entity")
                    .WithVerb(HttpMethod.Get)
                    .WithQueryParamValue("provider", "TestProvider")
                    .WithQueryParamValue("curveName", "TestCurveName")
                    .Times(1);
            }
        }

        [Test]
        public void ReadMarketDataByCurveRange()
        {
            using (var httpTest = new HttpTest())
            {
                var mds = new MetaDataService(_cfg);

                var mdq = mds.ReadCurveRangeAsync(100000001,1,1).ConfigureAwait(true).GetAwaiter().GetResult();

                httpTest.ShouldHaveCalled($"{_cfg.BaseAddress}v2.1/marketdata/entity/100000001")
                    .WithVerb(HttpMethod.Get)
                    .WithoutQueryParamValue("versionFrom", "P-20D")
                    .WithoutQueryParamValue("versionTo", "P20M")
                    .WithQueryParamValue("page", 1)
                    .WithQueryParamValue("pageSize", 1)
                    .Times(1);
            }
        }

        [Test]
        public void ReadMarketDataRegistryAsync()
        {
            using (var httpTest = new HttpTest())
            {
                var mds = new MetaDataService(_cfg);

                var mdq = mds.ReadMarketDataRegistryAsync(100000001).ConfigureAwait(true).GetAwaiter().GetResult();

                httpTest.ShouldHaveCalled($"{_cfg.BaseAddress}v2.1/marketdata/entity/100000001")
                   .WithVerb(HttpMethod.Get)
                   .Times(1);
            }
        }

        [Test]
        public void SearchFacetAsync()
        {
            using (var httpTest = new HttpTest())
            {
                var mds = new MetaDataService(_cfg);
                var filter = new ArtesianSearchFilter();
                filter.SearchText = "TestText";
                filter.Page = 1;
                filter.PageSize = 1;
                var mdq = mds.SearchFacetAsync(filter).ConfigureAwait(true).GetAwaiter().GetResult(); ;

                httpTest.ShouldHaveCalled($"{_cfg.BaseAddress}v2.1/marketdata/searchfacet")
                   .WithVerb(HttpMethod.Get)
                   .WithQueryParamValue("page", 1)
                   .WithQueryParamValue("pageSize", 1)
                   .WithQueryParamValue("searchText", "TestText")
                   .Times(1);

            }
        }

    }
}
