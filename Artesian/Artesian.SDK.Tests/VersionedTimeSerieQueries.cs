using System;
using System.Text;
using System.Collections.Generic;
using NUnit.Framework;
using Artesian.SDK.Service;
using Flurl.Http.Testing;
using Artesian.SDK.Dto;
using System.Net.Http;
using NodaTime;

namespace Artesian.SDK.Tests
{
    /// <summary>
    /// Summary description for VersionedTimeSerieQueries
    /// </summary>
    [TestFixture]
    public class VersionedTimeSerieQueries
    {
        private ArtesianServiceConfig _cfg = new ArtesianServiceConfig()
        {
         
        };

        [Test]
        public void VerInPeriodRelativeIntervalLastOfMonths()
        {
            {
                var qs = new QueryService(_cfg);

                var ver = qs.CreateVersioned()
                        .ForMarketData(new int[] { 100000028 })
                        .InGranularity(Granularity.Day)
                        .ForLastOfMonths(Period.FromMonths(-4))
                        .InRelativeInterval(RelativeInterval.RollingMonth)
                        .ExecuteAsync().Result;
            }
        }

        [Test]
        public void VerInPeriodAbsoluteDateRangeLastOfMonths()
        {
            {
                var qs = new QueryService(_cfg);

                var ver = qs.CreateVersioned()
                        .ForMarketData(new int[] { 100000028 })
                        .InGranularity(Granularity.Day)
                        .ForLastOfMonths(Period.FromMonths(-4))
                        .InAbsoluteDateRange(new LocalDate(2018, 5, 22), new LocalDate(2018, 7, 23))
                        .ExecuteAsync().Result;
            }
        }

        [Test]
        public void VerInPeriodRelativePeriodLastOfMonths()
        {
            {
                var qs = new QueryService(_cfg);

                var ver = qs.CreateVersioned()
                        .ForMarketData(new int[] { 100000028 })
                        .InGranularity(Granularity.Day)
                        .ForLastOfMonths(Period.FromMonths(-4))
                        .InRelativePeriod(Period.FromDays(5))
                        .ExecuteAsync().Result;
            }
        }


        [Test]
        public void VerInPeriodRelativePeriodRangeLastOfMonths()
        {
            {
                var qs = new QueryService(_cfg);

                var ver = qs.CreateVersioned()
                        .ForMarketData(new int[] { 100000028 })
                        .InGranularity(Granularity.Day)
                        .ForLastOfMonths(Period.FromMonths(-4))
                        .InRelativePeriodRange(Period.FromMonths(-2), Period.FromDays(5))
                        .ExecuteAsync().Result;
            }
        }

        [Test]
        public void VerInPeriodRangeRelativeIntervalLastOfMonths()
        {
            {
                var qs = new QueryService(_cfg);

                var ver = qs.CreateVersioned()
                        .ForMarketData(new int[] { 100000028 })
                        .InGranularity(Granularity.Day)
                        .ForLastOfMonths(Period.FromMonths(-4), Period.FromDays(20))
                        .InRelativeInterval(RelativeInterval.RollingMonth)
                        .ExecuteAsync().Result;
            }
        }

        [Test]
        public void VerInPeriodRangeAbsoluteDateRangeLastOfMonths()
        {
            {
                var qs = new QueryService(_cfg);

                var ver = qs.CreateVersioned()
                        .ForMarketData(new int[] { 100000028 })
                        .InGranularity(Granularity.Day)
                        .ForLastOfMonths(Period.FromMonths(-4), Period.FromDays(20))
                        .InAbsoluteDateRange(new LocalDate(2018, 5, 22), new LocalDate(2018, 7, 23))
                        .ExecuteAsync().Result;
            }
        }

        [Test]
        public void VerInPeriodRangeRelativePeriodLastOfMonths()
        {
            {
                var qs = new QueryService(_cfg);

                var ver = qs.CreateVersioned()
                        .ForMarketData(new int[] { 100000028 })
                        .InGranularity(Granularity.Day)
                        .ForLastOfMonths(Period.FromMonths(-4), Period.FromDays(20))
                        .InRelativePeriod(Period.FromDays(5))
                        .ExecuteAsync().Result;
            }
        }

