using Smarticube.API.DemoService.Models;

namespace Smarticube.API.DemoService.Data
{
    public interface IProductRepository:IDisposable
    {
        Task<IEnumerable<Product>> GetProducts();
        Task<Product> GetProduct(Guid id);
        Product AddProduct(Product product);

        Product UpdateProduct(Guid id, Product product);
        Product DeleteProduct(Guid id);

    }
}
