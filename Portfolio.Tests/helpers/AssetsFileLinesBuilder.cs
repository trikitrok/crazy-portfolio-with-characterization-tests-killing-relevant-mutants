using static Portfolio.Tests.helpers.CultureInfoUtils;

namespace Portfolio.Tests.helpers;

public class AssetsFileLinesBuilder
{
    private string _dateAsString;
    private string _description;
    private string _valueAsString;

    private AssetsFileLinesBuilder()
    {
        _dateAsString = "";
        _description = "description";
        _valueAsString = "";
    }

    public static AssetsFileLinesBuilder AnAsset()
    {
        return new AssetsFileLinesBuilder();
    }

    public AssetsFileLinesBuilder FromDate(string date)
    {
        _dateAsString = date;
        return this;
    }

    public AssetsFileLinesBuilder DescribedAs(string description)
    {
        _description = description;
        return this;
    }

    public AssetsFileLinesBuilder WithValue(int value)
    {
        _valueAsString = ConvertToString(value);
        return this;
    }

    public AssetsFileLinesBuilder WithValue(string value)
    {
        _valueAsString = value;
        return this;
    }

    public string Build()
    {
        var assetValue = _valueAsString;
        var assetFileLine = $"{_description},{_dateAsString},{assetValue}";
        return assetFileLine;
    }
}