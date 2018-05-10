using System;
using System.IO;
using System.Threading;

namespace smart.framework.Utils.SystemUtils
{
    public class FileUtil
    {
        public static double DelaultFileAccessTimeout = 10;
        public static double DelaultFileAccessPeriod = 0.5;
        public static void DeleteFileWithWait(string filePath)
        {
            for (var time = 0.0; time < DelaultFileAccessTimeout; time += DelaultFileAccessPeriod)
            {
                try
                {
                    File.Delete(filePath);
                }
                catch (IOException)
                {
                    Thread.Sleep(TimeSpan.FromSeconds(DelaultFileAccessPeriod));
                }
            }
        }
    }
}
