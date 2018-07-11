using Artesian.SDK.Dto;
using Artesian.SDK.QueryService.Config;

namespace Artesian.SDK.QueryService.Interface
{
    interface IActualQuery<T>: IQuery<T>
    {
        T InGranularity(Granularity granularity);
        T WithTimeTransform(int tr);
        T WithTimeTransform(SystemTimeTransform tr);
    }
}
