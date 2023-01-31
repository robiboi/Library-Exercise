using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models
{
    public class BooksBorrowedModel
    {
        public List<BookModel> Books { get; set; } = new List<BookModel>();
        public BorrowerModel Borrower { get; set; }
    }
}
