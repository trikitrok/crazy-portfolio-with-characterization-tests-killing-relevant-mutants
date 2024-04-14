using System.Globalization;

namespace Portfolio.Tests.helpers;

public class TestingPortfolio : Portfolio
{
    private readonly string[] _assetsFileLines;
    private readonly DateTime _now;
    public List<string> _messages;

    public TestingPortfolio(string[] assetsFileLines, string nowAsString) :
        base("")
    {
        _assetsFileLines = assetsFileLines;
        _now = DateTime.ParseExact(nowAsString, "yyyy/MM/dd", CultureInfo.InvariantCulture);
        _messages = new List<string>();
    }

    protected override DateTime GetNow()
    {
        return _now;
    }

    protected override string[] ReadAssetsFileLines()
    {
        return _assetsFileLines;
    }

    protected override void DisplayMessage(string message)
    {
        _messages.Add(message);
    }

    protected override string FormatDate(DateTime assetDate, CultureInfo notUsed)
    {
        return CultureInfoUtils.ConvertToString(assetDate);
    }

    protected override DateTime CreateAssetDateTime(string dateAsString, CultureInfo notUsed)
    {
        return CultureInfoUtils.ParseExact(dateAsString, "yyyy/MM/dd");
    }
}