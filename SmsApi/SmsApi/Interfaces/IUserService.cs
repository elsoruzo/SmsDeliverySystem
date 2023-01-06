using SmssApi.Requests;
using SmssApi.Responses;

namespace SmssApi.Interfaces
{
    public interface IUserService
    {
		Task<TokenResponse> LoginAsync(LoginRequest loginRequest);
		Task<SignupResponse> SignupAsync(SignupRequest signupRequest);

		Task<LogoutResponse> LogoutAsync(int userId);
		Task<UserResponse> GetInfoAsync(int userId);
    }
}
