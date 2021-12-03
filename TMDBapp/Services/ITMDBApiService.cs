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
        [Get("/movie/popular?api_key={apiKey}")]
        Task<PaginatedResponse<Movie>> GetMostPopular(string apiKey, [Query] int? page);

        [Get("/movie/top_rated?api_key={apiKey}")]
        Task<PaginatedResponse<Movie>> GetTopRated(string apiKey, [Query] int? page);

        [Get("/genre/movie/list?api_key={apiKey}")]
        Task<GenreResponse> GetGenres(string apiKey);
        
        [Get("/discover/movie?api_key={apiKey}&with_genres={genreId}&page={page}")]
        Task<PaginatedResponse<Movie>> GetByGenre(string apiKey, string sortDirection, int genreId, int page);

        [Get("/movie/{id}?api_key={apiKey}")]
        Task<MovieDetails> GetDetails(string apiKey, int id);
    }

    public class GenreResponse
    {
        public IEnumerable<Genre> Genres { get; set; }
    }
}
