using DataLayer.Domain;
using DataLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Services.Services
{
    public class BorrowedBookServices : IBorrowedBookServices
    {
        private readonly IRepository<BorrowedBook> _borrowedBookRepository;

        public BorrowedBookServices(IRepository<BorrowedBook> repository)
        {
            _borrowedBookRepository = repository;
        }

        public IEnumerable<BorrowedBook> GetBorrowedBooks()
        {
            return _borrowedBookRepository.Table.ToList();
        }

        public void InsertBorrowedBook(BorrowedBook borrowed)
        {
            _borrowedBookRepository.Insert(borrowed);
        }


    }
}
