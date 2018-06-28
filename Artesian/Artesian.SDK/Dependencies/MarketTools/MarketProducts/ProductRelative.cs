using NodaTime;
using System;
using Artesian.SDK.Dependencies.TimeTools;
using EnsureThat;

namespace Artesian.SDK.Dependencies.MarketTools.MarketProducts
{
    public struct ProductRelative
       : IMarketProduct
       , IEquatable<ProductRelative>
    {
        private MarketProductKind _kind;
        private int _offset;

        public ProductRelative(MarketProductKind kind, int offset)
        {
            EnsureArg.IsGte(offset, 0, nameof(offset));

            _kind = kind;
            _offset = offset;
        }

        public bool Equals(ProductRelative other)
        {
            return _kind == other._kind && _offset == other._offset;
        }

        public static bool operator ==(ProductRelative x, ProductRelative y)
        {
            return x.Equals(y);
        }

        public static bool operator !=(ProductRelative x, ProductRelative y)
        {
            return !x.Equals(y);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is ProductRelative))
                return false;

            return Equals((ProductRelative)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 7243;
                hash = hash * 92821 + _kind.GetHashCode();
                hash = hash * 92821 + _offset.GetHashCode();
                return hash;
            }
        }

        public ProductAbsolute ToAbsolute(LocalDate reportDate)
        {
            LocalDate date = new LocalDate();

            switch (_kind)
            {
                case MarketProductKind.Day:
                    {
                        date = reportDate.PlusDays(this._offset);
                        break;
                    }
                case MarketProductKind.Week:
                    {
                        date = reportDate.FirstDayOfTheWeek().PlusWeeks(this._offset);
                        break;
                    }
                case MarketProductKind.WeekEnd:
                    {
                        date = reportDate.FirstDayOfTheWeek().PlusWeeks(this._offset);
                        while (date.DayOfWeek != IsoDayOfWeek.Saturday)
                            date = date.PlusDays(1);
                        break;
                    }
                case MarketProductKind.WeekWorkingDays:
                    {
                        date = reportDate.FirstDayOfTheWeek().PlusWeeks(this._offset);
                        break;
                    }
                case MarketProductKind.Month:
                    {
                        date = reportDate.FirstDayOfTheMonth().PlusMonths(this._offset);
                        break;
                    }
                case MarketProductKind.Quarter:
                    {
                        date = reportDate.FirstDayOfTheQuarter().PlusMonths(this._offset * 3);
                        break;
                    }
                case MarketProductKind.Season:
                    {
                        date = reportDate.FirstDayOfTheSeason().PlusMonths(this._offset * 6);
                        break;
                    }
                case MarketProductKind.Calendar:
                    {
                        date = reportDate.FirstDayOfTheYear().PlusYears(this._offset);
                        break;
                    }
                case MarketProductKind.GasYear:
                    {
                        var octoberDate = new LocalDate(reportDate.Year, 10, 1);
                        if (reportDate.Month < 10)
                            date = octoberDate.PlusYears(this._offset - 1);
                        else
                            date = octoberDate.PlusYears(this._offset);
                        break;
                    }
            }

            return new ProductAbsolute(_kind, date);
        }

        private static bool _tryParse(string name, out ProductRelative product, out string errorMessage)
        {
            errorMessage = "";
            product = new ProductRelative();

            MarketProductKind kind = MarketProductKind.Day; // a caso
            ushort offset = 0;
            int offsetStart = -1;

            if (name == null)
            {
                errorMessage = "is not a relative product";
            }
            if (name.Length > 6 && name[0] == 'W' && name[1] == 'k' && name[2] == 'E' && name[3] == 'n' && name[4] == 'd' && name[5] == '+') //WkEnd+1
            {
                kind = MarketProductKind.WeekEnd;
                offsetStart = 6;
            }
            else if (name.Length > 6 && name[0] == 'W' && name[1] == 'k' && name[2] == 'D' && name[3] == 'a' && name[4] == 'y' && name[5] == '+') //WkDay+1
            {
                kind = MarketProductKind.WeekWorkingDays;
                offsetStart = 6;
            }
            else if (name.Length > 4 && name[0] == 'C' && name[1] == 'a' && name[2] == 'l' && name[3] == '+') //Cal+1
            {
                kind = MarketProductKind.Calendar;
                offsetStart = 4;
            }
            else if (name.Length > 3 && name[0] == 'G' && name[1] == 'Y' && name[2] == '+') //GY+1
            {
                kind = MarketProductKind.GasYear;
                offsetStart = 3;
            }
            else if (name.Length > 3 && name[0] == 'W' && name[1] == 'k' && name[2] == '+') //Wk+1
            {
                kind = MarketProductKind.Week;
                offsetStart = 3;
            }
            else if (name.Length > 2 && name[1] == '+')
            {
                switch (name[0])
                {
                    case 'D': //D+2
                        kind = MarketProductKind.Day;
                        offsetStart = 2;
                        break;
                    case 'M': //M+2
                        kind = MarketProductKind.Month;
                        offsetStart = 2;
                        break;
                    case 'Q': //Q+2
                        kind = MarketProductKind.Quarter;
                        offsetStart = 2;
                        break;
                    case 'S': //S+2
                        kind = MarketProductKind.Season;
                        offsetStart = 2;
                        break;
                    default:
                        errorMessage = "invalid kind";
                        break;
                }
            }
            else
            {
                errorMessage = "is not a relative product";
            }

            if (offsetStart != -1 && !ushort.TryParse(name.Substring(offsetStart), out offset))
            {
                errorMessage = "invalid offset";
            }

            if (errorMessage != "")
                return false;

            product = new ProductRelative() { _kind = kind, _offset = offset };
            return true;
        }

        public static ProductRelative Parse(string name)
        {
            ProductRelative p;
            string msg;

            if (!_tryParse(name, out p, out msg))
                throw new MarketProductParseException(name, msg);

            return p;
        }

        public static bool TryParse(string name, out ProductRelative product)
        {
            string msg;

            return _tryParse(name, out product, out msg);
        }

        public override string ToString()
        {
            string stringRes = @"#_#'";

            switch (_kind)
            {
                case MarketProductKind.Day: //D+2
                    {
                        stringRes = string.Format("D+{0}", _offset);
                        break;
                    }
                case MarketProductKind.Week: //Wk+1
                    {
                        stringRes = string.Format("Wk+{0}", _offset);
                        break;
                    }
                case MarketProductKind.WeekEnd: //WkEnd+1
                    {
                        stringRes = string.Format("WkEnd+{0}", _offset);
                        break;
                    }
                case MarketProductKind.WeekWorkingDays: //WkDay+1
                    {
                        stringRes = string.Format("WkDay+{0}", _offset);
                        break;
                    }
                case MarketProductKind.Month: //M+2
                    {
                        stringRes = string.Format("M+{0}", _offset);
                        break;
                    }
                case MarketProductKind.Quarter: //Q+2
                    {
                        stringRes = string.Format("Q+{0}", _offset);
                        break;
                    }
                case MarketProductKind.Season: //S+2
                    {
                        stringRes = string.Format("S+{0}", _offset);
                        break;
                    }
                case MarketProductKind.Calendar: //Cal+1
                    {
                        stringRes = string.Format("Cal+{0}", _offset);
                        break;
                    }
                case MarketProductKind.GasYear: //GY+1
                    {
                        stringRes = string.Format("GY+{0}", _offset);
                        break;
                    }
            }

            return stringRes;
        }


        public string Name
        {
            get { return ToString(); }
        }

        public MarketProductType Type
        {
            get { return MarketProductType.Relative; }
        }

        public MarketProductKind Kind
        {
            get { return _kind; }
        }

        public bool Equals(IMarketProduct other)
        {
            return Equals((object)other);
        }

        public IMarketProduct ChangeType(MarketProductType type, LocalDate reportDate)
        {
            Ensure.Comparable.IsGte(_offset, 0, nameof(_offset), o => o.WithMessage(string.Format("offset >= 0 for reportDate {0}", reportDate)));

            if (type == MarketProductType.Absolute)
                return ToAbsolute(reportDate);
            else if (type == MarketProductType.Relative)
                return new ProductRelative(_kind, _offset);
            else
                throw new ArgumentException("invalid type", "type");
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