        [Test]
        public void VerInPeriodRangeRelativePeriodRangeLastOfMonths()
        {
            {
                var qs = new QueryService(_cfg);

                var ver = qs.CreateVersioned()
                        .ForMarketData(new int[] { 100000028 })
                        .InGranularity(Granularity.Day)
                        .ForLastOfMonths(Period.FromMonths(-4), Period.FromDays(20))
                        .InRelativePeriodRange(Period.FromMonths(-2), Period.FromDays(5))
                        .ExecuteAsync().Result;
            }
        }

        [Test]
        public void VerInDateRangeRelativeIntervalLastOfMonths()
        {
            {
                var qs = new QueryService(_cfg);

                var ver = qs.CreateVersioned()
                        .ForMarketData(new int[] { 100000028 })
                        .InGranularity(Granularity.Day)
                        .ForLastOfMonths(new LocalDate(2018, 3, 22), new LocalDate(2018, 7, 23))
                        .InRelativeInterval(RelativeInterval.RollingMonth)
                        .ExecuteAsync().Result;
            }
        }

        [Test]
        public void VerInDateRangeAbsoluteDateRangeLastOfMonths()
        {
            {
                var qs = new QueryService(_cfg);

                var ver = qs.CreateVersioned()
                        .ForMarketData(new int[] { 100000028 })
                        .InGranularity(Granularity.Day)
                        .ForLastOfMonths(new LocalDate(2018, 5, 22), new LocalDate(2018, 7, 23))
                        .InAbsoluteDateRange(new LocalDate(2018, 5, 22), new LocalDate(2018, 7, 23))
                        .ExecuteAsync().Result;
            }
        }

        [Test]
        public void VerInDateRangeRelativePeriodRangeLastOfMonths()
        {
            {
                var qs = new QueryService(_cfg);

                var ver = qs.CreateVersioned()
                        .ForMarketData(new int[] { 100000028 })
                        .InGranularity(Granularity.Day)
                        .ForLastOfMonths(new LocalDate(2018, 5, 22), new LocalDate(2018, 7, 23))
                        .InRelativePeriodRange(Period.FromWeeks(2), Period.FromDays(20))
                        .ExecuteAsync().Result;
            }
        }

        [Test]
        public void VerDateRangeRelativePeriodLastOfMonths()
        {
            {
                var qs = new QueryService(_cfg);

                var ver = qs.CreateVersioned()
                        .ForMarketData(new int[] { 100000028 })
                        .InGranularity(Granularity.Day)
                        .ForLastOfMonths(new LocalDate(2018, 5, 22), new LocalDate(2018, 7, 23))
                        .InRelativePeriod(Period.FromDays(5))
                        .ExecuteAsync().Result;
            }
        }

        [Test]
        public void VerInPeriodRelativeIntervalLastOfDays()
        {
            {
                var qs = new QueryService(_cfg);

                var ver = qs.CreateVersioned()
                        .ForMarketData(new int[] { 100000028 })
                        .InGranularity(Granularity.Day)
                        .ForLastOfDays(Period.FromDays(-20))
                        .InRelativeInterval(RelativeInterval.RollingMonth)
                        .ExecuteAsync().Result;
            }
        }

        [Test]
        public void VerInPeriodAbsoluteDateRangeLastOfDays()
        {
            {
                var qs = new QueryService(_cfg);

                var ver = qs.CreateVersioned()
                        .ForMarketData(new int[] { 100000028 })
                        .InGranularity(Granularity.Day)
                        .ForLastOfDays(Period.FromDays(-20))
                        .InAbsoluteDateRange(new LocalDate(2018, 6, 22), new LocalDate(2018, 7, 23))
                        .ExecuteAsync().Result;
            }
        }

        [Test]
        public void VerInPeriodRelativePeriodLastOfDays()
        {
            {
                var qs = new QueryService(_cfg);

                var ver = qs.CreateVersioned()
                        .ForMarketData(new int[] { 100000028 })
                        .InGranularity(Granularity.Day)
                        .ForLastOfDays(Period.FromDays(-20))
                        .InRelativePeriod(Period.FromDays(5))
                        .ExecuteAsync().Result;
            }
        }


