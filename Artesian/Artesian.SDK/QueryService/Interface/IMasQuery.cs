using Artesian.SDK.Dependencies.MarketTools.MarketProducts;
using System.Collections.Generic;

namespace Artesian.SDK.QueryService.Interface
{
    interface IMasQuery<T>: IQuery<T>
    {
        T ForProducts(IEnumerable<IMarketProduct> products);
        string Build();
    }
}
