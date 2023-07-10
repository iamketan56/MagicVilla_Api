using MaggicVilaAPI.Models;
using System.Linq.Expressions;

namespace MaggicVilaAPI.Repository.IRepository
{
    public interface IVillaRepository : IRepository<Villa> 
    {
           Task<Villa> UpdateAsync(Villa entity);

    }
}
