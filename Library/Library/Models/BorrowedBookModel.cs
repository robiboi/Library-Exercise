using DataLayer.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models
{
    public class BorrowedBookModel
    {
        public int Id { get; set; }
        public BookModel Book { get; set; }
        public BorrowerModel Borrower { get; set; }
        public DateTime BorrowedDate { get; set; }
        public DateTime ReturnedDate { get; set; }
    }
}
