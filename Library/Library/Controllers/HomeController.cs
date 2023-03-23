using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Domain;
using DataLayer.Interface;
using Microsoft.AspNetCore.Mvc;
using Library.Models;
using Services.Services;

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
            return View("Book");
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
                Author = model.Author,
                PublishedDate = model.PublishedDate
            };
            _bookServices.InsertBook(book);

            return View();

        }

        public IActionResult BookList()
        {
            var books = _bookServices.GetBooks();
            var borrowedBooks = _borrowedBookServices.GetBorrowedBooks().ToList();

            List<BookModel> bookModels = new List<BookModel>();
            foreach (var book in books)
            {
                BookModel bookModel = new BookModel
                {
                    Id = book.Id,
                    BookName = book.BookName,
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

        public JsonResult FetchBookListItem([FromBody] int id)
        {
            var book = _bookServices.GetBookById(id);
            var borrowedBooks = _borrowedBookServices.GetBorrowedBooks().ToList();

            BookListItemModel bookModel = new BookListItemModel
            {
                Id = book.Id,
                BookName = book.BookName,
                Author = book.Author,
                PublishedDate = book.PublishedDate,
                Borrowed = false
            };

            if (borrowedBooks.Exists(x => x.BookId == book.Id && !x.DateReturned.HasValue))
            {
                bookModel.Borrowed = true;
                var borrowedBook = borrowedBooks.Find(x => x.BookId == book.Id && !x.DateReturned.HasValue);
                var borrower = _borrowerServices.GetBorrowerById(borrowedBook.BorrowerId);
                bookModel.DateBorrowed = borrowedBook.DateBorrowed.Value;
                bookModel.Borrower = new BorrowerModel
                {
                    Id = borrower.Id,
                    BorrowerName = borrower.BorrowerName,
                    Address = borrower.Address
                };
            }

            return Json(bookModel);
        }

        public IActionResult DeleteBook(int id)
        {
            var book = _bookServices.GetBookById(id);
            _bookServices.DeleteBook(book);

            return RedirectToAction("Book");
        }

        public IActionResult BorrowedBookList()
        {
            var borrowedBooks = _borrowedBookServices.GetBorrowedBooks();
            List<BorrowedBookModel> borrowedBookModels = new List<BorrowedBookModel>();

            foreach (var borrowedBook in borrowedBooks)
            {
                Book book = _bookServices.GetBookById(borrowedBook.BookId);

                if (book is null)
                {
                    continue;
                }

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

        //public IActionResult Borrowerdetails()
        //{
        //    return View();
        //}

        [HttpPost]
        public JsonResult BorrowerList()
        {
            var BorrowerList = _borrowerServices.GetBorrowers();
            return Json(BorrowerList);
        }

        //[HttpPost]
        //public JsonResult GetBorrowerDetails([FromBody] List<int> borrowerId)
        //{
        //    BorrowerModel borrowerModel = new BorrowerModel
        //    {
        //        BorrowerName = new List<BorrowFormModel>()

        //    }; 
        //    foreach (var borrowers in borrowerId)
        //    {
        //        var borrowers = _borrowerServices.GetBorrowerById;
        //        BorrowFormModel borrowFormModel = new BorrowFormModel
        //        {
        //            BorrowerName = borrowers
        //        };
        //    }
        //        return Json(borrowFormModel);

        //}
        [HttpPost]
        public IActionResult GetBorrowerDetails(int id)
        { 
            var borrower = _borrowerServices.GetBorrowerById(id);

            if (borrower != null)
            {
                var borrowerDetails = new
                {
                    borrowerName = borrower.BorrowerName,
                    address = borrower.Address
                };
                return Json(borrowerDetails);
            }

            return NotFound();
        }

        public JsonResult BatchBorrow([FromBody] List<int> ids)
        {
            BorrowedBooksModel borrowedBooks = new BorrowedBooksModel
            {
                Books = new List<BookModel>()
            };
            foreach (var id in ids)
            {
                var book = _bookServices.GetBookById(id);
                BookModel bookModel = new BookModel
                {
                    Id = book.Id,
                    BookName = book.BookName,
                    Author = book.Author,
                    PublishedDate = book.PublishedDate
                };

                borrowedBooks.Books.Add(bookModel);
            }

            return Json(borrowedBooks);
        }

        public IActionResult BorrowBooks(BorrowFormModel borrowForm)
        {
            Borrower borrower = new Borrower
            {
                BorrowerName = borrowForm.BorrowerName,
                Address = borrowForm.Address
            };
            _borrowerServices.NewBorrower(borrower);
            
            List<BorrowedBook> borrowedBookbatch = new List<BorrowedBook>();
            foreach (var bookId in borrowForm.BookIds)
            {
                BorrowedBook borrowed = new BorrowedBook
                {
                    BookId = bookId,
                    BorrowerId = borrower.Id,
                    DateBorrowed = DateTime.Now
                };

                borrowedBookbatch.Add(borrowed);
            }
            _borrowedBookServices.InsertBorrowedBooks(borrowedBookbatch);
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
                Author = book.Author,
                PublishedDate = book.PublishedDate
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