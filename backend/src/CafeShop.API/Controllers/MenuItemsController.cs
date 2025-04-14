using CafeShop.Application.Interfaces;
using CafeShop.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CafeShop.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MenuItemsController : ControllerBase
    {
        private readonly IMenuItemService _menuItemService;

        public MenuItemsController(IMenuItemService menuItemService)
        {
            _menuItemService = menuItemService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MenuItem>>> GetAll()
        {
            var menuItems = await _menuItemService.GetAllMenuItemsAsync();
            return Ok(menuItems);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MenuItem>> GetById(int id)
        {
            var menuItem = await _menuItemService.GetMenuItemByIdAsync(id);
            if (menuItem == null)
                return NotFound();

            return Ok(menuItem);
        }

        [HttpGet("category/{category}")]
        public async Task<ActionResult<IEnumerable<MenuItem>>> GetByCategory(MenuCategory category)
        {
            var menuItems = await _menuItemService.GetMenuItemsByCategoryAsync(category);
            return Ok(menuItems);
        }

        [HttpPost]
        public async Task<ActionResult<MenuItem>> Create(MenuItem menuItem)
        {
            var createdMenuItem = await _menuItemService.CreateMenuItemAsync(menuItem);
            return CreatedAtAction(nameof(GetById), new { id = createdMenuItem.Id }, createdMenuItem);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<MenuItem>> Update(int id, MenuItem menuItem)
        {
            if (id != menuItem.Id)
                return BadRequest();

            var updatedMenuItem = await _menuItemService.UpdateMenuItemAsync(menuItem);
            if (updatedMenuItem == null)
                return NotFound();

            return Ok(updatedMenuItem);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _menuItemService.DeleteMenuItemAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }
    }
} 