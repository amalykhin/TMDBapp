using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMDBapp.Models;

namespace TMDBapp.Services
{
    public interface IMovieService
    {
        IEnumerable<Movie> GetMostPopular(int? page);
        IEnumerable<Movie> GetTopRated(int? page);
        IDictionary<string, IEnumerable<Movie>> GetByGenre();
        MovieDetails GetDetails(int movieId);
    }
}
