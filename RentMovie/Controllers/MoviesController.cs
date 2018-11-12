using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RentMovie.Data;
using RentMovie.Domain;
using RentMovie.Repository.Interface;

namespace RentMovie.Controllers
{
    public class MoviesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMovieRepository _movieRepository;

        // Using Dependency Injection
        public MoviesController(ApplicationDbContext context, IMovieRepository movieRepository)
        {
            _context = context;
            _movieRepository = movieRepository;
        }

        public async Task<IActionResult> Index()
        {
            //AsNoTracking is used to decouple these retrieved objects from entity framework, it boosts performance
            return View(await _context.Movie.Where(x => x.Active).AsNoTracking().ToListAsync());
        }

        public IActionResult Create()
        {
            LoadLists();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        // Bind is used to only allow the mentioned properties to be received in a request
        public async Task<IActionResult> Create([Bind("MovieId,Name,CreationDate,MovieGenreId,Active")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                movie.CreationDate = DateTime.Now;
                movie.Active = true;

                _context.Add(movie);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            LoadLists();

            return View(movie);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MovieId,Name,CreationDate,MovieGenreId,Active")] Movie movie)
        {
            if (id != movie.MovieId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movie);

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.MovieId))
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
            return View(movie);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie.FirstOrDefaultAsync(m => m.MovieId == id);

            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie = await _context.Movie.FindAsync(id);

            movie.Active = false;
            _context.Movie.Update(movie);

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

            await _movieRepository.DeleteList(ids);

            return Ok();
        }

        private bool MovieExists(int id)
        {
            return _context.Movie.Any(e => e.MovieId == id);
        }

        private void LoadLists()
        {
            ViewBag.MovieGenreList = _context.MovieGenre
                .Where(x => x.Active == true)
                .Select(c => new SelectListItem()
                {
                    Text = c.Name,
                    Value = c.MovieGenreId.ToString()
                }).ToList();
        }
    }
}
