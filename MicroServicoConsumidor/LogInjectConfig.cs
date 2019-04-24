using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace MicroServicoConsumidor
{
    public class LogInjectConfig
    {
        private readonly ILogger _logger;

        public LogInjectConfig(ILogger<LogInjectConfig> logger)
        {
            _logger = logger;
        }

        public void Start()
        {
            _logger.LogInformation("==START MICRO SERVIÇO CONSUMIDOR==");
        }
    }
}
