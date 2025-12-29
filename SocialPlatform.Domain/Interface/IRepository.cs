using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialPlatform.Domain.Interface
{
    public interface IRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T entity , bool autoSave = true);
        Task UpdateAsync(T entity , bool autoSave = true);
        Task DeleteAsync(int id , bool autoSave = true);
    }
}
