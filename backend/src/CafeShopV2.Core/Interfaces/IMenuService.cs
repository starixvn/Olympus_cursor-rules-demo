using CafeShopV2.Core.Entities;
using CafeShopV2.Core.Enums;

namespace CafeShopV2.Core.Interfaces;

public interface IMenuService
{
    Task<IEnumerable<Menu>> GetAllMenusAsync();
    
    Task<Menu?> GetMenuByIdAsync(int id);
    
    Task<IEnumerable<Menu>> GetMenusByCategoryAsync(MenuCategory category);
    
    Task<Menu> CreateMenuAsync(Menu menu);
    
    Task<Menu> UpdateMenuAsync(Menu menu);
    
    Task DeleteMenuAsync(int id);
} 