        [Test]
        public void VerInPeriodRelativePeriodRangeLastOfDays()
        {
            {
                var qs = new QueryService(_cfg);

                var ver = qs.CreateVersioned()
                        .ForMarketData(new int[] { 100000028 })
                        .InGranularity(Granularity.Day)
                        .ForLastOfDays(Period.FromDays(-20))
                        .InRelativePeriodRange(Period.FromMonths(-1), Period.FromDays(5))
                        .ExecuteAsync().Result;
            }
        }

        [Test]
        public void VerInPeriodRangeRelativeIntervalLastOfDays()
        {
            {
                var qs = new QueryService(_cfg);

                var ver = qs.CreateVersioned()
                        .ForMarketData(new int[] { 100000028 })
                        .InGranularity(Granularity.Day)
                        .ForLastOfDays(Period.FromMonths(-1), Period.FromDays(20))
                        .InRelativeInterval(RelativeInterval.RollingMonth)
                        .ExecuteAsync().Result;
            }
        }

        [Test]
        public void VerInPeriodRangeAbsoluteDateRangeLastOfDays()
        {
            {
                var qs = new QueryService(_cfg);

                var ver = qs.CreateVersioned()
                        .ForMarketData(new int[] { 100000028 })
                        .InGranularity(Granularity.Day)
                        .ForLastOfDays(Period.FromMonths(-1), Period.FromDays(20))
                        .InAbsoluteDateRange(new LocalDate(2018, 6, 22), new LocalDate(2018, 7, 23))
                        .ExecuteAsync().Result;
            }
        }

        [Test]
        public void VerInPeriodRangeRelativePeriodLastOfDays()
        {
            {
                var qs = new QueryService(_cfg);

                var ver = qs.CreateVersioned()
                        .ForMarketData(new int[] { 100000028 })
                        .InGranularity(Granularity.Day)
                        .ForLastOfDays(Period.FromMonths(-1), Period.FromDays(20))
                        .InRelativePeriod(Period.FromDays(5))
                        .ExecuteAsync().Result;
            }
        }

        [Test]
        public void VerInPeriodRangeRelativePeriodRangeLastOfDays()
        {
            {
                var qs = new QueryService(_cfg);

                var ver = qs.CreateVersioned()
                        .ForMarketData(new int[] { 100000028 })
                        .InGranularity(Granularity.Day)
                        .ForLastOfDays(Period.FromMonths(-1), Period.FromDays(20))
                        .InRelativePeriodRange(Period.FromMonths(-1), Period.FromDays(20))
                        .ExecuteAsync().Result;
            }
        }

        [Test]
        public void VerInDateRangeRelativeIntervalLastOfDays()
        {
            {
                var qs = new QueryService(_cfg);

                var ver = qs.CreateVersioned()
                        .ForMarketData(new int[] { 100000028 })
                        .InGranularity(Granularity.Day)
                        .ForLastOfDays(new LocalDate(2018, 5, 22), new LocalDate(2018, 7, 23))
                        .InRelativeInterval(RelativeInterval.RollingMonth)
                        .ExecuteAsync().Result;
            }
        }

        [Test]
        public void VerInDateRangeAbsoluteDateRangeLastOfDays()
        {
            {
                var qs = new QueryService(_cfg);

                var ver = qs.CreateVersioned()
                        .ForMarketData(new int[] { 100000028 })
                        .InGranularity(Granularity.Day)
                        .ForLastOfDays(new LocalDate(2018, 6, 22), new LocalDate(2018, 7, 23))
                        .InAbsoluteDateRange(new LocalDate(2018, 6, 22), new LocalDate(2018, 7, 23))
                        .ExecuteAsync().Result;
            }
        }

