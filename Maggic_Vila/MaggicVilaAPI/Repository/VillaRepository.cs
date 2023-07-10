using AutoMapper;
using MaggicVilaAPI.Data;
using MaggicVilaAPI.Models;
using MaggicVilaAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MaggicVilaAPI.Repository
{
    public class VillaRepository : IVillaRepository
    {
        private readonly ApplicationDbContext _Db;
        public VillaRepository(ApplicationDbContext db)
        {
            _Db = db;
        }
        public async Task CreateAsync(Villa entity)
        {
           await _Db.Villas.AddAsync(entity);
            await SaveAsync();
        }

        public async Task<Villa> GetAsync(Expression<Func<Villa, bool>> filter = null, bool tracked = true)
        {
            IQueryable<Villa> query = _Db.Villas;
            if(!tracked)
            {
                query = query.AsNoTracking();
            }
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<Villa>> GetAllAsync(Expression<Func<Villa, bool>> filter = null)
        {
            IQueryable<Villa> query = _Db.Villas;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.ToListAsync();
        }

     

        public async Task RemoveAsync(Villa entity)
        {
            _Db.Villas.Remove(entity);
            await SaveAsync();
        }


        public async Task SaveAsync()
        {
            await _Db.SaveChangesAsync();
        }

       

        public async Task UpdateAsync(Villa entity)
        {
            _Db.Villas.Update(entity);
            await SaveAsync();
        }
    }
}
