using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models
{
    public class BorrowFormModel
    {
        public List<int> BookIds { get; set; }
        public string BorrowerName { get; set; }
        public string Address { get; set; }
    }
}