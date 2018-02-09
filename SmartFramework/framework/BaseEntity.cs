using log4net;
using log4net.Config;

namespace demo.framework
{
    public class BaseEntity
    {
        public static Logger Log;
        protected BaseEntity()
        {
            XmlConfigurator.Configure();
            Log = new Logger(LogManager.GetLogger(typeof(BaseEntity)));
        }
    }
}