        [Test]
        public void VerInDateRangeRelativePeriodRangeLastOfDays()
        {
            {
                var qs = new QueryService(_cfg);

                var ver = qs.CreateVersioned()
                        .ForMarketData(new int[] { 100000028 })
                        .InGranularity(Granularity.Day)
                        .ForLastOfDays(new LocalDate(2018, 5, 22), new LocalDate(2018, 7, 23))
                        .InRelativePeriodRange(Period.FromWeeks(2), Period.FromDays(20))
                        .ExecuteAsync().Result;
            }
        }

        [Test]
        public void VerDateRangeRelativePeriodLastOfDays()
        {
            {
                var qs = new QueryService(_cfg);

                var ver = qs.CreateVersioned()
                        .ForMarketData(new int[] { 100000028 })
                        .InGranularity(Granularity.Day)
                        .ForLastOfDays(new LocalDate(2018, 5, 22), new LocalDate(2018, 7, 23))
                        .InRelativePeriod(Period.FromDays(5))
                        .ExecuteAsync().Result;
            }
        }


        [Test]
        public void VerInRelativeIntervalLastN()
        {
            {
                var qs = new QueryService(_cfg);

                var ver = qs.CreateVersioned()
                        .ForMarketData(new int[] { 100000028 })
                        .InGranularity(Granularity.Day)
                        .ForLastNVersions(3)
                        .InRelativeInterval(RelativeInterval.RollingMonth)
                        .ExecuteAsync().Result;
            }
        }


        [Test]
        public void VerInAbsoluteDateRangeLastN()
        {
            {
                var qs = new QueryService(_cfg);

                var ver = qs.CreateVersioned()
                        .ForMarketData(new int[] { 100000028 })
                        .InGranularity(Granularity.Day)
                        .ForLastNVersions(3)
                        .InAbsoluteDateRange(new LocalDate(2018, 5, 22), new LocalDate(2018, 7, 23))
                        .ExecuteAsync().Result;
            }
        }

        [Test]
        public void VerInRelativePeriodExtractionWindowLastN()
        {
            {
                var qs = new QueryService(_cfg);

                var ver = qs.CreateVersioned()
                        .ForMarketData(new int[] { 100000028 })
                        .InGranularity(Granularity.Day)
                        .ForLastNVersions(3)
                        .InRelativePeriod(Period.FromDays(5))
                        .InRelativeInterval(RelativeInterval.RollingMonth)
                        .ExecuteAsync().Result;
            }
        }

        [Test]
        public void VerInRelativePeriodRangeExtractionWindowLastN()
        {
            {
                var qs = new QueryService(_cfg);

                var ver = qs.CreateVersioned()
                        .ForMarketData(new int[] { 100000028 })
                        .InGranularity(Granularity.Day)
                        .ForLastNVersions(3)
                        .InRelativePeriodRange(Period.FromWeeks(2), Period.FromDays(20))
                        .InRelativeInterval(RelativeInterval.RollingMonth)
                        .ExecuteAsync().Result;
            }
        }

        [Test]
        public void VerInRelativeIntervalVersion()
        {
            {
                var qs = new QueryService(_cfg);

                var ver = qs.CreateVersioned()
                        .ForMarketData(new int[] { 100000028 })
                        .InGranularity(Granularity.Day)
                        .ForVersion(new LocalDateTime(2018,07,12,10,0))
                        .InRelativeInterval(RelativeInterval.RollingMonth)
                        .ExecuteAsync().Result;
            }
        }

        [Test]
        public void VerInAbsoluteDateRangeVersion()
        {
            {
                var qs = new QueryService(_cfg);

                var ver = qs.CreateVersioned()
                        .ForMarketData(new int[] { 100000028 })
                        .InGranularity(Granularity.Day)
                        .ForVersion(new LocalDateTime(2018, 07, 12, 10, 0))
                        .InAbsoluteDateRange(new LocalDate(2018, 7, 22), new LocalDate(2018, 7, 23))
                        .ExecuteAsync().Result;
            }
        }

