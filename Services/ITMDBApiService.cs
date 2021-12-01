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
        Task<IEnumerable<Genre>> GetGenres();
        [Get("/movie/{id}?api_key=dad8a59d86a2793dda93aa485f7339c1")]
        Task<MovieDetails> GetDetails(int id);
    }
}
