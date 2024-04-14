using NUnit.Framework;
using static Portfolio.Tests.helpers.AssetsFileLinesBuilder;
using static Portfolio.Tests.helpers.TestingPortfolioBuilder;

namespace Portfolio.Tests;

public class PortfolioWithOnlyLotteryPrediction
{
    [Test]
    public void value_drops_to_zero_before_now()
    {
        var portfolio = APortFolio()
            .With(AnAsset().DescribedAs("Lottery Prediction").FromDate("2024/04/15").WithValue(50))
            .OnDate("2025/01/01")
            .Build();

        portfolio.ComputePortfolioValue();

        Assert.That(portfolio._messages[0], Is.EqualTo("0"));
    }

    [Test]
    public void value_grows_by_5_11_days_or_more_after_now()
    {
        var portfolio = APortFolio()
            .With(AnAsset().DescribedAs("Lottery Prediction").FromDate("2024/04/15").WithValue(50))
            .OnDate("2024/01/01")
            .Build();

        portfolio.ComputePortfolioValue();

        Assert.That(portfolio._messages[0], Is.EqualTo("55"));
    }

    [Test]
    public void value_grows_by_25_less_than_11_days_after_now()
    {
        var portfolio = APortFolio()
            .With(AnAsset().DescribedAs("Lottery Prediction").FromDate("2024/04/14").WithValue(50))
            .OnDate("2024/04/04")
            .Build();

        portfolio.ComputePortfolioValue();

        Assert.That(portfolio._messages[0], Is.EqualTo("75"));
    }

    [Test]
    public void value_grows_by_125_less_than_6_days_after_now()
    {
        var portfolio = APortFolio()
            .With(AnAsset().DescribedAs("Lottery Prediction").FromDate("2024/04/09").WithValue(50))
            .OnDate("2024/04/04")
            .Build();

        portfolio.ComputePortfolioValue();

        Assert.That(portfolio._messages[0], Is.EqualTo("175"));
    }

    [Test]
    public void value_can_not_be_more_than_800()
    {
        const int maxValueForLotteryPredictions = 800;
        var portfolio = APortFolio()
            .With(AnAsset().DescribedAs("Lottery Prediction").FromDate("2024/04/15").WithValue(800))
            .OnDate("2024/04/09")
            .Build();

        portfolio.ComputePortfolioValue();

        Assert.That(portfolio._messages[0], Is.EqualTo(maxValueForLotteryPredictions.ToString()));
    }
}