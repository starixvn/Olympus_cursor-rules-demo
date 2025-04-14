using CafeShopV2.Core.Entities;
using CafeShopV2.Core.Enums;
using CafeShopV2.Core.Interfaces;

namespace CafeShopV2.Service.Services;

public sealed class MenuService : IMenuService
{
    private readonly IMenuRepository _menuRepository;

    public MenuService(IMenuRepository menuRepository)
    {
        _menuRepository = menuRepository ?? throw new ArgumentNullException(nameof(menuRepository));
    }

    public async Task<IEnumerable<Menu>> GetAllMenusAsync()
    {
        return await _menuRepository.GetAllAsync();
    }

    public async Task<Menu?> GetMenuByIdAsync(int id)
    {
        return await _menuRepository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Menu>> GetMenusByCategoryAsync(MenuCategory category)
    {
        return await _menuRepository.GetByCategoryAsync(category);
    }

    public async Task<Menu> CreateMenuAsync(Menu menu)
    {
        ArgumentNullException.ThrowIfNull(menu);
        
        ValidateMenu(menu);
        return await _menuRepository.CreateAsync(menu);
    }

    public async Task<Menu> UpdateMenuAsync(Menu menu)
    {
        ArgumentNullException.ThrowIfNull(menu);
        
        ValidateMenu(menu);
        return await _menuRepository.UpdateAsync(menu);
    }

    public async Task DeleteMenuAsync(int id)
    {
        var result = await _menuRepository.DeleteAsync(id);
        if (!result)
        {
            throw new InvalidOperationException($"Menu with ID {id} not found");
        }
    }

    private static void ValidateMenu(Menu menu)
    {
        ArgumentException.ThrowIfNullOrEmpty(menu.Name);
        
        if (menu.Price < 0)
        {
            throw new ArgumentException("Price cannot be negative", nameof(menu.Price));
        }
        
        if (!string.IsNullOrEmpty(menu.ImageUrl) && !Uri.IsWellFormedUriString(menu.ImageUrl, UriKind.Absolute))
        {
            throw new ArgumentException("Invalid image URL format", nameof(menu.ImageUrl));
        }
    }
} 