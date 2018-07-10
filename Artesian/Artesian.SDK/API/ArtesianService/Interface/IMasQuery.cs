using Artesian.SDK.Dependencies.MarketTools.MarketProducts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Artesian.SDK.API.ArtesianService.Interface
{
    interface IMasQuery<T>: IQuery<T>
    {
        T ForProducts(IEnumerable<IMarketProduct> products);
        string Build();
    }
}
