using System.ComponentModel.DataAnnotations;

namespace YMS.Models
{
    public class CoilMovement
    {
        [Key]
        public int MovementID { get; set; }  // Primary Key
        public string CoilID { get; set; }  // Foreign Key to Coil
        public int ToPlaceHolderID { get; set; }  // Foreign Key to StoragePlaceOrder (destination)
        public int FromPlaceHolderID { get; set; }  // Foreign Key to StoragePlaceOrder (source)
        public int MovedByUserID { get; set; }  // Foreign Key to User (operator who moved the coil)
        public DateTime MovementDate { get; set; }  // Date and time of the movement
        public string Reason { get; set; }  // Reason for the movement

        public Coil Coil { get; set; }  // Navigation property to Coil
        public StoragePlaceholder FromPlaceHolder { get; set; }  // Navigation property to From StoragePlaceOrder
        public StoragePlaceholder ToPlaceHolder { get; set; }  // Navigation property to To StoragePlaceOrder
        public User MovedByUser { get; set; }  // Navigation property to User
    }

}
