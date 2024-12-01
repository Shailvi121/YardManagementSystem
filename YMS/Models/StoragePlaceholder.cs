using System.ComponentModel.DataAnnotations;

namespace YMS.Models
{
    public class StoragePlaceholder
    {
        [Key]
        public int PlaceHolderID { get; set; }

        public string PlaceHolderName { get; set; }

        public int YardID { get; set; }  // Assuming YardID is of type string
        public bool IsOccupied { get; set; }

        // Change the type of OccupyingCoilID to match CoilID type
        public string OccupyingCoilID { get; set; }  // Change from int? to string

        public float MaxWeightCapacity { get; set; }

        // Navigation property to Coil (now matching the string type of CoilID)
        public virtual Coil OccupyingCoil { get; set; }
        public virtual Yard Yard { get; set; }
    }


}
