using Microsoft.EntityFrameworkCore;
using Smarticube.API.HKJC.Models;

namespace Smarticube.API.HKJC.Data
{
    public class HkjcFootballRepository : IHkjcFootballRepository
    {
        private readonly HkjcDbContext _context;                
        private bool disposedValue;
        public HkjcFootballRepository(HkjcDbContext hkjcContext)
        {
            this._context = hkjcContext;
        }
        public async Task<IEnumerable<hkjcDataPool_result>> GetResults()
        {
           
            return await _context.hkjcDataPool_results.Take(100).ToListAsync();

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
