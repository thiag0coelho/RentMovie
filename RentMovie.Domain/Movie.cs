using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentMovie.Domain
{
    public class Movie
    {
        [Required]
        public int MovieId { get; set; }

        [DisplayName("Nome")]
        [Required]
        public string Name { get; set; }

        [DisplayName("Data de Criação")]
        [Required]
        public DateTime CreationDate { get; set; }

        [DisplayName("Ativo")]
        [Required]
        public bool Active { get; set; }

        [DisplayName("Gênero")]
        [Required]
        public int MovieGenderId { get; set; }
    }
}