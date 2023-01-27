using DataLayer.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Interface
{
    public interface IBorrowedBookServices
    {
        void InsertBorrowedBook(BorrowedBook borrowed);
    }
}
