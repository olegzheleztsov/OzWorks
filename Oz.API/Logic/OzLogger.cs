using Microsoft.Extensions.Logging;
using Oz.API.Logic.Interfaces;

namespace Oz.API.Logic
{
    public class OzLogger<T> : IOzLogger<T>
    {
        private readonly ILoggerFactory _loggerFactory;
        private ILogger<T> _logger;

        public OzLogger(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
        }

        private void CreateLoggerLazily()
        {
            _logger ??= _loggerFactory.CreateLogger<T>();
        }

        public void LogInformation(string message)
        {
            CreateLoggerLazily();
            _logger.LogInformation(message);
        }
    }
}