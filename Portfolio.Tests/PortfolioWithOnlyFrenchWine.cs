using NUnit.Framework;
using static Portfolio.Tests.helpers.AssetsFileLinesBuilder;
using static Portfolio.Tests.helpers.TestingPortfolioBuilder;

namespace Portfolio.Tests;

public class PortfolioWithOnlyFrenchWine
{
    [TestCase(100)]
    [TestCase(199)] // off point for asset value boundary between (-inf, 200) y [200, +inf] when asset date before now
    public void when_value_is_less_than_200_it_grows_by_20_before_now(int assetValue)
    {
        var portfolio = APortFolio()
            .With(AnAsset().DescribedAs("French Wine").FromDate("2024/01/15").WithValue(assetValue))
            .OnDate("2025/01/01")
            .Build();

        portfolio.ComputePortfolioValue();

        Assert.That(portfolio._messages[0], Is.EqualTo($"{assetValue + 20}"));
    }
    
    [TestCase(200)] // on point for asset value boundary between (-inf, 200) y [200, +inf] when asset date before now
    [TestCase(201)] 
    public void when_value_is_equal_or_greater_than_200_it_remains_the_same_before_now(int assetValue)
    {
        var portfolio = APortFolio()
            .With(AnAsset().DescribedAs("French Wine").FromDate("2024/01/15").WithValue(assetValue))
            .OnDate("2025/01/01")
            .Build();

        portfolio.ComputePortfolioValue();

        Assert.That(portfolio._messages[0], Is.EqualTo($"{assetValue}"));
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