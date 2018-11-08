using System;
using System.Collections.Generic;
using System.Text;

namespace RentMovie.Domain
{
    public class MovieRental
    {
        public int MovieRentalId { get; set; }
        public int RentId { get; set; }
        public int MovieId { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
