using System;

namespace RentMovie.Domain
{
    public class Movie
    {
        public int MovieId { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public bool Active { get; set; }
    }
}