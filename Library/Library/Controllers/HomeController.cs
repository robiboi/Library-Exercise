using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Domain;
using DataLayer.Interface;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBookServices _bookServices;

        public HomeController(IBookServices bookService)
        {
            _bookServices = bookService;
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
            Book book = new Book();
            book.BookName = model.BookName;
            _bookServices.InsertBook(book);
            return View(model);
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
