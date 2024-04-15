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

    [TestCase("2024/04/15")]
    [TestCase("2024/01/12")] // 11 days, on point for days boundary between [6, 11) y [11, +inf]
    public void value_grows_by_5_11_days_or_more_after_now(string assetDate)
    {
        var portfolio = APortFolio()
            .With(AnAsset().DescribedAs("Lottery Prediction").FromDate(assetDate).WithValue(50))
            .OnDate("2024/01/01")
            .Build();

        portfolio.ComputePortfolioValue();

        Assert.That(portfolio._messages[0], Is.EqualTo("55"));
    }

    [TestCase("2024/01/11")] // 10 days, off point for days boundary between [6, 11) and [11, +inf]
    [TestCase("2024/01/07")] // 6 days, on point for days boundary between [0, 6) and [6, 11)
    public void value_grows_by_25_less_than_11_days_after_now(string assetDate)
    {
        var portfolio = APortFolio()
            .With(AnAsset().DescribedAs("Lottery Prediction").FromDate(assetDate).WithValue(50))
            .OnDate("2024/01/01")
            .Build();

        portfolio.ComputePortfolioValue();

        Assert.That(portfolio._messages[0], Is.EqualTo("75"));
    }

    [TestCase("2024/01/06")] // 5 days, off point for days boundary between [0, 6) and [6, 11)
    public void value_grows_by_125_less_than_6_days_after_now(string assetDate)
    {
        var portfolio = APortFolio()
            .With(AnAsset().DescribedAs("Lottery Prediction").FromDate(assetDate).WithValue(50))
            .OnDate("2024/01/01")
            .Build();

        portfolio.ComputePortfolioValue();

        Assert.That(portfolio._messages[0], Is.EqualTo("175"));
    }

    [TestCase(800)] // on point for value of asset boundary between (-inf, 800) and [800, +inf] (for more than 11 days)
    [TestCase(801)]
    public void more_than_11_days_after_now_when_value_is_equal_or_more_than_800_it_remains_the_same(int assetValue)
    {
        var portfolio = APortFolio()
            .With(AnAsset().DescribedAs("Lottery Prediction").FromDate("2024/04/12").WithValue(assetValue))
            .OnDate("2024/04/01")
            .Build();

        portfolio.ComputePortfolioValue();

        Assert.That(portfolio._messages[0], Is.EqualTo($"{assetValue}"));
    }
    
    [TestCase(-794)]
    [TestCase(799)] // off point for value of asset boundary between (-inf, 800) and [800, +inf] (for more than 11 days)
    public void more_than_11_days_after_now_when_value_is_less_than_800_it_grows_by_5(int assetValue)
    {
        var portfolio = APortFolio()
            .With(AnAsset().DescribedAs("Lottery Prediction").FromDate("2024/04/12").WithValue(assetValue))
            .OnDate("2024/04/01")
            .Build();

        portfolio.ComputePortfolioValue();

        Assert.That(portfolio._messages[0], Is.EqualTo($"{assetValue + 5}"));
    }
    
    [TestCase(-794)] 
    [TestCase(794)]  // off point for value of asset boundary between (-inf, 795) and [795, 800) (for 11 > days >= 6)
    public void less_than_11_days_and_more_than_6_after_now_when_value_is_less_than_795_it_grows_by_25(int assetValue)
    {
        var portfolio = APortFolio()
            .With(AnAsset().DescribedAs("Lottery Prediction").FromDate("2024/04/08").WithValue(assetValue))
            .OnDate("2024/04/01")
            .Build();

        portfolio.ComputePortfolioValue();

        Assert.That(portfolio._messages[0], Is.EqualTo($"{assetValue + 25}"));
    }
    
    [TestCase(795)] // on point for value of asset boundary between (-inf, 795) and [795, 800) (for 11 > days >= 6)
    [TestCase(799)] // off point for value of asset boundary between [795, 800) and [800, +inf) (for 11 > days >= 6)
    public void less_than_11_days_and_more_than_6_after_now_when_value_is_equal_or_more_than_795_it_grows_by_5(int assetValue)
    {
        var portfolio = APortFolio()
            .With(AnAsset().DescribedAs("Lottery Prediction").FromDate("2024/04/08").WithValue(assetValue))
            .OnDate("2024/04/01")
            .Build();

        portfolio.ComputePortfolioValue();

        Assert.That(portfolio._messages[0], Is.EqualTo($"{assetValue + 5}"));
    }
    
    [TestCase(800)] // on point for value of asset boundary between [795, 800) and [800, +inf) (for 11 > days >= 6)
    [TestCase(900)]  
    public void less_than_11_days_and_more_than_6_after_now_when_value_is_equal_or_more_than_800_it_remains_the_same(int assetValue)
    {
        var portfolio = APortFolio()
            .With(AnAsset().DescribedAs("Lottery Prediction").FromDate("2024/04/08").WithValue(assetValue))
            .OnDate("2024/04/01")
            .Build();

        portfolio.ComputePortfolioValue();

        Assert.That(portfolio._messages[0], Is.EqualTo($"{assetValue}"));
    }
    
    [TestCase(-774)]
    [TestCase(774)] // off point for value of asset boundary between (-inf, 775) and [775, 795) (for days < 6)
    public void less_than_6_days_after_now_when_value_is_less_than_775_it_grows_by_125(int assetValue)
    {
        var portfolio = APortFolio()
            .With(AnAsset().DescribedAs("Lottery Prediction").FromDate("2024/04/06").WithValue(assetValue))
            .OnDate("2024/04/01")
            .Build();

        portfolio.ComputePortfolioValue();

        Assert.That(portfolio._messages[0], Is.EqualTo($"{assetValue + 125}"));
    }
    
    [TestCase(775)] // on point for value of asset boundary between (-inf, 775) and [775, 795) (for days < 6)
    [TestCase(794)] // off point for value of asset boundary between [775, 795) and [795, 800) (for days < 6)
    public void less_than_6_days_after_now_when_value_is_equal_or_more_than_775_it_grows_by_25(int assetValue)
    {
        var portfolio = APortFolio()
            .With(AnAsset().DescribedAs("Lottery Prediction").FromDate("2024/04/06").WithValue(assetValue))
            .OnDate("2024/04/01")
            .Build();

        portfolio.ComputePortfolioValue();

        Assert.That(portfolio._messages[0], Is.EqualTo($"{assetValue + 25}"));
    }
    
    [TestCase(795)] // on point for value of asset boundary between [775, 795) and [795, 800) (for days < 6)
    [TestCase(799)] // off point for value of asset boundary between [795, 800) and [800, +inf) (for days < 6)
    public void less_than_6_days_after_now_when_value_is_equal_or_more_than_795_but_less_than_800_it_grows_by_5(int assetValue)
    {
        var portfolio = APortFolio()
            .With(AnAsset().DescribedAs("Lottery Prediction").FromDate("2024/04/06").WithValue(assetValue))
            .OnDate("2024/04/01")
            .Build();

        portfolio.ComputePortfolioValue();

        Assert.That(portfolio._messages[0], Is.EqualTo($"{assetValue + 5}"));
    }
    
    [TestCase(800)] // on point for value of asset boundary between [795, 800) and [800, +inf) (for days < 6)
    [TestCase(1000)]
    public void less_than_6_days_after_now_when_value_is_equal_or_more_than_800_it_remains_the_same(int assetValue)
    {
        var portfolio = APortFolio()
            .With(AnAsset().DescribedAs("Lottery Prediction").FromDate("2024/04/06").WithValue(assetValue))
            .OnDate("2024/04/01")
            .Build();

        portfolio.ComputePortfolioValue();

        Assert.That(portfolio._messages[0], Is.EqualTo($"{assetValue}"));
    }
}