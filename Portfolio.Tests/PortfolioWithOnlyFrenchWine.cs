using NUnit.Framework;
using static Portfolio.Tests.helpers.AssetsFileLinesBuilder;
using static Portfolio.Tests.helpers.TestingPortfolioBuilder;

namespace Portfolio.Tests;

public class PortfolioWithOnlyFrenchWine
{
    [Test]
    public void value_grows_by_20_before_now()
    {
        var portfolio = APortFolio()
            .With(AnAsset().DescribedAs("French Wine").FromDate("2024/01/15").WithValue("100"))
            .OnDate("2025/01/01")
            .Build();

        portfolio.ComputePortfolioValue();

        Assert.That(portfolio._messages[0], Is.EqualTo("120"));
    }

    [Test]
    public void value_grows_by_10_after_now()
    {
        var portfolio = APortFolio()
            .With(AnAsset().DescribedAs("French Wine").FromDate("2024/01/15").WithValue(100))
            .OnDate("2024/01/01")
            .Build();

        portfolio.ComputePortfolioValue();

        Assert.That(portfolio._messages[0], Is.EqualTo("110"));
    }
}