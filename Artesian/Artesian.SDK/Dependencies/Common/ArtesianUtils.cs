using Artesian.SDK.Configuration;
using EnsureThat;
using FluentValidation;

namespace Artesian.SDK.Dependencies.Common
{
    public static class ArtesianUtils
    {
        
        public static IRuleBuilderOptions<TParent, string> IsValidString<TParent>(this IRuleBuilder<TParent, string> rule, int minLenght, int maxLenght)
        {
            return rule
                .NotEmpty()
                .Length(minLenght, maxLenght)
                .Matches(ArtesianConstants.CharacterValidatorRegEx)
                    .WithMessage("Invalid string '{0}'. Should not contains trailing or leading whitespaces or any of the following characters: ,:;'\"<space>", (parent, x) => x)
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
    }

}
