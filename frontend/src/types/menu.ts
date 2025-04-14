export interface MenuItem {
  id: string;
  name: string;
  description: string;
  price: number;
  category: MenuCategory;
  image?: string;
  isAvailable: boolean;
}

export enum MenuCategory {
  Food = 'Food',
  Drinks = 'Drinks',
  Desserts = 'Desserts',
  Snacks = 'Snacks'
}

export interface MenuItemFormData {
  name: string;
  description: string;
  price: number;
  category: MenuCategory;
  image?: string;
  isAvailable: boolean;
} 