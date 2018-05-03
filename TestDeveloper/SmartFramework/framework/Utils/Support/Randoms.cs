using System;

namespace smart.framework.Utils.Support
{
    public class Randoms
    {
        private static readonly Random Random = new Random();

        /**
         * Random of "A-Z, a-z, []{}\_^'~|" symbols
         */
        public static string GetRandomString(int size)
        {
            var resultString = "";
            var randomSize = Random.Next(1, size);
            for (var i = 0; i < randomSize; i++)
            {
                var letter = (char) Random.Next(65, 126);
                resultString += letter;
            }
            return resultString;
        }
    }
}
