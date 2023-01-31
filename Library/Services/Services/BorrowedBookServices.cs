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

        public BorrowedBookServices(IRepository<BorrowedBook> repository)
        {
            _borrowedBookRepository = repository;
        }

        public BorrowedBook GetBorrowedBookByBookId(int id)
        {
            return _borrowedBookRepository.Table.Where(x => x.BookId == id && !x.DateReturned.HasValue).FirstOrDefault();
        }

        public IEnumerable<BorrowedBook> GetBorrowedBooks()
        {
            return _borrowedBookRepository.Table.ToList();
        }

        public void InsertBorrowedBook(BorrowedBook borrowed)
        {
            _borrowedBookRepository.Insert(borrowed);
        }

        public void InsertBorrowedBooks(IEnumerable<BorrowedBook> borrowedBooks)
        {
            _borrowedBookRepository.Insert(borrowedBooks);
        }

        public bool IsBorrowed(int id)
        {
            return _borrowedBookRepository.Table.Where(x => x.Id == id).ToList().Exists(y => !y.DateReturned.HasValue);
        }

        public void UpdateBorrowedBook(BorrowedBook borrowed)
        {
            _borrowedBookRepository.Update(borrowed);
        }
    }
}
