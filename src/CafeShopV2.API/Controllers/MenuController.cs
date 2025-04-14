using CafeShopV2.Core.Entities;
using CafeShopV2.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CafeShopV2.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MenuController : ControllerBase
{
    private readonly IMenuService _menuService;

    public MenuController(IMenuService menuService)
    {
        _menuService = menuService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Menu>>> GetAll()
    {
        var menus = await _menuService.GetAllMenusAsync();
        return Ok(menus);
    }

    [HttpGet("{id}")]
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
    public async Task<ActionResult<IEnumerable<Menu>>> GetByCategory(string category)
    {
        var menus = await _menuService.GetMenusByCategoryAsync(category);
        return Ok(menus);
    }

    [HttpPost]
    public async Task<ActionResult<Menu>> Create(Menu menu)
    {
        var createdMenu = await _menuService.CreateMenuAsync(menu);
        return CreatedAtAction(nameof(GetById), new { id = createdMenu.Id }, createdMenu);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Menu menu)
    {
        if (id != menu.Id)
        {
            return BadRequest();
        }

        await _menuService.UpdateMenuAsync(menu);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _menuService.DeleteMenuAsync(id);
        return NoContent();
    }
} 