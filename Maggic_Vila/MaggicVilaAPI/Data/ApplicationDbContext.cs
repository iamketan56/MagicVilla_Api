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

    }
}
