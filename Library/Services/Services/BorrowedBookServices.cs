using DataLayer.Domain;
using DataLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Services.Services
{
    public class BorrowedBookServices : IBorrowedBookServices
    {
        private readonly IRepository<BorrowedBook> _borrowedBookRepository;

        public BorrowedBookServices(IRepository<BorrowedBook> borrowedBookRepository)
        {
            _borrowedBookRepository = borrowedBookRepository;
        }

        public BorrowedBook GetBorrowedBookById(int id)
        {
            var borrowedBook = _borrowedBookRepository.GetById(id);
            return borrowedBook;
        }

        public IEnumerable<BorrowedBook> GetBorrowedBookByBookId(int bookId)
        {
            var borrowedBooks = _borrowedBookRepository.Table.Where(b => b.BookId == bookId);
            return borrowedBooks;
        }

        public void InsertBorrowedBook(BorrowedBook borrowedBook)
        {
            _borrowedBookRepository.Insert(borrowedBook);
        }

        public bool IsBookAvailable(int bookId)
        {
            var borrowedBook = _borrowedBookRepository.Table.Where(b => b.BookId == bookId);
            return borrowedBook.ToList().Exists(e => !e.DateReturned.HasValue);
        }

        public void UpdateBorrowedBook(BorrowedBook borrowedBook)
        {
            _borrowedBookRepository.Update(borrowedBook);
        }
    }
}
