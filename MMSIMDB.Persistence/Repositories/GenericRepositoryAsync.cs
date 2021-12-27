using Microsoft.EntityFrameworkCore;
using MMSIMDB.Application.Interfaces;
using MMSIMDB.Domain.Common;
using MMSIMDB.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MMSIMDB.Persistence.Repositories
{
    public class GenericRepositoryAsync<T> : IGenericRepositoryAsync<T> where T : BaseEntity
    {
        protected readonly MMSIMDBDBContext _dbContext;

        public GenericRepositoryAsync(MMSIMDBDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual async Task<T> GetByIDAsync(int id, string include = "")
        {
            return await GetAll(include).FirstOrDefaultAsync(el => el.ID == id);
        }

        public virtual async Task<T> AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<T[]> AddAsync(T[] entities)
        {
            await _dbContext.Set<T>().AddRangeAsync(entities);
            await _dbContext.SaveChangesAsync();
            return entities;
        }

        public virtual async Task UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
        public virtual async Task UpdateAsync(T entity, object newValues)
        {
            _dbContext.Entry(entity).CurrentValues.SetValues(newValues);
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(T[] entities)
        {
            _dbContext.Set<T>().RemoveRange(entities);
            await _dbContext.SaveChangesAsync();
        }

        public virtual IQueryable<T> GetAll(string include = "")
        {
            IQueryable<T> query = _dbContext.Set<T>()
                .AsNoTracking();
            foreach (var includeProperty in include.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty.Trim());
            }
            return query;
        }
        public virtual IQueryable<T> GetAllByFilter(Expression<Func<T, bool>> filter, string include = "")
        {
            return GetAll(include)
                .Where(filter);
        }

        public virtual async Task<IReadOnlyList<T>> GetAllAsync(string include = "")
        {
            return await GetAll(include)
                 .ToListAsync();
        }

        public virtual async Task<IReadOnlyList<T>> GetByFilterAsync(Expression<Func<T, bool>> filter, string include = "")
        {
            return await GetAll(include)
                .Where(filter)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
