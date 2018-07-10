namespace Artesian.SDK.QueryService.Interface
{
    interface IActualQuery<T>: IQuery<T>
    {
        string Build();
    }
}
