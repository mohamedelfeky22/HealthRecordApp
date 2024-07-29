using HealthRecordApp.DataService.Data;
using HealthRecordApp.DataService.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthRecordApp.DataService.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {

        protected AppDBContext _context;

        internal DbSet<T> _dbSet;

        protected readonly ILogger _logger;
        public GenericRepository(AppDBContext context,ILogger logger)
        {
            _context = context;
            _dbSet = _context.Set<T>();
            _logger = logger;
        }
        public virtual async Task<bool> add(T entity)
        {
            await _dbSet.AddAsync(entity);
            return true;
        } 

        public virtual Task<bool> Delete(Guid Id, Guid userId)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<IEnumerable<T>> GetAll()
        {
           return  await _dbSet.ToListAsync();
        }

        public virtual async Task<T> GetbyId(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual Task<bool> upsert(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
