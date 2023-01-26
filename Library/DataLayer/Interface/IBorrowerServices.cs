using DataLayer.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Interface
{
    public interface IBorrowerServices
    {
        void NewBorrower(Borrower borrower);
        void GetBorrower(Borrower borrower);

    }
}
