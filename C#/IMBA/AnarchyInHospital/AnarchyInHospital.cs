using System;
using System.Collections.Generic;
using System.Linq;

namespace AnarchyInHospital
{
    internal class AnarchyInHospital
    {
        static void Main(string[] args)
        {
            var factory = new PatientFactory();
            var hospital = new Hospital(factory);
            hospital.Work();
        }
    }

    public class Hospital
    {
        private List<Patient> _patients;
        private List<string> _diseases;

        public Hospital(PatientFactory patientFactory)
        {
            _patients = patientFactory.Create();
            _diseases = patientFactory.FillDiseases();
        }

        public void Work()
        {
            const string CommandSortAllPatientsByFullName = "1";
            const string CommandSortAllPatientsByAge = "2";
            const string CommandShowPatientsWithCertainDisease = "3";
            const string CommandExit = "Exit";

            bool isProgramOperation = true;

            while (isProgramOperation)
            {
                Console.WriteLine("Список всех больных.");
                ShowInfo(_patients);

                Console.WriteLine("\nМеню больницы:");
                Console.WriteLine($"{CommandSortAllPatientsByFullName} - Отсортировать всех больных по фио.");
                Console.WriteLine($"{CommandSortAllPatientsByAge} - Отсортировать всех больных по возрасту.");
                Console.WriteLine($"{CommandShowPatientsWithCertainDisease} - Показать больных с определенным заболеванием.");
                Console.WriteLine($"Для выхода введите - {CommandExit}.");

                Console.Write("Введите пункт меню: ");

                switch (Console.ReadLine())
                {
                    case CommandSortAllPatientsByFullName:
                        _patients = _patients.OrderBy(patient => patient.Fullname).ToList();
                        break;

                    case CommandSortAllPatientsByAge:
                        _patients = _patients.OrderBy(patient => patient.Age).ToList();
                        break;

                    case CommandShowPatientsWithCertainDisease:
                        ShowPatientsWithCertainDisease();
                        break;

                    case CommandExit:
                        isProgramOperation = false;
                        Console.WriteLine("До свидания.");
                        break;

                    default:
                        Console.WriteLine("Ошибка! Введите верный пункт меню.");
                        break;
                }

                Console.Clear();
            }
        }

        private void ShowAllDiseases()
        {
            foreach (string disease in _diseases)
            {
                Console.WriteLine(disease);
            }
        }

        private void ShowPatientsWithCertainDisease()
        {
            Console.Clear();
            List<Patient> patients;

            patients = GetPatients();

            if (patients.Count == 0)
            {
                Console.WriteLine("Больных с такой болезнью нет.");
            }
            else
            {
                ShowInfo(patients);
            }

            Console.WriteLine("Нажмите любую клавишу...");
            Console.ReadKey();
        }

        private List<Patient> GetPatients()
        {
            Console.WriteLine("Список заболеваний больных:");
            ShowAllDiseases();

            Console.Write("\nВведите название заболевания для получения информации о больных: ");
            string disease = Console.ReadLine();

            var patients = _patients.Where(patient => disease.ToLower() == patient.Disease.ToLower()).ToList();

            return patients;
        }

        private void ShowInfo(List<Patient> patients)
        {
            foreach (Patient patient in patients)
            {
                patient.ShowInfo();
            }
        }
    }

    public class Patient
    {
        public Patient(int age, string fullName, string disease)
        {
            Age = age;
            Fullname = fullName;
            Disease = disease;
        }

        public int Age { get; private set; }
        public string Fullname { get; private set; }
        public string Disease { get; private set; }

        public void ShowInfo()
        {
            Console.WriteLine($"ФИО пациента: {Fullname}. Возраст: {Age}. ***** Заболевание: {Disease} *****.");
        }
    }

    public class PatientFactory
    {
        private List<string> _fullnames;
        private List<string> _diseases;

        public PatientFactory()
        {
            _fullnames = FillNames();
            _diseases = FillDiseases();
        }

        public List<Patient> Create()
        {
            List<Patient> patients = new List<Patient>();

            int minAge = 16;
            int maxAge = 45;
            int age;
            string disease;

            for (int i = 0; i < _fullnames.Count; i++)
            {
                age = Utils.GenerateRandomValue(minAge, maxAge);
                disease = GetDisease();

                Patient patient = new Patient(age, _fullnames[i], disease);

                patients.Add(patient);
            }

            return patients;
        }

        public List<string> FillDiseases()
        {
            var diseases = new List<string>
            {
                "Пневмония",
                "Сахарный диабет",
                "Острый аппендицит"
            };

            return diseases;
        }

        private string GetDisease()
            => _diseases[Utils.GenerateRandomValue(_diseases.Count)];

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
    }
}