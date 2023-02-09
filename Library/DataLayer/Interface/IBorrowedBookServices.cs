using DataLayer.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Interface
{
    public interface IBorrowedBookServices
    {
        void InsertBorrowedBook(BorrowedBook borrowed);
        void InsertBorrowedBooks(IEnumerable<BorrowedBook> borrowedBooks);
        IEnumerable<BorrowedBook> GetBorrowedBooks();
        bool IsBorrowed(int id);
        void UpdateBorrowedBook(BorrowedBook borrowed);
        BorrowedBook GetBorrowedBookByBookId(int id);
        void InsertBorrowedBooks(IEnumerable<BorrowedBook> borrowedBooks);
    }
}
