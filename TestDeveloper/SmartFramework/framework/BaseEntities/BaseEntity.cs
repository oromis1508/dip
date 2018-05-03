using log4net;
using log4net.Config;
using smart.framework.Utils.TestUtils;

namespace smart.framework.BaseEntities
{
    public class BaseEntity
    {
        protected internal static Logger Log;
        protected BaseEntity()
        {
            XmlConfigurator.Configure();
            Log = new Logger(LogManager.GetLogger("LOGGER"));
        }
    }
}
