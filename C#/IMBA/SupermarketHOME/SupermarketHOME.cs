using System;
using System.Collections.Generic;

namespace SupermarketHOME
{
    internal class SupermarketHOME
    {
        static void Main(string[] args)
        {
            Supermarket supermarket = new Supermarket();
            supermarket.Work();
        }
    }

    public class Supermarket
    {
        private Queue<Client> _clients = new Queue<Client>();
        private List<Product> _products = new List<Product>();

        public void Work()
        {
            FillProducts();
            FillQueue();

            while (_clients.Count > 0)
            {
                Console.Clear();
                Console.WriteLine("Добро пожаловать в супермаркет.\n");

                Client client = _clients.Peek();

                ShowClients();
                client.ShowCurrentInfo();

                Console.WriteLine($"К оплате: {client.СalculateProductsCost()} рубля(ей).");

                if (client.MoneyCount >= client.СalculateProductsCost())
                {
                    Console.WriteLine($"{client.Name} оплачивает покупки.");
                }
                else
                {
                    Console.WriteLine($"\nУ {client.Name}а недостаточно денег. ");

                    while (client.MoneyCount < client.СalculateProductsCost())
                        client.RemoveRandomProduct();

                    if (client.LengthProductList > 0)
                        Console.WriteLine($"Он оплатил покупку и уходит.");
                    else
                        Console.WriteLine("Он ничего не оплатил и ушёл.");
                }

                Console.WriteLine("\nНажмите любую клавишу...");
                Console.ReadKey();
                _clients.Dequeue();
            }

            Console.Clear();
            Console.WriteLine("Клиентов больше нет.");
            Console.WriteLine("\nНажмите любую клавишу...");
            Console.ReadKey();
        }

        private List<Product> GiveProducts()
        {
            int minCountProduct = 1;
            int maxCountProduct = 8;
            int randomCountProduct = Utils.GenerateRandomValue(minCountProduct, maxCountProduct);

            List<Product> products = new List<Product>();

            for (int i = 0; i < randomCountProduct; i++)
            {
                int randomIndex = Utils.GenerateRandomValue(maxCountProduct);

                Product product = _products[randomIndex];

                products.Add(product);
            }

            return products;
        }

        private void ShowClients()
        {
            foreach (Client client in _clients)
                client.ShowInfo();
        }

        private void FillProducts()
        {
            _products.Add(new Product("Курица", 23));
            _products.Add(new Product("Банан", 16));
            _products.Add(new Product("Жвачка", 8));
            _products.Add(new Product("Печенье", 14));
            _products.Add(new Product("Спички", 2));
            _products.Add(new Product("Шоколад", 13));
            _products.Add(new Product("Лоток яиц", 26));
            _products.Add(new Product("Яблоко", 11));
        }

        private void FillQueue()
        {
            _clients.Enqueue(new Client("Васильевич", GiveProducts()));
            _clients.Enqueue(new Client("Степаныч", GiveProducts()));
            _clients.Enqueue(new Client("Игорьевич", GiveProducts()));
            _clients.Enqueue(new Client("Михалыч", GiveProducts()));
            _clients.Enqueue(new Client("Егорыч", GiveProducts()));
            _clients.Enqueue(new Client("Валентинович", GiveProducts()));
            _clients.Enqueue(new Client("Александрович", GiveProducts()));
            _clients.Enqueue(new Client("Рудольфович", GiveProducts()));
        }
    }

    public class Client
    {
        private List<Product> _products = new List<Product>();

        public Client(string name, List<Product> products)
        {
            Name = name;
            SetMoneyCount();
            _products = new List<Product>(products);
        }

        public int LengthProductList => _products.Count;
        public int MoneyCount { get; private set; }
        public string Name { get; private set; }

        public void RemoveRandomProduct()
        {
            for (int i = 0; i < _products.Count; i++)
            {
                int productIndex = Utils.GenerateRandomValue(_products.Count);

                Console.WriteLine($"Вынимает {_products[productIndex].Name}, стоимостью {_products[productIndex].Price}.");
                _products.RemoveAt(productIndex);
            }
        }

        public void ShowCurrentInfo()
        {
            Console.WriteLine($"\nК кассе подходит: {Name}.\n\nВ его корзине: ");

            foreach (Product product in _products)
                product.ShowInfo();
        }

        public int СalculateProductsCost()
        {
            int productCost = 0;

            foreach (Product item in _products)
                productCost += item.Price;

            return productCost;
        }

        public void ShowInfo()
        {
            Console.WriteLine($"{Name}, взял собой {MoneyCount} рубля(ей).");
        }

        private void SetMoneyCount()
        {
            int minNumber = 3;
            int maxNumber = 70;

            MoneyCount = Utils.GenerateRandomValue(minNumber, maxNumber + 1);
        }
    }

    public class Product
    {
        public Product(string name, int price)
        {
            Name = name;
            Price = price;
        }

        public string Name { get; }
        public int Price { get; }

        public void ShowInfo()
        {
            Console.WriteLine($"{Name} стоимостью {Price} рубля(ей).");
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