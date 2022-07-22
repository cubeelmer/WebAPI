using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Smarticube.API.DemoService.Data;
using Smarticube.API.DemoService.Models;
namespace Smarticube.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class DemoServiceController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IJobDemoServiceRepository _jobDemoServiceRepository;
        private readonly ProductsDbContext productsDbContext;
        public DemoServiceController(IProductRepository productRepository, IJobDemoServiceRepository jobDemoServiceRepository, ProductsDbContext productsDbContext)
        {
            _productRepository = productRepository;
            _jobDemoServiceRepository = jobDemoServiceRepository;
            this.productsDbContext = productsDbContext;
        }
        [HttpGet]
        [ProducesResponseType(200, Type= typeof(IEnumerable<Product>))]
        public async Task<IActionResult> GetAllProducts()
        {
            IEnumerable<Product> products = await _productRepository.GetProducts();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(products);
        }        
        
        [HttpGet]
        [Route("{id:guid}")]        
        [ProducesResponseType(200, Type = typeof(Product))]
        public async Task<IActionResult> GetProduct([FromRoute] Guid id)
        {            
            Product product = await _productRepository.GetProduct(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (product != null)
                return Ok(product);
            
            return NotFound("Product not found");
        }
        
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(Product))]
        public IActionResult AddProduct([FromBody] Product product)
        {
            var result = _productRepository.AddProduct(product);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            return Ok(result);           
        }
        
        [HttpPut]
        [Route("{id:guid}")]
        [ProducesResponseType(200, Type = typeof(Product))]
        public IActionResult UpdateProduct([FromRoute] Guid id, [FromBody] Product product)
        {
            var result = _productRepository.UpdateProduct(id, product);

            if(result != null)            
                return Ok(result);

            return StatusCode(500, "Internal server error: Update product is failure.");
        }
       
        [HttpDelete]
        [Route("{id:guid}")]
        [ProducesResponseType(200, Type = typeof(Product))]
        public IActionResult DeleteProduct([FromRoute] Guid id)
        {
            var result = _productRepository.DeleteProduct(id);

            if(result != null)
                return Ok(result);

            return StatusCode(500, "Internal server error: Delete product is failure.");
            
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


        [HttpGet]
        //[ProducesResponseType(200, Type = typeof(IEnumerable<int>))]
        public async Task<IActionResult> GetPrimeNumbers(int fromNum, int toNum)
        {
            return Ok(_jobDemoServiceRepository.GetPrimeNumbers(fromNum, toNum));
        }


    }
}
