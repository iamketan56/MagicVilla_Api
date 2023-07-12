using AutoMapper;
using MaggicVilaAPI.Data;
using MaggicVilaAPI.Models;
using MaggicVilaAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MaggicVilaAPI.Repository
{
    public class VillaNumberRepository :  Repository<VillaNumber>, IVillaNumebrRepository
    {
        private ApplicationDbContext _Db;
        
        public VillaNumberRepository(ApplicationDbContext Db) : base(Db)
         {
              _Db = Db;
           }

        public async Task<VillaNumber> UpdateAsync(VillaNumber entity)
        {
            entity.updatedDate = DateTime.UtcNow;
            _Db.VillasNumber.Update(entity);
            await _Db.SaveChangesAsync();
            return entity;
        }
    }
}
