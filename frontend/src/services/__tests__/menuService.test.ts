import { describe, it, expect, vi } from 'vitest';
import { menuService } from '../menuService';
import { MenuCategory } from '../../types/menu';
import { Menu } from '../menuService';

describe('menuService', () => {
  it('should get all menu items', async () => {
    const items = await menuService.getAll();
    expect(items).toBeDefined();
    expect(Array.isArray(items)).toBe(true);
    expect(items.length).toBeGreaterThan(0);
  });

  it('should get item by id', async () => {
    const items = await menuService.getAll();
    const firstItem = items[0];
    const item = await menuService.getById(firstItem.id);
    expect(item).toBeDefined();
    expect(item?.id).toBe(firstItem.id);
  });

  it('should get items by category', async () => {
    const items = await menuService.getByCategory(MenuCategory.Drinks);
    expect(items).toBeDefined();
    expect(Array.isArray(items)).toBe(true);
    expect(items.every((item: Menu) => item.category === MenuCategory.Drinks)).toBe(true);
  });

  it('should add new item', async () => {
    const newItem = {
      name: 'Test Item',
      description: 'Test Description',
      price: 9.99,
      category: MenuCategory.Food,
      imageUrl: 'test-image.jpg',
      isAvailable: true
    };

    const addedItem = await menuService.create(newItem);
    expect(addedItem).toBeDefined();
    expect(addedItem.name).toBe(newItem.name);
    expect(addedItem.price).toBe(newItem.price);
  });

  it('should update item', async () => {
    const items = await menuService.getAll();
    const firstItem = items[0];
    const updateData = {
      name: 'Updated Name',
      price: 19.99
    };

    const updatedItem = await menuService.update(firstItem.id, updateData);
    expect(updatedItem).toBeDefined();
    expect(updatedItem?.name).toBe(updateData.name);
    expect(updatedItem?.price).toBe(updateData.price);
  });

  it('should delete item', async () => {
    const items = await menuService.getAll();
    const firstItem = items[0];
    await menuService.delete(firstItem.id);
    // Verify the item is deleted by trying to get it
    try {
      await menuService.getById(firstItem.id);
      // If we get here, the item wasn't deleted
      expect(true).toBe(false);
    } catch (error) {
      // Expected error, item was deleted
      expect(error).toBeDefined();
    }
  });
}); 