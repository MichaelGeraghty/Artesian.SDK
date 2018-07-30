using System;
using System.Net.Http;
using Artesian.SDK.Dto;
using Artesian.SDK.Service;
using Flurl;
using Flurl.Http.Testing;
using NodaTime;
using NUnit.Framework;

namespace Artesian.SDK.Tests
{
    [TestFixture]
    public class MetaDataQueries
    {
        private ArtesianServiceConfig _cfg = new ArtesianServiceConfig();

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

                httpTest.ShouldHaveCalled($"{_cfg.BaseAddress}v2.1/timeTransform/entity"
                    .SetQueryParam("pageSize", 1)
                    .SetQueryParam("page",1)
                    .SetQueryParam("userDefined", true))
                    .WithVerb(HttpMethod.Get)
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

                httpTest.ShouldHaveCalled($"{_cfg.BaseAddress}v2.1/marketdata/entity"
                    .SetQueryParam("provider", "TestProvider")
                    .SetQueryParam("curveName", "TestCurveName"))
                    .WithVerb(HttpMethod.Get)
                    .Times(1);
            }
        }

        [Test]
        public void ReadMarketDataByCurveRange()
        {
            using (var httpTest = new HttpTest())
            {
                var mds = new MetaDataService(_cfg);

                var mdq = mds.ReadCurveRangeAsync(100000001,1,1,"M+1", new LocalDateTime(2018, 07, 19, 12, 0), new LocalDateTime(2017, 07, 19, 12, 0)).ConfigureAwait(true).GetAwaiter().GetResult();

                httpTest.ShouldHaveCalled($"{_cfg.BaseAddress}v2.1/marketdata/entity/100000001/curves"
                    .SetQueryParam("versionFrom", new LocalDateTime(2018, 07, 19, 12, 0))
                    .SetQueryParam("versionTo", new LocalDateTime(2017, 07, 19, 12, 0))
                    .SetQueryParam("product","M+1")
                    .SetQueryParam("page", 1)
                    .SetQueryParam("pageSize", 1))
                    .WithVerb(HttpMethod.Get)
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
                var mdq = mds.SearchFacetAsync(filter).ConfigureAwait(true).GetAwaiter().GetResult();

                httpTest.ShouldHaveCalled($"{_cfg.BaseAddress}v2.1/marketdata/searchfacet"
                    .SetQueryParam("pageSize", 1)
                    .SetQueryParam("page",1)
                    .SetQueryParam("searchText", "TestText"))
                    .WithVerb(HttpMethod.Get)
                    .Times(1);

            }
        }

    }
}
