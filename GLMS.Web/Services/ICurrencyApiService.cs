namespace GLMS.Web.Services;

public interface ICurrencyApiService
{
    Task<CurrencyApiResult> GetUsdToZarRateAsync(CancellationToken cancellationToken = default);
}
