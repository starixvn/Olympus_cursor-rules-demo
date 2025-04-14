import React, { useState } from 'react';
import { MenuList } from '../components/Menu/MenuList';
import { MenuForm } from '../components/Menu/MenuForm';
import { Button } from '../components/Button/Button';
import { MenuItem } from '../types/menu';
import { menuService } from '../services/menuService';

export const MenuManagement: React.FC = () => {
  const [showForm, setShowForm] = useState(false);
  const [selectedItem, setSelectedItem] = useState<MenuItem | null>(null);

  const handleAddNew = () => {
    setSelectedItem(null);
    setShowForm(true);
  };

  const handleEdit = async (id: string) => {
    try {
      const item = await menuService.getItemById(id);
      if (item) {
        setSelectedItem(item);
        setShowForm(true);
      }
    } catch (error) {
      console.error('Error loading item:', error);
    }
  };

  const handleFormClose = () => {
    setShowForm(false);
    setSelectedItem(null);
  };

  const handleFormSave = () => {
    setShowForm(false);
    setSelectedItem(null);
    // Refresh the menu list
    window.location.reload();
  };

  return (
    <div className="menu-management">
      <div className="header">
        <h1>Menu Management</h1>
        <Button onClick={handleAddNew}>Add New Item</Button>
      </div>

      {showForm ? (
        <div className="form-container">
          <h2>{selectedItem ? 'Edit Menu Item' : 'Add New Menu Item'}</h2>
          <MenuForm
            item={selectedItem || undefined}
            onSave={handleFormSave}
            onCancel={handleFormClose}
          />
        </div>
      ) : (
        <MenuList onEdit={handleEdit} />
      )}
    </div>
  );
}; 