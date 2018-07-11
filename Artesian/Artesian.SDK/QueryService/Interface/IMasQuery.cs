namespace Artesian.SDK.QueryService.Interface
{
    interface IMasQuery<T>: IQuery<T>
    {
        T ForProducts(params string[] products);
        string Build();
    }
}
