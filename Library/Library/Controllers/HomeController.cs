using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Domain;
using DataLayer.Interface;
using Library.Interface;
using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Library.Models;

namespace Library.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBookServices _bookServices;
        private readonly IBookModelServices _bookModelServices;
        private readonly IBorrowedBookServices _borrowedBookService;
        private readonly IBorrowerServices _borrowerService;
        private readonly IBorrowedBookModelServices _borrowedBookModelServices;

        public HomeController(IBookServices bookService, 
            IBookModelServices bookModelServices, 
            IBorrowedBookServices borrowedBookServices, 
            IBorrowerServices borrowerServices,
            IBorrowedBookModelServices borrowedBookModelServices)
        {
            _bookServices = bookService;
            _bookModelServices = bookModelServices;
            _borrowedBookService = borrowedBookServices;
            _borrowerService = borrowerServices;
            _borrowedBookModelServices = borrowedBookModelServices;
        }

        public IActionResult Index()
        {
            var books = _bookServices.GetBooks();
            List<BookModel> bookModels = new List<BookModel>();
            foreach (var book in books)
            {
                BookModel bookModel = _bookModelServices.GetBookModelByBookId(book.Id);
                bookModels.Add(bookModel);
            }
            return View(bookModels);
        }

        public IActionResult EditBook(int id)
        {
            var book = _bookServices.GetBookById(id);
            BookModel model = new BookModel(book.Id);
            model.BookName = book.BookName;
            model.Author = book.Author;
            return View(model);
        }

        [HttpPost]
        public IActionResult EditBook(BookModel model)
        {
            Book book = _bookServices.GetBookById(model.Id);
            book.BookName = model.BookName;
            book.Author = model.Author;
            _bookServices.EditBook(book);
            return RedirectToAction("Index");
        }

        public IActionResult DeleteBook(int id)
        {
            _bookServices.DeleteBook(id);
            return RedirectToAction("Index");
        }

        public IActionResult Book()
        {
            BookModel bookModel = new BookModel();
            return View(bookModel);
        }

        [HttpPost]
        public IActionResult Book(BookModel model)
        {
            Book book = new Book();
            book.BookName = model.BookName;
            book.Author = model.Author;
            _bookServices.InsertBook(book);
            return RedirectToAction("Index");
        }

        public IActionResult BookList()
        {
            var books = _bookServices.GetBooks();
            List<BookModel> bookModels = new List<BookModel>();
            foreach(var book in books)
            {
                BookModel bookModel = new BookModel();
                bookModel.BookName = book.BookName;
                bookModels.Add(bookModel);
            }
            return View(bookModels);
        }

        public IActionResult BorrowBook(int id)
        {
            BorrowedBookModel borrowedBookModel = new BorrowedBookModel();
            BookModel bookModel = _bookModelServices.GetBookModelByBookId(id);
            borrowedBookModel.Book = bookModel;
            return View(borrowedBookModel);
        }

        [HttpPost]
        public IActionResult BorrowBook(BorrowedBookModel model)
        {
            Borrower borrower = new Borrower();
            borrower.Name = model.Borrower.Name;
            borrower.Address = model.Borrower.Address;
            _borrowerService.NewBorrower(borrower);

            BorrowedBook borrowedBook = new BorrowedBook();
            borrowedBook.BookId = model.Book.Id;
            borrowedBook.BorrowerId = borrower.Id;
            borrowedBook.DateBorrowed = DateTime.Now;

            _borrowedBookService.InsertBorrowedBook(borrowedBook);

            return RedirectToAction("Index");
        }

        public IActionResult BorrowBookDetail(int id)
        {
            BorrowedBookModel borrowedBookModel = _borrowedBookModelServices.GetUnreturnedBorrowedBookDetailByBookId(id);
            return View(borrowedBookModel);
        }

        public IActionResult ReturnBook(int id)
        {
            var borrowedBook = _borrowedBookService.GetBorrowedBookById(id);
            borrowedBook.DateReturned = DateTime.Now;
            _borrowedBookService.UpdateBorrowedBook(borrowedBook);
            return RedirectToAction("Index");
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
