using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HTTP5212_Project.Models
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        [ForeignKey("Genre")]
        public int GenreId { get; set; }
        public virtual Genre Genre { get; set; }
        [ForeignKey("Author")]
        public int AuthorId { get; set; }
        public virtual Author Author { get; set; }

    }

    public class BookDto
    {
        public int BookID { get; set; }
        public string title { get; set; }

        public string description { get; set; }

        public string AuthorName { get; set; }

        public string GenreName { get; set; }


    }

}