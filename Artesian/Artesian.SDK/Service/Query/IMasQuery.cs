namespace Artesian.SDK.Service
{
    interface IMasQuery<T>: IQuery<T>
    {
        T ForProducts(params string[] products);
    }
}