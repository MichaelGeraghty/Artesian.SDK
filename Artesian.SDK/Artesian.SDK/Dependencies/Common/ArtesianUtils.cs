using Artesian.SDK.API.DTO;
using Artesian.SDK.Common.Dto.Api.V2;
using Artesian.SDK.Dependencies.TimeTools;
using EnsureThat;
using FluentValidation;
using NodaTime;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Artesian.SDK.Common
{
    public static class ArtesianUtils
    {
        public static DatePeriod MapDatePeriod(Granularity granularity)
        {
            DatePeriod selectedPeriod = DatePeriod.Day;

            switch (granularity)
            {
                case Granularity.Week:
                    {
                        selectedPeriod = DatePeriod.Week;
                        break;
                    }
                case Granularity.Month:
                    {
                        selectedPeriod = DatePeriod.Month;
                        break;
                    }
                case Granularity.Quarter:
                    {
                        selectedPeriod = DatePeriod.Trimestral;
                        break;
                    }
                case Granularity.Year:
                    {
                        selectedPeriod = DatePeriod.Calendar;
                        break;
                    }
            }

            return selectedPeriod;
        }

        public static TimePeriod MapTimePeriod(Granularity granularity)
        {
            if (!granularity.IsTimeGranularity())
                throw new ArgumentException("not a time granularity", nameof(granularity));

            TimePeriod selectedPeriod = TimePeriod.Hour;

            switch (granularity)
            {
                case Granularity.Hour:
                    {
                        selectedPeriod = TimePeriod.Hour;
                        break;
                    }
                case Granularity.ThirtyMinute:
                    {
                        selectedPeriod = TimePeriod.HalfHour;
                        break;
                    }
                case Granularity.FifteenMinute:
                    {
                        selectedPeriod = TimePeriod.QuarterHour;
                        break;
                    }
                case Granularity.TenMinute:
                    {
                        selectedPeriod = TimePeriod.TenMinutes;
                        break;
                    }
                case Granularity.Minute:
                    {
                        selectedPeriod = TimePeriod.Minute;
                        break;
                    }
            }

            return selectedPeriod;
        }


        public static Uri Append(this Uri uri, params string[] paths)
        {
            return new Uri(paths.Aggregate(uri.AbsoluteUri, (current, path) => string.Format("{0}/{1}", current.TrimEnd('/'), path.TrimStart('/'))));
        }


        public static IRuleBuilderOptions<TParent, string> IsValidString<TParent>(this IRuleBuilder<TParent, string> rule, int minLenght, int maxLenght)
        {
            return rule
                .NotEmpty()
                .Length(minLenght, maxLenght)
                .Matches(ArtesianConstants.CharacterValidatorRegEx)
                    .WithMessage("Invalid string '{0}'. Should not contains trailing or leading whitespaces or any of the following characters: ,:;'\"<space>", (parent, x) => x)
                    ;
        }

        public static IRuleBuilderOptions<TParent, string> IsValidTagKey<TParent>(this IRuleBuilder<TParent, string> rule)
        {
            return rule
                .IsValidString(3, 50)
                .NotEqual("Type")
                .NotEqual("ProviderName")
                .NotEqual("MarketDataName")
                .NotEqual("OriginalGranularity")
                .NotEqual("OriginalTimezone")
                .NotEqual("MarketDataId")
                .NotEqual("AggregationRule")
                ;
        }

        public static IRuleBuilderOptions<TParent, string> IsValidTagValue<TParent>(this IRuleBuilder<TParent, string> rule)
        {
            return rule
                .IsValidString(1, 50)
                ;
        }

        public static IRuleBuilderOptions<TParent, string> IsValidProviderName<TParent>(this IRuleBuilder<TParent, string> rule)
        {
            return rule
                .NotEmpty()
                .IsValidString(1, 50)
                    ;
        }

        public static IRuleBuilderOptions<TParent, string> IsValidMarketDataName<TParent>(this IRuleBuilder<TParent, string> rule)
        {
            return rule
                .NotEmpty()
                .Length(1, 250)
                .Matches(ArtesianConstants.MarketDataNameValidatorRegEx)
                    .WithMessage("Invalid string '{0}'. Should not contains trailing or leading whitespaces and no other whitespace than <space> in the middle.", (parent, x) => x)
                    ;
            ;
        }

        public static IRuleBuilderOptions<TParent, string> IsValidPath<TParent>(this IRuleBuilder<TParent, string> rule)
        {
            return rule
                .MustAsync(async (parent, property, ctk) =>
                {
                    PathString parsed;

                    if (PathString.TryParse(property, out parsed))
                    {
                        if (parsed.IsRoot()
                            || parsed.GetToken(0) == "marketdata"
                            || parsed.GetToken(0) == "timetransform"
                            || parsed.GetToken(0) == "principal")
                            return true;
                    }

                    return false;
                })
                //     .When(x => !string.IsNullOrWhiteSpace(x.Path))
                .WithMessage($"Path should be valid and start with 'marketdata' or 'timetransform' or 'principal' or being root")
            ;
        }

        public static IRuleBuilderOptions<TParent, string> IsValidMarketDataPath<TParent>(this IRuleBuilder<TParent, string> rule)
        {
            return rule
                .MustAsync(async (parent, property, ctk) =>
                {
                    PathString parsed;

                    if (PathString.TryParse(property, out parsed))
                    {
                        if (parsed.IsResource() && parsed.GetToken(0) == "marketdata")
                            return true;
                    }

                    return false;
                })
                //     .When(x => !string.IsNullOrWhiteSpace(x.Path))
                .WithMessage($"Entity should have null or valid path")
            ;
        }

        public static void AddTag(this MarketDataEntity.V2.Input entity, string tagKey, string tagValue)
        {
            if (entity.Tags == null)
                entity.Tags = new Dictionary<string, List<string>>();

            if (!entity.Tags.TryGetValue(tagKey, out var l))
                entity.Tags.Add(tagKey, new List<string>() { { tagValue } });
            else
                l.Add(tagValue);
        }

        public static DateTimeZone AsTimezone(this string timezone)
        {
            EnsureArg.IsNotNullOrWhiteSpace(timezone, nameof(timezone));

            return "UTC".Equals(timezone, StringComparison.OrdinalIgnoreCase) ? DateTimeZone.Utc : DateTimeZoneProviders.Tzdb.GetZoneOrNull(timezone);
        }
    }

}
