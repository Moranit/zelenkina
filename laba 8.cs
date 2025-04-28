using laba_8;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba_8
{
    [Serializable]
    public class Book
    {
        public int Id
        {
            get;
            set;
        }
        public string Title
        {
            get;
            set;
        }
        public string Author
        {
            get;
            set;
        }
        public int Year
        {
            get;
            set;
        }
        public string Genre
        {
            get;
            set;
        }
        public decimal Price
        {
            get;
            set;
        }
        public bool IsAvailable
        {
            get;
            set;
        }

        public Book()
        {
        }

        public Book(int id, string title, string author, int year, string genre, decimal price, bool isAvailable)
        {
            Id = id;
            Title = title;
            Author = author;
            Year = year;
            Genre = genre;
            Price = price;
            IsAvailable = isAvailable;
        }

        public override string ToString()
        {
            return $"{Id}. {Title} ({Author}, {Year}) - {Genre} | Цена: {Price:C} | {(IsAvailable ? "В наличии" : "Нет в наличии")}";
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba_8
{
    public static class BookDatabase
    {
        private static string filePath = "books.bin";


        public static List<Book> ReadDatabase()
        {
            if (!File.Exists(filePath))
            {
                return new List<Book>();
            }

            using (BinaryReader reader = new BinaryReader(File.Open(filePath, FileMode.Open)))
            {
                List<Book> books = new List<Book>();
                while (reader.BaseStream.Position < reader.BaseStream.Length)
                {
                    Book book = new Book
                    {
                        Id = reader.ReadInt32(),
                        Title = reader.ReadString(),
                        Author = reader.ReadString(),
                        Year = reader.ReadInt32(),
                        Genre = reader.ReadString(),
                        Price = reader.ReadDecimal(),
                        IsAvailable = reader.ReadBoolean()
                    };
                    books.Add(book);
                }
                return books;
            }
        }


        public static void WriteDatabase(List<Book> books)
        {
            using (BinaryWriter writer = new BinaryWriter(File.Open(filePath, FileMode.Create)))
            {
                foreach (Book book in books)
                {
                    writer.Write(book.Id);
                    writer.Write(book.Title);
                    writer.Write(book.Author);
                    writer.Write(book.Year);
                    writer.Write(book.Genre);
                    writer.Write(book.Price);
                    writer.Write(book.IsAvailable);
                }
            }
        }

        public static bool IdExists(int id)
        {
            var books = ReadDatabase();
            return books.Any(b => b.Id == id);
        }

        // Валидация данных книги
        public static string ValidateBook(Book book)
        {
            if (book.Id <= 0)
                return "ID должен быть положительным числом";

            if (string.IsNullOrWhiteSpace(book.Title))
                return "Название книги не может быть пустым";

            if (book.Title.Length > 100)
                return "Название книги слишком длинное (макс. 100 символов)";

            if (string.IsNullOrWhiteSpace(book.Author))
                return "Автор не может быть пустым";

            if (book.Year < 1800 || book.Year > DateTime.Now.Year + 1)
                return $"Год издания должен быть между 1800 и {DateTime.Now.Year + 1}";

            if (string.IsNullOrWhiteSpace(book.Genre))
                return "Жанр не может быть пустым";

            if (book.Price < 0 || book.Price > 100000)
                return "Цена должна быть между 0 и 100 000";

            return null; // Нет ошибок
        }
        public static void ViewAllBooks()
        {
            var books = ReadDatabase();
            if (books.Count == 0)
            {
                Console.WriteLine("Каталог книг пуст.");
                return;
            }

            Console.WriteLine("=== КАТАЛОГ КНИГ ===");
            foreach (var book in books)
            {
                Console.WriteLine(book);
            }
        }


        public static void AddBook(Book book)
        {
            var books = ReadDatabase();
            books.Add(book);
            WriteDatabase(books);
        }


        public static void DeleteBook(int id)
        {
            var books = ReadDatabase();
            books.RemoveAll(b => b.Id == id);
            WriteDatabase(books);
        }


        public static List<Book> GetBooksByGenre(string genre)
        {
            var books = ReadDatabase();
            return books.Where(b => b.Genre.Equals(genre, StringComparison.OrdinalIgnoreCase)).ToList();
        }


        public static List<Book> GetBooksByAuthor(string author)
        {
            var books = ReadDatabase();
            return books.Where(b => b.Author.Contains(author, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public static decimal GetAveragePrice()
        {
            var books = ReadDatabase();
            return books.Count > 0 ? books.Average(b => b.Price) : 0;
        }


        public static Book GetOldestBook()
        {
            var books = ReadDatabase();
            return books.OrderBy(b => b.Year).FirstOrDefault();
        }
    }
}
using laba_8;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;

        while (true)
        {
            Console.WriteLine("\n=== КАТАЛОГ КНИГ ===");
            Console.WriteLine("1. Просмотреть все книги");
            Console.WriteLine("2. Добавить книгу");
            Console.WriteLine("3. Удалить книгу");
            Console.WriteLine("4. Запросы");
            Console.WriteLine("5. Выход");
            Console.Write("Выберите действие: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    ViewAllBooks();
                    break;
                case "2":
                    AddBook();
                    break;
                case "3":
                    DeleteBook();
                    break;
                case "4":
                    QueriesMenu();
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Неверный выбор. Попробуйте снова.");
                    break;
            }
        }
    }

    static void ViewAllBooks()
    {
        Console.WriteLine("\n=== ВСЕ КНИГИ ===");
        BookDatabase.ViewAllBooks();

    }

    static void AddBook()
    {
        Console.WriteLine("\n=== ДОБАВЛЕНИЕ НОВОЙ КНИГИ ===");

        int id;
        while (true)
        {
            Console.Write("ID: ");
            if (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Ошибка: ID должен быть целым числом!");
                continue;
            }

            if (BookDatabase.IdExists(id))
            {
                Console.WriteLine("Ошибка: Книга с таким ID уже существует!");
                continue;
            }

            if (id <= 0)
            {
                Console.WriteLine("Ошибка: ID должен быть положительным числом!");
                continue;
            }

            break;
        }

        string title;
        while (true)
        {
            Console.Write("Название: ");
            title = Console.ReadLine().Trim();

            if (string.IsNullOrWhiteSpace(title))
            {
                Console.WriteLine("Ошибка: Название не может быть пустым!");
                continue;
            }

            if (title.Length > 100)
            {
                Console.WriteLine("Ошибка: Название слишком длинное (макс. 100 символов)!");
                continue;
            }

            break;
        }

        // Аналогичные проверки для других полей
        string author = GetValidatedInput("Автор: ",
            s => !string.IsNullOrWhiteSpace(s),
            "Автор не может быть пустым");

        int year = GetValidatedNumber("Год издания: ",
            y => y >= 1800 && y <= DateTime.Now.Year + 1,
            $"Год должен быть между 1800 и {DateTime.Now.Year + 1}");

        string genre = GetValidatedInput("Жанр: ",
            s => !string.IsNullOrWhiteSpace(s),
            "Жанр не может быть пустым");

        decimal price = GetValidatedDecimal("Цена: ",
            p => p >= 0 && p <= 100000,
            "Цена должна быть между 0 и 100 000");

        bool isAvailable = GetYesNoInput("В наличии (да/нет): ");

        Book newBook = new Book(id, title, author, year, genre, price, isAvailable);
        var validationError = BookDatabase.ValidateBook(newBook);

        if (validationError != null)
        {
            Console.WriteLine($"Ошибка валидации: {validationError}");
            return;
        }

        BookDatabase.AddBook(newBook);
        Console.WriteLine("Книга успешно добавлена!");
    }
    static string GetValidatedInput(string prompt, Func<string, bool> validator, string errorMessage)
    {
        while (true)
        {
            Console.Write(prompt);
            string input = Console.ReadLine().Trim();

            if (validator(input))
                return input;

            Console.WriteLine($"Ошибка: {errorMessage}");
        }
    }

    static int GetValidatedNumber(string prompt, Func<int, bool> validator, string errorMessage)
    {
        while (true)
        {
            Console.Write(prompt);
            if (!int.TryParse(Console.ReadLine(), out int number))
            {
                Console.WriteLine("Ошибка: Введите целое число!");
                continue;
            }

            if (validator(number))
                return number;

            Console.WriteLine($"Ошибка: {errorMessage}");
        }
    }

    static decimal GetValidatedDecimal(string prompt, Func<decimal, bool> validator, string errorMessage)
    {
        while (true)
        {
            Console.Write(prompt);
            if (!decimal.TryParse(Console.ReadLine(), out decimal number))
            {
                Console.WriteLine("Ошибка: Введите число!");
                continue;
            }

            if (validator(number))
                return number;

            Console.WriteLine($"Ошибка: {errorMessage}");
        }
    }

    static bool GetYesNoInput(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string input = Console.ReadLine().Trim().ToLower();

            if (input == "да" || input == "д") return true;
            if (input == "нет" || input == "н") return false;

            Console.WriteLine("Ошибка: Введите 'да' или 'нет'!");
        }
    }
    static void DeleteBook()
    {
        Console.WriteLine("\n=== УДАЛЕНИЕ КНИГИ ===");
        BookDatabase.ViewAllBooks();

        if (!BookDatabase.ReadDatabase().Any())
        {
            Console.WriteLine("Нет книг для удаления!");
            return;
        }

        int id;
        while (true)
        {
            Console.Write("Введите ID книги для удаления (0 для отмены): ");
            if (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Ошибка: Введите целое число!");
                continue;
            }

            if (id == 0) return;

            if (!BookDatabase.IdExists(id))
            {
                Console.WriteLine("Ошибка: Книга с таким ID не найдена!");
                continue;
            }

            break;
        }

        Console.Write($"Вы уверены, что хотите удалить книгу с ID {id}? (да/нет): ");
        if (Console.ReadLine().Trim().ToLower() != "да")
        {
            Console.WriteLine("Удаление отменено.");
            return;
        }

        BookDatabase.DeleteBook(id);
        Console.WriteLine("Книга успешно удалена!");
    }

    static void QueriesMenu()
    {
        while (true)
        {
            Console.WriteLine("\n=== ЗАПРОСЫ ===");
            Console.WriteLine("1. Книги определенного жанра");
            Console.WriteLine("2. Книги автора");
            Console.WriteLine("3. Средняя цена книг");
            Console.WriteLine("4. Самая старая книга");
            Console.WriteLine("5. Назад");
            Console.Write("Выберите запрос: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Введите жанр: ");
                    string genre = Console.ReadLine();
                    var booksByGenre = BookDatabase.GetBooksByGenre(genre);
                    Console.WriteLine($"\nКниги жанра '{genre}':");
                    foreach (var book in booksByGenre)
                    {
                        Console.WriteLine(book);
                    }
                    break;
                case "2":
                    SearchByAuthor();
                    break;
                case "3":
                    decimal avgPrice = BookDatabase.GetAveragePrice();
                    Console.WriteLine($"\nСредняя цена книг: {avgPrice:C}");
                    break;
                case "4":
                    var oldestBook = BookDatabase.GetOldestBook();
                    Console.WriteLine("\nСамая старая книга:");
                    Console.WriteLine(oldestBook != null ? oldestBook.ToString() : "Каталог пуст");
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Неверный выбор. Попробуйте снова.");
                    break;
            }

            static void SearchByAuthor()
            {
                string author = GetValidatedInput("Введите автора для поиска: ",
                    s => !string.IsNullOrWhiteSpace(s),
                    "Автор не может быть пустым");

                var books = BookDatabase.GetBooksByAuthor(author);
                if (books.Count == 0)
                {
                    Console.WriteLine($"Книг автора '{author}' не найдено.");
                }
                else
                {
                    Console.WriteLine($"\nНайдено {books.Count} книг автора '{author}':");
                    foreach (var book in books)
                    {
                        Console.WriteLine(book);
                    }
                }
                Console.ReadKey();
            }


        }
    }
}
