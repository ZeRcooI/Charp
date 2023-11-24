﻿using System;

namespace warHOME
{
    internal class GladiatorFightsHOME
    {
        static void Main(string[] args)
        {
            Arena arena = new Arena();
            arena.Work();
        }
    }

    public class Arena
    {
        private Character _firstCharacter;
        private Character _secondCharacter;

        private Character[] _characters =
        {
            new Arko(),
            new ShaWuijing(),
            new ErlangShen(),
            new LordLoki(),
            new Willow()
        };

        public void Work()
        {
            const string CommandStartBattles = "1";
            const string CommandExit = "2";

            bool isProgramOperation = true;

            while (isProgramOperation)
            {
                Console.Clear();

                Console.WriteLine("Добро пожаловать на гладиаторскую арену.");
                Console.WriteLine($"{CommandStartBattles} - Начать сражения.");
                Console.WriteLine($"{CommandExit} - Покинуть арену.");

                Console.Write("Выберите пункт меню: ");
                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case CommandStartBattles:
                        StartBattles();
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

        private void StartBattles()
        {
            ShowHeroes();

            _firstCharacter = CreateFigher(GetIndexFighters());
            _secondCharacter = CreateFigher(GetIndexFighters());
            Fight();
        }

        private void ShowHeroes()
        {
            for (int i = 0; i < _characters.Length; i++)
            {
                Console.Write($"{i + 1}) ");
                _characters[i].ShowInfo();
            }
        }

        private Character CreateFigher(int index)
        {
            if (index >= 0 && index < _characters.Length)
            {
                return _characters[index].Clone();
            }

            return null;
        }

        private int GetIndexFighters()
        {
            int userInput;

            do
            {
                Console.Write("\nВыберите персонажа: ");
                userInput = Utils.GetNumber() - 1;

            } while ((userInput >= 0 && userInput < _characters.Length) == false);

            return userInput;
        }

        private void Fight()
        {
            while (_firstCharacter.Health > 0 && _secondCharacter.Health > 0)
            {
                _firstCharacter.Attack(_secondCharacter);
                Console.WriteLine($"Здоровье {_secondCharacter.Name}: {_secondCharacter.Health}");

                _secondCharacter.Attack(_firstCharacter);
                Console.WriteLine($"Здоровье {_firstCharacter.Name}: {_firstCharacter.Health}");
            }

            FindOutResultFight(_firstCharacter, _secondCharacter);
        }

        private void FindOutResultFight(Character firstCharacter, Character secondCharacter)
        {
            if (firstCharacter.Health <= 0 && secondCharacter.Health <= 0)
            {
                Console.WriteLine("Герои погибли одновременно. Ничья!");
            }
            else if (firstCharacter.Health <= 0)
            {
                Console.WriteLine($"{secondCharacter.Name} побеждает.");
            }
            else
            {
                Console.WriteLine($"{firstCharacter.Name} побеждает.");
            }
        }
    }

    public class Character
    {
        protected int Armor;

        public Character(string name, int health, int armor, int damage)
        {
            Health = health;
            Armor = armor;
            Damage = damage;
            Name = name;
        }

        public int Health { get; protected set; }
        public int Damage { get; protected set; }
        public string Name { get; protected set; }

        public virtual Character Clone()
        {
            return new Character(Name, Health, Armor, Damage);
        }

        public virtual void TakeDamage(int damage)
        {
            if (damage < 0)
            {
                return;
            }

            if (damage < Armor)
            {
                return;
            }

            Health -= damage - Armor;
        }

        public virtual void Attack(Character enemy)
        {
            enemy.TakeDamage(Damage);
        }

        public void ShowInfo()
        {
            Console.WriteLine($"{Name}: Здоровье {Health}. Брони {Armor}. Атака {Damage}.");
        }
    }

    public class Arko : Character
    {
        private int _minLevelHealths = 30;
        private bool _isAvailableSkill = true;
        private int _armorReduction = 3;

        public Arko() : base("Арко", 120, 30, 16) { }

        public override void TakeDamage(int damage)
        {
            base.TakeDamage(damage);
            Armor -= _armorReduction;

            if (Health <= _minLevelHealths && _isAvailableSkill == true)
            {
                IncreaseHealth();
                _isAvailableSkill = false;
            }
        }

        public override void Attack(Character enemy)
        {
            base.Attack(enemy);
        }

        public override Character Clone()
        {
            return new Arko();
        }

        public void IncreaseHealth()
        {
            int coefficient = 2;
            int minNumber = Health;
            int maxNumber = minNumber + minNumber * coefficient;

            Health += Utils.GenerateRandomValue(minNumber, maxNumber);

            Console.WriteLine($"{Name} применил навык и увеличил здоровье.");
        }
    }

    public class ShaWuijing : Character
    {
        private int _critDamagePercent = 375;
        private int _defaultDamage = 29;
        private int _damageHitCounter = 0;

        public ShaWuijing() : base("Ша Вуцзинь", 150, 22, 29) { }

        public override void TakeDamage(int damage)
        {
            base.TakeDamage(damage);
            _damageHitCounter++;
        }

        public override void Attack(Character enemy)
        {
            int hitCoefficient = 3;

            if (_damageHitCounter == hitCoefficient)
            {
                DealCritDamage();
                Console.WriteLine($"{Name} аткивирует крид удар.");
            }
            else
            {
                Damage = _defaultDamage;
            }

            base.Attack(enemy);
        }

        public override Character Clone()
        {
            return new ShaWuijing();
        }

        public void DealCritDamage()
        {
            int coefficient = 100;

            Damage += Damage * _critDamagePercent / coefficient;
        }
    }

    public class ErlangShen : Character
    {
        private int _minLevelHealths = 30;

        public ErlangShen() : base("Эрланг Шен", 122, 26, 40) { }

        public bool CanUseAbility => Health <= _minLevelHealths;

        public override void TakeDamage(int damage)
        {
            int chanceDodge = 75;

            if (CanUseAbility && Utils.IsSuccess(chanceDodge))
            {
                Dodge();
                return;
            }

            base.TakeDamage(damage);
        }

        public override void Attack(Character enemy)
        {
            base.Attack(enemy);
        }

        public override Character Clone()
        {
            return new ErlangShen();
        }

        public void Dodge()
        {
            Console.WriteLine($"{Name} уклонился");
        }
    }

    public class LordLoki : Character
    {
        private int _minLevelHealths = 40;
        private int _returnableDamage = 0;
        private int _baseDamage = 30;
        private int _damageHitCounter = 0;

        public LordLoki() : base("Лорд Локи", 200, 11, 30) { }

        public override void TakeDamage(int damage)
        {
            int minImpactFactor = 3;

            _returnableDamage = damage;

            if (Health <= _minLevelHealths || _damageHitCounter % minImpactFactor == 0)
            {
                IncreaseShield();
                Console.WriteLine($"{Name} увеличивает себе броню.");
            }

            base.TakeDamage(damage);

            _damageHitCounter++;
        }

        public override void Attack(Character enemy)
        {
            Damage = _baseDamage;

            _returnableDamage = (Health <= _minLevelHealths) ? Counterattack(_returnableDamage) : 0;

            Console.WriteLine($"{Name} успешно контраттакует.");

            Damage += _returnableDamage;

            base.Attack(enemy);
        }

        public override Character Clone()
        {
            return new LordLoki();
        }

        public int Counterattack(int returnableDamage)
        {
            int coefficientPercent = 100;
            int percentCounterattackDamage = 40;

            returnableDamage = returnableDamage / coefficientPercent * percentCounterattackDamage;

            return returnableDamage;
        }

        public void IncreaseShield()
        {
            int coefficient = 2;

            Armor += coefficient;
        }
    }

    public class Willow : Character
    {
        private int _knivesNumber = 4;
        private int _attackCounter = 0;
        private int _baseDamage = 21;

        public Willow() : base("Уиллоу", 170, 21, 21) { }

        public override void TakeDamage(int damage)
        {
            base.TakeDamage(damage);
        }

        public override void Attack(Character enemy)
        {
            int coefficient = 2;

            _attackCounter++;

            if (_attackCounter % coefficient == 0 && _knivesNumber > 0)
            {
                Damage = _baseDamage;

                ThrowKknife();

                Console.WriteLine($"{Name} дополнительно кидает нож.");

                _knivesNumber--;
            }

            base.Attack(enemy);
        }

        public override Character Clone()
        {
            return new Willow();
        }

        public void ThrowKknife()
        {
            int knifeDamage = 7;

            Damage += knifeDamage;
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

        public static bool IsSuccess(int chanceDodge)
        {
            int maxValue = 100;

            return GenerateRandomValue(maxValue) <= chanceDodge;
        }
    }
}