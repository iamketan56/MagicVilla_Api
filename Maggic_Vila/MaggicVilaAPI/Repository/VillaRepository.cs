using AutoMapper;
using MaggicVilaAPI.Data;
using MaggicVilaAPI.Models;
using MaggicVilaAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MaggicVilaAPI.Repository
{
    public class VillaRepository :  Repository<Villa>, IVillaRepository
    {
        private ApplicationDbContext _Db;
        
        public VillaRepository(ApplicationDbContext Db) : base(Db)
         {
              _Db = Db;
           }

        public async Task<Villa> UpdateAsync(Villa entity)
        {
            entity.UpdatedDate = DateTime.UtcNow;
            _Db.Villas.Update(entity);
            await _Db.SaveChangesAsync();
            return entity;
        }
    }
}
