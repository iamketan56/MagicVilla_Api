﻿using System.ComponentModel.DataAnnotations;

namespace MaggicVilaAPI.Models.Dto
{
    public class VillaUpdateDto
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public String Name { get; set; }
        public string Details { get; set; }
        [Required]
        public double rate { get; set; }
        [Required]
        public string ImgUrl { get; set; }
        public int SqFt { get; set; }
        [Required]
        public string Amenity { get; set; }
        public int Occupancy { get; set; }
    }
}