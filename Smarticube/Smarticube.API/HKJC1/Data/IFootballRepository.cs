using Smarticube.API.HKJC.Models;

namespace Smarticube.API.HKJC.Data
{
    public interface IHkjcFootballRepository:IDisposable
    {
        Task<IEnumerable<hkjcDataPool_result>> GetResults();
        //Task<Product> GetProduct(Guid id);
        //Product AddProduct(Product product);

        //Product UpdateProduct(Guid id, Product product);
        //Product DeleteProduct(Guid id);

    }
}
