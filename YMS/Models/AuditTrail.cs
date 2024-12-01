using System.ComponentModel.DataAnnotations;

namespace YMS.Models
{
    public class AuditTrail
    {
        [Key]
        public int AuditID { get; set; }  // Primary Key
        public string Action { get; set; }  // Action performed (e.g., "Coil Moved", "Coil Inspected")
        public string CoilID { get; set; }  // Foreign Key referencing Coil
        public int PerformedByUserID { get; set; }  // Foreign Key referencing User (operator who performed the action)
        public DateTime ActionDate { get; set; }  // Date and time of the action
        public string Details { get; set; }  // Additional details about the action

        public Coil Coil { get; set; }  // Navigation property to Coil
        public User PerformedByUser { get; set; }  // Navigation property to User (operator who performed the action)
    }

}
