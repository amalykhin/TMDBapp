using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TMDBapp.Models;
using TMDBapp.Services;

namespace TMDBapp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMovieService movieService;

        public HomeController(ILogger<HomeController> logger, IMovieService movieService)
        {
            _logger = logger;
            this.movieService = movieService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> MostPopular(int page = 1)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var movies = await movieService.GetMostPopular(userId, page);
            return View(movies);
        }
        
        public async Task<IActionResult> TopRated(int page = 1, string sortDirection = "desc", int? totalPages = null)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var movies = await movieService.GetTopRated(userId, page, sortDirection, totalPages);
            return View(movies);
        }

        public async Task<IActionResult> Details(int movieId)
        {
            var movie = await movieService.GetDetails(movieId);
            return View(movie);
        }

        public async Task<IActionResult> Genres()
        {
            var genres = await movieService.GetGenres();
            return View(genres);
        }

        public async Task<IActionResult> ByGenre(int genreId, string genreName, int page = 1, string sortDirection = "desc")
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var movies = await movieService .GetByGenre(userId, genreId, page, sortDirection);
            return View(new ByGenreViewModel
            {
                Id = genreId,
                Name = genreName,
                Movies = movies,
            });
        }

        public IActionResult AddFavourite(int movieId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            movieService.AddFavourite(movieId, userId);

            return NoContent();
        }

        public IActionResult RemoveFavourite(int movieId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            movieService.RemoveFavourite(movieId, userId);

            return NoContent();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
