using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Domain
{
    public class Borrower : BaseEntity
    {
        public String Name { get; set; }
        public String Address { get; set; }
    }
}
