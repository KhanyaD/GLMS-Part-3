namespace GLMS.Web.Services;

public class CurrencyCalculator : ICurrencyCalculator
{
    public decimal ConvertUsdToZar(decimal usdAmount, decimal exchangeRate)
    {
        if (usdAmount < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(usdAmount), "USD amount cannot be negative.");
        }

        if (exchangeRate <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(exchangeRate), "Exchange rate must be greater than zero.");
        }

        return Math.Round(usdAmount * exchangeRate, 2, MidpointRounding.AwayFromZero);
    }
}
