using System;

namespace YaUnior
{
    class Program
    {
        static void Main(string[] args)
        {
            string symbolString = "";

            Console.WriteLine("Вывести имя в прямоугольник из символа, который введет сам пользователь.");

            Console.Write("Выберите символ: ");

            char symbolForName = Convert.ToChar(Console.Read());

            Console.ReadLine();

            Console.Write("Введите Ваше Имя: ");
            string name = Console.ReadLine();

            string lineMiddle = $"{symbolForName} {name} {symbolForName}";

            for (int i = 0; i < lineMiddle.Length; i++)
            {
                symbolString += symbolForName;
            }

            Console.WriteLine($"\n{symbolString}\n{lineMiddle}\n{symbolString}");

            Console.ReadKey();
        }
    }
}