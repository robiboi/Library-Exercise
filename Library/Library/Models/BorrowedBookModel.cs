using DataLayer.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models
{
    public class BorrowedBookModel
    {
        public int Id { get; set; }
        public BookModel Book { get; set; }
        public BorrowerModel Borrower { get; set; }
        public DateTime DateBorrowed { get; set; }
        public DateTime? DateReturned { get; set; }

    }
}
