﻿using DataLayer.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Interface
{
    public interface IBorrowerServices
    {
        void NewBorrower(Borrower borrower);
        Borrower GetBorrowerById(int id);
        IEnumerable<Borrower> GetBorrowers();
    }
}
