import { MenuItem, MenuCategory, MenuItemFormData } from '../types/menu';

const API_BASE_URL = 'http://localhost:5000/api';

export const menuService = {
  getAllItems: async (): Promise<MenuItem[]> => {
    const response = await fetch(`${API_BASE_URL}/MenuItems`);
    if (!response.ok) {
      throw new Error('Failed to fetch menu items');
    }
    return response.json();
  },

  getItemById: async (id: string): Promise<MenuItem | undefined> => {
    const response = await fetch(`${API_BASE_URL}/MenuItems/${id}`);
    if (!response.ok) {
      if (response.status === 404) {
        return undefined;
      }
      throw new Error('Failed to fetch menu item');
    }
    return response.json();
  },

  getItemsByCategory: async (category: MenuCategory): Promise<MenuItem[]> => {
    const response = await fetch(`${API_BASE_URL}/MenuItems/category/${category}`);
    if (!response.ok) {
      throw new Error('Failed to fetch menu items by category');
    }
    return response.json();
  },

  addItem: async (item: MenuItemFormData): Promise<MenuItem> => {
    const response = await fetch(`${API_BASE_URL}/MenuItems`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(item),
    });
    if (!response.ok) {
      throw new Error('Failed to add menu item');
    }
    return response.json();
  },

  updateItem: async (id: string, item: Partial<MenuItemFormData>): Promise<MenuItem | undefined> => {
    const response = await fetch(`${API_BASE_URL}/MenuItems/${id}`, {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(item),
    });
    if (!response.ok) {
      if (response.status === 404) {
        return undefined;
      }
      throw new Error('Failed to update menu item');
    }
    return response.json();
  },

  deleteItem: async (id: string): Promise<boolean> => {
    const response = await fetch(`${API_BASE_URL}/MenuItems/${id}`, {
      method: 'DELETE',
    });
    if (!response.ok) {
      if (response.status === 404) {
        return false;
      }
      throw new Error('Failed to delete menu item');
    }
    return true;
  }
}; 