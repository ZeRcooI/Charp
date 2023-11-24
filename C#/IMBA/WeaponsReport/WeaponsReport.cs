using System;
using System.Collections.Generic;
using System.Linq;

namespace Linq
{
    internal class WeaponsReport
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
        private List<Soldier> _soldiers;

        public MilitaryUnit(SoldiersFactory factory)
        {
            _soldiers = factory.Create();
        }

        public void Work()
        {
            ShowInfo(_soldiers);

            var foundSoldiers = _soldiers.Select(soldier => new { soldier.Name, soldier.Rank });

            Console.WriteLine("\n*****Список солдат по имени и званию.*****\n");

            foreach (var item in foundSoldiers)
            {
                Console.WriteLine($"Имя солдата: {item.Name}. Звание: {item.Rank}.");
            }

            Console.WriteLine("Нажмите любую кнопку для продолжения...");
            Console.ReadKey();
        }

        private void ShowInfo(List<Soldier> players)
        {
            foreach (Soldier player in players)
            {
                player.ShowInfo();
            }
        }
    }

    public class Soldier
    {
        private string _armament;
        private int _serviceLife;

        public Soldier(string name, string armament, string rank, int serviceLife)
        {
            Name = name;
            _armament = armament;
            Rank = rank;
            _serviceLife = serviceLife;
        }

        public string Name { get ;private set; }
        public string Rank { get; private set; }

        public void ShowInfo()
        {
            Console.WriteLine($"Имя солдата: {Name}. Вооружение: {_armament}. Звание: {Rank}. Срок службы {_serviceLife} месяцев.");
        }
    }

    public class SoldiersFactory
    {
        private List<string> _names;
        private List<string> _ranks;
        private List<string> _armaments;

        public SoldiersFactory()
        {
            _names = FillNames();
            _ranks = FillRanks();
            _armaments = FillArmanents();
        }

        public List<Soldier> Create()
        {
            List<Soldier> soldiers = new List<Soldier>();

            string rank;
            string armanent;

            for (int i = 0; i < _names.Count; i++)
            {
                int minServiceLife = 1;
                int maxServiceLife = 12;

                int soldiersServiceLife = Utils.GenerateRandomValue(minServiceLife, maxServiceLife + 1);

                rank = GetRank();
                armanent = GetArmanents();

                Soldier soldier = new Soldier(_names[i], armanent, rank, soldiersServiceLife);

                soldiers.Add(soldier);
            }

            return soldiers;
        }

        private string GetRank()
            => _ranks[Utils.GenerateRandomValue(_ranks.Count)];

        private string GetArmanents()
            => _armaments[Utils.GenerateRandomValue(_armaments.Count)];

        private List<string> FillArmanents()
        {
            var armanents = new List<string>
            {
                "AK - 47",
                "M4A1 Carbine",
                "Glock 17",
                "Barrett M82",
                "Uzi",
                "Remington 870",
                "MP5",
                "Desert Eagle",
                "RPG - 7",
                "M249 SAW"
            };

            return armanents;
        }

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
                "Тимур",
                "Анастасия",
                "Дмитрий",
                "Екатерина",
                "Артем",
                "Анна",
                "Дмитрий",
                "Елена",
                "Алексей",
                "Ольга",
                "Иван",
                "Наталья",
                "Станислав",
                "Юлия",
                "Артем",
                "Екатерина",
                "Светлана",
                "Максим",
                "Анастасия",
                "Глеб",
                "Ольга",
                "Игорь",
                "Марина",
                "Владислав",
                "Екатерина",
                "Александр",
                "Мария",
                "Павел",
                "Виктория"
            };

            return names;
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
    }
}
