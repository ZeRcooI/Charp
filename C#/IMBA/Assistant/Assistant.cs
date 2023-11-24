using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assistant
{
    internal class Assistant
    {
        static void Main(string[] args)
        {

        }
    }

    public class Utils
    {
        private static Random s_random = new Random();

        public static int GenerateRandomValue(int minNumber, int maxNumber)
        {
            return s_random.Next(minNumber, maxNumber);
        }

        public static int GenerateRandomValue(int maxNumber)
        {
            return s_random.Next(maxNumber);
        }

        public static bool GenerateRandomBollean()
        {
            int maxValue = 2;

            return s_random.Next(maxValue) == 0;
        }

        public static int GetNumber()
        {
            int result;

            while (int.TryParse(Console.ReadLine(), out result) == false)
            {
                Console.Write("Ошибка! Введите число: ");
            }

            return result;
        }

        public static bool IsSuccess(int chance)
        {
            int maxValue = 100;

            return GenerateRandomValue(maxValue) <= chance;
        }
    }
}
