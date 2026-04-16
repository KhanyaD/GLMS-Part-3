using GLMS.Web.Services;
using Xunit;

namespace GLMS.Tests;

public class CurrencyCalculatorTests
{
    [Fact]
    public void ConvertUsdToZar_ReturnsRoundedExpectedValue()
    {
        var calculator = new CurrencyCalculator();

        var result = calculator.ConvertUsdToZar(100m, 18.75m);

        Assert.Equal(1875.00m, result);
    }

    [Fact]
    public void ConvertUsdToZar_Throws_WhenRateIsInvalid()
    {
        var calculator = new CurrencyCalculator();

        Assert.Throws<ArgumentOutOfRangeException>(() => calculator.ConvertUsdToZar(100m, 0m));
    }
}
