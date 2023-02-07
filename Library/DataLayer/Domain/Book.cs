using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Domain
{
    public class Book : BaseEntity
    {
        public string BookName { get; set; }
        public string Author { get; set; }
        public DateTime PublishedDate { get; set; }
    }
}