using System.ComponentModel.DataAnnotations;

namespace YMS.Models
{
    public class Coil
    {
        [Key]
        public string CoilID { get; set; }  // CoilID is a string

        public string CoilBarcodeID { get; set; }
        public string MaterialType { get; set; }
        public float Weight { get; set; }
        public float Width { get; set; }
        public float Diameter { get; set; }
        public DateTime ProductionDate { get; set; }
        public string Status { get; set; }

        public int? CurrentLocationID { get; set; }

        public DateTime LastMoved { get; set; }

        // Navigation property to StoragePlaceOrder
        public StoragePlaceholder CurrentLocation { get; set; }
    }


}
