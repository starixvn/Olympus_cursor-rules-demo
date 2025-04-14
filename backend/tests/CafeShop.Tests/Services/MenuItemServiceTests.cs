using CafeShop.Application.Services;
using CafeShop.Core.Entities;
using CafeShop.Core.Interfaces;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace CafeShop.Tests.Services
{
    public class MenuItemServiceTests
    {
        private readonly IMenuItemRepository _menuItemRepository;
        private readonly MenuItemService _menuItemService;

        public MenuItemServiceTests()
        {
            _menuItemRepository = Substitute.For<IMenuItemRepository>();
            _menuItemService = new MenuItemService(_menuItemRepository);
        }

        [Fact]
        public async Task GetAllMenuItemsAsync_ShouldReturnAllMenuItems()
        {
            // Arrange
            var expectedMenuItems = new List<MenuItem>
            {
                new MenuItem { Id = 1, Name = "Test Item 1", Price = 9.99m, Category = MenuCategory.Food, IsAvailable = true },
                new MenuItem { Id = 2, Name = "Test Item 2", Price = 12.99m, Category = MenuCategory.Drinks, IsAvailable = true }
            };
            _menuItemRepository.GetAllAsync().Returns(expectedMenuItems);

            // Act
            var result = await _menuItemService.GetAllMenuItemsAsync();

            // Assert
            Assert.Equal(expectedMenuItems, result);
            await _menuItemRepository.Received(1).GetAllAsync();
        }

        [Fact]
        public async Task GetMenuItemByIdAsync_ShouldReturnMenuItem_WhenExists()
        {
            // Arrange
            var expectedMenuItem = new MenuItem { Id = 1, Name = "Test Item", Price = 9.99m, Category = MenuCategory.Food, IsAvailable = true };
            _menuItemRepository.GetByIdAsync(1).Returns(expectedMenuItem);

            // Act
            var result = await _menuItemService.GetMenuItemByIdAsync(1);

            // Assert
            Assert.Equal(expectedMenuItem, result);
            await _menuItemRepository.Received(1).GetByIdAsync(1);
        }

        [Fact]
        public async Task GetMenuItemByIdAsync_ShouldReturnNull_WhenNotExists()
        {
            // Arrange
            _menuItemRepository.GetByIdAsync(1).Returns((MenuItem)null);

            // Act
            var result = await _menuItemService.GetMenuItemByIdAsync(1);

            // Assert
            Assert.Null(result);
            await _menuItemRepository.Received(1).GetByIdAsync(1);
        }

        [Fact]
        public async Task CreateMenuItemAsync_ShouldCreateAndReturnMenuItem()
        {
            // Arrange
            var menuItem = new MenuItem { Name = "Test Item", Price = 9.99m, Category = MenuCategory.Food, IsAvailable = true };
            var expectedMenuItem = new MenuItem { Id = 1, Name = "Test Item", Price = 9.99m, Category = MenuCategory.Food, IsAvailable = true };
            _menuItemRepository.AddAsync(menuItem).Returns(expectedMenuItem);

            // Act
            var result = await _menuItemService.CreateMenuItemAsync(menuItem);

            // Assert
            Assert.Equal(expectedMenuItem, result);
            await _menuItemRepository.Received(1).AddAsync(menuItem);
        }

        [Fact]
        public async Task UpdateMenuItemAsync_ShouldUpdateAndReturnMenuItem_WhenExists()
        {
            // Arrange
            var menuItem = new MenuItem { Id = 1, Name = "Updated Item", Price = 12.99m, Category = MenuCategory.Food, IsAvailable = true };
            _menuItemRepository.UpdateAsync(menuItem).Returns(menuItem);

            // Act
            var result = await _menuItemService.UpdateMenuItemAsync(menuItem);

            // Assert
            Assert.Equal(menuItem, result);
            await _menuItemRepository.Received(1).UpdateAsync(menuItem);
        }

        [Fact]
        public async Task UpdateMenuItemAsync_ShouldReturnNull_WhenNotExists()
        {
            // Arrange
            var menuItem = new MenuItem { Id = 1, Name = "Updated Item", Price = 12.99m, Category = MenuCategory.Food, IsAvailable = true };
            _menuItemRepository.UpdateAsync(menuItem).Returns((MenuItem)null);

            // Act
            var result = await _menuItemService.UpdateMenuItemAsync(menuItem);

            // Assert
            Assert.Null(result);
            await _menuItemRepository.Received(1).UpdateAsync(menuItem);
        }

        [Fact]
        public async Task DeleteMenuItemAsync_ShouldReturnTrue_WhenExists()
        {
            // Arrange
            _menuItemRepository.DeleteAsync(1).Returns(true);

            // Act
            var result = await _menuItemService.DeleteMenuItemAsync(1);

            // Assert
            Assert.True(result);
            await _menuItemRepository.Received(1).DeleteAsync(1);
        }

        [Fact]
        public async Task DeleteMenuItemAsync_ShouldReturnFalse_WhenNotExists()
        {
            // Arrange
            _menuItemRepository.DeleteAsync(1).Returns(false);

            // Act
            var result = await _menuItemService.DeleteMenuItemAsync(1);

            // Assert
            Assert.False(result);
            await _menuItemRepository.Received(1).DeleteAsync(1);
        }
    }
} 