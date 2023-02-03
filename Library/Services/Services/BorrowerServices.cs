using DataLayer.Domain;
using DataLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Services.Services
{
    public class BorrowerServices : IBorrowerServices
    {
        private readonly IRepository<Borrower> _borrowerRepository;
        public BorrowerServices(IRepository<Borrower> repository)
        {
            _borrowerRepository = repository;
        }

        public Borrower GetBorrowerById(int id)
        {
            return _borrowerRepository.GetById(id);
        }

        public IEnumerable<Borrower> GetBorrowers()
        {
            return _borrowerRepository.Table.ToList();
        }

        public void NewBorrower(Borrower borrower)
        {
            _borrowerRepository.Insert(borrower);

        }

    }
}
