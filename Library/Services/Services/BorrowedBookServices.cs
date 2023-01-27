using DataLayer.Domain;
using DataLayer.Interface;
using System;
using System.Collections.Generic;
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

        public void InsertBorrowedBook(BorrowedBook borrowed)
        {
            _borrowedBookRepository.Insert(borrowed);
        }


    }
}
