using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Domain
{
    public class BorrowedBook : BaseEntity
    {
        public int BookId { get; set; }
        public int BorrowerId { get; set; }
        public DateTime DateBorrowed { get; set; }
        public DateTime DateReturned { get; set; }

    }
}
