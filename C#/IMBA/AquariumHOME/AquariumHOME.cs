using System;
using System.Collections.Generic;

namespace AquariumHOME
{
    internal class AquariumHOME
    {
        static void Main(string[] args)
        {
            Aquarium aquarium = new Aquarium();
            aquarium.Work();
        }
    }

    public class Aquarium
    {
        private int _day = 1;
        private int _maxCountFish = 10;

        private List<Fish> _fishes = new List<Fish>();

        public void Work()
        {
            const string CommandAddFish = "1";
            const string CommandRemoveFish = "2";
            const string CommandExit = "3";

            bool isProgramOperation = true;

            while (isProgramOperation)
            {
                Console.WriteLine($"\tДень {_day}ый");
                Console.WriteLine($"{CommandAddFish} - Добавить рыбку в аквариум.");
                Console.WriteLine($"{CommandRemoveFish} - Вынуть рыбку из аквариума.");
                Console.WriteLine($"{CommandExit} - Отойти от аквариума.\n");

                ShowAllFishes();

                Console.Write("Введите пункт меню: ");
                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case CommandAddFish:
                        AddFish();
                        break;

                    case CommandRemoveFish:
                        RemoveFish();
                        break;

                    case CommandExit:
                        isProgramOperation = false;
                        break;

                    default:
                        Console.WriteLine("\nВы ввели неверную команду.");
                        break;
                }

                GrowOldFishes();
                PassDay();

                Console.Write("Нажмите любую клавишу...");
                Console.ReadKey();
                Console.Clear();
            }
        }

        private void AddFish()
        {
            if (_fishes.Count >= _maxCountFish)
            {
                Console.WriteLine("\nВ аквариуме больше нет места!");
                return;
            }

            _fishes.Add(new Fish());
        }

        private void RemoveFish()
        {
            if (_fishes.Count == 0)
            {
                Console.WriteLine("В аквариуме нет рыб.");
                return;
            }

            int index;

            do
            {
                int fishNumber = Utils.GetNumber("Какую рыбку вы хотите вынуть: ");
                index = fishNumber - 1;
            } while ((index >= 0 && index < _fishes.Count) == false);

            _fishes.Remove(_fishes[index]);
        }

        private void PassDay()
        {
            _day++;
        }

        private void GrowOldFishes()
        {
            for (int i = 0; i < _fishes.Count; i++)
            {
                _fishes[i].MakeOlder();
            }
        }

        private void ShowAllFishes()
        {
            int position;

            for (int i = 0; i < _fishes.Count; i++)
            {
                position = i + 1;

                Console.Write($"{position}ая рыбка - ");

                _fishes[i].ShowInfo();
            }
        }
    }

    public class Fish
    {
        private string _name;
        private int _maxAge;

        public Fish()
        {
            _name = FishNamesGenerator.GetName();
            _maxAge = GetAge();
        }

        public int Age { get; private set; }
        private bool IsAlive => Age < _maxAge;

        public void MakeOlder()
        {
            if (Age == _maxAge)
            {
                Age = _maxAge;
                return;
            }

            Age++;
        }

        public void ShowInfo()
        {
            string status;

            if (IsAlive)
            {
                status = "Жива";
            }
            else
            {
                status = "Мертва";
            }

            Console.WriteLine($"{_name}, прожила - {Age} дня(ей) из {_maxAge}. {status}.");
        }

        private int GetAge()
        {
            int minValue = 5;
            int maxValue = 18;

            return _maxAge = Utils.GenerateRandomValue(minValue, maxValue + 1);
        }
    }

    public class FishNamesGenerator
    {
        private static string[] s_fishNames;

        static FishNamesGenerator()
        {
            s_fishNames = new string[]
            {
                "Акварис",
                "Бэйт",
                "Харбор",
                "Гилз",
                "Лэйк",
                "Шип",
                "Сладж",
                "Уотер",
                "Флипер",
                "Сэйла",
                "Леви",
                "Шайн",
                "Кейва",
                "Пузырь",
                "Нони"
            };
        }

        public static string GetName()
            => s_fishNames[Utils.GenerateRandomValue(s_fishNames.Length)];
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

        public static int GetNumber(string message)
        {
            int result;

            Console.Write(message);

            while (int.TryParse(Console.ReadLine(), out result) == false)
            {
                Console.Write("Ошибка! Введите число: ");
            }

            return result;
        }
    }
}