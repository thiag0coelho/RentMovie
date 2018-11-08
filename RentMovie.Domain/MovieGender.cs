using System;
using System.ComponentModel;

namespace RentMovie.Domain
{
    public class MovieGender
    {
        public int MovieGenderId { get; set; }
        [DisplayName("Nome")]
        public string Name { get; set; }
        [DisplayName("Data de Criação")]
        public DateTime CreationDate { get; set; }
        [DisplayName("Ativo")]
        public bool Active { get; set; }
    }
}
