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
        PaginatedResponse<Movie> GetMostPopular(string userId, int? page);
        PaginatedResponse<Movie> GetTopRated(string userId, int? page);
        IEnumerable<Genre> GetGenres();
        PaginatedResponse<Movie> GetByGenre(string userId, int genreId, int page);
        MovieDetails GetDetails(int movieId);
        void AddFavourite(int movieId, string userId);
        void RemoveFavourite(int movieId, string userId);
    }
}
