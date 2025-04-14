using CafeShopV2.Core.Entities;
using CafeShopV2.Core.Interfaces;
using CafeShopV2.Service.Services;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Xunit;

namespace CafeShopV2.Tests.Services;

public sealed class MenuServiceTests
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
            new() { Id = 1, Name = "Menu 1", Category = "Category 1", Price = 10.99m },
            new() { Id = 2, Name = "Menu 2", Category = "Category 2", Price = 15.99m }
        };
        _menuRepository.GetAllAsync().Returns(expectedMenus);

        // Act
        var result = await _menuService.GetAllMenusAsync();

        // Assert
        Assert.Equal(expectedMenus, result);
        await _menuRepository.Received(1).GetAllAsync();
    }

    [Fact]
    public async Task GetMenuByIdAsync_WithValidId_ShouldReturnMenu()
    {
        // Arrange
        var expectedMenu = new Menu { Id = 1, Name = "Menu 1", Category = "Category 1", Price = 10.99m };
        _menuRepository.GetByIdAsync(1).Returns(expectedMenu);

        // Act
        var result = await _menuService.GetMenuByIdAsync(1);

        // Assert
        Assert.Equal(expectedMenu, result);
        await _menuRepository.Received(1).GetByIdAsync(1);
    }

    [Fact]
    public async Task GetMenuByIdAsync_WithInvalidId_ShouldReturnNull()
    {
        // Arrange
        _menuRepository.GetByIdAsync(999).ReturnsNull();

        // Act
        var result = await _menuService.GetMenuByIdAsync(999);

        // Assert
        Assert.Null(result);
        await _menuRepository.Received(1).GetByIdAsync(999);
    }

    [Fact]
    public async Task GetMenusByCategoryAsync_WithValidCategory_ShouldReturnMenus()
    {
        // Arrange
        var category = "Category 1";
        var expectedMenus = new List<Menu>
        {
            new() { Id = 1, Name = "Menu 1", Category = category, Price = 10.99m },
            new() { Id = 2, Name = "Menu 2", Category = category, Price = 15.99m }
        };
        _menuRepository.GetByCategoryAsync(category).Returns(expectedMenus);

        // Act
        var result = await _menuService.GetMenusByCategoryAsync(category);

        // Assert
        Assert.Equal(expectedMenus, result);
        await _menuRepository.Received(1).GetByCategoryAsync(category);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public async Task GetMenusByCategoryAsync_WithInvalidCategory_ShouldThrowArgumentException(string category)
    {
        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => _menuService.GetMenusByCategoryAsync(category));
        await _menuRepository.DidNotReceive().GetByCategoryAsync(Arg.Any<string>());
    }

    [Fact]
    public async Task CreateMenuAsync_WithValidMenu_ShouldCreateAndReturnMenu()
    {
        // Arrange
        var menu = new Menu { Name = "Menu 1", Category = "Category 1", Price = 10.99m };
        var expectedMenu = new Menu { Id = 1, Name = "Menu 1", Category = "Category 1", Price = 10.99m };
        _menuRepository.CreateAsync(menu).Returns(expectedMenu);

        // Act
        var result = await _menuService.CreateMenuAsync(menu);

        // Assert
        Assert.Equal(expectedMenu, result);
        await _menuRepository.Received(1).CreateAsync(menu);
    }

    [Fact]
    public async Task CreateMenuAsync_WithNullMenu_ShouldThrowArgumentNullException()
    {
        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => _menuService.CreateMenuAsync(null!));
        await _menuRepository.DidNotReceive().CreateAsync(Arg.Any<Menu>());
    }

    [Theory]
    [InlineData(null, "Category", 10.99)]
    [InlineData("", "Category", 10.99)]
    [InlineData(" ", "Category", 10.99)]
    [InlineData("Name", null, 10.99)]
    [InlineData("Name", "", 10.99)]
    [InlineData("Name", " ", 10.99)]
    public async Task CreateMenuAsync_WithInvalidData_ShouldThrowArgumentException(string name, string category, decimal price)
    {
        // Arrange
        var menu = new Menu { Name = name, Category = category, Price = price };

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => _menuService.CreateMenuAsync(menu));
        await _menuRepository.DidNotReceive().CreateAsync(Arg.Any<Menu>());
    }

    [Fact]
    public async Task UpdateMenuAsync_WithValidMenu_ShouldUpdateAndReturnMenu()
    {
        // Arrange
        var menu = new Menu { Id = 1, Name = "Updated Menu", Category = "Category 1", Price = 12.99m };
        _menuRepository.UpdateAsync(menu).Returns(menu);

        // Act
        var result = await _menuService.UpdateMenuAsync(menu);

        // Assert
        Assert.Equal(menu, result);
        await _menuRepository.Received(1).UpdateAsync(menu);
    }

    [Fact]
    public async Task DeleteMenuAsync_WithValidId_ShouldReturnTrue()
    {
        // Arrange
        _menuRepository.DeleteAsync(1).Returns(true);

        // Act
        var result = await _menuService.DeleteMenuAsync(1);

        // Assert
        Assert.True(result);
        await _menuRepository.Received(1).DeleteAsync(1);
    }

    [Fact]
    public async Task DeleteMenuAsync_WithInvalidId_ShouldReturnFalse()
    {
        // Arrange
        _menuRepository.DeleteAsync(999).Returns(false);

        // Act
        var result = await _menuService.DeleteMenuAsync(999);

        // Assert
        Assert.False(result);
        await _menuRepository.Received(1).DeleteAsync(999);
    }
} 