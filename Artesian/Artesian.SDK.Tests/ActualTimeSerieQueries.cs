using System;
using System.Net.Http;
using Artesian.SDK.Dto;
using Artesian.SDK.Service;
using Flurl.Http.Testing;
using NodaTime;
using NUnit.Framework;

namespace Artesian.SDK.Tests
{
    [TestFixture]
    public class ActualTimeSerieQueries
    {
        private ArtesianServiceConfig _cfg = new ArtesianServiceConfig()
        {
           
        };

        [Test]
        public void ActInRelativeIntervalExtractionWindow()
        {
            using (var httpTest = new HttpTest())
            {
                var qs = new QueryService(_cfg);

                var act = qs.CreateActual()
                       .ForMarketData(new int[] { 100000001 })
                       .InGranularity(Granularity.Day)
                       .InRelativeInterval(RelativeInterval.RollingMonth)
                       .ExecuteAsync().Result;

                httpTest.ShouldHaveCalled($"{_cfg.BaseAddress}query/v1.0/ts/Day/RollingMonth")
                    .WithQueryParamValue("id", 100000001)
                    .WithVerb(HttpMethod.Get)
                    .Times(1);

            }
        }

        [Test]
        public void ActInAbsoluteDateRangeExtractionWindow()
        {
            using (var httpTest = new HttpTest())
            {
                var qs = new QueryService(_cfg);

                var act = qs.CreateActual()
                       .ForMarketData(new int[] { 100000001 })
                       .InGranularity(Granularity.Day)
                       .InAbsoluteDateRange(new LocalDate(2018, 1, 1), new LocalDate(2018, 1, 10))
                       .ExecuteAsync().Result;


                httpTest.ShouldHaveCalled($"{_cfg.BaseAddress}query/v1.0/ts/Day/2018-01-01/2018-01-10")
                        .WithVerb(HttpMethod.Get)
                        .WithQueryParamValue("id", 100000001)
                        .Times(1);

            }
        }

        [Test]
        public void ActInRelativePeriodExtractionWindow()
        {
            using (var httpTest = new HttpTest())
            {
                var qs = new QueryService(_cfg);

                var act = qs.CreateActual()
                       .ForMarketData(new int[] { 100000001 })
                       .InGranularity(Granularity.Day)
                       .InRelativePeriod(Period.FromDays(5))
                       .ExecuteAsync().Result;


                httpTest.ShouldHaveCalled($"{_cfg.BaseAddress}query/v1.0/ts/Day/P5D")
                        .WithVerb(HttpMethod.Get)
                        .WithQueryParamValue("id", 100000001)
                        .Times(1);

            }
        }

        [Test]
        public void ActInRelativePeriodRangeExtractionWindow()
        {
            using (var httpTest = new HttpTest())
            {
                var qs = new QueryService(_cfg);

                var act = qs.CreateActual()
                       .ForMarketData(new int[] { 100000001 })
                       .InGranularity(Granularity.Day)
                       .InRelativePeriodRange(Period.FromWeeks(2), Period.FromDays(20))
                       .ExecuteAsync().Result;


                httpTest.ShouldHaveCalled($"{_cfg.BaseAddress}query/v1.0/ts/Day/P2W/P20D")
                        .WithVerb(HttpMethod.Get)
                        .WithQueryParamValue("id", 100000001)
                        .Times(1);

            }
        }

        [Test]
        public void ActMultipleMarketDataWindow()
        {
            using (var httpTest = new HttpTest())
            {
                var qs = new QueryService(_cfg);

                var act = qs.CreateActual()
                       .ForMarketData(new int[] { 100000001, 100000002, 100000003 })
                       .InGranularity(Granularity.Day)
                       .InRelativePeriodRange(Period.FromWeeks(2), Period.FromDays(20))
                       .ExecuteAsync().Result;

                httpTest.ShouldHaveCalled($"{_cfg.BaseAddress}query/v1.0/ts/Day/P2W/P20D")
                        .WithVerb(HttpMethod.Get)
                        .WithQueryParamValues(new { id = new int[] { 100000001, 100000002, 100000003 } })
                        .Times(1);

            }
        }

        [Test]
        public void ActWithTimeZone()
        {
            using (var httpTest = new HttpTest())
            {
                var qs = new QueryService(_cfg);

                var act = qs.CreateActual()
                       .ForMarketData(new int[] { 100000001 })
                       .InGranularity(Granularity.Day)
                       .InAbsoluteDateRange(new LocalDate(2018, 1, 1), new LocalDate(2018, 1, 10))
                       .InTimezone("CET")
                       .ExecuteAsync().Result;


                httpTest.ShouldHaveCalled($"{_cfg.BaseAddress}query/v1.0/ts/Day/2018-01-01/2018-01-10")
                        .WithVerb(HttpMethod.Get)
                        .WithQueryParamValue("id", 100000001)
                        .WithQueryParamValue("tz", "CET")
                        .Times(1);

            }
        }

        [Test]
        public void ActWithTimeTransfrom()
        {
            using (var httpTest = new HttpTest())
            {
                var qs = new QueryService(_cfg);

                var act = qs.CreateActual()
                       .ForMarketData(new int[] { 100000001 })
                       .InGranularity(Granularity.Day)
                       .InAbsoluteDateRange(new LocalDate(2018, 1, 1), new LocalDate(2018, 1, 10))
                       .WithTimeTransform(1)
                       .ExecuteAsync().Result;


                httpTest.ShouldHaveCalled($"{_cfg.BaseAddress}query/v1.0/ts/Day/2018-01-01/2018-01-10")
                        .WithVerb(HttpMethod.Get)
                        .WithQueryParamValue("id", 100000001)
                        .WithQueryParamValue("tr", 1)
                        .Times(1);

            }

            using (var httpTest = new HttpTest())
            {
                var qs = new QueryService(_cfg);

                var act = qs.CreateActual()
                       .ForMarketData(new int[] { 100000001 })
                       .InGranularity(Granularity.Day)
                       .InAbsoluteDateRange(new LocalDate(2018, 1, 1), new LocalDate(2018, 1, 10))
                       .WithTimeTransform(SystemTimeTransform.THERMALYEAR)
                       .ExecuteAsync().Result;


                httpTest.ShouldHaveCalled($"{_cfg.BaseAddress}query/v1.0/ts/Day/2018-01-01/2018-01-10")
                        .WithVerb(HttpMethod.Get)
                        .WithQueryParamValue("id", 100000001)
                        .WithQueryParamValue("tr", 2)
                        .Times(1);

            }
        }

    }
}
