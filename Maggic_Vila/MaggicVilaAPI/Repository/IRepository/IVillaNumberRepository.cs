using MaggicVilaAPI.Models;
using System.Linq.Expressions;

namespace MaggicVilaAPI.Repository.IRepository
{
    public interface IVillaNumebrRepository : IRepository<VillaNumber> 
    {
           Task<VillaNumber> UpdateAsync(VillaNumber entity);

    }
}
