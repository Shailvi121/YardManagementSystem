using YMS.Models;

namespace YMS.Services.IServices
{
    public interface IAuthService
    {
        Task<string> RegisterAsync(Register model);
        Task<string> LoginAsync(Login model);
        Task<bool> VerifyPasswordAsync(string enteredPassword, string storedHash);
    }
}
