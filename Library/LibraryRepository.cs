using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace Library
{
    public class LibraryRepository
    {
        private const string Path = @"c:\\Users\useris\Downloads\c#\library.json";
        public void AddBook(Book book)
        {
            string booksJson = File.ReadAllText(Path);
            List<Book> booksList = JsonConvert.DeserializeObject<List<Book>>(booksJson);
            booksList.Add(book);
            string newJson = JsonConvert.SerializeObject(booksList, Formatting.Indented);
            File.WriteAllText(Path, newJson);
        }

        public void DeleteBookByISBN(long isbn)
        {
            string booksJson = File.ReadAllText(Path);
            List<Book> booksList = JsonConvert.DeserializeObject<List<Book>>(booksJson);
            booksList.Remove(booksList.SingleOrDefault(x => x.ISBN == isbn));
            string newJson = JsonConvert.SerializeObject(booksList, Formatting.Indented);
            File.WriteAllText(Path, newJson);
        }

        public List<Book> GetBooksList()
        {
            string booksJson = File.ReadAllText(Path);
            List<Book> booksList = JsonConvert.DeserializeObject<List<Book>>(booksJson);
            return booksList;
        }

        // virtual only for testing purposes
        public virtual Book GetBookByISBN(long isbn)
        {
            string booksJson = File.ReadAllText(Path);
            List<Book> booksList = JsonConvert.DeserializeObject<List<Book>>(booksJson);
            return booksList.SingleOrDefault(x => x.ISBN == isbn);
        }

        public virtual void UpdateBook(Book book)
        {
            string booksJson = File.ReadAllText(Path);
            List<Book> booksList = JsonConvert.DeserializeObject<List<Book>>(booksJson);
            booksList[booksList.FindIndex(x => x.ISBN == book.ISBN)] = book;
            string newJson = JsonConvert.SerializeObject(booksList, Formatting.Indented);
            File.WriteAllText(Path, newJson);
        }
    }
}
