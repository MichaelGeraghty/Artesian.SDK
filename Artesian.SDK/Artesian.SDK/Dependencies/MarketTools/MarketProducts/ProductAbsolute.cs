using NodaTime;
using System;
using System.Globalization;
using Artesian.SDK.Dependencies.TimeTools;

using NodaTime.Calendars;
using EnsureThat;

namespace Artesian.SDK.Dependencies.MarketTools.MarketProducts
{
    public struct ProductAbsolute
         : IMarketProduct
         , IEquatable<ProductAbsolute>
    {
        private readonly MarketProductKind _kind;
        private readonly LocalDate _referenceDate;

        public ProductAbsolute(MarketProductKind kind, LocalDate referenceDate)
        {
            _kind = kind;
            bool referenceDateIsValid = false;
            LocalDate octoberDate = new LocalDate(referenceDate.Year, 10, 1);
            switch (_kind)
            {
                case MarketProductKind.Week:
                    referenceDateIsValid = referenceDate == referenceDate.FirstDayOfTheWeek();
                    break;
                case MarketProductKind.Month:
                    referenceDateIsValid = referenceDate == referenceDate.FirstDayOfTheMonth();
                    break;
                case MarketProductKind.Quarter:
                    referenceDateIsValid = referenceDate == referenceDate.FirstDayOfTheQuarter();
                    break;
                case MarketProductKind.Season:
                    referenceDateIsValid = referenceDate == referenceDate.FirstDayOfTheSeason();
                    break;
                case MarketProductKind.Calendar:
                    referenceDateIsValid = referenceDate == referenceDate.FirstDayOfTheYear();
                    break;
                case MarketProductKind.GasYear:
                    {
                        if (referenceDate.Month < 10)
                            octoberDate = octoberDate.PlusYears(-1);
                        referenceDateIsValid = referenceDate == referenceDate.FirstDayOfTheMonth() && referenceDate.Month == 10;
                        break;
                    }
                case MarketProductKind.Day:
                    referenceDateIsValid = true;
                    break;
                case MarketProductKind.WeekEnd:
                    referenceDateIsValid = referenceDate.DayOfWeek == NodaTime.IsoDayOfWeek.Saturday; //must be saturday
                    break;
                case MarketProductKind.WeekWorkingDays:
                    referenceDateIsValid = referenceDate == referenceDate.FirstDayOfTheWeek();
                    break;
            }
            if (referenceDateIsValid)
                _referenceDate = referenceDate;
            else
                throw new MarketProductException("Invalid Reference Date {0} for Product {1}", referenceDate, kind);

        }

        public bool Equals(ProductAbsolute other)
        {
            return _kind == other._kind && _referenceDate == other._referenceDate;
        }

        public static bool operator ==(ProductAbsolute x, ProductAbsolute y)
        {
            return x.Equals(y);
        }

        public static bool operator !=(ProductAbsolute x, ProductAbsolute y)
        {
            return !x.Equals(y);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is ProductAbsolute))
                return false;

