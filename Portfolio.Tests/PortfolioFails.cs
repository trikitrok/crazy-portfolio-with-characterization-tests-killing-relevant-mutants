using NUnit.Framework;
using static Portfolio.Tests.helpers.AssetsFileLinesBuilder;
using static Portfolio.Tests.helpers.TestingPortfolioBuilder;

namespace Portfolio.Tests;

public class PortfolioFails
{
    [Test]
    public void when_the_file_contains_no_assets()
    {
        var portfolio = AnEmptyPortFolio()
            .OnDate("2025/01/01")
            .Build();
        Assert.Throws<IndexOutOfRangeException>(
            () => portfolio.ComputePortfolioValue()
        );
    }

    [Test]
    public void when_an_asset_has_an_invalid_date()
    {
        var portfolio = APortFolio()
            .With(AnAsset().DescribedAs("Another asset").FromDate("wrong date").WithValue(0))
            .OnDate("2025/01/01")
            .Build();
        Assert.Throws<FormatException>(
            () => portfolio.ComputePortfolioValue()
        );
    }

    [Test]
    public void when_an_asset_has_an_invalid_value()
    {
        var portfolio = APortFolio()
            .With(AnAsset().DescribedAs("").FromDate("2025/01/01").WithValue(""))
            .OnDate("2025/01/01")
            .Build();
        Assert.Throws<FormatException>(
            () => portfolio.ComputePortfolioValue()
        );
    }
}