        [Test]
        public void VerInRelativePeriodExtractionWindowVersion()
        {
            {
                var qs = new QueryService(_cfg);

                var ver = qs.CreateVersioned()
                        .ForMarketData(new int[] { 100000028 })
                        .InGranularity(Granularity.Day)
                        .ForVersion(new LocalDateTime(2018, 07, 12, 10, 0))
                        .InRelativePeriod(Period.FromDays(5))
                        .InRelativeInterval(RelativeInterval.RollingMonth)
                        .ExecuteAsync().Result;
            }
        }

        [Test]
        public void VerInRelativePeriodRangeExtractionWindowVersion()
        {
            {
                var qs = new QueryService(_cfg);

                var ver = qs.CreateVersioned()
                        .ForMarketData(new int[] { 100000028 })
                        .InGranularity(Granularity.Day)
                        .ForVersion(new LocalDateTime(2018, 07, 12, 10, 0))
                        .InRelativePeriodRange(Period.FromWeeks(2), Period.FromDays(20))
                        .InRelativeInterval(RelativeInterval.RollingMonth)
                        .ExecuteAsync().Result;
            }
        }

        [Test]
        public void VerInRelativeIntervalMUV()
        {
            {
                var qs = new QueryService(_cfg);

                var ver = qs.CreateVersioned()
                       .ForMarketData(new int[] { 100001250 })
                       .InGranularity(Granularity.Day)
                       .ForMUV()
                       .InRelativeInterval(RelativeInterval.RollingMonth)
                       .ExecuteAsync().Result;

            }
        }

        [Test]
        public void VerInAbsoluteDateRangeMUV()
        {
            {
                var qs = new QueryService(_cfg);

                var ver = qs.CreateVersioned()
                       .ForMarketData(new int[] { 100001250 })
                       .InGranularity(Granularity.Day)
                       .ForMUV()
                       .InAbsoluteDateRange(new LocalDate(2018, 7, 22), new LocalDate(2018, 7, 23))
                       .ExecuteAsync().Result;

            }
        }

        [Test]
        public void VerInRelativePeriodExtractionWindowMUV()
        {
            {
                var qs = new QueryService(_cfg);

                var ver = qs.CreateVersioned()
                       .ForMarketData(new int[] { 100001250 })
                       .InGranularity(Granularity.Day)
                       .InRelativePeriod(Period.FromDays(5))
                       .ForMUV()
                       .InRelativeInterval(RelativeInterval.RollingMonth)
                       .ExecuteAsync().Result;

            }
        }

        [Test]
        public void VerInRelativePeriodRangeExtractionWindowMUV()
        {
            {
                var qs = new QueryService(_cfg);

                var ver = qs.CreateVersioned()
                       .ForMarketData(new int[] { 100001250 })
                       .InGranularity(Granularity.Day)
                       .InRelativePeriodRange(Period.FromWeeks(2), Period.FromDays(20))
                       .ForMUV()
                       .InRelativeInterval(RelativeInterval.RollingMonth)
                       .ExecuteAsync().Result;

            }
        }

        [Test]
        public void VerWithMultipleMarketDataWindowLastOfDays()
        {
            {
                var qs = new QueryService(_cfg);

                var ver = qs.CreateVersioned()
                       .ForMarketData(new int[] { 100001250, 100000028, 100000029 })
                       .InGranularity(Granularity.Day)
                       .ForLastOfDays(new LocalDate(2018, 6, 22), new LocalDate(2018, 7, 23))
                       .InRelativePeriodRange(Period.FromWeeks(2), Period.FromDays(20))
                       .ExecuteAsync().Result;

            }
        }

        [Test]
        public void VerWithMultipleMarketDataWindowLastOfMonths()
        {
            {
                var qs = new QueryService(_cfg);

                var ver = qs.CreateVersioned()
                       .ForMarketData(new int[] { 100001250, 100000028, 100000029 })
                       .InGranularity(Granularity.Day)
                       .ForLastOfMonths(new LocalDate(2018, 5, 22), new LocalDate(2018, 7, 23))
                       .InRelativePeriodRange(Period.FromWeeks(2), Period.FromDays(20))
                       .ExecuteAsync().Result;

            }
        }

