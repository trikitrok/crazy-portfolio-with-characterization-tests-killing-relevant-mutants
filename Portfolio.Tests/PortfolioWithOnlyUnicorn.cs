using NUnit.Framework;
using static Portfolio.Tests.helpers.AssetsFileLinesBuilder;
using static Portfolio.Tests.helpers.TestingPortfolioBuilder;

namespace Portfolio.Tests;

public class PortfolioWithOnlyUnicorn
{
    [Test]
    public void has_infinite_value_before_now()
    {
        var portfolio = APortFolio()
            .With(AnAsset().DescribedAs("Unicorn").FromDate("2023/01/15").WithValue(100))
            .OnDate("2024/01/01")
            .Build();

        portfolio.ComputePortfolioValue();

        Assert.That(portfolio._messages[0],
            Is.EqualTo("Portfolio is priceless because it got a unicorn on 15/1/2023 0:00:00!!!!!"));
    }

    [Test]
    public void infinite_has_value_after_now()
    {
        var portfolio = APortFolio()
            .With(AnAsset().DescribedAs("Unicorn").FromDate("2043/01/15").WithValue(100))
            .OnDate("2024/01/01")
            .Build();

        portfolio.ComputePortfolioValue();

        Assert.That(portfolio._messages[0],
            Is.EqualTo("Portfolio is priceless because it got a unicorn on 15/1/2043 0:00:00!!!!!"));
    }
}