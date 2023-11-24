using System;
using System.Collections.Generic;
using System.Linq;

namespace Linq
{
    internal class SearchForStew
    {
        static void Main(string[] args)
        {
            var factory = new StewFactory();
            var warehouse = new Warehouse(factory);

            warehouse.Work();

            Console.ReadLine();
        }
    }

    public class Warehouse
    {
        private List<Stew> _stews;

        public Warehouse(StewFactory factory)
        {
            _stews = factory.Create();
        }

        public void Work()
        {
            int currentYear = DateTime.Now.Year;

            ShowInfo(_stews);

            Console.WriteLine("\nСвежие тушенки.");

            ShowInfo(GetStews(currentYear));
        }

        private List<Stew> GetStews(int currentYear)
        {
            var foundStews = _stews.Where(stew => stew.ProductionYear + stew.ExpirationDate > currentYear).ToList();

            Console.Write($"Текущий год: {currentYear}. ");

            return foundStews;
        }

        private void ShowInfo(List<Stew> stews)
        {
            Stew stew = stews.FirstOrDefault();

            Console.WriteLine($"Срок годности всех тушенок {stew.ExpirationDate} лет.");

            foreach (Stew product in stews)
            {
                product.ShowInfo();
            }
        }
    }

    public class Stew
    {
        private string _name;

        public Stew(int productionYear)
        {
            _name = "Тушенка \"ГастроТуш\"";
            ExpirationDate = 5;
            ProductionYear = productionYear;
        }

        public int ExpirationDate { get; private set; }
        public int ProductionYear { get; private set; }

        public void ShowInfo()
        {
            Console.WriteLine($"{_name}. | Год выпуска: {ProductionYear}.");
        }
    }

    public class StewFactory
    {
        public List<Stew> Create()
        {
            int count = 20;

            List<Stew> stews = new List<Stew>();

            for (int i = 0; i < count; i++)
            {
                int minProductionYear = 2015;
                int maxProductionYear = 2023;

                int productionYear = Utils.GenerateRandomValue(minProductionYear, maxProductionYear + 1);

                Stew stew = new Stew(productionYear);

                stews.Add(stew);
            }

            return stews;
        }
    }

    public class Utils
    {
        private static Random s_random = new Random();

        public static int GenerateRandomValue(int minNumber, int maxNumber)
        {
            return s_random.Next(minNumber, maxNumber);
        }
    }
}