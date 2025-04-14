using CafeShop.Core.Entities;
using CafeShop.Core.Interfaces;
using CafeShop.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CafeShop.Infrastructure.Repositories
{
    public class MenuItemRepository : IMenuItemRepository
    {
        private readonly CafeShopDbContext _context;

        public MenuItemRepository(CafeShopDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MenuItem>> GetAllAsync()
        {
            return await _context.MenuItems.ToListAsync();
        }

        public async Task<MenuItem> GetByIdAsync(int id)
        {
            return await _context.MenuItems.FindAsync(id);
        }

        public async Task<IEnumerable<MenuItem>> GetByCategoryAsync(MenuCategory category)
        {
            return await _context.MenuItems
                .Where(m => m.Category == category)
                .ToListAsync();
        }

        public async Task<MenuItem> AddAsync(MenuItem menuItem)
        {
            menuItem.CreatedAt = DateTime.UtcNow;
            _context.MenuItems.Add(menuItem);
            await _context.SaveChangesAsync();
            return menuItem;
        }

        public async Task<MenuItem> UpdateAsync(MenuItem menuItem)
        {
            var existingItem = await _context.MenuItems.FindAsync(menuItem.Id);
            if (existingItem == null)
                return null;

            existingItem.Name = menuItem.Name;
            existingItem.Description = menuItem.Description;
            existingItem.Price = menuItem.Price;
            existingItem.Category = menuItem.Category;
            existingItem.ImageUrl = menuItem.ImageUrl;
            existingItem.IsAvailable = menuItem.IsAvailable;
            existingItem.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return existingItem;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var menuItem = await _context.MenuItems.FindAsync(id);
            if (menuItem == null)
                return false;

            _context.MenuItems.Remove(menuItem);
            await _context.SaveChangesAsync();
            return true;
        }
    }
} 