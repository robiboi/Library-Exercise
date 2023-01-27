using DataLayer.Domain;
using DataLayer.Interface;
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
        public bool IsAvailable { get; set; }

        public BookModel()
        {

        }

        public BookModel(int id)
        {
            this.Id = id;
        }
    }
}
