namespace GLMS.Web.Services;

public interface IApiTokenService
{
    Task<string> GetAccessTokenAsync(CancellationToken cancellationToken = default);
}
