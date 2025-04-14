using CafeShop.Application.Interfaces;
using CafeShop.Core.Entities;
using CafeShop.Core.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CafeShop.Application.Services
{
    public class MenuItemService : IMenuItemService
    {
        private readonly IMenuItemRepository _menuItemRepository;

        public MenuItemService(IMenuItemRepository menuItemRepository)
        {
            _menuItemRepository = menuItemRepository;
        }

        public async Task<IEnumerable<MenuItem>> GetAllMenuItemsAsync()
        {
            return await _menuItemRepository.GetAllAsync();
        }

        public async Task<MenuItem> GetMenuItemByIdAsync(int id)
        {
            return await _menuItemRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<MenuItem>> GetMenuItemsByCategoryAsync(MenuCategory category)
        {
            return await _menuItemRepository.GetByCategoryAsync(category);
        }

        public async Task<MenuItem> CreateMenuItemAsync(MenuItem menuItem)
        {
            return await _menuItemRepository.AddAsync(menuItem);
        }

        public async Task<MenuItem> UpdateMenuItemAsync(MenuItem menuItem)
        {
            return await _menuItemRepository.UpdateAsync(menuItem);
        }

        public async Task<bool> DeleteMenuItemAsync(int id)
        {
            return await _menuItemRepository.DeleteAsync(id);
        }
    }
} 