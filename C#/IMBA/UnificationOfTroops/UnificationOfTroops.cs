using System;
using System.Collections.Generic;
using System.Linq;

namespace Linq
{
    internal class UnificationOfTroops
    {
        static void Main(string[] args)
        {
            var factory = new SoldiersFactory();
            var militaryUnit = new MilitaryUnit(factory);
            militaryUnit.Work();
        }
    }

    public class MilitaryUnit
    {
        private List<Soldier> _firstArmy;
        private List<Soldier> _secondArmy;

        public MilitaryUnit(SoldiersFactory factory)
        {
            _firstArmy = factory.Create();
            _secondArmy = factory.Create();
        }

        public void Work()
        {
            Console.WriteLine("Первая армия:");
            ShowInfo(_firstArmy);

            Console.WriteLine("\nВторая армия:");
            ShowInfo(_secondArmy);

            _secondArmy = _firstArmy.Where(soldier => soldier.Name.StartsWith("Б")).Union(_secondArmy).ToList();
            _firstArmy = _firstArmy.Except(_secondArmy).ToList();

            Console.WriteLine("\nПервая армия после перегруппировки:");
            ShowInfo(_firstArmy);

            Console.WriteLine("\nВторая армия после перегруппировки:");
            ShowInfo(_secondArmy);

            Console.WriteLine("Нажмите любую кнопку для продолжения...");
            Console.ReadKey();
        }

        private void ShowInfo(IEnumerable<Soldier> soldiers)
        {
            foreach (var soldier in soldiers)
            {
                soldier.ShowInfo();
            }
        }
    }

    public class Soldier
    {
        public Soldier(string name, string rank)
        {
            Name = name;
            Rank = rank;
        }

        public string Name { get; private set; }
        public string Rank { get; private set; }

        public void ShowInfo()
        {
            Console.WriteLine($"Имя солдата: {Name}. Звание: {Rank}.");
        }
    }

    public class SoldiersFactory
    {
        private List<string> _names;
        private List<string> _ranks;

        public SoldiersFactory()
        {
            _names = FillNames();
            _ranks = FillRanks();
        }

        public List<Soldier> Create()
        {
            List<Soldier> soldiers = new List<Soldier>();

            int quantity = 10;
            string rank;
            string name;

            for (int i = 0; i < quantity; i++)
            {
                rank = GetRank();
                name = GetName();

                Soldier soldier = new Soldier(name, rank);

                soldiers.Add(soldier);
            }

            return soldiers;
        }

        private string GetRank()
            => _ranks[Utils.GenerateRandomValue(_ranks.Count)];

        private string GetName()
           => _names[Utils.GenerateRandomValue(_names.Count)];

        private List<string> FillRanks()
        {
            var ranks = new List<string>
            {
                "Рядовой",
                "Ефрейтор",
                "Младший сержант",
                "Сержант",
                "Старший сержант",
            };

            return ranks;
        }

        private List<string> FillNames()
        {
            var names = new List<string>
            {
                "Тимуров",
                "Настасьев",
                "Дмитриев",
                "Бабакатеринов",
                "Бартемов",
                "Бариантов",
                "Нагиев",
                "Еленов",
                "Алексеев",
                "Нольгов",
                "Иванов",
                "Бадальев",
                "Станиславов",
                "Юлиев",
                "Артемов",
                "Рамблеров",
                "Тумблеров",
                "Жарков",
                "Босков",
                "Глебов",
                "Хлебов",
                "Игорев",
                "Маринов",
                "Владиславов",
                "Зятев",
                "Сашков",
                "Дашков",
                "Пашков",
                "Викторов"
            };

            return names;
        }
    }

    public class Utils
    {
        private static Random s_random = new Random();

        public static int GenerateRandomValue(int maxNumber)
        {
            return s_random.Next(maxNumber);
        }
    }
}
