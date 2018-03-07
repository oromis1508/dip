using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace RxFramework.Utils
{
    public static class FileUtil
    {
        private const int FileTimeWait = 2000;
        private const int FileWaitPeriod = 250;

        public static bool IsFileStatus(string filePath, WatcherChangeTypes status)
        {
            for (var i = 0; i < FileTimeWait/FileWaitPeriod; i++)
            {
                var result = status == WatcherChangeTypes.Created ? File.Exists(filePath) : !File.Exists(filePath);
                if (result)
                {
                    return true;
                }
                Thread.Sleep(FileWaitPeriod);
            }
            return false;
        }
    }
}
