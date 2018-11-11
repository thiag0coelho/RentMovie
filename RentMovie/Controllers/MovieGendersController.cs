using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentMovie.Data;
using RentMovie.Domain;
using RentMovie.Repository.Interface;

namespace RentMovie.Controllers
{
    public class MovieGendersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMovieGenderRepository _movieGenderRepository;

        public MovieGendersController(ApplicationDbContext context, IMovieGenderRepository movieGenderRepository)
        {
            _context = context;
            _movieGenderRepository = movieGenderRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.MovieGender.Where(x => x.Active).AsNoTracking().ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MovieGenderId,Name,CreationDate,Active")] MovieGender movieGender)
        {
            if (ModelState.IsValid)
            {
                movieGender.CreationDate = DateTime.Now;
                movieGender.Active = true;

                _context.Add(movieGender);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(movieGender);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieGender = await _movieGenderRepository.GetByID(id.GetValueOrDefault());

            if (movieGender == null)
            {
                return NotFound();
            }

            return View(movieGender);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MovieGenderId,Name,CreationDate,Active")] MovieGender movieGender)
        {
            if (id != movieGender.MovieGenderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movieGender);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieGenderExists(movieGender.MovieGenderId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(movieGender);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieGender = await _context.MovieGender.FirstOrDefaultAsync(m => m.MovieGenderId == id);
            if (movieGender == null)
            {
                return NotFound();
            }

            return View(movieGender);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movieGender = await _context.MovieGender.FindAsync(id);

            movieGender.Active = false;
            _context.Update(movieGender);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ActionName("DeleteSelected")]
        public async Task<IActionResult> DeleteSelected(string ids)
        {
            if (string.IsNullOrEmpty(ids))
            {
                return NotFound();
            }

            await _movieGenderRepository.DeleteList(ids);

            return Ok();
        }

        private bool MovieGenderExists(int id)
        {
            return _context.MovieGender.Any(e => e.MovieGenderId == id);
        }
    }
}
