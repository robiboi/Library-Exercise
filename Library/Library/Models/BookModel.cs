using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models
{
    public class BookModel
    {
        public int Id { get; set; }
        public String BookName { get; set; }
        public String Author { get; set; }
        public BorrowerModel Borrower { get; set; }
    }
}