        [Test]
        public void VerWithMultipleMarketDataWindowLastN()
        {
            {
                var qs = new QueryService(_cfg);

                var ver = qs.CreateVersioned()
                       .ForMarketData(new int[] { 100001250, 100000028, 100000029 })
                       .InGranularity(Granularity.Day)
                       .ForLastNVersions(3)
                       .InRelativeInterval(RelativeInterval.RollingMonth)
                       .ExecuteAsync().Result;

            }
        }

        [Test]
        public void VerWithMultipleMarketDataWindowMUV()
        {
            {
                var qs = new QueryService(_cfg);

                var ver = qs.CreateVersioned()
                       .ForMarketData(new int[] { 100001250, 100000028, 100000029 })
                       .InGranularity(Granularity.Day)
                       .InRelativePeriod(Period.FromDays(5))
                       .ForMUV()
                       .InRelativeInterval(RelativeInterval.RollingMonth)
                       .ExecuteAsync().Result;
            }
        }

        [Test]
        public void VerWithMultipleMarketDataWindowVersion()
        {
            {
                var qs = new QueryService(_cfg);

                var ver = qs.CreateVersioned()
                       .ForMarketData(new int[] { 100001250, 100000028, 100000029 })
                       .InGranularity(Granularity.Day)
                       .ForVersion(new LocalDateTime(2018, 07, 12, 10, 0))
                       .InRelativeInterval(RelativeInterval.RollingMonth)
                       .ExecuteAsync().Result;
            }
        }

        [Test]
        public void VerWithTimeTransfromLastOfDays()
        {
            {
                var qs = new QueryService(_cfg);

                var ver = qs.CreateVersioned()
                       .ForMarketData(new int[] { 100000028 })
                       .InGranularity(Granularity.Day)
                       .ForLastOfDays(new LocalDate(2018, 6, 22), new LocalDate(2018, 7, 23))
                       .InRelativePeriodRange(Period.FromWeeks(2), Period.FromDays(20))
                       .WithTimeTransform(1)
                       .ExecuteAsync().Result;

            }
        }

        [Test]
        public void VerWithTimeTransfromLastOfMonths()
        {
            {
                var qs = new QueryService(_cfg);

                var ver = qs.CreateVersioned()
                       .ForMarketData(new int[] { 100000028 })
                       .InGranularity(Granularity.Day)
                       .ForLastOfMonths(new LocalDate(2018, 5, 22), new LocalDate(2018, 7, 23))
                       .InRelativePeriodRange(Period.FromWeeks(2), Period.FromDays(20))
                       .WithTimeTransform(1)
                       .ExecuteAsync().Result;

            }
        }

        [Test]
        public void VerWithTimeTransfromLastN()
        {
            {
                var qs = new QueryService(_cfg);

                var ver = qs.CreateVersioned()
                       .ForMarketData(new int[] { 100000028 })
                       .InGranularity(Granularity.Day)
                       .ForLastNVersions(3)
                       .InRelativeInterval(RelativeInterval.RollingMonth)
                       .WithTimeTransform(1)
                       .ExecuteAsync().Result;

            }
        }

        [Test]
        public void VerWithTimeTransfromMUV()
        {
            {
                var qs = new QueryService(_cfg);

                var ver = qs.CreateVersioned()
                       .ForMarketData(new int[] { 100001250 })
                       .InGranularity(Granularity.Day)
                       .InRelativePeriod(Period.FromDays(5))
                       .ForMUV()
                       .InRelativeInterval(RelativeInterval.RollingMonth)
                       .WithTimeTransform(1)
                       .ExecuteAsync().Result;
            }
        }

        [Test]
        public void VerWithTimeTransfromVersion()
        {
            {
                var qs = new QueryService(_cfg);

                var ver = qs.CreateVersioned()
                       .ForMarketData(new int[] { 100000028 })
                        .InGranularity(Granularity.Day)
                        .ForVersion(new LocalDateTime(2018, 07, 12, 10, 0))
                        .InRelativeInterval(RelativeInterval.RollingMonth)
                        .WithTimeTransform(1)
                       .ExecuteAsync().Result;
            }
        }
    }
}
