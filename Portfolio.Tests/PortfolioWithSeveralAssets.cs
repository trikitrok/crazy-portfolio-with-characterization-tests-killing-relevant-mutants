using NUnit.Framework;
using static Portfolio.Tests.helpers.AssetsFileLinesBuilder;
using static Portfolio.Tests.helpers.TestingPortfolioBuilder;

namespace Portfolio.Tests;

public class PortfolioWithSeveralAssets
{
    [Test]
    public void when_including_a_unicorn_then_only_the_unicorn_matters()
    {
        var portfolio = APortFolio()
            .With(AnAsset().DescribedAs("Some Regular Asset").FromDate("2024/01/15").WithValue(100))
            .With(AnAsset().DescribedAs("Unicorn").FromDate("2024/01/15").WithValue(80))
            .OnDate("2023/01/01")
            .Build();

        portfolio.ComputePortfolioValue();

        Assert.That(portfolio._messages[0],
            Is.EqualTo("Portfolio is priceless because it got a unicorn on 15/1/2024 0:00:00!!!!!"));
    }

    [Test]
    public void when_not_including_a_unicorn_sums_the_value_of_all_assets()
    {
        var portfolio = APortFolio()
            .With(AnAsset().DescribedAs("Some Regular Asset").FromDate("2024/01/15").WithValue(100))
            .With(AnAsset().DescribedAs("French Wine").FromDate("2024/01/15").WithValue(100))
            .OnDate("2023/01/01")
            .Build();

        portfolio.ComputePortfolioValue();

        Assert.That(portfolio._messages[0], Is.EqualTo("200"));
    }
}