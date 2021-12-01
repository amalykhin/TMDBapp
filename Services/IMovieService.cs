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
        IDictionary<string, IEnumerable<Movie>> GetByGenre();
        MovieDetails GetDetails(int movieId);
    }
}
