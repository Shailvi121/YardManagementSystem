using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace YMS.Models
{
    public class Inspection
    {
        [Key]
        [JsonIgnore]
        public int InspectionID { get; set; }  // Primary Key
        public string CoilID { get; set; }  // Foreign Key to Coil
        public int InspectorID { get; set; }  // Foreign Key to User (Inspector)
        public DateTime InspectionDate { get; set; }  // Date of the inspection
        public float Width { get; set; }  // Width of the coil during inspection
        public float Diameter { get; set; }  // Diameter of the coil during inspection
        public float Weight { get; set; }  // Weight of the coil during inspection
        public string VisualCondition { get; set; }  // Visual condition of the coil
        public string Remark1 { get; set; }  // Additional remark
        public string Remark2 { get; set; }  // Additional remark
        public string Remark3 { get; set; }  // Additional remark
        public List<string> ImagePaths { get; set; }  // List of image paths for the inspection

        public Coil Coil { get; set; }  // Navigation property to Coil
        public User Inspector { get; set; }  // Navigation property to User (Inspector)
    }

}
