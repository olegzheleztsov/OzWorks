using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Oz.API.Config;
using Oz.API.Logic;

namespace Oz.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JugProblemController : Controller
    {
        private readonly IOptions<JugConfig> _jugConfig;
        private readonly ILoggerFactory _loggerFactory;

        public JugProblemController(ILoggerFactory loggerFactory, IOptions<JugConfig> jugConfig)
        {
            _loggerFactory = loggerFactory;
            _jugConfig = jugConfig;
        }

        [HttpGet]
        public async Task<IActionResult> GetJugProblem([FromQuery] int firstJugCapacity,
            [FromQuery] int secondJugCapacity,
            [FromQuery] int targetAmount)
        {
            var solverInputs = new JugProblemInputs(firstJugCapacity, secondJugCapacity, targetAmount);

            try
            {
                var solver = new JugProblemSolver(solverInputs, new OzLogger<JugProblemSolver>(_loggerFactory), _jugConfig.Value);
                return await Task.Run(async () =>
                {
                    var (success, steps) = solver.RunJugProcess();
                    return await Task.FromResult(new OkObjectResult(new
                    {
                        Success = success,
                        Steps = steps
                    }));
                });
            }
            catch (ArgumentException exception)
            {
                return await Task.FromResult(new OkObjectResult(new
                {
                    Success = false,
                    Steps = new List<JugStep>()
                }));
            }
        }
    }
}