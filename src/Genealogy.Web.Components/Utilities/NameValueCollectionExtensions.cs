using System;
using System.Collections.Specialized;
using System.Globalization;

namespace Genealogy.Web.Components.Utilities;

internal static class NameValueCollectionExtensions
{
    public static bool? GetBoolValue(this NameValueCollection nvc, string key)
    {
        if (nvc[key] is { Length: > 0 } strValue)
        {
            return strValue == "1" || strValue.Equals("true", StringComparison.OrdinalIgnoreCase);
        }

        return null;
    }

    public static DateTime? GetDateValue(this NameValueCollection nvc, string key, string format, IFormatProvider? formatProvider = null)
    {
        if (nvc[key] is { } strValue)
        {
            if (DateTime.TryParseExact(strValue, format, formatProvider, DateTimeStyles.None, out DateTime dateValue))
            {
                return dateValue;
            }
        }

        return null;
    }

    public static int? GetIntValue(this NameValueCollection nvc, string key)
    {
        if (nvc[key] is { } strValue && int.TryParse(strValue, out int intValue))
        {
            return intValue;
        }

        return null;
    }

    public static string? GetStringValue(this NameValueCollection nvc, string key)
    {
        if (nvc[key] is { Length: > 0 } value)
        {
            return value;
        }

        return null;
    }
}