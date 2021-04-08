using System;
using Xunit;
using Library;
using FakeItEasy;

namespace Library.UnitTests
{
    public class LibraryServiceTests
    {
        private readonly LibraryService _libraryService;
        private readonly LibraryRepository _libraryRepository;

        public LibraryServiceTests()
        {
            _libraryRepository = A.Fake<LibraryRepository>();
            _libraryService = new LibraryService(_libraryRepository);
        }

        [Fact]
        public void ReturnBook_NoBook_NoBookMessage()
        {
            long isbn = 4564867671687;

            var callToGetBook = A.CallTo(() => _libraryRepository.GetBookByISBN(isbn));
            callToGetBook.Returns(null);

            var result = _libraryService.ReturnBook(isbn);
            Assert.Equal("We never had this book, but you can add it our library, select another menu option", result);
        }

        [Fact]
        public void ReturnBook_BookNotAvailable_NotAvailableMessage()
        {
            long isbn = 4564867671687;
            var book = new Book("asd", "asdasd", "asdasd", "asdsasd", DateTime.Now, isbn);
            var callToGetBook = A.CallTo(() => _libraryRepository.GetBookByISBN(isbn));
            callToGetBook.Returns(book);

            var result = _libraryService.ReturnBook(isbn);
            Assert.Equal("This book is returned allready", result);
            callToGetBook.MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void ReturnBook_Success_SuccessMessage()
        {
            long isbn = 4564867671687;
            var book = new Book("asd", "asdasd", "asdasd", "asdsasd", DateTime.Now, isbn)
            {
                IsAvailable = false,
                ReturnDate = DateTime.Today
            };
            var callToGetBook = A.CallTo(() => _libraryRepository.GetBookByISBN(isbn));
            callToGetBook.Returns(book);

            var callToUpdateBook = A.CallTo(() => _libraryRepository.UpdateBook(A<Book>.That.Matches(s =>
            s.Name == book.Name &&
            s.ISBN == book.ISBN &&
            s.IsAvailable == true &&
            s.UserName == null &&
            s.ReturnDate == DateTime.MinValue)));

            var result = _libraryService.ReturnBook(isbn);
            Assert.Equal("Book returned successfully", result);
            callToGetBook.MustHaveHappenedOnceExactly();
            callToUpdateBook.MustHaveHappenedOnceExactly();
        }
    }
}
