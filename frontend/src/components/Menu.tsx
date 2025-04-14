import React, { useEffect, useState } from 'react';
import { Menu as MenuType, MenuCategory, menuService } from '../services/menuService';

const Menu: React.FC = () => {
  const [menus, setMenus] = useState<MenuType[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const [selectedCategory, setSelectedCategory] = useState<MenuCategory | null>(null);

  useEffect(() => {
    loadMenus();
  }, [selectedCategory]);

  const loadMenus = async () => {
    try {
      setLoading(true);
      setError(null);
      const data = selectedCategory
        ? await menuService.getByCategory(selectedCategory)
        : await menuService.getAll();
      setMenus(data);
    } catch (err) {
      setError('Failed to load menu items');
      console.error('Error loading menus:', err);
    } finally {
      setLoading(false);
    }
  };

  const handleCategoryChange = (category: MenuCategory | null) => {
    setSelectedCategory(category);
  };

  if (loading) {
    return (
      <div className="flex justify-center items-center min-h-[400px]">
        <div className="animate-spin rounded-full h-12 w-12 border-t-2 border-b-2 border-blue-500"></div>
      </div>
    );
  }

  if (error) {
    return (
      <div className="text-center p-8">
        <div className="text-red-500 text-xl mb-4">{error}</div>
        <button
          onClick={loadMenus}
          className="px-4 py-2 bg-blue-500 text-white rounded hover:bg-blue-600 transition-colors"
        >
          Try Again
        </button>
      </div>
    );
  }

  return (
    <div className="container mx-auto px-4 py-8">
      <h1 className="text-4xl font-bold mb-8 text-center text-gray-800">Our Menu</h1>
      
      <div className="flex justify-center gap-4 mb-12">
        <button
          onClick={() => handleCategoryChange(null)}
          className={`px-6 py-2 rounded-full transition-all ${
            selectedCategory === null
              ? 'bg-blue-500 text-white shadow-lg'
              : 'bg-gray-100 text-gray-700 hover:bg-gray-200'
          }`}
        >
          All
        </button>
        <button
          onClick={() => handleCategoryChange(MenuCategory.Drinks)}
          className={`px-6 py-2 rounded-full transition-all ${
            selectedCategory === MenuCategory.Drinks
              ? 'bg-blue-500 text-white shadow-lg'
              : 'bg-gray-100 text-gray-700 hover:bg-gray-200'
          }`}
        >
          Drinks
        </button>
        <button
          onClick={() => handleCategoryChange(MenuCategory.Food)}
          className={`px-6 py-2 rounded-full transition-all ${
            selectedCategory === MenuCategory.Food
              ? 'bg-blue-500 text-white shadow-lg'
              : 'bg-gray-100 text-gray-700 hover:bg-gray-200'
          }`}
        >
          Food
        </button>
      </div>

      <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-8">
        {menus.map((menu) => (
          <div
            key={menu.id}
            className="bg-white rounded-xl shadow-lg overflow-hidden hover:shadow-xl transition-shadow duration-300"
          >
            <div className="relative h-48">
              <img
                src={menu.imageUrl}
                alt={menu.name}
                className="w-full h-full object-cover"
                onError={(e) => {
                  const target = e.target as HTMLImageElement;
                  target.src = 'https://via.placeholder.com/400x300?text=No+Image';
                }}
              />
              <div className="absolute top-4 right-4">
                <span
                  className={`px-3 py-1 rounded-full text-sm font-medium ${
                    menu.isAvailable
                      ? 'bg-green-100 text-green-800'
                      : 'bg-red-100 text-red-800'
                  }`}
                >
                  {menu.isAvailable ? 'Available' : 'Unavailable'}
                </span>
              </div>
            </div>
            <div className="p-6">
              <h2 className="text-xl font-semibold mb-2 text-gray-800">{menu.name}</h2>
              <p className="text-gray-600 mb-4 line-clamp-2">{menu.description}</p>
              <div className="flex justify-between items-center">
                <span className="text-2xl font-bold text-blue-600">
                  ${menu.price.toFixed(2)}
                </span>
                <button
                  className="px-4 py-2 bg-blue-500 text-white rounded-full hover:bg-blue-600 transition-colors disabled:opacity-50 disabled:cursor-not-allowed"
                  disabled={!menu.isAvailable}
                >
                  Order Now
                </button>
              </div>
            </div>
          </div>
        ))}
      </div>
    </div>
  );
};

export default Menu; 