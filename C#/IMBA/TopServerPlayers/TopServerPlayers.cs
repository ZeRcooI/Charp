using System;
using System.Collections.Generic;
using System.Linq;

namespace Linq
{
    internal class TopServerPlayers
    {
        static void Main(string[] args)
        {
            var factory = new PlayerFactory();
            var server = new Server(factory);
            server.Work();
        }
    }

    public class Server
    {
        private List<Player> _players;

        public Server(PlayerFactory factory)
        {
            _players = factory.Create();
        }

        public void Work()
        {
            const string CommandTopPlayersByLevel = "1";
            const string CommandTopPlayersByPower = "2";
            const string CommandExit = "Exit";

            int placesInTopNumber = 3;
            bool isProgramOperation = true;

            while (isProgramOperation)
            {
                Console.WriteLine("Список всех игроков.");
                ShowInfo(_players);

                Console.WriteLine("\nМеню больницы:");
                Console.WriteLine($"{CommandTopPlayersByLevel} - Определенить топ 3 игроков по уровню.");
                Console.WriteLine($"{CommandTopPlayersByPower} - Определенить топ 3 игроков по силе.");
                Console.WriteLine($"Для выхода введите - {CommandExit}.");

                Console.Write("Введите пункт меню: ");

                switch (Console.ReadLine())
                {
                    case CommandTopPlayersByLevel:
                        ShowTopPlayersByLevel(placesInTopNumber);
                        break;

                    case CommandTopPlayersByPower:
                        ShowTopPlayersByPower(placesInTopNumber);
                        break;

                    case CommandExit:
                        isProgramOperation = false;
                        Console.WriteLine("До свидания.");
                        break;

                    default:
                        Console.WriteLine("Ошибка! Введите верный пункт меню.");
                        break;
                }

                WaitInput();
                Console.Clear();
            }
        }

        private void ShowTopPlayersByLevel(int placesInTopNumber)
        {
            Console.Clear();

            Console.WriteLine("Топ 3 игроков по уровню:");
            var topPlayersByLevel = _players.OrderByDescending(player => player.Level).Take(placesInTopNumber).ToList();

            ShowInfo(topPlayersByLevel);
        }

        private void ShowTopPlayersByPower(int placesInTopNumber)
        {
            Console.Clear();

            Console.WriteLine("Топ 3 игроков по силе:");
            var topPlayersByPower = _players.OrderByDescending(player => player.Power).Take(placesInTopNumber).ToList();

            ShowInfo(topPlayersByPower);
        }

        private void WaitInput()
        {
            Console.WriteLine("Нажмите любую кнопку для продолжения...");
            Console.ReadKey();
        }

        private void ShowInfo(List<Player> players)
        {
            foreach (Player player in players)
            {
                player.ShowInfo();
            }
        }
    }

    public class Player
    {
        private string _fullname;

        public Player(int level, int power, string fullName)
        {
            Level = level;
            Power = power;
            _fullname = fullName;
        }

        public int Level { get; private set; }
        public int Power { get; private set; }

        public void ShowInfo()
        {
            Console.WriteLine($"Никнейм игрока: {_fullname}. Уровень игрока: {Level}. Мощь: {Power}.");
        }
    }

    public class PlayerFactory
    {
        private List<string> _nickNames;

        public PlayerFactory()
        {
            _nickNames = FillNames();
        }

        public List<Player> Create()
        {
            List<Player> players = new List<Player>();

            for (int i = 0; i < _nickNames.Count; i++)
            {
                int minLevel = 1;
                int maxLevel = 30;

                int minPower = 2000;
                int maxPower = 9999;

                int playerLevel = Utils.GenerateRandomValue(minLevel, maxLevel);
                int playerPower = Utils.GenerateRandomValue(minPower, maxPower);

                Player player = new Player(playerLevel, playerPower, _nickNames[i]);

                players.Add(player);
            }

            return players;
        }

        private List<string> FillNames()
        {
            var names = new List<string>
            {
                "ShadowRiderX",
                "MysticEcho",
                "QuantumPhoenix",
                "ThunderPulse",
                "LunaShard",
                "NebulaWhisper",
                "CipherStorm",
                "FrostBiteX",
                "BlazeWanderer",
                "EnigmaVortex",
                "SolarSpecter",
                "NovaShifter",
                "EchoSwift",
                "QuantumQuasar",
                "FrostbyteFury",
                "CelestialSerenade",
                "VoidVoyager",
                "ZenithSeeker",
                "EmberBlitz",
                "SapphireRift",
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
    }
}
