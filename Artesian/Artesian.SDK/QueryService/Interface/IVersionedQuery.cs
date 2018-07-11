using Artesian.SDK.QueryService.Config;
using Artesian.SDK.QueryService.Configuration;
using NodaTime;

namespace Artesian.SDK.QueryService.Interface
{
    interface IVersionedQuery<T>: IQuery<T>
    {
        T ForLastNVersions(int lastN);
        
        T ForMUV();
        
        T ForLastOfDays(LocalDateRange lastOfDateRange);
        
        T ForLastOfDays(Period lastOfPeriod);
        
        T ForLastOfDays(PeriodRange lastOfPeriodRange);
        
        T ForLastOfMonths(LocalDateRange lastOfDateRange);
        
        T ForLastOfMonths(Period lastOfPeriod);
        
        T ForLastOfMonths(PeriodRange lastOfPeriodRange);
        
        T ForVersion(LocalDateTime version);

        string Build();
    }
}
