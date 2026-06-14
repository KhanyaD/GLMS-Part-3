namespace GLMS.Api.Services;

public interface ICurrencyApiService
{
    Task<CurrencyApiResult> GetUsdToZarRateAsync(CancellationToken cancellationToken = default);
}
