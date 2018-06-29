using EnsureThat;
using NodaTime;
using System;

namespace Artesian.SDK.Dependencies.MarketTools.MarketProducts
{
    public enum MarketProductType
    {
        Absolute,
        Relative,
        Special
    }

    public enum MarketProductKind
    {
        Day,
        Week,
        Month,
        Quarter,
        Season,
        Calendar,
        GasYear,
        WeekEnd,
        WeekWorkingDays
    }

    public class MarketProductParseException : Exception
    {
        public MarketProductParseException(string source, string message)
            : base("Failed parsing product: " + message)
        {
            TextSource = source;
        }

        public MarketProductParseException(string source, string message, Exception innerException)
            : base("Failed parsing product: " + message, innerException)
        {
            TextSource = source;
        }

        public MarketProductParseException(string source, string format, params object[] args)
            : base("Failed parsing product: " + string.Format(format, args))
        {
            EnsureArg.IsNotNull(format);
            EnsureArg.IsNotNull(args);

            TextSource = source;
        }

        public string TextSource { get; private set; }
    }

    public class MarketProductException : Exception
    {
        public MarketProductException(string message)
            : base(message)
        {
        }


        public MarketProductException(string format, params object[] args)
            : base(string.Format(format, args))
        {
            EnsureArg.IsNotNull(format);
            EnsureArg.IsNotNull(args);
        }

    }


    public interface IMarketProduct
        : IEquatable<IMarketProduct>
    {
        string Name { get; }
        MarketProductType Type { get; }
        MarketProductKind Kind { get; }

        IMarketProduct ChangeType(MarketProductType type, LocalDate reportDate);
        bool TryChangeType(MarketProductType type, LocalDate reportDate, out IMarketProduct product);
    }
}
