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
        private readonly IBorrowedBookServices _borrowedBookServices;

        public HomeController(IBookServices bookService, IBorrowerServices borrowerServices, IBorrowedBookServices borrowedBookServices)
        {
            _bookServices = bookService;
            _borrowerServices = borrowerServices;
            _borrowedBookServices = borrowedBookServices;
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
            var books = _bookServices.GetBooks();
            var borrowedBooks = _borrowedBookServices.GetBorrowedBooks().ToList();

            List<BookModel> bookModels = new List<BookModel>();
            foreach (var b in books)
            {
                BookModel bookModel = new BookModel
                {
                    Id = b.Id,
                    BookName = b.BookName,
                    Author = b.Author,
                    Borrowed = false
                };
                if (borrowedBooks.Exists(x => x.BookId == b.Id && !x.DateReturned.HasValue))
                {
                    bookModel.Borrowed = true;
                }
                bookModels.Add(bookModel);

            }

            return PartialView("BookList", bookModels);
        }

        public IActionResult BookList()
        {
            var books = _bookServices.GetBooks();
            var borrowedBooks = _borrowedBookServices.GetBorrowedBooks().ToList();

            List<BookModel> bookModels = new List<BookModel>();
            foreach(var book in books)
            {
                BookModel bookModel = new BookModel
                {
                    Id = book.Id,
                    BookName = book.BookName,
                    Author = book.Author,
                    Borrowed = false
                };
                if (borrowedBooks.Exists(x => x.BookId == book.Id && !x.DateReturned.HasValue))
                {
                    bookModel.Borrowed = true;
                }
                bookModels.Add(bookModel);

            }
            return PartialView(bookModels);
        }

        public IActionResult BorrowedBookList()
        {
            var borrowedBooks = _borrowedBookServices.GetBorrowedBooks();
            List<BorrowedBookModel> borrowedBookModels = new List<BorrowedBookModel>();

            foreach (var borrowedBook in borrowedBooks)
            {
                Book book = _bookServices.GetBookById(borrowedBook.BookId);
                BookModel bookModel = new BookModel
                {
                    Id = book.Id,
                    BookName = book.BookName,
                    Author = book.Author
                };
                Borrower borrower = _borrowerServices.GetBorrowerById(borrowedBook.BorrowerId);
                BorrowerModel borrowerModel = new BorrowerModel
                {
                    Id = borrower.Id,
                    BorrowerName = borrower.BorrowerName,
                    Address = borrower.Address
                };
                BorrowedBookModel borrowedBookModel = new BorrowedBookModel
                {
                    Id = borrowedBook.Id,
                    Book = bookModel,
                    Borrower = borrowerModel,
                    DateBorrowed = borrowedBook.DateBorrowed.HasValue ? borrowedBook.DateBorrowed.Value : DateTime.Now,
                    DateReturned = borrowedBook.DateReturned
                };
                borrowedBookModels.Add(borrowedBookModel);
            }
            return View(borrowedBookModels);
        }

        public IActionResult BookDetails(BookModel book)
        {
            return View(book);
        }

        public IActionResult BorrowBook(int id)
        {
            var book = _bookServices.GetBookById(id);
            
            BookModel bookModel = new BookModel
            {
                Id = book.Id,
                BookName = book.BookName,
                Author = book.Author
            };

            BorrowedBookModel toBeBorrowed = new BorrowedBookModel 
            { 
                Book = bookModel
            };

            return View(toBeBorrowed);
        }

       [HttpPost]
       public IActionResult BorrowBook(BorrowedBookModel borrowedBook)
        {
            Borrower borrower = new Borrower
            {
                BorrowerName = borrowedBook.Borrower.BorrowerName,
                Address = borrowedBook.Borrower.Address
            };
            _borrowerServices.NewBorrower(borrower);

            BorrowedBook borrowed = new BorrowedBook
            {
                BookId = borrowedBook.Book.Id,
                BorrowerId = borrower.Id,
                DateBorrowed = DateTime.Now
            };

            _borrowedBookServices.InsertBorrowedBook(borrowed);

            return RedirectToAction("Book");
        }

        public JsonResult BorrowBooks([FromBody]List<int> bookIds)
        {
            BooksBorrowedModel booksBorrowedModel = new BooksBorrowedModel();

            foreach(var id in bookIds)
            {
                Book book = _bookServices.GetBookById(id);
                BookModel bookModel = new BookModel();
                bookModel.Author = book.Author;
                bookModel.BookName = book.BookName;
                bookModel.Id = book.Id;
                booksBorrowedModel.Books.Add(bookModel);
            }
            booksBorrowedModel.Borrower = new BorrowerModel();
            
            return Json(booksBorrowedModel);
        }

        public IActionResult SubmitBorrowBooks([FromBody]SubmitBorrowBooksModel submitBorrowBooksModel)
        {
            try
            {
                Borrower borrower = new Borrower
                {
                    BorrowerName = submitBorrowBooksModel.BorrowerName,
                    Address = submitBorrowBooksModel.Address
                };
                _borrowerServices.NewBorrower(borrower);

                foreach (var id in submitBorrowBooksModel.BookIds)
                {
                    BorrowedBook borrowed = new BorrowedBook
                    {
                        BookId = id,
                        BorrowerId = borrower.Id,
                        DateBorrowed = DateTime.Now
                    };

                    _borrowedBookServices.InsertBorrowedBook(borrowed);
                }
            }
            catch(Exception ex)
            {
                //logging

                throw new Exception(ex.Message, ex);
            }
            return RedirectToAction("Book");
        }


        public IActionResult ReturnBook(int id)
        {
            var borrowedBook = _borrowedBookServices.GetBorrowedBookByBookId(id);
            var book = _bookServices.GetBookById(id);
            var borrower = _borrowerServices.GetBorrowerById(borrowedBook.BorrowerId);

            BookModel bookModel = new BookModel
            {
                Id = book.Id,
                BookName = book.BookName,
                Author = book.Author
            };

            BorrowerModel borrowerModel = new BorrowerModel 
            { 
                Id = borrower.Id,
                BorrowerName = borrower.BorrowerName,
                Address = borrower.Address
            };

            BorrowedBookModel toBeBorrowed = new BorrowedBookModel
            {
                Id = borrowedBook.Id,
                Book = bookModel,
                Borrower = borrowerModel,
                DateBorrowed = borrowedBook.DateBorrowed.Value
            };

            return View(toBeBorrowed);
        }

        [HttpPost]
        public IActionResult ReturnBook(BorrowedBookModel tobeReturned)
        {
            var borrowedBook = _borrowedBookServices.GetBorrowedBookByBookId(tobeReturned.Book.Id);
            borrowedBook.DateReturned = DateTime.Now;
            _borrowedBookServices.UpdateBorrowedBook(borrowedBook);

            return RedirectToAction("Book");
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
