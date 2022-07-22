using Microsoft.EntityFrameworkCore;
using Smarticube.API.DemoService.Models;

namespace Smarticube.API.DemoService.Data
{
    public class JobDemoServiceRepository : IJobDemoServiceRepository
    {
              
        private bool disposedValue;
        public JobDemoServiceRepository()
        {

        }

        public async Task<IEnumerable<int>> GetPrimeNumbers(int fromNum, int toNum)
        {

            List<int> result = new List<int>();
            if (fromNum <= toNum && fromNum >= 1 && toNum >= 1 && toNum <= 100000)
            {
                // Generate desired number range
                result = Enumerable.Range(fromNum, toNum - fromNum + 1).ToList();
                // Eliminate unexpected element
                // Rule1: Only number 2 is even and also prime number
                // Rule2: All numbers end with 0, 2, 4, 6, 8 are not prime number
                result = result.Where(x => (x.Equals(2))
                || ((x > 2 && x.ToString().EndsWith("1"))
                || (x > 2 && x.ToString().EndsWith("3"))
                || (x > 2 && x.ToString().EndsWith("5"))
                || (x > 2 && x.ToString().EndsWith("7"))
                || (x > 2 && x.ToString().EndsWith("9"))
                )).ToList();
            }

            result = result.Where(x => IsPrime(x).Equals(true)).ToList();

            return result;
        }

        private static bool IsPrime(int inputNum)
        {
            if (inputNum.Equals(0)) return false;
            if (inputNum.Equals(1)) return false;
            if (inputNum.Equals(2)) return true;
            if (inputNum > 2
                && (inputNum.ToString().EndsWith("0")
                || inputNum.ToString().EndsWith("2")
                || inputNum.ToString().EndsWith("4")
                || inputNum.ToString().EndsWith("6")
                || inputNum.ToString().EndsWith("8")
                )) return false;

            if (inputNum > 10 && inputNum.ToString().EndsWith("5")) return false;


            if (inputNum > 2 &&
                (inputNum.ToString().EndsWith("1")
                || inputNum.ToString().EndsWith("3")
                || inputNum.ToString().EndsWith("5")
                || inputNum.ToString().EndsWith("7")
                || inputNum.ToString().EndsWith("9")
                ))
            {
                int round = inputNum / 2;
                bool tempResult = true;

                for (int i = 2; i <= round; i++)
                {
                    if ((inputNum % i).Equals(0))
                    {
                        tempResult = false;
                        break;
                    }
                }
                return tempResult;
            }



            return false;

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
