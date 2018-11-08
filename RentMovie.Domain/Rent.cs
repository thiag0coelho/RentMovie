using System;
using System.Collections.Generic;
using System.Text;

namespace RentMovie.Domain
{
    public class Rent
    {
        public int RentId { get; set; }
        public string Cpf { get; set; }
        public DateTime RentDate { get; set; }
        public List<Movie> Movies { get; set; }
    }
}
