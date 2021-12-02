using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMDBapp.Models;

namespace TMDBapp.Services
{
    public interface ITMDBApiService
    {
        [Get("/movie/popular?api_key=dad8a59d86a2793dda93aa485f7339c1")]
        Task<PaginatedResponse<Movie>> GetMostPopular([Query] int? page);

        [Get("/movie/top_rated?api_key=dad8a59d86a2793dda93aa485f7339c1")]
        Task<PaginatedResponse<Movie>> GetTopRated([Query] int? page);

        [Get("/genre/movie/list?api_key=dad8a59d86a2793dda93aa485f7339c1")]
        Task<GenreResponse> GetGenres();
        
        [Get("/discover/movie?api_key=dad8a59d86a2793dda93aa485f7339c1&with_genres={genreId}&page={page}")]
        Task<PaginatedResponse<Movie>> GetByGenre(int genreId, int page);

        [Get("/movie/{id}?api_key=dad8a59d86a2793dda93aa485f7339c1")]
        Task<MovieDetails> GetDetails(int id);
    }

    public class GenreResponse
    {
        public IEnumerable<Genre> Genres { get; set; }
    }
}
