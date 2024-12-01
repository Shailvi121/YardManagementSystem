using System.ComponentModel.DataAnnotations;

namespace YMS.Models
{
    public class Yard
    {
        [Key]
        public int YardID { get; set; }  // Primary Key
        public string YardName { get; set; }  // Name of the yard
        public string BuildingName { get; set; }  // Name of the building
        public int Capacity { get; set; }  // Total capacity of the yard
        public int CurrentOccupancy { get; set; }  // Current occupancy (number of coils)

        public virtual ICollection<StoragePlaceholder> StoragePlaceholders { get; set; } // Navigation property to StoragePlaceOrder
    }

}
