using System;
using System.Collections.Generic;

namespace MainProject
{

    public class DeckCard
    {
        private static void Main(string[] args)
        {
            const string CommandTakeCard = "1";
            const string CommandExit = "2";

            Deck deck = new Deck();
            Player player = new Player();

            int separatorCount = 114;
            char separator = '-';
            bool isProgramOperation = true;

            while (isProgramOperation)
            {
                deck.ShowDeck();

                Console.Write($"Карт в колоде - {deck.CardCounterInDeck}.\n** {new string(separator, separatorCount)} **");
                Console.WriteLine($"{CommandTakeCard} - взять карту.\n{CommandExit} - выход из программы." +
                    $"\nКарт у игрока - {player.CardCounterInHand}.");

                player.ShowCard();

                Console.Write($"\n** {new string(separator, separatorCount)} **");

                Console.Write("Выберите действие: ");
                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case CommandTakeCard:
                        player.TakeCard(deck.GiveCard());
                        break;

                    case CommandExit:
                        isProgramOperation = false;
                        break;

                    default:
                        Console.Clear();
                        Console.WriteLine("Ошибка! Вы ввели неверную команду!");
                        break;
                }
            }

            Console.Write("Нажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }
    }

    public class Deck
    {
        private List<Card> _cards = new List<Card>();

        public Deck()
        {
            FillDeck();
        }

        public int CardCounterInDeck => _cards.Count;

        public void ShowDeck()
        {
            Console.Clear();

            if (_cards.Count > 0)
            {
                for (int i = 0; i < _cards.Count; i++)
                {
                    _cards[i].ShowInfo();
                }

                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("\nВ колоде нет карт!\n");
            }
        }

        public Card GiveCard()
        {
            Console.Clear();

            if (TryTakeCard(out Card card))
            {
                _cards.Remove(card);
            }

            return card;
        }
        private void FillDeck()
        {
            List<string> _suit = new List<string>() { "♠", "♣", "♥", "♦" };
            List<string> _nominal = new List<string>() { "6", "7", "8", "9", "10", "J", "Q", "K", "T" };

            for (int i = 0; i < _suit.Count; i++)
            {
                for (int j = 0; j < _nominal.Count; j++)
                {
                    _cards.Add(new Card(_suit[i], _nominal[j]));
                }
            }
        }

        private bool TryTakeCard(out Card card)
        {
            card = null;

            if (_cards.Count > 0)
            {
                Random random = new Random();

                int randomIndex = random.Next(_cards.Count);
                card = _cards[randomIndex];

                return true;
            }

            Console.WriteLine("\nВ колоде нет карт!\n");

            return false;
        }
    }
}

public class Card
{
    private string _suit;
    private string _nominal;

    public Card(string suit, string nominal)
    {
        _suit = suit;
        _nominal = nominal;
    }

    public void ShowInfo()
    {
        Console.Write($"{_nominal} {_suit}|");
    }
}

public class Player
{
    private List<Card> _cards;

    public Player()
    {
        _cards = new List<Card>();
    }

    public int CardCounterInHand => _cards.Count;

    public void TakeCard(Card card)
    {
        if (card != null)
        {
            _cards.Add(card);
        }
        else
        {
            Console.WriteLine("Карт нет!");
        }
    }

    public void ShowCard()
    {
        for (int i = 0; i < _cards.Count; i++)
        {
            _cards[i].ShowInfo();
        }
    }
}