using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.Models;

namespace Library.Interface
{
    public interface IBookModelServices
    {
        BookModel GetBookModelByBookId(int id);
    }
}
