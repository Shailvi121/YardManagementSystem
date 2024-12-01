using System.ComponentModel.DataAnnotations;

namespace YMS.Dtos
{
    public class CoilDto
    {
        [Required]
        public string CoilID { get; set; }  // CoilID is a string

        public string CoilBarcodeID { get; set; }
        public string MaterialType { get; set; }
        public float Weight { get; set; }
        public float Width { get; set; }
        public float Diameter { get; set; }
        public DateTime ProductionDate { get; set; }
        public string Status { get; set; }
        public DateTime LastMoved { get; set; }

    }
}
