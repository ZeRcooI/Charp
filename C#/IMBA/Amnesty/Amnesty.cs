using System;
using System.Collections.Generic;
using System.Linq;

namespace Linq
{
    internal class Amnesty
    {
        static void Main(string[] args)
        {
            var factory = new CriminalFactory();
            var jail = new Jail(factory);
            jail.Work();
        }
    }

    public class Jail
    {
        private List<Criminal> _criminals;

        public Jail(CriminalFactory criminalFactory)
        {
            _criminals = criminalFactory.Create();
        }

        public void Work()
        {
            string articleForRelease = "Антиправительственное";

            Console.WriteLine("Список заключенных до амнистии:\n");
            ShowInfo();

            _criminals = _criminals.Where(criminal => criminal.ArticleCrimes != articleForRelease).ToList();

            Console.WriteLine("\nСписок заключенных после амнистии:");
            ShowInfo();

            Console.WriteLine("Нажмите любую кнопку для продолжения...");
            Console.ReadKey();
        }

        private void ShowInfo()
        {
            foreach (Criminal criminal in _criminals)
            {
                criminal.ShowInfo();
            }
        }
    }

    public class Criminal
    {
        private string _fullname;
        private bool _isConcluded;

        public Criminal(string fullName, string articleCrimes)
        {
            _fullname = fullName;
            _isConcluded = true;
            ArticleCrimes = articleCrimes;
        }

        public string ArticleCrimes { get; private set; }

        public void ShowInfo()
        {
            string status;

            if (_isConcluded)
            {
                status = "Заключен";
            }
            else
            {
                status = "Освобожден";
            }

            Console.WriteLine($"ФИО преступника: {_fullname}.***** Заключен по статье {ArticleCrimes} *****.{status}.");
        }
    }

    public class CriminalFactory
    {
        private List<string> _fullnames;
        private string _criminalsArticle;

        private string[] _articlesCrimes =
        {
             "Антиправительственное",
             "Незаконный угон зонтов",
             "Кража перьев из подушек"
        };

        public CriminalFactory()
        {
            _fullnames = FillNames();
        }

        public List<Criminal> Create()
        {
            List<Criminal> criminals = new List<Criminal>();

            for (int i = 0; i < _fullnames.Count; i++)
            {
                _criminalsArticle = GetArticleCrimes();

                Criminal criminal = new Criminal(_fullnames[i], _criminalsArticle);

                criminals.Add(criminal);
            }

            return criminals;
        }

        private string GetArticleCrimes()
            => _articlesCrimes[Utils.GenerateRandomValue(_articlesCrimes.Length)];

        private List<string> FillNames()
        {
            var names = new List<string>
            {
                "Тимур Кант Дамбетов",
                "Петрова Анастасия Сергеевна",
                "Козлов Дмитрий Владимирович",
                "Смирнова Екатерина Александровна",
                "Николаев Артем Станиславович",
                "Анна Сергеевна Иванова",
                "Дмитрий Александрович Смирнов",
                "Елена Владимировна Петрова",
                "Алексей Игоревич Козлов",
                "Ольга Андреевна Новикова",
                "Иван Петрович Морозов",
                "Наталья Алексеевна Кузнецова",
                "Станислав Дмитриевич Жуков",
                "Юлия Валерьевна Соколова",
                "Артем Николаевич Васильев",
                "Екатерина Игоревна Павлова",
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