            return Equals((ProductAbsolute)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 7243;
                hash = hash * 92821 + _kind.GetHashCode();
                hash = hash * 92821 + _referenceDate.GetHashCode();
                return hash;
            }
        }



        public ProductRelative ToRelative(LocalDate reportDate)
        {
            EnsureArg.IsLt(reportDate, ReferenceDate, nameof(reportDate));

            long offset = 0;

            switch (_kind)
            {
                case MarketProductKind.Day:
                    offset = Period.Between(reportDate, _referenceDate, PeriodUnits.Days).Days;
                    break;
                case MarketProductKind.Week:
                    offset = Period.Between(reportDate.FirstDayOfTheWeek(), _referenceDate, PeriodUnits.Weeks).Weeks;
                    break;
                case MarketProductKind.Month:
                    offset = Period.Between(reportDate.FirstDayOfTheMonth(), _referenceDate, PeriodUnits.Months).Months;
                    break;
                case MarketProductKind.Quarter:
                    offset = Period.Between(reportDate.FirstDayOfTheQuarter(), _referenceDate, PeriodUnits.Months).Months / 3;
                    break;
                case MarketProductKind.Season:
                    offset = Period.Between(reportDate.FirstDayOfTheSeason(), _referenceDate, PeriodUnits.Months).Months / 6;
                    break;
                case MarketProductKind.Calendar:
                    offset = Period.Between(reportDate.FirstDayOfTheYear(), _referenceDate, PeriodUnits.Years).Years;
                    break;
                case MarketProductKind.GasYear:
                    {
                        var octoberDate = new LocalDate(reportDate.Year, 10, 1);
                        if (reportDate.Month < 10)
                            octoberDate = octoberDate.PlusYears(-1);
                        offset = Period.Between(octoberDate, _referenceDate, PeriodUnits.Years).Years;
                        break;
                    }
                case MarketProductKind.WeekEnd:
                    offset = Period.Between(reportDate.FirstDayOfTheWeek(), _referenceDate, PeriodUnits.Weeks).Weeks;
                    break;
                case MarketProductKind.WeekWorkingDays:
                    offset = Period.Between(reportDate.FirstDayOfTheWeek(), _referenceDate, PeriodUnits.Weeks).Weeks;
                    break;

            }

            Ensure.Comparable.IsGte(offset, 0, nameof(offset), o => o.WithMessage(string.Format("offset >= 0 for reportDate {0}", reportDate)));

            return new ProductRelative(_kind, (int)offset);
        }

        private static bool _tryParse(string name, out ProductAbsolute product, out string errorMessage)
        {
            EnsureArg.IsNotEmpty(name);

            MarketProductKind kind;
            LocalDate referenceDate = new LocalDate();

            errorMessage = string.Format("Failed to parse '{0}': ", name);
            product = new ProductAbsolute();

            if (name.Length < 4)
            {
                errorMessage += "too short.";
                return false;
            }
            //WATCH OUT: don't change case order.
            if (name[0] == 'G') //GasYr-15
                kind = MarketProductKind.GasYear;
            else if (name[0] == 'Q') //Q112
                kind = MarketProductKind.Quarter;
            else if (name[0] == 'W' && name[1] == 'k' && name[2] == 'E') //WkEnd25-15
                kind = MarketProductKind.WeekEnd;
            else if (name[0] == 'W' && name[1] == 'k' && name[2] == 'D') //WkDay52-15
                kind = MarketProductKind.WeekWorkingDays;
            else if (name[0] == 'W' && name[1] == 'k') //Wk18-16
                kind = MarketProductKind.Week;
            else if (name[0] == 'W' && name[1] == 'i' && name[2] == 'n') //Win-09
                kind = MarketProductKind.Season;
            else if (name[0] == 'S' && name[1] == 'u' && name[2] == 'm') //Sum-09
                kind = MarketProductKind.Season;
            else if (name.Length == 6) //Aug-16
                kind = MarketProductKind.Month;
            else if (name.Length == 4) //2015
                kind = MarketProductKind.Calendar;
            else //Thu2015-05-28
                kind = MarketProductKind.Day;

            switch (kind)
            {
                case MarketProductKind.Day: //Thu2015-05-28
                    {
                        if (name.Length != 13)
                        {
                            errorMessage = "invalid lenght.";
                            return false;
                        }

                        int year, month, day;
                        if (
                            !int.TryParse(name.Substring(3, 4), out year) ||
                            !int.TryParse(name.Substring(8, 2), out month) ||
                            !int.TryParse(name.Substring(11, 2), out day))
                        {
                            errorMessage = "cannot parse date.";
                            return false;
                        }

                        try
                        {
                            referenceDate = new LocalDate(year, month, day);
                        }
                        catch
                        {
                            errorMessage = "invalid date.";
                            return false;
                        }
                        break;
                    }
                case MarketProductKind.Week: //Wk18-15
                    {
                        if (name.Length != 7)
                        {
                            errorMessage = "invalid lenght.";
                            return false;
                        }

                        int weekOfWeekYear, year;
                        if (
                            !int.TryParse(name.Substring(5, 2), out year) ||
                            !int.TryParse(name.Substring(2, 2), out weekOfWeekYear))
                        {
                            errorMessage = "cannot parse date.";
                            return false;
                        }

                        year += year >= 70 ? 1900 : 2000;

                        try
                        {
                            referenceDate = LocalDate.FromWeekYearWeekAndDay(year, weekOfWeekYear, IsoDayOfWeek.Monday);
                        }
                        catch
                        {
                            errorMessage = "invalid date.";
                            return false;
                        }
                        break;
                    }
                case MarketProductKind.WeekEnd: //WkEnd25-15
                    {
                        if (name.Length != 10)
                        {
                            errorMessage = "invalid lenght.";
                            return false;
                        }

                        int weekOfWeekYear, year;
                        if (
                            !int.TryParse(name.Substring(8, 2), out year) ||
                            !int.TryParse(name.Substring(5, 2), out weekOfWeekYear))
                        {
                            errorMessage = "cannot parse date.";
                            return false;
                        }

                        year += year >= 70 ? 1900 : 2000;

                        try
                        {
                            referenceDate = LocalDate.FromWeekYearWeekAndDay(year, weekOfWeekYear, IsoDayOfWeek.Saturday);
                        }
                        catch
                        {
                            errorMessage = "invalid date.";
                            return false;
                        }
                        break;
                    }
                case MarketProductKind.WeekWorkingDays: //WkDay52-15
                    {
                        if (name.Length != 10)
                        {
                            errorMessage = "invalid lenght.";
                            return false;
                        }

                        int weekOfWeekYear, year;
                        if (
                            !int.TryParse(name.Substring(8, 2), out year) ||
                            !int.TryParse(name.Substring(5, 2), out weekOfWeekYear))
                        {
                            errorMessage = "cannot parse date.";
                            return false;
                        }

                        year += year >= 70 ? 1900 : 2000;

                        try
                        {
                            referenceDate = LocalDate.FromWeekYearWeekAndDay(year, weekOfWeekYear, IsoDayOfWeek.Monday);
                        }
                        catch
                        {
                            errorMessage = "invalid date.";
                            return false;
                        }
                        break;
                    }
                case MarketProductKind.Month: //Aug-16
                    {
                        if (name.Length != 6)
                        {
                            errorMessage = "invalid lenght.";
                            return false;
                        }

                        int month, year;
                        if (!int.TryParse(name.Substring(4, 2), out year))
                        {
                            errorMessage = "cannot parse year.";
                            return false;
                        }

                        year += year >= 70 ? 1900 : 2000;

                        month = 1 + Array.IndexOf(
                            CultureInfo.InvariantCulture.DateTimeFormat.AbbreviatedMonthNames,
                            CultureInfo.InvariantCulture.TextInfo.ToTitleCase(name.Substring(0, 3).ToLowerInvariant()));

                        if (month == 0)
                        {
                            errorMessage = "invalid month name.";
                            return false;
                        }

                        try
                        {
                            referenceDate = new LocalDate(year, month, 1);
                        }
                        catch
                        {
                            errorMessage = "invalid date.";
                            return false;
                        }
                        break;
                    }
                case MarketProductKind.Quarter: //Q112
                    {
                        if (name.Length != 4)
                        {
                            errorMessage = "invalid lenght.";
                            return false;
                        }

                        int year;
                        if (!int.TryParse(name.Substring(2, 2), out year))
                        {
                            errorMessage = "cannot parse year.";
                            return false;
                        }

                        year += year >= 70 ? 1900 : 2000;
                        int q = name[1] - '0';

                        if (!(q >= 1 && q <= 4))
                        {
                            errorMessage = "cannot parse quarter.";
                            return false;
                        }

                        try
                        {
                            referenceDate = new LocalDate(year, (q - 1) * 3 + 1, 1);
                        }
                        catch
                        {
                            errorMessage = "invalid date.";
                            return false;
                        }
                        break;
                    }
                case MarketProductKind.Season: //Win-09
                    {
                        if (name.Length != 6)
                        {
                            errorMessage = "invalid lenght.";
                            return false;
                        }

                        int year;
                        if (!int.TryParse(name.Substring(4, 2), out year))
                        {
                            errorMessage = "cannot parse year.";
                            return false;
                        }

                        year += year >= 70 ? 1900 : 2000;
                        int month = name[0] == 'W' ? 10 : 4;

                        try
                        {
                            referenceDate = new LocalDate(year, month, 1);
                        }
                        catch
                        {
                            errorMessage = "invalid date.";
                            return false;
                        }
                        break;
                    }
                case MarketProductKind.Calendar: //2015
                    {
                        if (name.Length != 4)
                        {
                            errorMessage = "invalid lenght.";
                            return false;
                        }

                        int year;
                        if (!int.TryParse(name, out year))
                        {
                            errorMessage = "cannot parse year.";
                            return false;
                        }

                        try
                        {
                            referenceDate = new LocalDate(year, 1, 1);
                        }
                        catch
                        {
                            errorMessage = "invalid date.";
                            return false;
                        }
                        break;
                    }
                case MarketProductKind.GasYear: //GasYr-15
                    {
                        if (name.Length != 8)
                        {
                            errorMessage = "invalid lenght.";
                            return false;
                        }

                        int year;
                        if (!int.TryParse(name.Substring(6, 2), out year))
                        {
                            errorMessage = "cannot parse year.";
                            return false;
                        }

                        year += year >= 70 ? 1900 : 2000;

                        try
                        {
                            referenceDate = new LocalDate(year, 10, 1);
                        }
                        catch
                        {
                            errorMessage = "invalid date.";
                            return false;
                        }
                        break;
                    }
            }

            product = new ProductAbsolute(kind, referenceDate);
            return true;
        }

        public static ProductAbsolute Parse(string name)
        {
            EnsureArg.IsNotEmpty(name);

            ProductAbsolute p;
            string msg;
            if (_tryParse(name, out p, out msg))
            {
                return p;
            }
            else
            {
                throw new MarketProductParseException(name, msg);
            }
        }

        public static bool TryParse(string name, out ProductAbsolute product)
        {
            EnsureArg.IsNotEmpty(name);

            string msg;
            return _tryParse(name, out product, out msg);
        }

        public override string ToString()
        {
            string stringRes = @"#_#'";

            switch (_kind)
            {
                case MarketProductKind.Day: //Thu2015-05-28
                    {
                        var enDay = _referenceDate.ToString("ddd", CultureInfo.InvariantCulture);
                        var date = _referenceDate.ToString(@"yyyy-MM-dd", CultureInfo.InvariantCulture);
                        stringRes = string.Format("{0}{1}", enDay, date);
                        break;
                    }
                case MarketProductKind.Week: //Wk18-16
                    {
                        int weekNumber = WeekYearRules.Iso.GetWeekOfWeekYear(_referenceDate);
                        var year = WeekYearRules.Iso.GetWeekYear(_referenceDate);
                        stringRes = string.Format("Wk{0:00}-{1:00}", weekNumber, year % 100);
                        break;
                    }
                case MarketProductKind.WeekEnd: //WkEnd25-15
                    {
                        int weekNumber = WeekYearRules.Iso.GetWeekOfWeekYear(_referenceDate);
                        var year = WeekYearRules.Iso.GetWeekYear(_referenceDate);
                        stringRes = string.Format("WkEnd{0:00}-{1:00}", weekNumber, year % 100);
                        break;
                    }
                case MarketProductKind.WeekWorkingDays: //WkDay52-15
                    {
                        int weekNumber = WeekYearRules.Iso.GetWeekOfWeekYear(_referenceDate);
                        var year = WeekYearRules.Iso.GetWeekYear(_referenceDate);
                        stringRes = string.Format("WkDay{0:00}-{1:00}", weekNumber, year % 100);
                        break;
                    }
                case MarketProductKind.Month: //Aug-16
                    {
                        stringRes = _referenceDate.ToString("MMM-yy", CultureInfo.InvariantCulture);
                        break;
                    }
                case MarketProductKind.Quarter: //Q112
                    {
                        var year = _referenceDate.ToString("yy", CultureInfo.InvariantCulture);
                        var quarter = (int)((_referenceDate.Month - 1) / 3) + 1;
                        stringRes = string.Format("Q{0}{1}", quarter, year);
                        break;
                    }
                case MarketProductKind.Season: //Win-09
                    {
                        var year = _referenceDate.Month <= 3 ? _referenceDate.Year - 1 : _referenceDate.Year;
                        var season = _referenceDate.Month >= 9 || _referenceDate.Month <= 3 ? "Win" : "Sum";
                        stringRes = string.Format("{0}-{1:00}", season, year % 100);
                        break;
                    }
                case MarketProductKind.Calendar: //2015
                    {
                        stringRes = _referenceDate.ToString("yyyy", CultureInfo.InvariantCulture);
                        break;
                    }
                case MarketProductKind.GasYear: //GasYr-15
                    {
                        stringRes = _referenceDate.ToString("'GasYr'-yy", CultureInfo.InvariantCulture);
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
            get { return MarketProductType.Absolute; }
        }

        MarketProductKind IMarketProduct.Kind
        {
            get { return _kind; }
        }

        public LocalDate ReferenceDate
        {
            get { return _referenceDate; }
        }

        public bool Equals(IMarketProduct other)
        {
            return Equals((object)other);
        }


        public IMarketProduct ChangeType(MarketProductType type, LocalDate reportDate)
        {
            if (type == MarketProductType.Absolute)
                return new ProductAbsolute(_kind, _referenceDate);
            else if (type == MarketProductType.Relative)
                return ToRelative(reportDate);
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
