using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMDBapp.Models;

namespace TMDBapp.Services
{
    public class TMDBMovieService : IMovieService
    {
        private readonly ITMDBApiService api;

        public TMDBMovieService(ITMDBApiService api) 
            => this.api = api;

        public PaginatedResponse<Movie> GetByGenre(string genre)
            => api.GetByGenre(genre).Result;

        public MovieDetails GetDetails(int movieId)
            => api.GetDetails(movieId).Result;

        public IEnumerable<Genre> GetGenres()
            => api.GetGenres().Result.Genres;

        public PaginatedResponse<Movie> GetMostPopular(int? page)
            => api.GetMostPopular(page).Result;

        public PaginatedResponse<Movie> GetTopRated(int? page)
            => api.GetTopRated(page).Result;
    }
}
