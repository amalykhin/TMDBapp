using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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

        public IActionResult MostPopular(int page = 1)
        {
            var movies = movieService.GetMostPopular(page);
            return View(movies);
        }
        
        public IActionResult TopRated(int page = 1)
        {
            var movies = movieService.GetTopRated(page);
            return View(movies);
        }

        public IActionResult Details(int movieId)
        {
            var movie = movieService.GetDetails(movieId);
            return View(movie);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
