using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Domain
{
    public class Book : BaseEntity
    {
        public string BookName { get; set; }
        public String Author { get; set; }
    }
}
