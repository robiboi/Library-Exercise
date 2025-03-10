﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models
{
    public class BookModel
    {
        public int Id { get; set; }
        public string BookName { get; set; }
        public string Author { get; set; }
        public DateTime PublishedDate { get; set; }
        public bool Borrowed { get; set; }
        public BorrowerModel Borrower { get; set; }
    }
}