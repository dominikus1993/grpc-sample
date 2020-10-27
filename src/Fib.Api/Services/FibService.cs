using System.Threading.Tasks;
using Fib.Api.Application.Services;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace Fib.Api.Services
{
    public class FibCalculatorService : FibCalculator.FibCalculatorBase
    {
        private readonly ILogger<FibCalculatorService> _logger;
        private readonly FibonacciCalculator _calculator;

        public FibCalculatorService(ILogger<FibCalculatorService> logger, FibonacciCalculator calculator)
        {
            _logger = logger;
            _calculator = calculator;
        }

        public override async Task<FibReply> Calculate(CalculateRequest request, ServerCallContext context)
        {
            _logger.LogInformation("Start caclulation");
            var result = await _calculator.Calculate(request.Num);
            _logger.LogInformation("caclulated {Num}", result);
            return new FibReply() { Result = result };
        }
    }
}