using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INTERFACE
{
    internal class INTERFACETEST
    {
        static void Main(string[] args)
        {
            Player player = new Player();

            Weapon[] inventory = { new Gun(), new LaserGun(), new Bow(), };

            foreach (var item in inventory)
            {
                player.CheckInfo(item);
                player.Fire(item);
                Console.WriteLine();
            }

            player.CheckInfo(new Box());

            Console.ReadLine();
        }
    }

    interface IHasInfo
    {
        void ShowInfo();
    }

    interface IWeapon
    {
        int Damage { get; }

        void Fire();
    }

    abstract class Weapon : IHasInfo, IWeapon
    {
        public abstract int Damage { get; }
        public abstract void Fire();

        public void ShowInfo()
        {
            Console.WriteLine($"{GetType().Name} Damage: {Damage}");
        }
    }

    class Box : IHasInfo
    {
        public void ShowInfo()
        {
            Console.WriteLine("Йа коробко");
        }
    }

    class Gun : Weapon
    {
        public override int Damage { get { return 5; } }

        public override void Fire()
        {
            Console.WriteLine("Пыщ!");
        }
    }

    class LaserGun : Weapon
    {
        public override int Damage { get { return 8; } }

        public override void Fire()
        {
            Console.WriteLine("Пиу!");
        }
    }

    class Bow : Weapon
    {
        public override int Damage => 3;

        public override void Fire()
        {
            Console.WriteLine("Чпуньк!");
        }
    }

    class Player
    {
        public void Fire(IWeapon weapon)
        {
            weapon.Fire();
        }

        public void CheckInfo(IHasInfo hasInfo)
        {
            hasInfo.ShowInfo();
        }
    }
}
