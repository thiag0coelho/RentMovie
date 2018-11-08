using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace RentMovie.Domain
{
    public class Movie
    {
        public int MovieId { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public bool Active { get; set; }
        public MovieGender MovieGender { get; set; }
    }
}