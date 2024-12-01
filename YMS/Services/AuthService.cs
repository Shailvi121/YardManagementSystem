using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using YMS.Data;
using YMS.Models;
using YMS.Services.IServices;

namespace YMS.Services
{

    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;

        public AuthService(ApplicationDbContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        public async Task<string> RegisterAsync(Register model)
        {
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.UserName == model.UserName);
            if (existingUser != null)
            {
                throw new Exception("Username already exists.");
            }

            var role = await _context.Roles.FindAsync(model.RoleID);
            if (role == null)
            {
                throw new Exception("Invalid Role.");
            }

            var user = new User
            {
                UserName = model.UserName,
                PasswordHash = HashPassword(model.Password),
                FullName = model.FullName,
                RoleID = model.RoleID,
                IsActive = true,
                Shift = model.Shift
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return "User registered successfully";
        }

        public async Task<string> LoginAsync(Login model)
        {
            var user = await _userService.GetUserByUserNameAsync(model.UserName);
            if (user == null || !await VerifyPasswordAsync(model.Password, user.PasswordHash))
            {
                throw new UnauthorizedAccessException("Invalid username or password.");
            }

            if (!user.IsActive)
            {
                throw new UnauthorizedAccessException("Your account is not active.");
            }

            return GenerateJwtToken(user);
        }

        public async Task<bool> VerifyPasswordAsync(string enteredPassword, string storedHash)
        {
            using (var sha256 = SHA256.Create())
            {
                var enteredHash = Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(enteredPassword)));
                return enteredHash == storedHash;
            }
        }

        public string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, user.UserID.ToString()),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Role, user.Role.RoleName)
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("mySuperSecretKeyThatIsExactly32BytesLong!"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: "your-issuer",
                audience: "your-audience",
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashBytes);
            }
        }
    }

}
