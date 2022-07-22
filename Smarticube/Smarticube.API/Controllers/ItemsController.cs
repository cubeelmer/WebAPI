using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Smarticube.API.Data;
using Smarticube.API.Models;

namespace Smarticube.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]    
    public class ItemsController : Controller
    {
        private readonly ProductsDbContext productsDbContext;
        public ItemsController(ProductsDbContext productsDbContext)
        {
            this.productsDbContext = productsDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllItems()
        {
            var items = await productsDbContext.Items.ToListAsync();
            return Ok(items);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetItem")]
        public async Task<IActionResult> GetItem([FromRoute] Guid id)
        {
            var item = await productsDbContext.Items.FirstOrDefaultAsync(x => x.Id == id);
            if (item != null)
            {
                return Ok(item);

            }
            return NotFound("Item not found");
        }
        
        [HttpGet]
        public async Task<IActionResult> GetItemCategories()
        {            
            var itemsCategories = await productsDbContext.Items.Select(o => o.Category).Distinct().ToListAsync();
            return Ok(itemsCategories);
        }


        [HttpGet]
        [Route("{cateId}")]
        public async Task<IActionResult> GetItemByCategory([FromRoute] string cateId)
        {
            var item = await productsDbContext.Items.Where(o => o.Category.Equals(cateId)).ToListAsync();
            return Ok(item);
        }

        

        [HttpPost]
        public async Task<IActionResult> AddItem([FromBody] Item item)
        {
            item.Id = Guid.NewGuid();
            item.CreatedOn = DateTime.Now;
            await productsDbContext.Items.AddAsync(item);
            await productsDbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetItem), new { id = item.Id }, item); //[ActionName("GetItem")]
        }


        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateItem([FromRoute] Guid id, [FromBody] Item item)
        {
            var existingItem = await productsDbContext.Items.FirstOrDefaultAsync(x => x.Id == id);
            if (existingItem != null)
            {
                existingItem.LongDesc = item.LongDesc;
                existingItem.ShortDesc = item.ShortDesc;
                existingItem.Unit = item.Unit;
                existingItem.Category = item.Category;
                existingItem.IsActive = item.IsActive;

                await productsDbContext.SaveChangesAsync();
                return Ok(existingItem);
            }
            return NotFound("Item not found");
        }


        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteItem([FromRoute] Guid id)
        {
            var existingItem = await productsDbContext.Items.FirstOrDefaultAsync(x => x.Id == id);
            if (existingItem != null)
            {
                //existingItem.IsActive = false;
                productsDbContext.Remove(existingItem);
                await productsDbContext.SaveChangesAsync();
                return Ok(existingItem);
            }
            return NotFound("Item not found");
        }
    }
}
