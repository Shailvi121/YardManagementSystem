using System.ComponentModel.DataAnnotations;

namespace YMS.Models
{
    public class OutgoingDispatch
    {
        [Key]
        public int DispatchID { get; set; }  // Primary Key
        public string CoilID { get; set; }  // Foreign Key referencing Coil
        public DateTime DispatchedDate { get; set; }  // Date and time of dispatch
        public int DispatchedByUserID { get; set; }  // Foreign Key to User (operator who dispatched the coil)
        public string Destination { get; set; }  // Destination of the dispatched coil
        public string TransportMethod { get; set; }  // Method of transport (e.g., "Truck", "Rail")

        public Coil Coil { get; set; }  // Navigation property to Coil
        public User DispatchedByUser { get; set; }  // Navigation property to User (operator who dispatched the coil)
    }

}
