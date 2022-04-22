using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using HTTP5212_Project.Models;

namespace HTTP5212_Project.Controllers
{
    public class BookDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/BookData/ListBooks
        [HttpGet]
        public IEnumerable<BookDto> ListBooks()
        {
            List<Book> Books = db.Books.ToList();
            List<BookDto> BookDtos = new List<BookDto>();

            Books.ForEach(a => BookDtos.Add(new BookDto()
            {
                BookID = a.BookId,
                title = a.title,
                description = a.description,
                AuthorName = a.Author.FirstName + " " + a.Author.LastName,
                GenreName = a.Genre.GenreName
            })); ;

            return BookDtos;
        }

        // GET: api/BookData/ListGenres
        [HttpGet]
        public List<Genre> ListGenres()
        {
            List<Genre> Genres = db.Genres.ToList();

            return Genres;
        }
        // GET: api/BookData/ListAuthors
        [HttpGet]
        public List<Author> ListAuthors()
        {
            List<Author> Authors = db.Authors.ToList();
           
            return Authors;
        }
        // GET: api/BookData/FindBook/7
        [ResponseType(typeof(Book))]
        [HttpGet]
        public Book FindBook(int id)
        {
            Book Book = db.Books.Find(id);
            
            if (Book == null)
            {
                return null;
            }

            return Book;
        }
        private bool BookExists(int id)
        {
            return db.Books.Count(e => e.BookId == id) > 0;
        }

        // POST: api/BookData/UpdateBook/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateBook(int id, Book book)
        {
            Debug.WriteLine("I have reached the update book method!");
            if (!ModelState.IsValid)
            {
                Debug.WriteLine("Model State is invalid");
                return BadRequest(ModelState);
            }
            Debug.WriteLine(id);
            Debug.WriteLine(book.BookId);
            if (id != book.BookId)
            {
                Debug.WriteLine("ID mismatch");
                Debug.WriteLine("GET parameter" + id);
                Debug.WriteLine("POST parameter" + book.BookId);
                Debug.WriteLine("POST parameter" + book.title);
                Debug.WriteLine("POST parameter " + book.description);
                Debug.WriteLine("POST parameter" + book.Genre.GenreName);
                Debug.WriteLine("POST parameter " + book.Author.FirstName);
                return BadRequest();
            }
            BookDataController cont = new BookDataController();
            List<Genre> genres = cont.ListGenres();
            List<Author> authors = cont.ListAuthors();
            Genre genre = new Genre();
            Author author = new Author();
            foreach (Genre g in genres)
            {
                Debug.WriteLine("Genre-" + g);
                if (g != null && g.GenreName != null && g.GenreName.ToLower().Equals(book.Genre.GenreName.ToLower()))
                {
                    genre.GenreId = g.GenreId;
                    genre.GenreName = g.GenreName;
                }

            }
            if (genre.GenreId == 0)
            {
                genre.GenreId = genres.Count();
                Debug.WriteLine(book.Genre.GenreName);
                genre.GenreName = book.Genre.GenreName;
                db.Genres.Add(genre);
            }
            foreach (Author a in authors)
            {
                Debug.WriteLine("Author-" + a);
                if (a != null && a.FirstName != null && a.LastName != null && a.FirstName.ToLower().Equals(book.Author.FirstName.ToLower()) && a.LastName.ToLower().Equals(book.Author.LastName.ToLower()))
                {
                    author.AuthorId = a.AuthorId;
                    author.FirstName = a.FirstName;
                    author.LastName = a.LastName;
                }

            }

            if (author.AuthorId == 0)
            {
                author.AuthorId = authors.Count();
                Debug.WriteLine(book.Author.FirstName);
                author.FirstName = book.Author.FirstName;
                Debug.WriteLine(book.Author.LastName);
                author.LastName = book.Author.LastName;
                db.Authors.Add(author);
            }
            book.Author = author;
            book.Genre = genre;

            db.Entry(book).State = EntityState.Modified;

            try
            {
                Debug.WriteLine("Before save");
                db.SaveChanges();
                Debug.WriteLine("After save");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
                {
                    Debug.WriteLine("Book not found");
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            Debug.WriteLine("None of the conditions triggered");
            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/BookData/AddBook
        [ResponseType(typeof(Book))]
        [HttpPost]
        public IHttpActionResult AddBook(Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Debug.WriteLine("Author Name Data-" + book.Author.FirstName);
            Debug.WriteLine("Genre Name Data-" + book.Genre.GenreName);
            BookDataController cont = new BookDataController();
            List<Genre> genres= cont.ListGenres();
            List<Author> authors = cont.ListAuthors();
            Genre genre = new Genre();
            Author author = new Author();
            Debug.WriteLine("Genres Count-" + genres.Count());
            Debug.WriteLine("Author Count-" + authors.Count());
           
            foreach(Genre g in genres)
                {
                Debug.WriteLine("Genre-" + g);
                if (g != null && g.GenreName != null && g.GenreName.ToLower().Equals(book.Genre.GenreName.ToLower()))
                { 
                    genre.GenreId = g.GenreId;
                    genre.GenreName = g.GenreName;
                }

            }
            if (genre.GenreId == 0)
            {
                genre.GenreId = genres.Count();
                Debug.WriteLine(book.Genre.GenreName);
                genre.GenreName = book.Genre.GenreName;
                db.Genres.Add(genre);
            }
            foreach (Author a in authors)
            {
                Debug.WriteLine("Author-" + a);
                if (a != null && a.FirstName != null && a.LastName != null && a.FirstName.ToLower().Equals(book.Author.FirstName.ToLower()) && a.LastName.ToLower().Equals(book.Author.LastName.ToLower()))
                {
                    author.AuthorId = a.AuthorId;
                    author.FirstName = a.FirstName;
                    author.LastName = a.LastName;
                }

            }

            if (author.AuthorId == 0)
            {
                author.AuthorId = authors.Count();
                Debug.WriteLine(book.Author.FirstName);
                author.FirstName = book.Author.FirstName;
                Debug.WriteLine(book.Author.LastName);
                author.LastName = book.Author.LastName;
                db.Authors.Add(author);
            }
            book.Author = author;
            book.Genre =genre;
            db.Books.Add(book);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = book.BookId }, book);
        }

        // POST: api/BookData/DeleteBook/5
        [ResponseType(typeof(Book))]
        [HttpPost]
        public IHttpActionResult DeleteBook(int id)
        {
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return NotFound();
            }

            db.Books.Remove(book);
            db.SaveChanges();

            return Ok();
        }
    }
}