namespace GLMS.Api.Services;

public interface ICurrencyCalculator
{
    decimal ConvertUsdToZar(decimal usdAmount, decimal exchangeRate);
}
