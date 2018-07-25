using Artesian.SDK.Dto;

namespace Artesian.SDK.Service
{
    interface IActualQuery<T>: IQuery<T>
    {
        T InGranularity(Granularity granularity);
        T WithTimeTransform(int tr);
        T WithTimeTransform(SystemTimeTransform tr);
    }
}