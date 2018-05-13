using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace ShoppingGo.Repositories
{
    public interface IRepository<TEntity> where TEntity : class, new()
    {        
        Task<List<TEntity>> GetAsync();
        Task<TEntity> GetAsync(int? id);
        Task<int> InsertAsync(TEntity entity);        
        Task<int> UpdateAsync(TEntity entity);
        Task<int> DeleteAsync(TEntity entity);
    }
}