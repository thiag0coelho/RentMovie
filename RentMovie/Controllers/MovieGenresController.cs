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
    public class MovieGenresController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMovieGenreRepository _MovieGenreRepository;

        public MovieGenresController(ApplicationDbContext context, IMovieGenreRepository MovieGenreRepository)
        {
            _context = context;
            _MovieGenreRepository = MovieGenreRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.MovieGenre.Where(x => x.Active).AsNoTracking().ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MovieGenreId,Name,CreationDate,Active")] MovieGenre MovieGenre)
        {
            if (ModelState.IsValid)
            {
                MovieGenre.CreationDate = DateTime.Now;
                MovieGenre.Active = true;

                _context.Add(MovieGenre);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(MovieGenre);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var MovieGenre = await _MovieGenreRepository.GetByID(id.GetValueOrDefault());

            if (MovieGenre == null)
            {
                return NotFound();
            }

            return View(MovieGenre);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MovieGenreId,Name,CreationDate,Active")] MovieGenre MovieGenre)
        {
            if (id != MovieGenre.MovieGenreId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(MovieGenre);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieGenreExists(MovieGenre.MovieGenreId))
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
            return View(MovieGenre);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var MovieGenre = await _context.MovieGenre.FirstOrDefaultAsync(m => m.MovieGenreId == id);
            if (MovieGenre == null)
            {
                return NotFound();
            }

            return View(MovieGenre);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var MovieGenre = await _context.MovieGenre.FindAsync(id);

            MovieGenre.Active = false;
            _context.Update(MovieGenre);
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

            await _MovieGenreRepository.DeleteList(ids);

            return Ok();
        }

        private bool MovieGenreExists(int id)
        {
            return _context.MovieGenre.Any(e => e.MovieGenreId == id);
        }
    }
}
