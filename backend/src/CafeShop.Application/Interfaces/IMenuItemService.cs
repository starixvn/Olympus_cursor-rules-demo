using CafeShop.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CafeShop.Application.Interfaces
{
    public interface IMenuItemService
    {
        Task<IEnumerable<MenuItem>> GetAllMenuItemsAsync();
        Task<MenuItem> GetMenuItemByIdAsync(int id);
        Task<IEnumerable<MenuItem>> GetMenuItemsByCategoryAsync(MenuCategory category);
        Task<MenuItem> CreateMenuItemAsync(MenuItem menuItem);
        Task<MenuItem> UpdateMenuItemAsync(MenuItem menuItem);
        Task<bool> DeleteMenuItemAsync(int id);
    }
} 