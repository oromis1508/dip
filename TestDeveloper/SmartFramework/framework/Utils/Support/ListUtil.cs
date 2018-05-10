using System.Collections.Generic;

namespace smart.framework.Utils.Support
{
    public class ListUtil
    {
        public static bool IsListSorted(List<string> list, bool increase)
        {
            for (var i=1; i<list.Count; i++)
            {
                if (increase)
                {
                    if (string.Compare(list[i-1], list[i]) > 0)
                    {
                        return false;
                    }
                }
                else
                {
                    if (string.Compare(list[i], list[i-1]) > 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
