using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using YMS.Data;
using YMS.Models;
using YMS.Services.IServices;

namespace YMS.Services
{

    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public string GetCurrentUserId()
        {
           var user = _httpContextAccessor.HttpContext?.User;
            string userId = user?.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            return userId;
        }
        public async Task<User> GetUserByIdAsync(int userId)
        {
            return await _context.Users.Include(u => u.Role)
                                       .FirstOrDefaultAsync(u => u.UserID == userId);
        }

        public async Task<User> GetUserByUserNameAsync(string userName)
        {
            return await _context.Users.Include(u => u.Role)
                                       .FirstOrDefaultAsync(u => u.UserName == userName);
        }

        public async Task<bool> IsUserActiveAsync(int userId)
        {
            var user = await GetUserByIdAsync(userId);
            return user != null && user.IsActive;
        }
    }

}
