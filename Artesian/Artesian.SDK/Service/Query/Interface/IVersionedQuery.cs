﻿using Artesian.SDK.Dto;
using NodaTime;

namespace Artesian.SDK.Service
{
    interface IVersionedQuery<T> : IQuery<T>
    {
        T InGranularity(Granularity granularity);
        T ForLastNVersions(int lastN);
        T ForMUV();
        T ForLastOfDays(LocalDateRange lastOfDateRange);
        T ForLastOfDays(Period lastOfPeriod);
        T ForLastOfDays(PeriodRange lastOfPeriodRange);
        T ForLastOfMonths(LocalDateRange lastOfDateRange);
        T ForLastOfMonths(Period lastOfPeriod);
        T ForLastOfMonths(PeriodRange lastOfPeriodRange);
        T ForVersion(LocalDateTime version);
        T WithTimeTransform(int tr);
        T WithTimeTransform(SystemTimeTransform tr);
    }
}