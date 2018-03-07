using System;
using System.Collections.Generic;

namespace RxFramework.Utils
{
    public static class ExceptionUtils
    {
        public static Exception[] Catch(params Action[] methods)
        {
            var exceptions = new List<Exception>();
            foreach (var method in methods)
            {
                try
                {
                    method();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    exceptions.Add(ex);
                }
            }
            return exceptions.ToArray();
        }
    }
}
