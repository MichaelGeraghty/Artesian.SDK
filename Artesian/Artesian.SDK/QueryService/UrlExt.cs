using Artesian.SDK.QueryService.Configuration;
using NodaTime;
using NodaTime.Text;

namespace Artesian.SDK.QueryService
{
    class UrlExt
    {
        private static LocalDatePattern _localDatePattern = LocalDatePattern.Iso;
        private static LocalDateTimePattern _localDateTimePattern = LocalDateTimePattern.GeneralIso;

        public static string ToUrlParam(LocalDateRange range)
        {
            return $"{_localDatePattern.Format(range.Start)}/{_localDatePattern.Format(range.End)}";
        }

        public static string ToUrlParam(LocalDateTime dateTime)
        {
            return _localDateTimePattern.Format(dateTime);
        }
    }
}
