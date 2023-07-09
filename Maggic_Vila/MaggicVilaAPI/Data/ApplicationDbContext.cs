using MaggicVilaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MaggicVilaAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { 
        }
        //table name Villas
        public DbSet<Villa> Villas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Villa>().HasData(
                new Villa
                {
                    Id = 1,
                    Name = "Royal Villa",
                    Details = "lorem lorem",
                    ImgUrl = "",
                    Occupency = 5,
                    rate = 200,
                    Sqft = 300,
                    Amenity = "xyz",
                    CreatedDate = DateTime.Now
                },
                 new Villa
                 {
                     Id = 2,
                     Name = "Royal Villa 1",
                     Details = "lorem lorem",
                     ImgUrl = "",
                     Occupency = 6,
                     rate = 300,
                     Sqft = 400,
                     Amenity = "xyz",
                     CreatedDate = DateTime.Now
                 },
                  new Villa
                  {
                      Id = 3,
                      Name = "Royal Villa 2",
                      Details = "lorem lorem",
                      ImgUrl = "",
                      Occupency = 4,
                      rate = 100,
                      Sqft = 150,
                      Amenity = "xyz",
                      CreatedDate = DateTime.Now
                  },
                   new Villa
                   {
                       Id = 4,
                       Name = "Royal Villa 3",
                       Details = "lorem lorem",
                       ImgUrl = "",
                       Occupency = 7,
                       rate = 1000,
                       Sqft = 500,
                       Amenity = "xyz",
                       CreatedDate = DateTime.Now
                   }) ;
        }
    }
}
