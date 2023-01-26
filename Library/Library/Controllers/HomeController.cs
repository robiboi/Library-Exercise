using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Domain;
using DataLayer.Interface;
using Microsoft.AspNetCore.Mvc;
using Library.Models;

namespace Library.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBookServices _bookServices;
        private readonly IBorrowerServices _borrowerServices;

        public HomeController(IBookServices bookService, IBorrowerServices borrowerServices)
        {
            _bookServices = bookService;
            _borrowerServices = borrowerServices;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Book()
        {
            BookModel bookModel = new BookModel();
            return View(bookModel);
        }

        [HttpPost]
        public IActionResult Book(BookModel model)
        {
            Book book = new Book
            {
                BookName = model.BookName,
                Author = model.Author
            };
            _bookServices.InsertBook(book);
            return View(model);
        }

        public IActionResult BookList()
        {
            var books = _bookServices.GetBooks();
            List<BookModel> bookModels = new List<BookModel>();
            foreach(var book in books)
            {
                BookModel bookModel = new BookModel
                {
                    Id = book.Id,
                    BookName = book.BookName,
                    Author = book.Author
                };
                bookModels.Add(bookModel);
            }
            return View(bookModels);
        }

        public IActionResult BookDetails(int id)
        {
            var book = _bookServices.GetBookById(id);
            BookModel bookModel = new BookModel
            {
                Id = book.Id,
                BookName = book.BookName,
                Author = book.Author
            };
            return View(bookModel);
        }
        
        public IActionResult Borrower()
        {
            BorrowerModel borrower = new BorrowerModel();
            return View(borrower);
        }

        [HttpPost]
       public IActionResult Borrower(BorrowerModel model)
        {
            Borrower borrower = new Borrower
            {
                BorrowerName = model.BorrowerName,
                Address = model.Address
            };
            _borrowerServices.NewBorrower(borrower);
            return View(model);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
