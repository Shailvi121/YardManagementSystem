using System.ComponentModel.DataAnnotations;

namespace YMS.Models
{
    public class Role
    {
        [Key]
        public int RoleID { get; set; }  // Primary Key
        public string RoleName { get; set; }  // Name of the role (e.g., Admin, User)
        public string Description { get; set; }  // Description of the role

        public ICollection<User> Users { get; set; }  // Navigation property for related users
        public ICollection<RolePermission> RolePermissions { get; set; }  // Navigation property for permissions
    }


}
