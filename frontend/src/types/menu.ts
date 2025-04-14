export interface MenuItem {
  id: number;
  name: string;
  description: string;
  price: number;
  category: MenuCategory;
  imageUrl: string;
  isAvailable: boolean;
  createdAt: string;
  updatedAt: string;
}

export enum MenuCategory {
  Food = 'Food',
  Drinks = 'Drinks'
}

export interface MenuItemFormData {
  name: string;
  description: string;
  price: number;
  category: MenuCategory;
  imageUrl?: string;
  isAvailable: boolean;
} 