using CafeShopV2.Core.Entities;

namespace CafeShopV2.Core.Interfaces;

public interface IMenuRepository
{
    Task<IEnumerable<Menu>> GetAllAsync();
    Task<Menu?> GetByIdAsync(int id);
    Task<Menu> AddAsync(Menu menu);
    Task<Menu> UpdateAsync(Menu menu);
    Task DeleteAsync(int id);
    Task<IEnumerable<Menu>> GetByCategoryAsync(string category);
} 