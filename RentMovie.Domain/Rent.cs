using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentMovie.Domain
{
    public class Rent
    {
        public int RentId { get; set; }

        [Required]
        [StringLength(11)]
        public string Cpf { get; set; }

        [DisplayName("Data Aluguel")]
        [Required]
        public DateTime RentDate { get; set; }

        [NotMapped]
        [DisplayName("Filmes")]
        [Required]
        public string MovieIds { get; set; }
    }
}
