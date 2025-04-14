using CafeShopV2.Core.Entities;
using CafeShopV2.Core.Enums;

namespace CafeShopV2.Core.Interfaces;

public interface IMenuRepository
{
    Task<IEnumerable<Menu>> GetAllAsync();
    
    Task<Menu?> GetByIdAsync(int id);
    
    Task<IEnumerable<Menu>> GetByCategoryAsync(MenuCategory category);
    
    Task<Menu> CreateAsync(Menu menu);
    
    Task<Menu> UpdateAsync(Menu menu);
    
    Task<bool> DeleteAsync(int id);
} 