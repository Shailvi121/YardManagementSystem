using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace YMS.MESModels
{
    public class MESCoil
    {
        [Key]
        public string CoilID { get; set; }  // CoilID is a string

        public string CoilBarcodeID { get; set; }
        public string MaterialType { get; set; }
        public float Weight { get; set; }
        public float Width { get; set; }
        public float Diameter { get; set; }
        public DateTime ProductionDate { get; set; }

    }
}
