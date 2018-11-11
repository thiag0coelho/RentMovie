using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RentMovie.Domain
{
    public class Movie
    {
        [Required]
        public int MovieId { get; set; }

        [DisplayName("Name")]
        [Required]
        public string Name { get; set; }

        [DisplayName("Creation Date")]
        [Required]
        public DateTime CreationDate { get; set; }

        [DisplayName("Active")]
        [Required]
        public bool Active { get; set; }

        [DisplayName("Movie Genre")]
        [Required]
        public int MovieGenderId { get; set; }
    }
}