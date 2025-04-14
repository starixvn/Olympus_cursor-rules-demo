import axios from 'axios';

export enum MenuCategory {
  Food = 'Food',
  Drinks = 'Drinks'
}

export interface Menu {
  id: number;
  name: string;
  description: string;
  category: MenuCategory;
  price: number;
  imageUrl: string;
  isAvailable: boolean;
  createdAt: string;
  updatedAt: string;
}

const api = axios.create({
  baseURL: '/api',
  headers: {
    'Content-Type': 'application/json',
  },
});

export const menuService = {
  getAll: async (): Promise<Menu[]> => {
    const response = await api.get<Menu[]>('/menu');
    return response.data;
  },

  getById: async (id: number): Promise<Menu> => {
    const response = await api.get<Menu>(`/menu/${id}`);
    return response.data;
  },

  getByCategory: async (category: MenuCategory): Promise<Menu[]> => {
    const response = await api.get<Menu[]>(`/menu/category/${category}`);
    return response.data;
  },

  create: async (menu: Omit<Menu, 'id' | 'createdAt' | 'updatedAt'>): Promise<Menu> => {
    const response = await api.post<Menu>('/menu', menu);
    return response.data;
  },

  update: async (id: number, menu: Partial<Menu>): Promise<Menu> => {
    const response = await api.put<Menu>(`/menu/${id}`, menu);
    return response.data;
  },

  delete: async (id: number): Promise<void> => {
    await api.delete(`/menu/${id}`);
  },
}; 