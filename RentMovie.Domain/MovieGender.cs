using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RentMovie.Domain
{
    public class MovieGender
    {
        public int MovieGenderId { get; set; }

        [DisplayName("Name")]
        [Required]
        public string Name { get; set; }

        [DisplayName("Creation Date")]
        [Required]
        public DateTime CreationDate { get; set; }

        [DisplayName("Active")]
        [Required]
        public bool Active { get; set; }
    }
}
