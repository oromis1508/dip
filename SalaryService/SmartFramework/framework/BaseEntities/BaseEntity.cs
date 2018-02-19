using demo.framework.Utils;
using log4net;
using log4net.Config;

namespace demo.framework.BaseEntities
{
    public class BaseEntity
    {
        public static Logger Log;
        protected BaseEntity()
        {
            XmlConfigurator.Configure();
            Log = new Logger(LogManager.GetLogger("LOGGER"));
        }
    }
}
