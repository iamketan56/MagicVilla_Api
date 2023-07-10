using System.ComponentModel.DataAnnotations;

namespace MaggicVilaAPI.Models.Dto
{
    public class VillaUpdateDto
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        public string Details { get; set; }
        public double rate { get; set; }
        public string ImgUrl { get; set; }
        public int Sqft { get; set; }
        public string Amenity { get; set; }
        public int Occupency { get; set; }
    }
}
