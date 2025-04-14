using CafeShopV2.Core.Entities;

namespace CafeShopV2.Core.Interfaces;

public interface IMenuService
{
    Task<IEnumerable<Menu>> GetAllMenusAsync();
    
    Task<Menu?> GetMenuByIdAsync(int id);
    
    Task<IEnumerable<Menu>> GetMenusByCategoryAsync(string category);
    
    Task<Menu> CreateMenuAsync(Menu menu);
    
    Task<Menu> UpdateMenuAsync(Menu menu);
    
    Task<bool> DeleteMenuAsync(int id);
} 