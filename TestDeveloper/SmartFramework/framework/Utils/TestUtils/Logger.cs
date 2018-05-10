using log4net;

namespace smart.framework.Utils.TestUtils
{
    public class Logger
    {
        private readonly ILog _logger;
        private int _number;

        public Logger(ILog logger)
        {
            _logger = logger;
        }

        public void Step(string message = "")
        {
            _logger.Info($"== Step {++_number} ==");
            _logger.Info($"== {message} ==");
        }

        public void Info(string info) => _logger.Info(info);

        public void Fatal(string fatal) => _logger.Fatal(fatal);
    }
}
