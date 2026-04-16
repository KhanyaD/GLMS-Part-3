namespace GLMS.Web.Services;

public interface ICurrencyCalculator
{
    decimal ConvertUsdToZar(decimal usdAmount, decimal exchangeRate);
}
