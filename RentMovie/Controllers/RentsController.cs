﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RentMovie.Data;
using RentMovie.Domain;

namespace RentMovie.Controllers
{
    public class RentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Rent.AsNoTracking().ToListAsync());
        }

        public IActionResult Create()
        {
            LoadLists();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RentId,Cpf,MovieIds")] Rent rent)
        {
            if (ModelState.IsValid)
            {
                var sysDate = DateTime.Now;
                rent.RentDate = sysDate;

                _context.Add(rent);
                await _context.SaveChangesAsync();

                List<MovieRental> rentals = rent.MovieIds.Split(',')
                    .Select(movie => new MovieRental
                    {
                        CreationDate = sysDate,
                        MovieId = int.Parse(movie),
                        RentId = rent.RentId
                    }).ToList();

                await _context.AddRangeAsync(rentals);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(rent);
        }

        private void LoadLists()
        {
            ViewBag.MovieList = _context.Movie
                .Where(x => x.Active == true)
                .Select(c => new SelectListItem()
                {
                    Text = c.Name,
                    Value = c.MovieId.ToString()
                }).ToList();
        }
    }
}
