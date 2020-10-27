using System.Threading.Tasks;

namespace Fib.Api.Application.Services
{
    public class FibonacciCalculator
    {
        public ValueTask<long> Calculate(int n)
        {
            var result = Shared.Calculator.Calculate(n);
            return new ValueTask<long>(result);
        }
    }
}