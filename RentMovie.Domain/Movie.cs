using System;
using System.ComponentModel;

namespace RentMovie.Domain
{
    public class Movie
    {
        public int MovieId { get; set; }
        [DisplayName("Nome")]
        public string Name { get; set; }
        [DisplayName("Data de Criação")]
        public DateTime CreationDate { get; set; }
        [DisplayName("Ativo")]
        public bool Active { get; set; }
        [DisplayName("Gênero")]
        public int MovieGenderId { get; set; }
    }
}