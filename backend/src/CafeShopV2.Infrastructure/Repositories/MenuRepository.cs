using CafeShopV2.Core.Entities;
using CafeShopV2.Core.Interfaces;
using CafeShopV2.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CafeShopV2.Infrastructure.Repositories;

public sealed class MenuRepository : IMenuRepository
{
    private readonly ApplicationDbContext _context;

    public MenuRepository(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<IEnumerable<Menu>> GetAllAsync()
    {
        return await _context.Menus.ToListAsync();
    }

    public async Task<Menu?> GetByIdAsync(int id)
    {
        return await _context.Menus.FindAsync(id);
    }

    public async Task<IEnumerable<Menu>> GetByCategoryAsync(string category)
    {
        ArgumentException.ThrowIfNullOrEmpty(category);
        
        return await _context.Menus
            .Where(m => m.Category.ToLower() == category.ToLower())
            .ToListAsync();
    }

    public async Task<Menu> CreateAsync(Menu menu)
    {
        ArgumentNullException.ThrowIfNull(menu);
        
        menu.CreatedAt = DateTime.UtcNow;
        _context.Menus.Add(menu);
        await _context.SaveChangesAsync();
        return menu;
    }

    public async Task<Menu> UpdateAsync(Menu menu)
    {
        ArgumentNullException.ThrowIfNull(menu);
        
        var existingMenu = await _context.Menus.FindAsync(menu.Id);
        if (existingMenu == null)
        {
            throw new InvalidOperationException($"Menu with ID {menu.Id} not found");
        }

        menu.UpdatedAt = DateTime.UtcNow;
        _context.Entry(existingMenu).CurrentValues.SetValues(menu);
        await _context.SaveChangesAsync();
        return menu;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var menu = await _context.Menus.FindAsync(id);
        if (menu == null)
        {
            return false;
        }

        _context.Menus.Remove(menu);
        await _context.SaveChangesAsync();
        return true;
    }
} 