namespace Artesian.SDK.API.ArtesianService.Interface
{
    interface IActualQuery<T>: IQuery<T>
    {
        string Build();
    }
}
