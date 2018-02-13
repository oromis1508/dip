using log4net;

namespace demo.framework
{
    public class Logger
    {
        private readonly ILog _logger;
        public Logger(ILog logger)
        {
            _logger = logger;
        }

        public void Step(int number, string message = "")
        {
            _logger.Info($"== Step {number} ==");
            _logger.Info($"== {message} ==");
        }

        public void Info(string info)
        {
            _logger.Info(info);
        }

        public void Fatal(string fatal)
        {
            _logger.Fatal(fatal);
        }
    }
}
