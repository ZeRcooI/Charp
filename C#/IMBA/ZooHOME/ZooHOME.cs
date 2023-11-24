using System;
using System.Collections.Generic;

namespace ZooHOME
{
    internal class ZooHOME
    {
        static void Main(string[] args)
        {
            Zoo zoo = new Zoo();
            zoo.Work();
        }
    }

    public class Zoo
    {
        private List<Animal> _animalsType = new List<Animal>();
        private List<Aviary> _aviaries = new List<Aviary>();

        public Zoo()
        {
            _animalsType = CreateAnimals();
            CreateAviaries();
        }

        public void Work()
        {
            const int CommandExit = 0;

            bool isProgramOperation = true;

            while (isProgramOperation)
            {
                Console.Clear();

                for (int i = 0; i < _aviaries.Count; i++)
                    Console.WriteLine($"{i + 1})Подойти к {i + 1}му вальеру.");

                Console.WriteLine($"Для выхода из приложения нажмите {CommandExit}.");
                Console.Write("Выберите вольер: ");
                int aviaryNumber = Utils.GetNumber();

                if (aviaryNumber == CommandExit)
                {
                    isProgramOperation = false;
                }
                else if (aviaryNumber > 0 && aviaryNumber <= _aviaries.Count)
                {
                    _aviaries[aviaryNumber - 1].ShowInfo();
                }
                else
                {
                    Console.WriteLine("Такого вольера нет!");
                }

                Console.WriteLine("\nНажмите любую клавишу для продолжения...");
                Console.ReadKey();
            }
        }

        private void CreateAviaries()
        {
            for (int i = 0; i < _animalsType.Count; i++)
            {
                int count = GetAnimalsCount();
                List<Animal> animals = new List<Animal>();

                for (int j = 0; j < count; j++)
                {
                    Animal animal = new Animal(_animalsType[i]);
                    animals.Add(animal);
                }

                _aviaries.Add(new Aviary(animals));
            }
        }

        private List<Animal> CreateAnimals()
        {
            List<Animal> animals = new List<Animal>
            {
                new Animal("Обезьяна", "Продаю: людей, бананы. Шучу, не бананы"),
                new Animal("Лемур", "Узбагойся я тебе говорю!"),
                new Animal("Волк", "ПОМНИ, если тебя съели, у тебя есть два выхода. Безумно можно быть пееервым.."),
                new Animal("Тигр", "Ээээ, ч за лев этот тигр, а?!")
            };

            return animals;
        }

        private int GetAnimalsCount()
        {
            int minValue = 1;
            int maxValue = 8;

            int randomNumber = Utils.GenerateRandomValue(minValue, maxValue + 1);

            return randomNumber;
        }
    }

    public class Animal
    {
        public Animal(string type, string sound)
        {
            Gender = GetGender();
            Type = type;
            Sound = sound;
        }

        public Animal(Animal animal)
        {
            Gender = GetGender();
            Type = animal.Type;
            Sound = animal.Sound;
        }

        public string Gender { get; private set; }
        public string Type { get; private set; }
        public string Sound { get; private set; }

        public void ShowInfo()
        {
            Console.Write($"{Type}, пол {Gender}. Издает звук - ");

            MakeSound();
        }

        private void MakeSound() =>
            Console.WriteLine($"{Sound}");

        private string GetGender()
        {
            string[] gender = { "Самец", "Самка" };

            int randomValue = Utils.GenerateRandomValue(gender.Length);

            return gender[randomValue];
        }
    }

    public class Aviary
    {
        private List<Animal> _animals = new List<Animal>();

        public Aviary(List<Animal> animals)
        {
            _animals = animals;
        }

        public void ShowInfo()
        {
            for (int i = 0; i < _animals.Count; i++)
            {
                _animals[i].ShowInfo();
            }
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

        public static int GetNumber()
        {
            int result;

            while (int.TryParse(Console.ReadLine(), out result) == false)
            {
                Console.Write("Ошибка! Введите число: ");
            }

            return result;
        }
    }
}