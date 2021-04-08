using System;

namespace Library
{
    public class Book
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public string Category { get; set; }
        public string Language { get; set; }
        public DateTime PublicationDate { get; set; }
        public long ISBN { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime TakenDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public string UserName { get; set; }

        public Book(string name, string author, string category, string language, DateTime publicationDate, long isbn)
        {
            Name = name;
            Author = author;
            Category = category;
            Language = language;
            PublicationDate = publicationDate;
            ISBN = isbn;
            IsAvailable = true;
        }
    }
}
