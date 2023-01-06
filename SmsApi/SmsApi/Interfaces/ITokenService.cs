using SmssApi.Requests;
using SmssApi.Responses;

namespace SmssApi.Interfaces
{
    public interface ITokenService
    {
		Task<Tuple<string, string>> GenerateTokensAsync(int userId);
		Task<ValidateRefreshTokenResponse> ValidateRefreshTokenAsync(RefreshTokenRequest refreshTokenRequest);
		Task<bool> RemoveRefreshTokenAsync(User user);
    }
}
