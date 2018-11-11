using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RentMovie.Domain;

namespace RentMovie.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<RentMovie.Domain.MovieGenre> MovieGenre { get; set; }
        public DbSet<RentMovie.Domain.Movie> Movie { get; set; }
        public DbSet<RentMovie.Domain.Rent> Rent { get; set; }
        public DbSet<RentMovie.Domain.MovieRental> MovieRental { get; set; }
    }
}
