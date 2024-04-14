namespace Portfolio.Tests.helpers;

public class TestingPortfolioBuilder
{
    private readonly List<string> _lines;
    private string _now;

    private TestingPortfolioBuilder()
    {
        _lines = new List<string>();
        _now = "";
    }

    public static TestingPortfolioBuilder APortFolio()
    {
        return new TestingPortfolioBuilder();
    }

    public static TestingPortfolioBuilder AnEmptyPortFolio()
    {
        var builder = APortFolio();
        builder._lines.Add("");
        return builder;
    }

    public TestingPortfolioBuilder With(AssetsFileLinesBuilder lineBuilder)
    {
        _lines.Add(lineBuilder.Build());
        return this;
    }

    public TestingPortfolioBuilder OnDate(string dateAsString)
    {
        _now = dateAsString;
        return this;
    }

    public TestingPortfolio Build()
    {
        return new TestingPortfolio(_lines.ToArray(), _now);
    }
}