using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models
{
    public class BorrowFormModel
    {
        public List<int> BookIds { get; set; }
        public BorrowerModel Borrower { get; set; }
    }
}