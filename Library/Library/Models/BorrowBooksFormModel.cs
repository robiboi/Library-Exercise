using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models
{
    public class BorrowBooksFormModel
    {
        public List<int> Ids { get; set; }
        public BorrowerModel borrower { get; set; }
    }
}
