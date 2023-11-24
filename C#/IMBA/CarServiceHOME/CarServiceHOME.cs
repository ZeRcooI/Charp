using System;
using System.Collections.Generic;

namespace CarServiceHOME
{
    internal class CarServiceHOME
    {
        static void Main(string[] args)
        {
            CarService carService = new CarService();
            carService.Work();
        }
    }

    public class CarService
    {
        private int _balance = 500;

        private DetailCreator _creator = new DetailCreator();
        private List<Detail> _details = new List<Detail>();
        private Queue<Car> _cars = new Queue<Car>();

        public void Work()
        {
            FillDetails();
            CreateAuto();

            while (_cars.Count > 0)
            {
                Console.Clear();

                ShowInfo();

                Car car = _cars.Dequeue();

                ShowCar(car);

                Console.Write("Какую деталь вы хотите сменить: ");

                if (TryGetDetail(out Detail detail) == false)
                {
                    PayFine();
                    continue;
                }

                car.ChangeDetail(detail);

                if (car.IsBroken)
                {
                    PayFine();
                    continue;
                }

                Console.WriteLine($"Автосервис заработал с клиента {detail.Price} рублей.");

                _balance += detail.Price;
            }

            Console.Clear();
            Console.WriteLine("Клиенты на сегодня закончились.\n");
            Console.WriteLine("Нажмите любую кнопку....");
            Console.ReadKey();
        }

        public void ShowCar(Car car)
        {
            Console.WriteLine("\nК вам подъезжает авто.");

            car.Inspect();
        }

        private void PayFine()
        {
            int fine = 12;

            Console.Write($"Вы оплатитли штраф. В размере {fine} рублей. ");
            Console.WriteLine("Клиент разозлился и уехал.");

            _balance -= fine;

            Console.ReadLine();
        }

        private bool TryGetDetail(out Detail detail)
        {
            detail = null;

            int userInput = Utils.GetNumber() - 1;

            if (userInput >= 0 && userInput < _details.Count)
            {
                detail = _details[userInput];
                _details.Remove(detail);

                return true;
            }

            return false;
        }

        private void ShowInfo()
        {
            Console.WriteLine($"Баланс автосервиса составляет - {_balance} рублей.");
            Console.WriteLine($"Осталось обслужить клиентов - {_cars.Count}.\n");
            Console.WriteLine("На складе: ");

            for (int i = 0; i < _details.Count; i++)
            {
                Console.WriteLine($"{i + 1}) {_details[i].Name} по цене: {_details[i].Price}.");
            }
        }

        private void CreateAuto()
        {
            int minValue = 3;
            int maxValue = 8;

            int randomCarsCount = Utils.GenerateRandomValue(minValue, maxValue + 1);

            for (int i = 0; i < randomCarsCount; i++)
            {
                Car car = new Car(_creator.GetDetails());
                car.Break();

                _cars.Enqueue(car);
            }
        }

        private void FillDetails()
        {
            int minValue = 5;
            int maxValue = 15;

            int randomDetailsCount = Utils.GenerateRandomValue(minValue, maxValue + 1);

            List<Detail> details = _creator.GetDetails();

            for (int i = 0; i < randomDetailsCount; i++)
            {
                int index = Utils.GenerateRandomValue(details.Count);

                Detail detail = details[index];

                _details.Add(detail);
            }
        }
    }

    public class Car
    {
        private List<Detail> _details;

        public Car(List<Detail> details)
        {
            _details = details;
        }

        public bool IsBroken
        {
            get
            {
                for (int i = 0; i < _details.Count; i++)
                {
                    if (_details[i].IsBroken)
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        public void ChangeDetail(Detail detail)
        {
            for (int i = 0; i < _details.Count; i++)
            {
                if (_details[i].Name == detail.Name)
                {
                    _details[i] = detail;
                }
            }
        }

        public void Inspect()
        {
            for (int i = 0; i < _details.Count; i++)
            {
                string status = _details[i].IsBroken ? "Сломана" : "Не сломана";

                Console.WriteLine($"Деталь: {_details[i].Name} - {status}.");
            }
        }

        public void Break()
        {
            int maxValue = _details.Count;

            int randomDetailsIndex = Utils.GenerateRandomValue(maxValue);

            _details[randomDetailsIndex].Break();
        }
    }

    public class DetailCreator
    {
        private List<Detail> _baseDetails;

        public DetailCreator()
        {
            _baseDetails = new List<Detail>
            {
                new Detail("Колесо", 35),
                new Detail("Капот", 15),
                new Detail("Дверь", 22)
            };
        }

        public List<Detail> GetDetails()
        {
            List<Detail> details = new List<Detail>();

            for (int i = 0; i < _baseDetails.Count; i++)
            {
                Detail detail = new Detail(_baseDetails[i]);

                details.Add(detail);
            }

            return details;
        }
    }

    public class Detail
    {
        public Detail(string name, int price)
        {
            Name = name;
            Price = price;
            IsBroken = false;
        }

        public Detail(Detail detail)
        {
            Name = detail.Name;
            Price = detail.Price;
            IsBroken = detail.IsBroken;
        }

        public string Name { get; private set; }
        public int Price { get; private set; }
        public bool IsBroken { get; private set; }

        public void Break()
        {
            IsBroken = true;
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
