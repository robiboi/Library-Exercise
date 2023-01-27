using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Interface
{
    public interface IBorrowedBookModelServices
    {
        BorrowedBookModel GetUnreturnedBorrowedBookDetailByBookId(int id);
    }
}
