using System;
using System.Collections.Generic;

namespace Library
{
    internal class StorageBook
    {
        static void Main(string[] args)
        {
            const string CommandAddBook = "1";
            const string CommandDeleteBook = "2";
            const string CommandShowAllBooks = "3";
            const string CommandSearchBookByName = "4";
            const string CommandSearchBooksByAuthor = "5";
            const string CommandSearchBookByYear = "6";
            const string CommandSearchBookByPageCount = "7";
            const string CommandExit = "8";

            Library library = new Library();

            bool isProgramOperation = true;

            while (isProgramOperation)
            {
                Console.WriteLine($"Меню Библиотеки:\n{CommandAddBook} - Добавить книгу в библиотеку.");
                Console.WriteLine($"{CommandDeleteBook} - Убрать книгу из библиотекии.");
                Console.WriteLine($"{CommandShowAllBooks} - Показать все книги.");
                Console.WriteLine($"{CommandSearchBookByName} - Найти книгу по названию.");
                Console.WriteLine($"{CommandSearchBooksByAuthor} - Найти книгу по автору.");
                Console.WriteLine($"{CommandSearchBookByYear} - Найти книгу по дате издания.");
                Console.WriteLine($"{CommandSearchBookByPageCount} - Найти книгу по количеству страниц.");
                Console.WriteLine($"{CommandExit} - Выйти из библиотеки.");

                Console.Write("Выберите интересующий пункт и окунитесь в увлекательный мир чтения: ");
                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case CommandAddBook:
                        library.AddBook();
                        break;

                    case CommandDeleteBook:
                        library.RemoveBook();
                        break;

                    case CommandShowAllBooks:
                        library.ShowAllBooks();
                        break;

                    case CommandSearchBookByName:
                        library.SearchBooksByName();
                        break;

                    case CommandSearchBooksByAuthor:
                        library.SearchBooksByAuthor();
                        break;

                    case CommandSearchBookByYear:
                        library.SearchBookByYear();
                        break;

                    case CommandSearchBookByPageCount:
                        library.SearchBookByPageCount();
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
            Console.Clear();
        }
    }

    class Library
    {
        private List<Book> _books = new List<Book>();

        private bool IsEmpty => _books.Count == 0;

        public void AddBook()
        {
            Console.Write("Введите название книги: ");
            string nameBook = Console.ReadLine();
            Console.Write("Введите автора книги: ");
            string authorBook = Console.ReadLine();

            int year = GetNumber("Введите год выпуска книги: ");
            int pageCountBook = GetNumber("Введите количество страниц книги: ");

            Console.Clear();

            _books.Add(new Book(nameBook, authorBook, year, pageCountBook));
        }

        public void ShowAllBooks()
        {
            Console.Clear();

            for (int i = 0; i < _books.Count; i++)
            {
                _books[i].ShowInfo();
            }
        }

        public void RemoveBook()
        {
            if (IsEmpty)
            {
                return;
            }

            if (TryGetBook(out Book book))
            {
                _books.Remove(book);
            }
        }

        public void SearchBooksByName()
        {
            if (IsEmpty)
            {
                return;
            }

            Console.Write("Введите название книги: ");
            string userInput = Console.ReadLine();

            for (int i = 0; i < _books.Count; i++)
            {
                if (userInput.ToLower() == _books[i].Name.ToLower())
                {
                    _books[i].ShowInfo();
                }
            }
        }

        public void SearchBooksByAuthor()
        {
            if (IsEmpty)
            {
                return;
            }

            Console.Write("Введите автора: ");
            string userInput = Console.ReadLine();

            for (int i = 0; i < _books.Count; i++)
            {
                if (userInput.ToLower() == _books[i].Author.ToLower())
                {
                    _books[i].ShowInfo();
                }
            }
        }

        public void SearchBookByYear()
        {
            if (IsEmpty)
            {
                return;
            }

            int userInput = GetNumber("Введите год издания: ");

            for (int i = 0; i < _books.Count; i++)
            {
                if (userInput == _books[i].Year)
                {
                    _books[i].ShowInfo();
                }
            }
        }

        public void SearchBookByPageCount()
        {
            if (IsEmpty)
            {
                return;
            }

            int userInput = GetNumber("Введите количество: ");

            for (int i = 0; i < _books.Count; i++)
            {
                if (userInput == _books[i].PageСount)
                {
                    _books[i].ShowInfo();
                }
            }
        }

        private bool TryGetBook(out Book book)
        {
            book = null;

            int userInput = GetNumber("Введите индекс: ");
            int index = userInput - 1;

            if (index >= 0 && index < _books.Count)
            {
                book = _books[index];
                return true;
            }

            Console.WriteLine("Книги под таким индексом, не существует.");
            return false;
        }

        private int GetNumber(string message)
        {
            int result;

            do
            {
                Console.Write(message);
            } while (int.TryParse(Console.ReadLine(), out result) == false);

            return result;
        }
    }

    class Book
    {
        public Book(string name, string author, int year, int pageСount)
        {
            Author = author;
            Name = name;
            Year = year;
            PageСount = pageСount;
        }

        public string Author { get; private set; }
        public string Name { get; private set; }
        public int Year { get; private set; }
        public int PageСount { get; private set; }

        public void ShowInfo()
        {
            Console.WriteLine($"Название книги: {Name}. Автор: {Author}. Год издания: {Year}. " +
                $"Количество страниц: {PageСount}.");
        }
    }
}