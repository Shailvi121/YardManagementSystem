using System.ComponentModel.DataAnnotations;

namespace YMS.Models
{
    public class RolePermission
    {
        [Key]
        public int PermissionID { get; set; }  // Primary Key
        public int RoleID { get; set; }  // Foreign Key to Role
        public string ModuleName { get; set; }  // Name of the module (e.g., "Admin")
        public bool CanView { get; set; }  // Can view the module?
        public bool CanEdit { get; set; }  // Can edit the module?
        public bool CanAdd { get; set; }  // Can add to the module?
        public bool CanDelete { get; set; }  // Can delete from the module?

        public Role Role { get; set; }  // Navigation property to Role
    }


}
