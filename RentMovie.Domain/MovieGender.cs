using System;

namespace RentMovie.Domain
{
    public class MovieGender
    {
        public int MovieGenderId { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public bool Active { get; set; }
    }
}
