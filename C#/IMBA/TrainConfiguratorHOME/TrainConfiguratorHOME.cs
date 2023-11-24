using System;
using System.Collections.Generic;

namespace TrainConfiguratorHOME
{
    internal class TrainConfiguratorHOME
    {
        static void Main(string[] args)
        {
            Depot railwayStation = new Depot();
            railwayStation.Work();
        }
    }

    public class Depot
    {
        private List<Train> _trains = new List<Train>();

        public void Work()
        {
            const string CommandCreateTrain = "1";
            const string CommandSendTrain = "2";
            const string CommandExit = "3";

            bool isProgramOperation = true;

            while (isProgramOperation)
            {
                Console.Clear();
                ShowCurrentFlightInfo();

                Console.WriteLine($"Добро пожаловать в конфигуратор поездов.");
                Console.WriteLine($"{CommandCreateTrain} - Создать поезд.");
                Console.WriteLine($"{CommandSendTrain} - Отправить поезд.");
                Console.WriteLine($"{CommandExit} - Выйти из конфигуратора.");

                Console.Write("Выберите пункт меню: ");
                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case CommandCreateTrain:
                        CreateTrain();
                        break;

                    case CommandSendTrain:
                        SendTrain();
                        break;

                    case CommandExit:
                        isProgramOperation = false;
                        break;

                    default:
                        Console.Clear();
                        Console.WriteLine("Ошибка! Вы ввели неверную команду!");
                        break;
                }

                Console.WriteLine("\nНажмите любую клавишу для продолжения...");
                Console.ReadKey();
            }
        }

        private void CreateTrain()
        {
            Direction direction = GetDirection();
            int ticketsCount = SellTickets();

            List<Van> vans = FillVans(ticketsCount);

            Train train = new Train(direction, vans, ticketsCount);

            _trains.Add(train);
        }

        private Direction GetDirection()
        {
            string departureStation = string.Empty;
            string arrivalStation;

            do
            {
                if (departureStation != string.Empty)
                {
                    Console.WriteLine("Станция прибытия, не может быть станцией отправления.");
                }

                Console.Write("Введите станцию отправления: ");
                departureStation = Console.ReadLine();

                Console.Write("Введите станцию прибытия: ");
                arrivalStation = Console.ReadLine();

            } while (departureStation == arrivalStation);

            return new Direction(departureStation, arrivalStation);
        }

        private void SendTrain()
        {
            if (_trains.Count == 0)
            {
                Console.WriteLine("Нет поезов для отправления.");
                return;
            }

            for (int i = 0; i < _trains.Count; i++)
            {
                if (_trains[i].IsLeft == false)
                {
                    _trains[i].Send();
                }
            }
        }

        private List<Van> FillVans(int ticketsCount)
        {
            List<Van> vans = new List<Van>();

            while (ticketsCount > 0)
            {
                int capacity = GeneratCapacity();
                int ticketsAmount = Math.Min(capacity, ticketsCount);

                Van van = new Van(capacity, ticketsAmount);
                vans.Add(van);

                ticketsCount -= capacity;
            }

            return vans;
        }

        private int GeneratCapacity()
        {
            int minCountPassengers = 30;
            int maxCountPassengers = 100;

            return Utils.GenerateRandomValue(minCountPassengers, maxCountPassengers);
        }

        private int SellTickets()
        {
            int minCountPassengers = 12;
            int maxCountPassengers = 485;

            int randomCountPassengers = Utils.GenerateRandomValue(minCountPassengers, maxCountPassengers);

            return randomCountPassengers;
        }

        private void ShowCurrentFlightInfo()
        {
            if (_trains.Count == 0)
            {
                Console.WriteLine("Нет информации о текущих рейсах.\n");
                return;
            }

            for (int i = 0; i < _trains.Count; i++)
            {
                _trains[i].ShowInfo();
            }
        }
    }

    public class Train
    {
        private List<Van> _vans = new List<Van>();
        private Direction _direction;
        private int _ticketsCount;

        public Train(Direction direction, List<Van> vans, int ticketsCount)
        {
            _direction = direction;
            _ticketsCount = ticketsCount;
            _vans = vans;
            IsLeft = false;
        }

        public bool IsLeft { get; private set; }

        public void Send()
        {
            IsLeft = true;
        }

        public void ShowInfo()
        {
            string statusTrain;

            if (IsLeft)
            {
                statusTrain = "Отправлен.";
            }
            else
            {
                statusTrain = "Не отправлен.";
            }

            Console.Write($"{_ticketsCount} пассажира(ов) купили билеты на рейс:" +
                $" {_direction.DepartureStation} - {_direction.ArrivalStation}. ");
            Console.WriteLine($"Количество вагонов: {_vans.Count}. ");

            foreach (Van van in _vans)
            {
                Console.WriteLine($"Занято мест в вагоне {van.TicketsAmount} / {van.Capacity} ");
            }

            Console.WriteLine($"Статус - {statusTrain}\n");
        }
    }

    public class Direction
    {
        public Direction(string departureStation, string arrivalStation)
        {
            DepartureStation = departureStation;
            ArrivalStation = arrivalStation;
        }

        public string ArrivalStation { get; }
        public string DepartureStation { get; }
    }

    public class Van
    {
        public Van(int capacity, int ticketsAmount)
        {
            Capacity = capacity;
            TicketsAmount = ticketsAmount;
        }

        public int Capacity { get; }
        public int TicketsAmount { get; }
    }

    public class Utils
    {
        private static Random s_random = new Random();

        public static int GenerateRandomValue(int minNumber, int maxNumber)
        {
            return s_random.Next(minNumber, maxNumber + 1);
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
