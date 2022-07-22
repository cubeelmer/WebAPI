using Smarticube.API.HKJC.Football.Models;

namespace Smarticube.API.HKJC.Football.Data
{
    public interface IHkjcFootballRepository:IDisposable
    {
        Task<IEnumerable<HkjcDataPoolResult>> GetResults();
        Task<bool> ImportHkjcFootballRecordsWsByString(string csvObjs);
        //Task<Product> GetProduct(Guid id);
        //Product AddProduct(Product product);

        //Product UpdateProduct(Guid id, Product product);
        //Product DeleteProduct(Guid id);

    }
}
