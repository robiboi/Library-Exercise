using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models
{
    public class BorrowedBooksModel
    {
        public List<BookModel> Books { get; set; }
        public BorrowerModel Borrower { get; set; }
    }
}