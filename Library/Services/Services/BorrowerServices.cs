using DataLayer.Domain;
using DataLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Services
{
    public class BorrowerServices : IBorrowerServices
    {
        private readonly IRepository<Borrower> _borrowerRepository;

        public BorrowerServices(IRepository<Borrower> borrowerRepository)
        {
            _borrowerRepository = borrowerRepository;
        }

        public void GetBorrower(Borrower borrower)
        {
            throw new NotImplementedException();
        }

        public Borrower GetBorrowerById(int id)
        {
            return _borrowerRepository.GetById(id);
        }

        public void NewBorrower(Borrower borrower)
        {
            _borrowerRepository.Insert(borrower);
        }
    }
}
