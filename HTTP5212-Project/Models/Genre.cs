using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace HTTP5212_Project.Models
{
    public class Genre
    {
        [Key]
        public int GenreId { get; set; }

        public string GenreName { get; set; }
    }
}