using EnsureThat;


namespace Artesian.SDK.Dependencies.MarketTools.MarketProducts
{
    public abstract class MarketProductBuilder
    {
        public static ProductRelative ParseRelative(string name)
        {
            return ProductRelative.Parse(name);
        }

        public static ProductAbsolute ParseAbsolute(string name)
        {
            return ProductAbsolute.Parse(name);
        }

        public static bool TryParseRelative(string name, out ProductRelative product)
        {
            return ProductRelative.TryParse(name, out product);
        }

        public static bool TryParseAbsolute(string name, out ProductAbsolute product)
        {
            return ProductAbsolute.TryParse(name, out product);
        }
        public static bool TryParseSpecial(string name, out ProductSpecial product)
        {
            return ProductSpecial.TryParse(name, out product);
        }

        public static IMarketProduct Parse(string name)
        {
            EnsureArg.IsNotEmpty(name);

            ProductAbsolute abs;
            ProductRelative rel;
            ProductSpecial special;

            bool isAbs = TryParseAbsolute(name, out abs);
            bool isRel = TryParseRelative(name, out rel);
            bool isSpecial = TryParseSpecial(name, out special);

            int i = 0;
            if (isAbs)
                i++;
            if (isRel)
                i++;
            if (isSpecial)
                i++;

            if (i >= 2)
                throw new MarketProductParseException(name, "ambiguous product name");

            if (isAbs)
                return abs;
            if (isRel)
                return rel;
            if (isSpecial)
                return special;

            throw new MarketProductParseException(name, "cannot parse product");

        }

        public static bool TryParse(string name, out IMarketProduct product)
        {
            EnsureArg.IsNotEmpty(name);

            ProductAbsolute abs;
            ProductRelative rel;
            ProductSpecial special;

            product = new ProductAbsolute(); // fake

            bool isAbs = TryParseAbsolute(name, out abs);
            bool isRel = TryParseRelative(name, out rel);
            bool isSpecial = TryParseSpecial(name, out special);


            if (isAbs)
                product = abs;
            if (isRel)
                product = rel;
            if (isSpecial)
                product = special;

            int i = 0;
            if (isAbs)
                i++;
            if (isRel)
                i++;
            if (isSpecial)
                i++;

            return i == 1;
        }
    }
}
