import React from 'react';
import { describe, it, expect, vi, beforeEach } from 'vitest';
import { render, screen, fireEvent } from '@testing-library/react';
import { MenuList } from '../MenuList';
import { MenuCategory } from '../../../types/menu';
import { menuService } from '../../../services/menuService';
import { MenuItem } from '../../../types/menu';

// Mock the menuService
vi.mock('../../../services/menuService', () => ({
  menuService: {
    getAllItems: vi.fn(),
    getItemsByCategory: vi.fn(),
    deleteItem: vi.fn()
  }
}));

const mockMenuItems: MenuItem[] = [
  {
    id: '1',
    name: 'Test Item 1',
    description: 'Test Description 1',
    price: 9.99,
    category: MenuCategory.FOOD,
    isAvailable: true
  },
  {
    id: '2',
    name: 'Test Item 2',
    description: 'Test Description 2',
    price: 12.99,
    category: MenuCategory.DRINK,
    isAvailable: true
  }
];

describe('MenuList', () => {
  beforeEach(() => {
    // Reset mocks before each test
    vi.clearAllMocks();
    // Setup default mock implementation
    (menuService.getAllItems as ReturnType<typeof vi.fn>).mockResolvedValue(mockMenuItems);
  });

  it('renders menu items correctly', async () => {
    render(<MenuList />);
    
    // Wait for the items to be loaded
    const items = await screen.findAllByText(/Test Item/);
    expect(items).toHaveLength(2);
    
    // Check if all menu items are rendered
    mockMenuItems.forEach(item => {
      expect(screen.getByText(item.name)).toBeInTheDocument();
      expect(screen.getByText(item.description)).toBeInTheDocument();
      expect(screen.getByText(`$${item.price.toFixed(2)}`)).toBeInTheDocument();
    });
  });

  it('calls onEdit when edit button is clicked', async () => {
    const mockOnEdit = vi.fn();
    render(<MenuList onEdit={mockOnEdit} />);
    
    // Wait for the items to be loaded
    await screen.findAllByText(/Test Item/);
    
    // Find the Edit button using the data-testid
    const editButton = screen.getByTestId('edit-button-1');
    
    // Click the Edit button
    fireEvent.click(editButton);
    
    // Verify that onEdit was called with the correct ID
    expect(mockOnEdit).toHaveBeenCalledWith('1');
  });
}); 