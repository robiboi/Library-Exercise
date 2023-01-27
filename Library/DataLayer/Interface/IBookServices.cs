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
        bool EditBook(Book book);
        bool DeleteBook(int id);
        Book GetBookById(int id);
        bool IsAvailable(int id);
    }
}
