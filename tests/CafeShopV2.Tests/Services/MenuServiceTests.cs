using CafeShopV2.Core.Entities;
using CafeShopV2.Core.Interfaces;
using CafeShopV2.Service.Services;
using NSubstitute;
using Xunit;

namespace CafeShopV2.Tests.Services;

public class MenuServiceTests
{
    private readonly IMenuRepository _menuRepository;
    private readonly IMenuService _menuService;

    public MenuServiceTests()
    {
        _menuRepository = Substitute.For<IMenuRepository>();
        _menuService = new MenuService(_menuRepository);
    }

    [Fact]
    public async Task GetAllMenusAsync_ShouldReturnAllMenus()
    {
        // Arrange
        var expectedMenus = new List<Menu>
        {
            new Menu { Id = 1, Name = "Coffee", Price = 2.99m },
            new Menu { Id = 2, Name = "Tea", Price = 1.99m }
        };
        _menuRepository.GetAllAsync().Returns(expectedMenus);

        // Act
        var result = await _menuService.GetAllMenusAsync();

        // Assert
        Assert.Equal(expectedMenus, result);
        await _menuRepository.Received(1).GetAllAsync();
    }

    [Fact]
    public async Task GetMenuByIdAsync_WhenMenuExists_ShouldReturnMenu()
    {
        // Arrange
        var expectedMenu = new Menu { Id = 1, Name = "Coffee", Price = 2.99m };
        _menuRepository.GetByIdAsync(1).Returns(expectedMenu);

        // Act
        var result = await _menuService.GetMenuByIdAsync(1);

        // Assert
        Assert.Equal(expectedMenu, result);
        await _menuRepository.Received(1).GetByIdAsync(1);
    }

    [Fact]
    public async Task GetMenuByIdAsync_WhenMenuDoesNotExist_ShouldReturnNull()
    {
        // Arrange
        _menuRepository.GetByIdAsync(1).Returns((Menu?)null);

        // Act
        var result = await _menuService.GetMenuByIdAsync(1);

        // Assert
        Assert.Null(result);
        await _menuRepository.Received(1).GetByIdAsync(1);
    }

    [Fact]
    public async Task CreateMenuAsync_ShouldReturnCreatedMenu()
    {
        // Arrange
        var menuToCreate = new Menu { Name = "Coffee", Price = 2.99m };
        var expectedMenu = new Menu { Id = 1, Name = "Coffee", Price = 2.99m };
        _menuRepository.AddAsync(menuToCreate).Returns(expectedMenu);

        // Act
        var result = await _menuService.CreateMenuAsync(menuToCreate);

        // Assert
        Assert.Equal(expectedMenu, result);
        await _menuRepository.Received(1).AddAsync(menuToCreate);
    }

    [Fact]
    public async Task UpdateMenuAsync_ShouldReturnUpdatedMenu()
    {
        // Arrange
        var menuToUpdate = new Menu { Id = 1, Name = "Updated Coffee", Price = 3.99m };
        _menuRepository.UpdateAsync(menuToUpdate).Returns(menuToUpdate);

        // Act
        var result = await _menuService.UpdateMenuAsync(menuToUpdate);

        // Assert
        Assert.Equal(menuToUpdate, result);
        await _menuRepository.Received(1).UpdateAsync(menuToUpdate);
    }

    [Fact]
    public async Task DeleteMenuAsync_ShouldCallRepositoryDelete()
    {
        // Arrange
        var menuId = 1;

        // Act
        await _menuService.DeleteMenuAsync(menuId);

        // Assert
        await _menuRepository.Received(1).DeleteAsync(menuId);
    }

    [Fact]
    public async Task GetMenusByCategoryAsync_ShouldReturnMenusInCategory()
    {
        // Arrange
        var category = "Drinks";
        var expectedMenus = new List<Menu>
        {
            new Menu { Id = 1, Name = "Coffee", Category = "Drinks" },
            new Menu { Id = 2, Name = "Tea", Category = "Drinks" }
        };
        _menuRepository.GetByCategoryAsync(category).Returns(expectedMenus);

        // Act
        var result = await _menuService.GetMenusByCategoryAsync(category);

        // Assert
        Assert.Equal(expectedMenus, result);
        await _menuRepository.Received(1).GetByCategoryAsync(category);
    }
} 