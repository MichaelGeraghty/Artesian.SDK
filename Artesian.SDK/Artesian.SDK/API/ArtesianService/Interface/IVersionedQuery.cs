using Artesian.SDK.API.ArtesianService.Config;
using Artesian.SDK.Dependencies;
using NodaTime;
using System;
using System.Collections.Generic;
using System.Text;

namespace Artesian.SDK.API.ArtesianService.Interface
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
