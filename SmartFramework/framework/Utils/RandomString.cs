using System;

namespace demo.framework.Utils
{
    public class RandomString
    {
        private static readonly Random Random = new Random();

        public static string Generate(int size)
        {
            var resultString = "";
            for (var i = 0; i < size; i++)
            {
                var letter = (char) Random.Next(255);
                resultString += letter;
            }

            return resultString;
        }
    }
}
