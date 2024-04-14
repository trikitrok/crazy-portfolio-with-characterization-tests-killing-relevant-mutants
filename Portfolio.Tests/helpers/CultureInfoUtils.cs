using System.Globalization;

namespace Portfolio.Tests.helpers;

public class CultureInfoUtils
{
    private const string CultureInfoString = "es-ES";

    public static string ConvertToString(int value)
    {
        return value.ToString(CreateCultureInfo());
    }

    public static string ConvertToString(DateTime date)
    {
        return date.ToString(CultureInfo.CreateSpecificCulture(CultureInfoString));
    }

    public static DateTime ParseExact(string dateAsString, string format)
    {
        return DateTime.ParseExact(dateAsString, format, CultureInfo.InvariantCulture);
    }

    private static CultureInfo CreateCultureInfo()
    {
        return new CultureInfo(CultureInfoString);
    }
}