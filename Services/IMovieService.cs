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
        Task<PaginatedResponse<Movie>> GetMostPopular(string userId, int? page);
        Task<PaginatedResponse<Movie>> GetTopRated(string userId, int page, string sortDirection, int? totalPages);
        Task<IEnumerable<Genre>> GetGenres();
        Task<PaginatedResponse<Movie>> GetByGenre(string userId, int genreId, int page, string sortDirection);
        Task<MovieDetails> GetDetails(int movieId);
        void AddFavourite(int movieId, string userId);
        void RemoveFavourite(int movieId, string userId);
    }
}
