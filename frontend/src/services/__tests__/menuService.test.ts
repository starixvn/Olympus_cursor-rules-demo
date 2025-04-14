import { describe, it, expect, vi } from 'vitest';
import { menuService } from '../menuService';
import { MenuCategory } from '../../types/menu';

describe('menuService', () => {
  it('should get all menu items', async () => {
    const items = await menuService.getAllItems();
    expect(items).toBeDefined();
    expect(Array.isArray(items)).toBe(true);
    expect(items.length).toBeGreaterThan(0);
  });

  it('should get item by id', async () => {
    const items = await menuService.getAllItems();
    const firstItem = items[0];
    const item = await menuService.getItemById(firstItem.id);
    expect(item).toBeDefined();
    expect(item?.id).toBe(firstItem.id);
  });

  it('should get items by category', async () => {
    const items = await menuService.getItemsByCategory(MenuCategory.DRINK);
    expect(items).toBeDefined();
    expect(Array.isArray(items)).toBe(true);
    expect(items.every(item => item.category === MenuCategory.DRINK)).toBe(true);
  });

  it('should add new item', async () => {
    const newItem = {
      name: 'Test Item',
      description: 'Test Description',
      price: 9.99,
      category: MenuCategory.FOOD,
      isAvailable: true
    };

    const addedItem = await menuService.addItem(newItem);
    expect(addedItem).toBeDefined();
    expect(addedItem.name).toBe(newItem.name);
    expect(addedItem.price).toBe(newItem.price);
  });

  it('should update item', async () => {
    const items = await menuService.getAllItems();
    const firstItem = items[0];
    const updateData = {
      name: 'Updated Name',
      price: 19.99
    };

    const updatedItem = await menuService.updateItem(firstItem.id, updateData);
    expect(updatedItem).toBeDefined();
    expect(updatedItem?.name).toBe(updateData.name);
    expect(updatedItem?.price).toBe(updateData.price);
  });

  it('should delete item', async () => {
    const items = await menuService.getAllItems();
    const firstItem = items[0];
    const result = await menuService.deleteItem(firstItem.id);
    expect(result).toBe(true);
  });
}); 