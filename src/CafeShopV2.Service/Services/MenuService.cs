using CafeShopV2.Core.Entities;
using CafeShopV2.Core.Interfaces;

namespace CafeShopV2.Service.Services;

public class MenuService : IMenuService
{
    private readonly IMenuRepository _menuRepository;

    public MenuService(IMenuRepository menuRepository)
    {
        _menuRepository = menuRepository;
    }

    public async Task<IEnumerable<Menu>> GetAllMenusAsync()
    {
        return await _menuRepository.GetAllAsync();
    }

    public async Task<Menu?> GetMenuByIdAsync(int id)
    {
        return await _menuRepository.GetByIdAsync(id);
    }

    public async Task<Menu> CreateMenuAsync(Menu menu)
    {
        return await _menuRepository.AddAsync(menu);
    }

    public async Task<Menu> UpdateMenuAsync(Menu menu)
    {
        return await _menuRepository.UpdateAsync(menu);
    }

    public async Task DeleteMenuAsync(int id)
    {
        await _menuRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<Menu>> GetMenusByCategoryAsync(string category)
    {
        return await _menuRepository.GetByCategoryAsync(category);
    }
} 