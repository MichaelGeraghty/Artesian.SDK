using Artesian.SDK.Dto;
using NodaTime;

namespace Artesian.SDK.Service
{
    interface IVersionedQuery<T> : IQuery<T>
    {
        T InGranularity(Granularity granularity);
        T ForLastNVersions(int lastN);
        T ForMUV();
        T ForLastOfDays(LocalDate start, LocalDate end);
        T ForLastOfDays(Period lastOfPeriod);
        T ForLastOfDays(Period from, Period to);
        T ForLastOfMonths(LocalDate start, LocalDate end);
        T ForLastOfMonths(Period lastOfPeriod);
        T ForLastOfMonths(Period from, Period to);
        T ForVersion(LocalDateTime version);
        T WithTimeTransform(int tr);
        T WithTimeTransform(SystemTimeTransform tr);
    }
}
