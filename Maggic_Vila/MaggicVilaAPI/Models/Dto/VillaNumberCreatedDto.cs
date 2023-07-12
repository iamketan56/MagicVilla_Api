using System.ComponentModel.DataAnnotations;

namespace MaggicVilaAPI.Models.Dto
{
    public class VillaNumberCreatedDto
    {
        [Required]
        public int VillaNo { get; set; }
        [Required]
        public int VillaId { get; set; }
        public string SpecialDetails { get; set; }
    }
}
