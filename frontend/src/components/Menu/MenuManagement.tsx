import React, { useState } from 'react';
import { MenuItem, MenuCategory } from '../../types/menu';
import { menuService } from '../../services/menuService';
import { MenuList } from './MenuList';
import { MenuForm } from './MenuForm';
import { Button, Dialog, DialogTitle, DialogContent, DialogActions, Alert, Snackbar } from '@mui/material';
import { Add as AddIcon } from '@mui/icons-material';

export const MenuManagement: React.FC = () => {
  const [selectedItem, setSelectedItem] = useState<MenuItem | null>(null);
  const [isFormOpen, setIsFormOpen] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const [successMessage, setSuccessMessage] = useState<string | null>(null);

  const handleEdit = async (id: number) => {
    try {
      const item = await menuService.getById(id);
      setSelectedItem(item);
      setIsFormOpen(true);
    } catch (err) {
      setError('Error loading menu item');
      console.error('Error loading menu item:', err);
    }
  };

  const handleDelete = async (id: number) => {
    if (!window.confirm('Are you sure you want to delete this item?')) {
      return;
    }

    try {
      await menuService.delete(id);
      setSuccessMessage('Menu item deleted successfully');
      // Refresh the list after deletion
      window.location.reload();
    } catch (err) {
      setError('Error deleting menu item');
      console.error('Error deleting menu item:', err);
    }
  };

  const handleSubmit = async (item: Omit<MenuItem, 'id' | 'createdAt' | 'updatedAt'>) => {
    try {
      if (selectedItem) {
        await menuService.update(selectedItem.id, item);
        setSuccessMessage('Menu item updated successfully');
      } else {
        await menuService.create(item);
        setSuccessMessage('Menu item created successfully');
      }
      setIsFormOpen(false);
      setSelectedItem(null);
      // Refresh the list after creating/updating
      window.location.reload();
    } catch (err) {
      setError('Error saving menu item');
      console.error('Error saving menu item:', err);
    }
  };

  const handleCloseError = () => {
    setError(null);
  };

  const handleCloseSuccess = () => {
    setSuccessMessage(null);
  };

  return (
    <div>
      <Button
        variant="contained"
        color="primary"
        startIcon={<AddIcon />}
        onClick={() => {
          setSelectedItem(null);
          setIsFormOpen(true);
        }}
        sx={{ mb: 2 }}
      >
        Add New Item
      </Button>

      <MenuList onEdit={handleEdit} onDelete={handleDelete} />

      <Dialog 
        open={isFormOpen} 
        onClose={() => setIsFormOpen(false)} 
        maxWidth="sm" 
        fullWidth
      >
        <DialogTitle>
          {selectedItem ? 'Edit Menu Item' : 'Add New Menu Item'}
        </DialogTitle>
        <DialogContent>
          <MenuForm
            initialData={selectedItem}
            onSubmit={handleSubmit}
            onCancel={() => setIsFormOpen(false)}
          />
        </DialogContent>
      </Dialog>

      <Snackbar 
        open={!!error} 
        autoHideDuration={6000} 
        onClose={handleCloseError}
        anchorOrigin={{ vertical: 'top', horizontal: 'center' }}
      >
        <Alert onClose={handleCloseError} severity="error">
          {error}
        </Alert>
      </Snackbar>

      <Snackbar 
        open={!!successMessage} 
        autoHideDuration={6000} 
        onClose={handleCloseSuccess}
        anchorOrigin={{ vertical: 'top', horizontal: 'center' }}
      >
        <Alert onClose={handleCloseSuccess} severity="success">
          {successMessage}
        </Alert>
      </Snackbar>
    </div>
  );
}; 