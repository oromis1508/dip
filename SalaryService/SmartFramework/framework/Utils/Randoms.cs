using System;

namespace demo.framework.Utils
{
    public class Randoms
    {
        private static readonly Random Random = new Random();

        /**
         * Random of "A-Z, a-z" symbols with random case
         */
        public static string String(int maxSize, int minSize = 1)
        {
            var resultString = "";
            var randomSize = Random.Next(minSize, maxSize);
            for (var i = 0; i < randomSize; i++)
            {
                var randomCase = Random.Next(1);
                var letter = randomCase == 0 ? (char)Random.Next(97, 122) : (char) Random.Next(65, 90);
                resultString += letter;
            }
            return resultString;
        }

        public static int Number(int maxDigits, int minDigits = 1)
        {
            var resultNumber = "";
            var randomSize = Random.Next(minDigits, maxDigits);
            for (var i = 0; i < randomSize; i++)
            {
                var digit = (char)Random.Next(48, 57);
                resultNumber += digit;
            }
            return int.Parse(resultNumber);
        }

        public static string StringWithDigits(int maxSize, int minSize = 1)
        {
            var resultString = "";
            var randomSize = Random.Next(minSize, maxSize);
            for (var i = 0; i < randomSize; i++)
            {
                var randomCase = Random.Next(2);
                char letter;
                switch (randomCase)
                {
                    case 0:
                        letter = (char) Random.Next(97, 122);
                        break;
                    case 1:
                        letter = (char) Random.Next(65, 90);
                        break;
                    default:
                        letter = (char) Random.Next(48, 57);
                        break;
                }
                resultString += letter;
            }
            return resultString;
        }
    }
}
