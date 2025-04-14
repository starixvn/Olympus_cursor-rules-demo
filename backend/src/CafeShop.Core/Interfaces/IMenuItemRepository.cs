using CafeShop.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CafeShop.Core.Interfaces
{
    public interface IMenuItemRepository
    {
        Task<IEnumerable<MenuItem>> GetAllAsync();
        Task<MenuItem> GetByIdAsync(int id);
        Task<IEnumerable<MenuItem>> GetByCategoryAsync(MenuCategory category);
        Task<MenuItem> AddAsync(MenuItem menuItem);
        Task<MenuItem> UpdateAsync(MenuItem menuItem);
        Task<bool> DeleteAsync(int id);
    }
} 