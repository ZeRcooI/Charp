using System;
using System.Collections.Generic;

namespace warHOME
{
    internal class warHOME
    {
        static void Main(string[] args)
        {
            Arena arena = new Arena();
            arena.Work();
        }
    }

    public class Arena
    {
        private List<Character> _redTeam;
        private List<Character> _blueTeam;

        private Character[] _characters =
        {
            new Arko(),
            new ShaWuijing(),
            new ErlangShen(),
            new LordLoki(),
            new Willow()
        };

        public Arena()
        {
            _redTeam = SetCountHeroesInArmy();
            _blueTeam = SetCountHeroesInArmy();
        }

        public void Work()
        {
            ShowArmies();
            Fight();
            Console.ReadLine();
        }

        private void ShowArmies()
        {
            Console.WriteLine("Красная армия: ");
            ShowAllHeroesArmy(_redTeam, _redTeam.Count);

            Console.WriteLine("\nСиняя армия:");
            ShowAllHeroesArmy(_blueTeam, _blueTeam.Count);
        }

        private void ShowAllHeroesArmy(List<Character> characters, int quantityAllHeroesArmy)
        {
            for (int i = 0; i < quantityAllHeroesArmy; i++)
            {
                Console.Write($"{i + 1}) ");
                characters[i].ShowInfo();
            }
        }

        private List<Character> SetCountHeroesInArmy()
        {
            List<Character> army = new List<Character>();

            int maxCountHeroes = _characters.Length;

            int minValue = 3;
            int maxValue = 7;
            int randomCountHeroesInArmy = Utils.GenerateRandomValue(minValue, maxValue);

            for (int i = 0; i < randomCountHeroesInArmy; i++)
            {
                int randomIndex = Utils.GenerateRandomValue(maxCountHeroes);

                Character character = _characters[randomIndex];

                army.Add(character);
            }

            return army;
        }

        private void Fight()
        {
            Console.WriteLine("\nДА НАЧНЁТСЯ БИТВА!!!\n");

            while (_redTeam.Count > 0 && _blueTeam.Count > 0)
            {
                _redTeam[0].Attack(_blueTeam[0]);
                Console.WriteLine($"Игрок красной команды {_redTeam[0].Name} - бьёт. Здоровье {_blueTeam[0].Name}: {_blueTeam[0].Health}");

                _blueTeam[0].Attack(_redTeam[0]);
                Console.WriteLine($"Игрок синей команды {_blueTeam[0].Name} - бьёт. Здоровье {_redTeam[0].Name}: {_redTeam[0].Health}\n\n");

                if (_redTeam[0].Health <= 0)
                {
                    _redTeam.RemoveAt(0);
                }

                if (_blueTeam[0].Health <= 0)
                {
                    _blueTeam.RemoveAt(0);
                }
            }

            FindOutResultFight();
        }

        private void FindOutResultFight()
        {
            if (_redTeam.Count == 0 && _blueTeam.Count == 0)
            {
                Console.WriteLine("Битва завершилась поражением для обоих команд. Ничья!");
            }
            else if (_redTeam.Count == 0)
            {
                Console.WriteLine($"Синяя команда побеждает.");
            }
            else
            {
                Console.WriteLine($"Красная команда побеждает.");
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
        public string Name { get; protected set; }
        protected int Damage { get; set; }

        public virtual Character Clone()
        {
            return new Character(Name, Health, Armor, Damage);
        }

        public virtual void Attack(Character enemy)
        {
            enemy.TakeDamage(Damage);
        }

        public void ShowInfo()
        {
            Console.WriteLine($"{Name}: Здоровье {Health}. Брони {Armor}. Атака {Damage}.");
        }

        protected virtual void TakeDamage(int damage)
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
    }

    public class Arko : Character
    {
        private int _minLevelHealths = 30;
        private bool _isAvailableSkill = true;
        private int _armorReduction = 3;

        public Arko() : base("Арко", 120, 30, 16) { }

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

        protected override void TakeDamage(int damage)
        {
            base.TakeDamage(damage);
            Armor -= _armorReduction;

            if (Health <= _minLevelHealths && _isAvailableSkill == true)
            {
                IncreaseHealth();
                _isAvailableSkill = false;
            }
        }
    }

    public class ShaWuijing : Character
    {
        private int _critDamagePercent = 375;
        private int _defaultDamage = 29;
        private int _damageHitCounter = 0;

        public ShaWuijing() : base("Ша Вуцзинь", 150, 22, 29) { }

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

        protected override void TakeDamage(int damage)
        {
            base.TakeDamage(damage);
            _damageHitCounter++;
        }
    }

    public class ErlangShen : Character
    {
        private int _minLevelHealths = 30;

        public ErlangShen() : base("Эрланг Шен", 122, 26, 40) { }

        public bool CanUseAbility => Health <= _minLevelHealths;

        public override Character Clone()
        {
            return new ErlangShen();
        }

        public void Dodge()
        {
            Console.WriteLine($"{Name} уклонился");
        }

        protected override void TakeDamage(int damage)
        {
            int chanceDodge = 75;

            if (CanUseAbility && Utils.IsSuccess(chanceDodge))
            {
                Dodge();
                return;
            }

            base.TakeDamage(damage);
        }
    }

    public class LordLoki : Character
    {
        private int _minLevelHealths = 40;
        private int _returnableDamage = 0;
        private int _baseDamage = 30;
        private int _damageHitCounter = 0;

        public LordLoki() : base("Лорд Локи", 200, 11, 30) { }

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
            int coefficient = 1;

            Armor += coefficient;
        }

        protected override void TakeDamage(int damage)
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
    }

    public class Willow : Character
    {
        private int _knivesNumber = 4;
        private int _attackCounter = 0;
        private int _baseDamage = 21;

        public Willow() : base("Уиллоу", 170, 21, 21) { }

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

        protected override void TakeDamage(int damage)
        {
            base.TakeDamage(damage);
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