using Microsoft.EntityFrameworkCore;
using Smarticube.API.DemoService.Models;

namespace Smarticube.API.DemoService.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductsDbContext _context;                
        private bool disposedValue;
        public ProductRepository(ProductsDbContext productContext)
        {
            this._context = productContext;
        }
        public async Task<IEnumerable<Product>> GetProducts()
        {
            /*
            return await _context.Products
                .Select(b => new Product
                {
                    Id = b.Id,
                    LongDesc = b.LongDesc,
                    ShortDesc = b.ShortDesc,
                    WeightValue = b.WeightValue,
                    WeightUnit = b.WeightUnit,
                    Category = b.Category,
                    CreatedBy = b.CreatedBy,
                    CreatedOn = b.CreatedOn,
                    IsActive = b.IsActive,
                    ProductItems = (from p in this._context.ProductItems
                                    where p.ProductId == b.Id
                                    select new ProductItem
                                    {
                                        ProductId = p.ProductId,
                                        ItemId = p.ItemId,
                                        Qty = p.Qty,
                                        ItemInvolved = (from m in this._context.Items where m.Id == p.ItemId select m).FirstOrDefault()
                                    }).ToList()
                }).ToListAsync();
            */            
            return await _context.Products.Include(pi => pi.ProductItems).ThenInclude(i => i.ItemInvolved).ToListAsync();

        }
        public async Task<Product> GetProduct(Guid id)
        {
            /*
            var product = this._context.Products.Where(o => o.Id == id)
                .Select(b => new Product
                {
                    Id = b.Id,
                    LongDesc = b.LongDesc,
                    ShortDesc = b.ShortDesc,
                    WeightValue = b.WeightValue,
                    WeightUnit = b.WeightUnit,
                    Category = b.Category,
                    CreatedBy = b.CreatedBy,
                    CreatedOn = b.CreatedOn,
                    IsActive = b.IsActive,
                    ProductItems = (from p in this._context.ProductItems
                                    where p.ProductId == b.Id
                                    select new ProductItem
                                    {
                                        ProductId = p.ProductId,
                                        ItemId = p.ItemId,
                                        Qty = p.Qty,
                                        ItemInvolved = (from m in this._context.Items where m.Id == p.ItemId select m).FirstOrDefault()
                                    }).ToList()
                }).FirstOrDefault();
            */
            return await _context.Products.Include(pi => pi.ProductItems).ThenInclude(i => i.ItemInvolved).FirstOrDefaultAsync();
        }
        public Product AddProduct(Product product)
        {
            try
            {
                product.Id = Guid.NewGuid();
                product.CreatedOn = DateTime.Now;

                foreach (ProductItem productItem in product.ProductItems)
                {
                    productItem.ProductId = product.Id;
                    productItem.ItemInvolved = null;
                }

                this._context.Products.Add(product);
                this._context.ProductItems.AddRange(product.ProductItems);
                this._context.SaveChanges();                
                return product;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public Product UpdateProduct(Guid id, Product product)
        {           
            
            if (id == product.Id)
            {
                Product existingProduct = this._context.Products.Find(product.Id);
                    
                if (existingProduct != null)
                {
                    List<ProductItem> existingProductItems = this._context.ProductItems.Where(o => o.ProductId.Equals(id)).ToList();


                    existingProduct.LongDesc = product.LongDesc;
                    existingProduct.ShortDesc = product.ShortDesc;
                    existingProduct.WeightValue = product.WeightValue;
                    existingProduct.WeightUnit = product.WeightUnit;
                    existingProduct.Category = product.Category;
                    existingProduct.IsActive = product.IsActive;
                        
                    foreach (ProductItem newProductItem in product.ProductItems)
                    {
                        newProductItem.ProductId = existingProduct.Id;
                        newProductItem.ItemInvolved = null;
                    }

                    this._context.ProductItems.RemoveRange(existingProductItems);

                    this._context.ProductItems.AddRange(product.ProductItems);
                    this._context.SaveChanges();
                    return product;
                }
            }
           

            return null;


        }
        public Product DeleteProduct(Guid id)
        {
            Product existingProduct = this._context.Products.Find(id);

            if (existingProduct != null)
            {
                this._context.Remove(existingProduct);
                this._context.SaveChangesAsync();
                return existingProduct;
            }

            return null;
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }
        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~ProductRepository()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
