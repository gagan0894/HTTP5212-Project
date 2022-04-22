using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HTTP5212_Project.Models;

namespace HTTP5212_Project.Controllers
{
    public class BookController : Controller
    {
        // GET: Book
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]

        public ActionResult ListBooks()
        {
            IEnumerable<BookDto> Books = new List<BookDto>();
            BookDataController controller = new BookDataController();
            Books = controller.ListBooks();

            return View(Books);

        }

        [HttpGet]
        [Route("Book/New")]
        public ActionResult New()
        {
            IEnumerable<Genre> Genres = new List<Genre>();
            IEnumerable<Author> Authors = new List<Author>();
            BookDataController controller = new BookDataController();
            Genres = controller.ListGenres();
            Authors = controller.ListAuthors();
            dynamic mymodel = new ExpandoObject();
            mymodel.Genres = Genres;
            mymodel.Authors = Authors;
            return View(mymodel);
        }

        [HttpGet]
        [Route("Book/Update/{id}")]
        public ActionResult Update(int id)
        {
            Book book = new Book();
            BookDataController controller = new BookDataController();
            book = controller.FindBook(id);
            Debug.WriteLine("Book Title-" + book.title);
            return View(book);
        }

        [HttpPost]
        [Route("Book/Modify/{id}")]
        public ActionResult Modify(int id, string name, string description, string genre, string afname, string alname)
        {
            Debug.WriteLine("Book Name-" + name + ",Description-" + description + ",Genre-" + genre + ",Author-" + afname + " " + alname);
            BookDataController controller = new BookDataController();
            Book book = new Book();
            book.BookId = id;
            book.title = name;
            book.description = description;
            Genre gen = new Genre();
            gen.GenreName = genre;
            book.Genre = gen;
            Author author = new Author();
            author.FirstName = afname;
            author.LastName = alname;
            book.Author = author;
            Debug.WriteLine("Author Name-" + book.Author.FirstName);
            Debug.WriteLine("Genre Name-" + book.Genre.GenreName);
            controller.UpdateBook(id,book);
            return RedirectToAction("ListBooks");
        }

        [HttpPost]
        [Route("Book/Create")]
        public ActionResult Create(string name, string description, string genre, string afname, string alname)
        {
            Debug.WriteLine("Book Name-" + name + ",Description-" + description + ",Genre-" + genre + ",Author-" + afname+ " "+alname);
            BookDataController controller = new BookDataController();
            Book book = new Book();
            
            book.title = name;
            book.description = description;
            Genre gen = new Genre();
            gen.GenreName = genre;
            book.Genre = gen;
            Author author = new Author();
            author.FirstName = afname;
            author.LastName = alname;
            book.Author = author;
            Debug.WriteLine("Author Name-" + book.Author.FirstName);
            Debug.WriteLine("Genre Name-" + book.Genre.GenreName);
            controller.AddBook(book);
            return RedirectToAction("ListBooks");
        }
        [HttpGet]
        [Route("/Book/DeleteConfirm/{id}")]
        public ActionResult DeleteConfirm(int id)
        {
            ViewBag.BookId = id.ToString();

            return View();
        }


        //POST : /Book/Delete/{id}
        [HttpPost]
        public ActionResult Delete(int id)
        {
            BookDataController controller = new BookDataController();
            controller.DeleteBook(id);
            return RedirectToAction("ListBooks");
        }
    }
}