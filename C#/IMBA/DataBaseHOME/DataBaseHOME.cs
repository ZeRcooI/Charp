using System;
using System.Collections.Generic;

namespace DataBaseHOME
{
    public class DataBaseHOMEWork
    {
        private static void Main(string[] args)
        {
            const int CommandAddPlayer = 1;
            const int CommandBanPlayer = 2;
            const int CommandUnbanPlayer = 3;
            const int CommandDeletePlayer = 4;
            const int CommandShowAllPlayer = 5;
            const int CommandExit = 6;

            Database dataBase = new Database();

            bool IsProgramOperation = true;

            Console.WriteLine("Добро пожаловать в базу данных игроков.");

            while (IsProgramOperation)
            {
                Console.WriteLine($"{CommandAddPlayer} - Добавить игрока.\n{CommandBanPlayer} - Забанить игрока по id" +
                    $"\n{CommandUnbanPlayer} - Разбанить игрока по id.\n{CommandDeletePlayer} - Удалить игрока." +
                    $"\n{CommandShowAllPlayer} - Показать всех игроков.\n{CommandExit} - Выйти из программы.");

                Console.Write("Выберите действие: ");
                string userInput = Console.ReadLine();

                if (int.TryParse(userInput, out int numberCommandMenu))
                {
                    switch (numberCommandMenu)
                    {
                        case CommandAddPlayer:
                            dataBase.AddPlayer();
                            break;

                        case CommandBanPlayer:
                            dataBase.BanPlayer();
                            break;

                        case CommandUnbanPlayer:
                            dataBase.UnbanPlayer();
                            break;

                        case CommandDeletePlayer:
                            dataBase.RemovePlayer();
                            break;

                        case CommandShowAllPlayer:
                            dataBase.ShowPlayers();
                            break;

                        case CommandExit:
                            IsProgramOperation = false;
                            break;

                        default:
                            ShowError();
                            break;
                    }
                }
                else
                {
                    ShowError();
                }

                Console.ReadKey();
                Console.Clear();
            }
        }

        public static void ShowError()
        {
            Console.Clear();
            Console.WriteLine("Ошибка! Вы ввели неверную команду!");
        }
    }

    public class Database
    {
        private int _playerIdCounter = 0;

        private List<Player> _players = new List<Player>();

        public void AddPlayer()
        {
            Console.Write("Введите никнейм: ");
            string nickName = Console.ReadLine();

            int level = GetNumber("Введите уровень игрока: ");

            _players.Add(new Player(_playerIdCounter, level, nickName));
            _playerIdCounter++;

            Console.WriteLine("Нажмите любую клавишу для продолжения..");
            Console.ReadKey();
        }

        public void ShowPlayers()
        {
            for (int i = 0; i < _players.Count; i++)
            {
                _players[i].ShowInfo();
            }
        }

        public void BanPlayer()
        {
            if (TryGetPlayer(out Player player))
            {
                player.Ban();
            }
        }

        public void UnbanPlayer()
        {
            if (TryGetPlayer(out Player player))
            {
                player.Unban();
            }
        }

        public void RemovePlayer()
        {
            if (TryGetPlayer(out Player player))
            {
                _players.Remove(player);
            }
        }

        private int GetNumber(string message)
        {
            int result = 0;
            bool isWork = false;

            while (isWork == false)
            {
                Console.Write(message);

                if (int.TryParse(Console.ReadLine(), out result))
                {
                    isWork = true;
                }
                else
                {
                    Console.WriteLine("\nОшибка! Введите число.");
                }
            }

            return result;
        }

        private bool TryGetPlayer(out Player player)
        {
            player = null;

            int userInput = GetNumber("Введите id: ");
            int id = userInput - 1;

            for (int i = 0; i < _players.Count; i++)
            {
                if (id == _players[i].Id)
                {
                    player = _players[i];
                    return true;
                }
            }

            Console.WriteLine("\nИгрока под таким id, не существует.");

            return false;
        }
    }

    public class Player
    {
        private int _level;
        private string _nickName;
        private bool _isBanned;

        public Player(int id, int level, string nickName)
        {
            Id = id;
            _isBanned = false;
            _nickName = nickName;
            _level = level;
        }

        public int Id { get; private set; }

        public void Ban()
        {
            _isBanned = true;
        }

        public void Unban()
        {
            _isBanned = false;
        }

        public void ShowInfo()
        {
            string statusBan;

            if (_isBanned)
            {
                statusBan = "Заблокирован";
            }
            else
            {
                statusBan = "Не заблокирован";
            }

            Console.WriteLine($"Id игрока: {Id}. Никнейм игрока: {_nickName}. Уровень игрока: {_level}." +
                $" Статус игрока: {statusBan}.");
        }
    }
}