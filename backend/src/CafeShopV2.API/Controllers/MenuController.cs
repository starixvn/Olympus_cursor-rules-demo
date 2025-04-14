using CafeShopV2.Core.Entities;
using CafeShopV2.Core.Enums;
using CafeShopV2.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CafeShopV2.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class MenuController : ControllerBase
{
    private readonly IMenuService _menuService;

    public MenuController(IMenuService menuService)
    {
        _menuService = menuService ?? throw new ArgumentNullException(nameof(menuService));
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Menu>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Menu>>> GetAll()
    {
        var menus = await _menuService.GetAllMenusAsync();
        return Ok(menus);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(Menu), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Menu>> GetById(int id)
    {
        var menu = await _menuService.GetMenuByIdAsync(id);
        if (menu == null)
        {
            return NotFound();
        }
        return Ok(menu);
    }

    [HttpGet("category/{category}")]
    [ProducesResponseType(typeof(IEnumerable<Menu>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<Menu>>> GetByCategory(string category)
    {
        try
        {
            if (!Enum.TryParse<MenuCategory>(category, true, out var menuCategory))
            {
                return BadRequest($"Invalid category: {category}. Valid categories are: {string.Join(", ", Enum.GetNames(typeof(MenuCategory)))}");
            }
            
            var menus = await _menuService.GetMenusByCategoryAsync(menuCategory);
            return Ok(menus);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    [ProducesResponseType(typeof(Menu), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Menu>> Create(Menu menu)
    {
        try
        {
            var createdMenu = await _menuService.CreateMenuAsync(menu);
            return CreatedAtAction(nameof(GetById), new { id = createdMenu.Id }, createdMenu);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(Menu), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Menu>> Update(int id, Menu menu)
    {
        if (id != menu.Id)
        {
            return BadRequest("ID mismatch");
        }

        try
        {
            var updatedMenu = await _menuService.UpdateMenuAsync(menu);
            return Ok(updatedMenu);
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _menuService.DeleteMenuAsync(id);
            return NoContent();
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
    }
} 