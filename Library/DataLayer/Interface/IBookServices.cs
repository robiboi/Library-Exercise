using DataLayer.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Interface
{
    public interface IBookServices
    {
        void InsertBook(Book book);
        IEnumerable<Book> GetBooks();
    }
}
