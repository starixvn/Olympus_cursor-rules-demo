import React, { useEffect, useState } from 'react';
import { MenuItem, MenuCategory } from '../../types/menu';
import { menuService } from '../../services/menuService';
import { Button } from '../Button/Button';

interface MenuListProps {
  onEdit?: (id: string) => void;
}

export const MenuList: React.FC<MenuListProps> = ({ onEdit }) => {
  const [menuItems, setMenuItems] = useState<MenuItem[]>([]);
  const [loading, setLoading] = useState(true);
  const [selectedCategory, setSelectedCategory] = useState<MenuCategory | 'ALL'>('ALL');

  useEffect(() => {
    loadMenuItems();
  }, [selectedCategory]);

  const loadMenuItems = async () => {
    try {
      setLoading(true);
      const items = selectedCategory === 'ALL'
        ? await menuService.getAllItems()
        : await menuService.getItemsByCategory(selectedCategory);
      setMenuItems(items);
    } catch (error) {
      console.error('Error loading menu items:', error);
    } finally {
      setLoading(false);
    }
  };

  const handleDelete = async (id: string) => {
    if (window.confirm('Are you sure you want to delete this item?')) {
      try {
        await menuService.deleteItem(id);
        setMenuItems(items => items.filter(item => item.id !== id));
      } catch (error) {
        console.error('Error deleting item:', error);
      }
    }
  };

  if (loading) {
    return <div>Loading...</div>;
  }

  return (
    <div className="menu-list">
      <div className="category-filters">
        <Button
          variant={selectedCategory === 'ALL' ? 'primary' : 'secondary'}
          onClick={() => setSelectedCategory('ALL')}
        >
          All
        </Button>
        {Object.values(MenuCategory).map(category => (
          <Button
            key={category}
            variant={selectedCategory === category ? 'primary' : 'secondary'}
            onClick={() => setSelectedCategory(category)}
          >
            {category}
          </Button>
        ))}
      </div>

      <div className="menu-items">
        {menuItems.map(item => (
          <div key={item.id} className="menu-item">
            <h3>{item.name}</h3>
            <p>{item.description}</p>
            <p className="price">${item.price.toFixed(2)}</p>
            <p className="category">Category: {item.category}</p>
            <p className="availability">
              Status: {item.isAvailable ? 'Available' : 'Not Available'}
            </p>
            <div className="actions">
              {onEdit && (
                <Button 
                  onClick={() => onEdit(item.id)}
                  data-testid={`edit-button-${item.id}`}
                >
                  Edit
                </Button>
              )}
              <Button onClick={() => handleDelete(item.id)}>
                Delete
              </Button>
            </div>
          </div>
        ))}
      </div>
    </div>
  );
}; 