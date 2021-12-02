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
        PaginatedResponse<Movie> GetMostPopular(int? page);
        PaginatedResponse<Movie> GetTopRated(int? page);
        IEnumerable<Genre> GetGenres();
        PaginatedResponse<Movie> GetByGenre(string genre);
        MovieDetails GetDetails(int movieId);
    }
}
