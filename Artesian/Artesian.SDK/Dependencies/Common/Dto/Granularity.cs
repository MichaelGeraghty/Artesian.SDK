using System;
using Artesian.SDK.Common;
using Artesian.SDK.Dependencies.TimeTools;
using NodaTime;

namespace Artesian.SDK.Dependencies.Common.DTO
{
    //keep Granularity sorted (order is important ( VERY IMPORTANT (DON'T SCREW THIS UP)))
    public enum Granularity
    {
        Hour = 0
        , Day = 1
        , Week = 2
        , Month = 3
        , Quarter = 4
        , Year = 5
        , TenMinute = 6
        , FifteenMinute = 7
        , Minute = 8
        , ThirtyMinute = 9
    }

    public static partial class GranularityExtensions
    {

        public static bool IsTimeGranularity(this Granularity granularity)
        {
            return granularity.IsPartOf(Granularity.Day);
        }


        public static bool IsPartOf(this Granularity smaller, Granularity bigger)
        {
            if (bigger == Granularity.Week)
                return smaller == Granularity.Day || smaller.IsPartOf(Granularity.Day);
            if (smaller == Granularity.Week)
                return false;

            if (bigger == Granularity.FifteenMinute)
                return smaller == Granularity.Minute;

            return smaller._orderOf() < bigger._orderOf();
        }

        private static int _orderOf(this Granularity granularity)
        {
            switch (granularity)
            {
                case Granularity.Minute: return 1;
                case Granularity.TenMinute: return 10;
                case Granularity.FifteenMinute: return 15;
                case Granularity.ThirtyMinute: return 30;
                case Granularity.Hour: return 60;
                case Granularity.Day: return 1440;
                case Granularity.Week: return 10080;
                case Granularity.Month: return 43200;
                case Granularity.Quarter: return 129600;
                case Granularity.Year: return 525600;
            }

            throw new InvalidOperationException($"Granularity {granularity} is not supported");
        }

        public static bool IsGreaterThan(this Granularity first, Granularity second)
        {
            return first._orderOf() > second._orderOf();
        }

        public static bool IsGreaterOrEqualThan(this Granularity first, Granularity second)
        {
            return first._orderOf() >= second._orderOf();
        }

        public static bool IsLessThan(this Granularity first, Granularity second)
        {
            return first._orderOf() < second._orderOf();
        }

        public static bool IsLessOrEqualThan(this Granularity first, Granularity second)
        {
            return first._orderOf() <= second._orderOf();
        }


        public static Period ToPeriod(this Granularity granularity)
        {
            switch (granularity)
            {
                case Granularity.Minute: return Period.FromMinutes(1);
                case Granularity.TenMinute: return Period.FromMinutes(10);
                case Granularity.FifteenMinute: return Period.FromMinutes(15);
                case Granularity.ThirtyMinute: return Period.FromMinutes(30);
                case Granularity.Hour: return Period.FromHours(1);
                case Granularity.Day: return Period.FromDays(1);
                case Granularity.Week: return Period.FromWeeks(1);
                case Granularity.Month: return Period.FromMonths(1);
                case Granularity.Quarter: return Period.FromMonths(3);
                case Granularity.Year: return Period.FromYears(1);
            }

            throw new InvalidOperationException($"Granularity {granularity} is not supported");
        }

    }
}
