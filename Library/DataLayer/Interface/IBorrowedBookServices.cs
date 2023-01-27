using DataLayer.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Interface
{
    public interface IBorrowedBookServices
    {
        bool IsBookAvailable(int bookId);
        BorrowedBook GetBorrowedBookById(int id);
        IEnumerable<BorrowedBook> GetBorrowedBookByBookId(int bookId);
        void InsertBorrowedBook(BorrowedBook borrowedBook);
        void UpdateBorrowedBook(BorrowedBook borrowedBook);
    }
}
