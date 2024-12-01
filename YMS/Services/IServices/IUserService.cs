using YMS.Models;

namespace YMS.Services.IServices
{
    public interface IUserService
    {
        string GetCurrentUserId();
        Task<User> GetUserByIdAsync(int userId);
        Task<User> GetUserByUserNameAsync(string userName);
        Task<bool> IsUserActiveAsync(int userId);
    }
}
