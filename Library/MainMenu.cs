using System;
using System.Globalization;

namespace Library
{
    public class MainMenu
    {
        private readonly LibraryService _service;

        public MainMenu()
        {
            var repository = new LibraryRepository();
            _service = new LibraryService(repository);
        }

        public void Start()
        {
            var showMenu = true;
            while (showMenu)
            {
                ShowMainMenuInstructions();
                switch (Console.ReadLine())
                {
                    case "1":
                        MainMenuCase1();
                        break;
                    case "2":
                        MainMenuCase2();
                        break;
                    case "3":
                        MainMenuCase3();
                        break;
                    case "4":
                        MainMenuCase4();
                        break;
                    case "5":
                        bool showFilterMenu = true;
                        while (showFilterMenu)
                        {
                            showFilterMenu = FilterMenu();
                        }
                        break;
                    case "6":
                        showMenu = false;
                        break;
                    default:
                        break;
                }
            }
        }

        private bool FilterMenu()
        {
            ShowFilterMenuInstructions();
            switch (Console.ReadLine())
            {
                case "1":
                    Console.WriteLine("Enter author");
                    var author = Console.ReadLine();
                    _service.FilterByAuthor(author);
                    Console.ReadKey();
                    return true;
                case "2":
                    Console.WriteLine("Enter category");
                    var category = Console.ReadLine();
                    _service.FilterByCategory(category);
                    Console.ReadKey();
                    return true;
                case "3":
                    Console.WriteLine("Enter language");
                    var language = Console.ReadLine();
                    _service.FilterByLanguage(language);
                    Console.ReadKey();
                    return true;
                case "4":
                    Console.WriteLine("Enter book isbn");
                    long isbn = Convert.ToInt64(Console.ReadLine());
                    _service.FilterByISBN(isbn);
                    Console.ReadKey();
                    return true;
                case "5":
                    Console.WriteLine("Enter book title");
                    var title = Console.ReadLine();
                    _service.FilterByName(title);
                    Console.ReadKey();
                    return true;
                case "6":
                    _service.FilterAvailableBooks();
                    Console.ReadKey();
                    return true;
                case "7":
                    return false;
                default:
                    return true;
            }
        }

        private void ShowMainMenuInstructions()
        {
            Console.Clear();
            Console.WriteLine("Greetings traveller,choose an option:");
            Console.WriteLine("1) Add new book to our library");
            Console.WriteLine("2) Delete book form our library");
            Console.WriteLine("3) Rent book form our library");
            Console.WriteLine("4) Return book tu our library");
            Console.WriteLine("5) Filter books by some parameters");
            Console.WriteLine("6) Exit");
            Console.Write("\r\nSelect an option: ");
        }

        private void ShowFilterMenuInstructions()
        {
            Console.Clear();
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1) Filter by author");
            Console.WriteLine("2) Filter by category");
            Console.WriteLine("3) Filter by language");
            Console.WriteLine("4) Filter by ISBN");
            Console.WriteLine("5) Filter by book title");
            Console.WriteLine("6) Find all available books");
            Console.WriteLine("7) Exit to main menu");
            Console.Write("\r\nSelect an option: ");
        }

        private void MainMenuCase1()
        {
            Console.Clear();
            Console.WriteLine("Enter book name");
            var name = Console.ReadLine();
            Console.WriteLine("Enter book author");
            var author = Console.ReadLine();
            Console.WriteLine("Enter book category");
            var category = Console.ReadLine();
            Console.WriteLine("Enter book language");
            var language = Console.ReadLine();
            Console.WriteLine("Enter book publication date. Ex: 1999-02-29");
            var date = DateTime.ParseExact(Console.ReadLine(), "yyyy-dd-MM", CultureInfo.InvariantCulture);
            Console.WriteLine("Enter book isbn");
            long isbn = Convert.ToInt64(Console.ReadLine());
            Console.WriteLine(_service.AddBook(name, author, category, language, date, isbn));
            Console.ReadKey();

        }
        private void MainMenuCase2()
        {
            Console.WriteLine("Enter book isbn for removing it form our library");
            long isbnDel = Convert.ToInt64(Console.ReadLine());
            Console.WriteLine(_service.DeleteBook(isbnDel));
            Console.ReadKey();
        }

        private void MainMenuCase3()
        {
            Console.WriteLine("Enter your name");
            var userName = Console.ReadLine();
            Console.WriteLine("enter the number of days");
            int days = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter book isbn you want to take");
            long isbnRent = Convert.ToInt64(Console.ReadLine());
            Console.WriteLine(_service.RentABook(userName, isbnRent, days));
            Console.ReadKey();
        }

        private void MainMenuCase4()
        {
            Console.WriteLine("Enter book isbn you want to return");
            long isbnReturn = Convert.ToInt64(Console.ReadLine());
            Console.WriteLine(_service.ReturnBook(isbnReturn));
            Console.ReadKey();

        }
    }
}
