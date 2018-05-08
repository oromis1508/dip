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

        public static object GetEnumRandomValue(Type type)
        {
            var values = Enum.GetValues(type);
            return values.GetValue(Random.Next(values.Length));
        }

        public static string GetRandomDate(string dateFormat)
        {
            var start = new DateTime(1995, 1, 1);
            var range = (DateTime.Today - start).Seconds;
            return start.AddSeconds(Random.Next(range)).ToString(dateFormat);
        }
    }
}
