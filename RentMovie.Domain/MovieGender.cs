using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RentMovie.Domain
{
    public class MovieGender
    {
        public int MovieGenderId { get; set; }

        [DisplayName("Nome")]
        [Required]
        public string Name { get; set; }

        [DisplayName("Data de Criação")]
        [Required]
        public DateTime CreationDate { get; set; }

        [DisplayName("Ativo")]
        [Required]
        public bool Active { get; set; }
    }
}
