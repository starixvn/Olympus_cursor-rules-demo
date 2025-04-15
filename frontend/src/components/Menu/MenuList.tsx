import React, { useEffect, useState } from 'react';
import { MenuItem, MenuCategory } from '../../types/menu';
import { menuService } from '../../services/menuService';
import { Button, Card, CardContent, CardMedia, Grid, Typography, Box } from '@mui/material';
import { Edit as EditIcon, Delete as DeleteIcon } from '@mui/icons-material';

interface MenuListProps {
  onEdit?: (id: number) => void;
  onDelete?: (id: number) => void;
  category?: MenuCategory;
}

export const MenuList: React.FC<MenuListProps> = ({ onEdit, onDelete, category }) => {
  const [menuItems, setMenuItems] = useState<MenuItem[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const fetchMenuItems = async () => {
      try {
        setLoading(true);
        const items = category 
          ? await menuService.getByCategory(category)
          : await menuService.getAll();
        setMenuItems(items);
      } catch (err) {
        setError('Failed to load menu items');
        console.error('Error fetching menu items:', err);
      } finally {
        setLoading(false);
      }
    };

    fetchMenuItems();
  }, [category]);

  const handleDelete = async (id: number) => {
    try {
      await menuService.delete(id);
      setMenuItems(menuItems.filter(item => item.id !== id));
      onDelete?.(id);
    } catch (err) {
      setError('Failed to delete menu item');
      console.error('Error deleting menu item:', err);
    }
  };

  if (loading) {
    return <Typography>Loading...</Typography>;
  }

  if (error) {
    return <Typography color="error">{error}</Typography>;
  }

  return (
    <Grid container spacing={3}>
      {menuItems.map((item) => (
        <Grid item xs={12} sm={6} md={4} key={item.id}>
          <Card>
            <CardMedia
              component="img"
              height="140"
              image={item.imageUrl}
              alt={item.name}
            />
            <CardContent>
              <Typography variant="h6" component="div">
                {item.name}
              </Typography>
              <Typography variant="body2" color="text.secondary">
                {item.description}
              </Typography>
              <Typography variant="h6" color="primary">
                ${item.price.toFixed(2)}
              </Typography>
              <Box sx={{ mt: 2, display: 'flex', justifyContent: 'flex-end', gap: 1 }}>
                <Button
                  size="small"
                  startIcon={<EditIcon />}
                  onClick={() => onEdit?.(item.id)}
                  data-testid={`edit-button-${item.id}`}
                >
                  Edit
                </Button>
                <Button
                  size="small"
                  color="error"
                  startIcon={<DeleteIcon />}
                  onClick={() => handleDelete(item.id)}
                >
                  Delete
                </Button>
              </Box>
            </CardContent>
          </Card>
        </Grid>
      ))}
    </Grid>
  );
}; 