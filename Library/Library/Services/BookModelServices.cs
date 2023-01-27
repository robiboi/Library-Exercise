using DataLayer.Interface;
using Library.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.Models;

namespace Library.Services
{
    public class BookModelServices : IBookModelServices
    {
        private readonly IBookServices _bookServices;
        private readonly IBorrowedBookServices _borrowedBookServices;
        public BookModelServices(IBookServices bookServices, IBorrowedBookServices borrowedBookServices)
        {
            _bookServices = bookServices;
            _borrowedBookServices = borrowedBookServices;
        }
        public BookModel GetBookModelByBookId(int id)
        {
            var book = _bookServices.GetBookById(id);
            var borrowedBook = _borrowedBookServices.GetBorrowedBookByBookId(id);
            bool isAvailable = !(borrowedBook.Count() > 0 && borrowedBook.ToList().Exists(e => !e.DateReturned.HasValue));

            BookModel bookModel = new BookModel(book.Id);
            bookModel.BookName = book.BookName;
            bookModel.Author = book.Author;
            bookModel.IsAvailable = isAvailable;

            return bookModel;
        }
    }
}
