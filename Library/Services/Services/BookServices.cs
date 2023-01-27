using DataLayer.Domain;
using DataLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Services.Services
{
    public class BookServices : IBookServices
    {
        private readonly IRepository<Book> _bookRepository;
        public BookServices(IRepository<Book> repository)
        {
            _bookRepository = repository;
        }

        public void InsertBook(Book book)
        {
            _bookRepository.Insert(book);
        }

        public IEnumerable<Book> GetBooks()
        {
            return _bookRepository.Table.ToList();
        }

        public bool EditBook(Book book)
        {
            if (book != null)
            {
                _bookRepository.Update(book);
                return true;
            }
            return false;
        }

        public bool DeleteBook(int id)
        {
            var book = _bookRepository.GetById(id);
            if(book != null)
            {
                _bookRepository.Delete(book);
                return true;
            }
            return false;
        }

        public Book GetBookById(int id)
        {
            return _bookRepository.GetById(id);
        }

        public bool IsAvailable(int id)
        {
            throw new NotImplementedException();
        }
    }
}
