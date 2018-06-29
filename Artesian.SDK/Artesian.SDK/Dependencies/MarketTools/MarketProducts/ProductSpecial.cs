using NodaTime;
using System;

namespace Artesian.SDK.Dependencies.MarketTools.MarketProducts
{
    public struct ProductSpecial
       : IMarketProduct
       , IEquatable<ProductSpecial>
    {
        private readonly ProductSpecialName _name;

        public ProductSpecial(ProductSpecialName name)
        {
            _name = name;
        }

        public bool Equals(ProductSpecial other)
        {
            return _name == other._name;
        }

        public static bool operator ==(ProductSpecial x, ProductSpecial y)
        {
            return x.Equals(y);
        }

        public static bool operator !=(ProductSpecial x, ProductSpecial y)
        {
            return !x.Equals(y);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is ProductSpecial))
                return false;

            return Equals((ProductSpecial)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 7243;
                hash = hash * 92821 + _name.GetHashCode();
                return hash;
            }
        }

        public static ProductSpecial Parse(string name)
        {
            var res = _parse(name);
            if (res.HasValue)
                return res.Value;

            throw new MarketProductParseException(name, "Unsupported Product cannot be parsed");
        }

        public static ProductSpecial? _parse(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return null;

            switch (name)
            {
                case "BOFM":
                    return new ProductSpecial(ProductSpecialName.BalanceOfFrontMonth);
                case "BOM":
                    return new ProductSpecial(ProductSpecialName.BalanceOfMonth);
                case "BOQ":
                    return new ProductSpecial(ProductSpecialName.BalanceOfQuarter);
                case "BOW":
                    return new ProductSpecial(ProductSpecialName.BalanceOfWeek);
                case "BOY":
                    return new ProductSpecial(ProductSpecialName.BalanceOfYear);
                case "DA":
                    return new ProductSpecial(ProductSpecialName.DayAhead);
                case "MA":
                    return new ProductSpecial(ProductSpecialName.MonthAhead);
                case "WkA":
                    return new ProductSpecial(ProductSpecialName.WeekAhead);
                case "WE":
                    return new ProductSpecial(ProductSpecialName.Weekend);
                case "WD":
                    return new ProductSpecial(ProductSpecialName.WithinDay);
                case "WDY-NW":
                    return new ProductSpecial(ProductSpecialName.WorkingDayNextWeek);
                default:
                    return null;
            }
        }

        public static bool TryParse(string name, out ProductSpecial product)
        {
            var res = _parse(name);
            product = res.GetValueOrDefault();

            return res.HasValue;
        }

        public override string ToString()
        {
            switch (_name)
            {
                case ProductSpecialName.BalanceOfFrontMonth:
                    return "BOFM";
                case ProductSpecialName.BalanceOfMonth:
                    return "BOM";
                case ProductSpecialName.BalanceOfQuarter:
                    return "BOQ";
                case ProductSpecialName.BalanceOfWeek:
                    return "BOW";
                case ProductSpecialName.BalanceOfYear:
                    return "BOY";
                case ProductSpecialName.DayAhead:
                    return "DA";
                case ProductSpecialName.MonthAhead:
                    return "MA";
                case ProductSpecialName.WeekAhead:
                    return "WkA";
                case ProductSpecialName.Weekend:
                    return "WE";
                case ProductSpecialName.WithinDay:
                    return "WD";
                case ProductSpecialName.WorkingDayNextWeek:
                    return "WDY-NW";

            }
            return "IMPOSSIBLE";
        }

        public string Name
        {
            get { return ToString(); }
        }

        public MarketProductType Type
        {
            get { return MarketProductType.Special; }
        }

        MarketProductKind IMarketProduct.Kind
        {
            get
            {

                switch (_name)
                {
                    case ProductSpecialName.BalanceOfMonth:
                        return MarketProductKind.Month;
                    case ProductSpecialName.BalanceOfWeek:
                        return MarketProductKind.Week;
                    case ProductSpecialName.DayAhead:
                        return MarketProductKind.Day;
                    case ProductSpecialName.MonthAhead:
                        return MarketProductKind.Month;
                    case ProductSpecialName.WeekAhead:
                        return MarketProductKind.Week;
                    case ProductSpecialName.WorkingDayNextWeek:
                        return MarketProductKind.Day;
                    case ProductSpecialName.Weekend:
                        return MarketProductKind.Day;
                    case ProductSpecialName.WithinDay:
                        return MarketProductKind.Day;
                    case ProductSpecialName.BalanceOfFrontMonth:
                        return MarketProductKind.Month;
                    case ProductSpecialName.BalanceOfQuarter:
                        return MarketProductKind.Quarter;
                    case ProductSpecialName.BalanceOfYear:
                        return MarketProductKind.Calendar;
                }
                throw new InvalidOperationException(string.Format("Invalid productName for ProductSpecial {0}", _name));
            }
        }


        public bool Equals(IMarketProduct other)
        {
            return Equals((object)other);
        }


        public IMarketProduct ChangeType(MarketProductType type, LocalDate reportDate)
        {
            if (type == MarketProductType.Special)
                return new ProductSpecial(_name);
            throw new NotSupportedException();
        }
        public bool TryChangeType(MarketProductType type, LocalDate reportDate, out IMarketProduct product)
        {
            try
            {
                product = this.ChangeType(type, reportDate);
            }
            catch
            {
                product = null;
                return false;
            }
            return true;
        }
    }
}
