using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeAPI.Helpers
{
    public class LogHelper
    {
        private ILogger _logger;
        public LogHelper()
        {
            _logger = LogManager.GetLogger("Information");
        }

        public void Debug(string message)
        {
            _logger.Debug($"{message}");
        }

        public void Info(string message)
        {
            _logger.Info($"{message}");
        }

        public void Warn(string message)
        {
            _logger.Warn($"{message}");
        }

        public void Error(string message)
        {
            _logger.Error($"{message}");
        }
    }
}
