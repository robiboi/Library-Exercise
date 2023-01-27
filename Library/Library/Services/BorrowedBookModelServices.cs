using DataLayer.Interface;
using Library.Interface;
using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Services
{
    public class BorrowedBookModelServices : IBorrowedBookModelServices
    {
        private readonly IBorrowedBookServices _borrowedBookServices;
        private readonly IBorrowerServices _borrowerServices;
        private readonly IBookModelServices _bookModelServices;
        public BorrowedBookModelServices(IBorrowedBookServices borrowedBookServices, IBorrowerServices borrowerServices, IBookModelServices bookModelServices)
        {
            _borrowedBookServices = borrowedBookServices;
            _borrowerServices = borrowerServices;
            _bookModelServices = bookModelServices;
        }
        public BorrowedBookModel GetUnreturnedBorrowedBookDetailByBookId(int id)
        {
            var borrowedBooks = _borrowedBookServices.GetBorrowedBookByBookId(id);
            var borrowedBook = borrowedBooks.FirstOrDefault(b => !b.DateReturned.HasValue);
            var borrower = _borrowerServices.GetBorrowerById(borrowedBook.BorrowerId);

            BorrowedBookModel borrowedBookModel = new BorrowedBookModel();
            borrowedBookModel.BookId = borrowedBook.BookId;
            borrowedBookModel.BorrowerId = borrowedBook.BorrowerId;
            borrowedBookModel.Borrower = new BorrowerModel
            {
                Id = borrower.Id,
                Name = borrower.Name,
                Address = borrower.Address
            };
            borrowedBookModel.Book = _bookModelServices.GetBookModelByBookId(borrowedBook.BookId);
            borrowedBookModel.DateBorrowed = borrowedBook.DateBorrowed;
            borrowedBookModel.DateReturned = borrowedBook.DateReturned;
            borrowedBookModel.Id = borrowedBook.Id;

            return borrowedBookModel;
        }
    }
}
