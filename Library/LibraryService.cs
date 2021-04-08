
using System;
using System.Collections.Generic;
using System.Linq;

namespace Library
{
    public class LibraryService
    {
        private readonly LibraryRepository _repository;

        public LibraryService(LibraryRepository repository)
        {
            _repository = repository;
        }

        public string RentABook(string username, long isbn, int days)
        {
            if (GetUsersBooksCount(username) >= 3)
                return "you have to many books already";
            if (days >= 61)
                return "sorry, you can not take book for so long";
            var book = _repository.GetBookByISBN(isbn);
            if (book == null)
                return "No Book";
            if (!book.IsAvailable)
                return "Book is not available";
            book.IsAvailable = false;
            book.TakenDate = DateTime.Now;
            book.ReturnDate = CountDateTime(days);
            book.UserName = username;
            _repository.UpdateBook(book);
            return "Book taken successfully";
        }

        public string ReturnBook(long isbn)
        {
            var book = _repository.GetBookByISBN(isbn);
            if (book == null)
                return "We never had this book, but you can add it our library, select another menu option";
            if (book.IsAvailable)
                return "This book is returned allready";
            if (CountDays(book.ReturnDate) < 0)
                return "in so much time you have probably memorized it,anyway book returned successfully";
            book.IsAvailable = true;
            book.TakenDate = DateTime.MinValue;
            book.ReturnDate = DateTime.MinValue;
            book.UserName = null;
            _repository.UpdateBook(book);
            return "Book returned successfully";
        }

        public string AddBook(string name, string author, string category, string language, DateTime publicationDate, long isbn)
        {
            if (_repository.GetBooksList().Exists(book => book.ISBN == isbn))
                return "book already exists";
            Book newBook = new Book(name, author, category, language, publicationDate, isbn);
            _repository.AddBook(newBook);
            return "Book added successfully";

        }

        public string DeleteBook(long isbn)
        {
            if (!_repository.GetBooksList().Exists(book => book.ISBN == isbn))
                return "we do not have this book";
            _repository.DeleteBookByISBN(isbn);
            return "book deleted successfully";
        }

        public void FilterByCategory(string category)
        {
            if (!_repository.GetBooksList().Exists(book => book.Category == category))
                Console.WriteLine("we do not have books in this category");
            var filteredList = _repository.GetBooksList().Where(book => book.Category == category).ToList();
            WriteFilteredList(filteredList);
        }

        public void FilterByAuthor(string author)
        {
            if (!_repository.GetBooksList().Exists(book => book.Author == author))
                Console.WriteLine("we do not have books by this author");
            var filteredList = _repository.GetBooksList().Where(book => book.Author == author).ToList();
            WriteFilteredList(filteredList);
        }

        public void FilterByLanguage(string language)
        {
            if (!_repository.GetBooksList().Exists(book => book.Language == language))
                Console.WriteLine("we do not have books in this language");
            var filteredList = _repository.GetBooksList().Where(book => book.Language == language).ToList();
            WriteFilteredList(filteredList);
        }

        public void FilterByISBN(long isbn)
        {
            if (!_repository.GetBooksList().Exists(book => book.ISBN == isbn))
                Console.WriteLine("we do not have books with ISBN like this");
            var filteredList = _repository.GetBooksList().Where(book => book.ISBN == isbn).ToList();
            WriteFilteredList(filteredList);
        }

        public void FilterByName(string name)
        {
            if (!_repository.GetBooksList().Exists(book => book.Name == name))
                Console.WriteLine("we do not have books with this title");
            var filteredList = _repository.GetBooksList().Where(book => book.Name == name).ToList();
            WriteFilteredList(filteredList);
        }

        public void FilterAvailableBooks()
        {
            if (!_repository.GetBooksList().Exists(book => book.IsAvailable == true))
                Console.WriteLine("no books available at the moment");
            var filteredeList = _repository.GetBooksList().Where(book => book.IsAvailable == true).ToList();
            WriteFilteredList(filteredeList);
        }

        public void WriteFilteredList(List<Book> filteredList)
        {
            foreach (var book in filteredList)
            {
                Console.WriteLine("                         ");
                Console.WriteLine("-------------------------");
                Console.WriteLine($"name: {book.Name} ");
                Console.WriteLine($"author: {book.Author} ");
                Console.WriteLine($"category: {book.Category} ");
                Console.WriteLine($"language: {book.Language} ");
                Console.WriteLine($"publication date: {book.PublicationDate.ToString("dd/MM/yyyy HH:mm:ss")} ");
                Console.WriteLine($"ISBN: {book.ISBN.ToString()} ");
                Console.WriteLine($"is available: {book.IsAvailable.ToString()} ");
                Console.WriteLine("                         ");
                Console.WriteLine("-------------------------");
                Console.WriteLine("                         ");
            }
        }

        public int GetUsersBooksCount(string username)
        {
            var bookList = _repository.GetBooksList();
            return bookList.FindAll(book => book.UserName == username).Count;
        }

        public int CountDays(DateTime date)
        {
            return (date - DateTime.Today).Days;
        }

        public DateTime CountDateTime(int days)
        {
            return DateTime.Today.AddDays(days);
        }
    }
}
