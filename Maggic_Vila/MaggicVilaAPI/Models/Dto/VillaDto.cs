using System.ComponentModel.DataAnnotations;

namespace MaggicVilaAPI.Models.Dto
{
    public class VillaDto
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public String Name { get; set; }

        public int Occupancy {get; set; }

        public int SqFt { get; set; } 
     

    }
}
