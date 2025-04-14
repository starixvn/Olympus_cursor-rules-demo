import React, { useState } from 'react';
import { MenuItem, MenuCategory, MenuItemFormData } from '../../types/menu';
import { menuService } from '../../services/menuService';
import { Button } from '../Button/Button';

interface MenuFormProps {
  item?: MenuItem;
  onSave: () => void;
  onCancel: () => void;
}

export const MenuForm: React.FC<MenuFormProps> = ({ item, onSave, onCancel }) => {
  const [formData, setFormData] = useState<MenuItemFormData>({
    name: item?.name || '',
    description: item?.description || '',
    price: item?.price || 0,
    category: item?.category || MenuCategory.FOOD,
    isAvailable: item?.isAvailable ?? true,
  });

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    try {
      if (item) {
        await menuService.updateItem(item.id, formData);
      } else {
        await menuService.addItem(formData);
      }
      onSave();
    } catch (error) {
      console.error('Error saving menu item:', error);
    }
  };

  const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement | HTMLSelectElement>) => {
    const { name, value, type } = e.target;
    setFormData(prev => ({
      ...prev,
      [name]: type === 'number' ? parseFloat(value) : value
    }));
  };

  const handleCheckboxChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, checked } = e.target;
    setFormData(prev => ({
      ...prev,
      [name]: checked
    }));
  };

  return (
    <form onSubmit={handleSubmit} className="menu-form">
      <div className="form-group">
        <label htmlFor="name">Name:</label>
        <input
          type="text"
          id="name"
          name="name"
          value={formData.name}
          onChange={handleChange}
          required
        />
      </div>

      <div className="form-group">
        <label htmlFor="description">Description:</label>
        <textarea
          id="description"
          name="description"
          value={formData.description}
          onChange={handleChange}
          required
        />
      </div>

      <div className="form-group">
        <label htmlFor="price">Price:</label>
        <input
          type="number"
          id="price"
          name="price"
          value={formData.price}
          onChange={handleChange}
          min="0"
          step="0.01"
          required
        />
      </div>

      <div className="form-group">
        <label htmlFor="category">Category:</label>
        <select
          id="category"
          name="category"
          value={formData.category}
          onChange={handleChange}
          required
        >
          {Object.values(MenuCategory).map(category => (
            <option key={category} value={category}>
              {category}
            </option>
          ))}
        </select>
      </div>

      <div className="form-group">
        <label>
          <input
            type="checkbox"
            name="isAvailable"
            checked={formData.isAvailable}
            onChange={handleCheckboxChange}
          />
          Available
        </label>
      </div>

      <div className="form-actions">
        <Button type="submit">
          {item ? 'Update' : 'Add'} Item
        </Button>
        <Button type="button" variant="secondary" onClick={onCancel}>
          Cancel
        </Button>
      </div>
    </form>
  );
}; 