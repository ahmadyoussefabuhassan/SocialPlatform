using Microsoft.EntityFrameworkCore;
using SocialPlatform.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialPlatform.Infrastructure.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;
        public Repository(ApplicationDbContext context )
            => (_context, _dbSet) = (context, context.Set<T>());

        public async Task AddAsync(T entity, bool autoSave = true)
        {
            await _dbSet.AddAsync(entity);
            if (autoSave)
                await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id, bool autoSave = true)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                if (autoSave)
                    await _context.SaveChangesAsync();
            }
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
            => await _dbSet.ToListAsync();

        public async Task<T?> GetByIdAsync(int id)
            => await _dbSet.FindAsync(id);

        public async Task UpdateAsync(T entity, bool autoSave = true)
        {
            var entry = _dbSet.Update(entity);
            if (autoSave)
                await _context.SaveChangesAsync();
        }
    }
}
