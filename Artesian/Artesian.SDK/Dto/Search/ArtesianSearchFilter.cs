using Artesian.SDK.Dependencies.Common;
using FluentValidation;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Artesian.SDK.Dto.Search
{
    /// <summary>
    /// The dto for a new search facet based
    /// </summary>
    public class ArtesianSearchFilter
    {
        /// <summary>
        /// Free search text
        /// </summary>
        [Required]
        public string SearchText { get; set; }
        /// <summary>
        /// Filter by facet name, facet values
        /// </summary>
        public IDictionary<string, string[]> Filters { get; set; }
        /// <summary>
        /// sort by field name
        /// </summary>
        public IList<string> Sorts { get; set; }
        /// <summary>
        /// page size
        /// </summary>
        [Required]
        public int PageSize { get; set; }
        /// <summary>
        /// page
        /// </summary>
        [Required]
        public int Page { get; set; }
    }

    public class ArtesianSearchFilterValidator : AbstractValidator<ArtesianSearchFilter>
    {
        public ArtesianSearchFilterValidator()
        {
            RuleFor(x => x.SearchText)
                .NotNull()
                ;

            var validSorts = @"^(MarketDataId|ProviderName|MarketDataName|OriginalGranularity|Type|OriginalTimezone|Created|LastUpdated)( (asc|desc))?$";
            RuleForEach(x => x.Sorts)
                .Matches(validSorts)
                    .WithMessage("{0} is invalid search param", (parent, x) => x)
                ;

            RuleFor(x => x.PageSize)
                .GreaterThanOrEqualTo(0)
                ;

            RuleFor(x => x.Page)
                .GreaterThanOrEqualTo(0)
                ;

            RuleFor(x => x.Filters)
                .SetCollectionValidator(new FilterPairValidator())
                ;
        }

        private class FilterPairValidator : AbstractValidator<KeyValuePair<string, string[]>>
        {
            public FilterPairValidator()
            {
                RuleFor(x => x.Key)
                    .IsValidString(3, 50) // cannot use IsValidTagKey since here ProviderName, OriginaGranularity etc. are valid.
                ;

                When(x => x.Value != null, () =>
                {
                    When(x => x.Key != "MarketDataName", () =>
                    {
                        RuleFor(x => x.Value)
                           .SetCollectionValidator(new FilterValueValidator(50));
                    });

                    When(x => x.Key == "MarketDataName", () =>
                    {
                        RuleFor(x => x.Value)
                           .SetCollectionValidator(new FilterValueValidator(250));
                    });
                });
            }

            private class FilterValueValidator : AbstractValidator<string>
            {
                public FilterValueValidator(int lenght)
                {
                    When(x => !string.IsNullOrEmpty(x), () =>
                    {
                        RuleFor(x => x)
                            .IsValidString(1, lenght)
                            ;
                    });
                }
            }
        }
    }
}
