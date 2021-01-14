using System;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SimplePages.Pages
{
    public class LogSample : PageModel
    {
        private readonly ILogger<LogSample> _log;
        private readonly ILogger _secondLogger;
        

        public LogSample(ILogger<LogSample> log, ILoggerFactory factory)
        {
            _log = log;
            _secondLogger = factory.CreateLogger("RecipeApp.RecipeService");
        }
        
        public void OnGet()
        {
            var random = new Random();
            _log.LogInformation("Loaded {RecipeCount} recipes", random.Next(10000));

            if (random.Next() % 2 == 0)
            {
                _log.LogWarning("Could not find recipe with id {RecipeId}", random.Next(100));
            }
            _secondLogger.LogInformation("Additionally loaded: {AdditionalCount} recipes", random.Next(100));
            _log.LogInformation("User {UserId} loaded recipe {RecipeId}", 123, 456);
        }
    }
}