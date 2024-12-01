using System.ComponentModel.DataAnnotations;

namespace YMS.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }  // Primary Key
        public string UserName { get; set; }  // User's username
        public string PasswordHash { get; set; }  // Hashed password for security
        public string FullName { get; set; }  // User's full name
        public int RoleID { get; set; }  // Foreign Key to Role
        public string Shift { get; set; }  // Shift (e.g., "Morning", "Night")
        public bool IsActive { get; set; }  // Whether the user is active or not

        public Role Role { get; set; }  // Navigation property to Role
    }

}
