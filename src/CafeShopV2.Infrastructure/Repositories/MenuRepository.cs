using CafeShopV2.Core.Entities;
using CafeShopV2.Core.Interfaces;
using CafeShopV2.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CafeShopV2.Infrastructure.Repositories;

public class MenuRepository : IMenuRepository
{
    private readonly CafeShopDbContext _context;

    public MenuRepository(CafeShopDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Menu>> GetAllAsync()
    {
        return await _context.Menus.ToListAsync();
    }

    public async Task<Menu?> GetByIdAsync(int id)
    {
        return await _context.Menus.FindAsync(id);
    }

    public async Task<Menu> AddAsync(Menu menu)
    {
        menu.CreatedAt = DateTime.UtcNow;
        _context.Menus.Add(menu);
        await _context.SaveChangesAsync();
        return menu;
    }

    public async Task<Menu> UpdateAsync(Menu menu)
    {
        menu.UpdatedAt = DateTime.UtcNow;
        _context.Entry(menu).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return menu;
    }

    public async Task DeleteAsync(int id)
    {
        var menu = await _context.Menus.FindAsync(id);
        if (menu != null)
        {
            _context.Menus.Remove(menu);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Menu>> GetByCategoryAsync(string category)
    {
        return await _context.Menus.Where(m => m.Category == category).ToListAsync();
    }
} 