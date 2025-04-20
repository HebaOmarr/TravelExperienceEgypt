using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TravelExperienceEgypt.DataAccess.Data;
using TravelExperienceEgypt.DataAccess.Repository.Contract;

namespace TravelExperienceEgypt.DataAccess.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly ApplicationDBContext _applicationDBContext;
        public GenericRepository(ApplicationDBContext applicationDBContext)
        {
            _applicationDBContext = applicationDBContext;
        }

        public async Task AddAsync(TEntity entity)
        {
            await _applicationDBContext.Set<TEntity>().AddAsync(entity);
        }

        public async Task<bool> Delete(Expression<Func<TEntity, bool>> Predicate)
        {
            TEntity? result = await _applicationDBContext.Set<TEntity>().FirstOrDefaultAsync(Predicate);
            if (result is not null)
            {

                _applicationDBContext.Set<TEntity>().Remove(result);

                return true;
            }
            return false;
        }
        /// <summary>
        /// Return a Tracked List OF Entity
        /// </summary>
        /// <param name="Selector">Name Of Navigation Property</param>
        /// <returns>Task<IEnumerable<TEntity>></returns>
        public async Task<IEnumerable<TEntity>> GetAllAsync(string? Selector = null)
        {
            if (Selector is not null)
            {
                return await _applicationDBContext.Set<TEntity>().Include(Selector).ToListAsync();
            }
            return await _applicationDBContext.Set<TEntity>().ToListAsync();
        }

        /// <summary>
        /// Return a Nullable Item Of Entity
        /// </summary>
        /// <param name="expression">Lambda Expression</param>
        /// <param name="Selector">Name Of Navigation Property</param>
        /// <returns>Task<TEntity?></returns>
        public async Task<TEntity?> GetItemAsync(Expression<Func<TEntity, bool>> expression, string? Selector = null)
        {
            if (Selector is not null)
            {
                return await _applicationDBContext.Set<TEntity>().Include(Selector).FirstOrDefaultAsync(expression);
            }
            return await _applicationDBContext.Set<TEntity>().FirstOrDefaultAsync(expression);
        }

        /// <summary>
        ///  Return a Read Only List of Entity
        ///  AsNoTracking List
        /// </summary>
        /// <param name="Selector">Name Of Navigation Property</param>
        /// <returns>Task<IEnumerable<TEntity>></returns>
        /// 
        public async Task<IEnumerable<TEntity>> ReadAllAsync(string? Selector = null)
        {
            if (Selector is not null)
            {
                return await _applicationDBContext.Set<TEntity>().Include(Selector).AsNoTracking().ToListAsync();
            }
            return await _applicationDBContext.Set<TEntity>().AsNoTracking().ToListAsync();
        }

        public async Task<bool> UpdateAsync(Expression<Func<TEntity, bool>> Predicate, TEntity entity)
        {
            TEntity? existedItem = await _applicationDBContext.Set<TEntity>().FirstOrDefaultAsync(Predicate);
            if (existedItem is not null)
            {
                _applicationDBContext.Entry(existedItem).CurrentValues.SetValues(entity);
                _applicationDBContext.Entry(existedItem).State = EntityState.Modified;
                return true;
            }
            return false;
        }
    }
}
