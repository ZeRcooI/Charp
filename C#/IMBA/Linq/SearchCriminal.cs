using System;
using System.Collections.Generic;
using System.Linq;

namespace Linq
{
    internal class SearchCriminal
    {
        static void Main(string[] args)
        {
            var detective = new Detective();
            var factory = new CriminalFactory();
            var jail = new Jail(factory);
            detective.RequestCriminalData(jail.Criminals);
        }
    }

    public class Detective
    {
        public void RequestCriminalData(IEnumerable<Criminal> criminals)
        {
            Console.WriteLine("Список преступников: ");

            ShowInfo(criminals);

            IEnumerable<Criminal> foundCriminals = GetDataCriminals(criminals);

            ShowInfo(foundCriminals);
        }

        private IEnumerable<Criminal> GetDataCriminals(IEnumerable<Criminal> criminals)
        {
            Console.Write("\nВведите рост: ");
            int height = Utils.GetNumber();
            Console.Write("Введите вес: ");
            int width = Utils.GetNumber();
            Console.Write("Введите национальность: ");
            string nationality = Console.ReadLine();

            IEnumerable<Criminal> foundCriminals = criminals.Where(criminal => height == criminal.Height &&
             width == criminal.Width && nationality == criminal.Nationality.ToLower() && criminal.IsConcluded == false);

            if (foundCriminals == null)
            {
                Console.WriteLine("Преступник не найден");
            }

            return foundCriminals;
        }

        private void ShowInfo(IEnumerable<Criminal> criminals)
        {
            foreach (Criminal criminal in criminals)
            {
                criminal.ShowInfo();
            }
        }
    }

    public class Jail
    {
        private int _maxCountCriminals = 5;
        private List<Criminal> _criminals;

        public Jail(CriminalFactory criminalFactory)
        {
            _criminals = new List<Criminal>();

            for (int i = 0; i < _maxCountCriminals; i++)
            {
                _criminals.Add(criminalFactory.Create());
            }
        }

        public IReadOnlyList<Criminal> Criminals => _criminals;
    }

    public class Criminal
    {
        private string _fullname;

        public Criminal(int height, int width, string fullName, string nationality, bool isConcluded)
        {
            Height = height;
            Width = width;
            _fullname = fullName;
            Nationality = nationality;
            IsConcluded = isConcluded;
        }

        public int Height { get; private set; }
        public int Width { get; private set; }
        public string Nationality { get; private set; }
        public bool IsConcluded { get; private set; }

        public void ShowInfo()
        {
            string status;

            if (IsConcluded)
            {
                status = "Заключен";
            }
            else
            {
                status = "Не заключен";
            }

            Console.WriteLine($"ФИО преступника: {_fullname}. Вес: {Width}. Рост: {Height}. Национальность: {Nationality}. {status}.");
        }
    }

    public class CriminalFactory
    {
        private List<string> _fullnames;
        private List<string> _nationalities;
        private int lastIndex = 0;

        public CriminalFactory()
        {
            _fullnames = FillNames();
            _nationalities = FillNationalities();
        }

        private List<string> FillNames()
        {
            var names = new List<string>
            {
                "Тимур Кант Дамбетов",
                "Петрова Анастасия Сергеевна",
                "Козлов Дмитрий Владимирович",
                "Смирнова Екатерина Александровна",
                "Николаев Артем Станиславович"
            };

            return names;
        }

        private List<string> FillNationalities()
        {
            var nationalities = new List<string>
            {
                "Русский",
                "Казах",
                "Ирландец",
                "Австриец",
                "Голландец"
            };

            return nationalities;
        }

        public Criminal Create()
        {
            int minHeight = 149;
            int maxHeight = 188;

            int minWidht = 62;
            int maxWidht = 125;

            int height = Utils.GenerateRandomValue(minHeight, maxHeight);
            int widht = Utils.GenerateRandomValue(minWidht, maxWidht);

            string nationality = _nationalities[Utils.GenerateRandomValue(_nationalities.Count)];

            string name = _fullnames[lastIndex];

            bool isConcluted = Utils.GenerateRandomBollean();

            if (lastIndex < _fullnames.Count - 1)
            {
                lastIndex++;
            }
            else
            {
                lastIndex = 0;
            }

            return new Criminal(height, widht, name, nationality, isConcluted);
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
    }
}