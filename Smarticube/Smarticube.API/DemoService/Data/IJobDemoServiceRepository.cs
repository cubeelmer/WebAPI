using Smarticube.API.DemoService.Models;

namespace Smarticube.API.DemoService.Data
{
    public interface IJobDemoServiceRepository : IDisposable
    {
        Task<IEnumerable<int>> GetPrimeNumbers(int fromNum, int toNum);
    }
}
