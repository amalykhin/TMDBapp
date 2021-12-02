using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMDBapp.Data;
using TMDBapp.Models;
using TMDBapp.Repositories;

namespace TMDBapp.Services
{
    public class TMDBMovieService : IMovieService
    {
        private readonly ITMDBApiService api;
        private readonly IFavouritesRepository favRepo;

        public TMDBMovieService(ITMDBApiService api, IFavouritesRepository favRepo) 
            => (this.api, this.favRepo) = (api, favRepo);

        public void AddFavourite(int movieId, string userId) 
            => favRepo.AddFavourite(movieId, userId);

        public PaginatedResponse<Movie> GetByGenre(string userId, int genreId, int page)
        {
            var response = api.GetByGenre(genreId, page).Result;
            return PopulateFavourites(userId, response);
        }

        public MovieDetails GetDetails(int movieId)
            => api.GetDetails(movieId).Result;

        public IEnumerable<Genre> GetGenres()
            => api.GetGenres().Result.Genres;

        public PaginatedResponse<Movie> GetMostPopular(string userId, int? page)
        {
            var response = api.GetMostPopular(page).Result;
            return PopulateFavourites(userId, response);
        }

        public PaginatedResponse<Movie> GetTopRated(string userId, int? page)
        {
            var response = api.GetTopRated(page).Result;
            return PopulateFavourites(userId, response);
        }

        public void RemoveFavourite(int movieId, string userId) 
            => favRepo.RemoveFavourite(movieId, userId);

        private PaginatedResponse<Movie> PopulateFavourites(string userId, PaginatedResponse<Movie> response)
        {
            var favourites = favRepo.GetFavourites(userId).Select(f => f.MovieId).ToHashSet();

            foreach (var movie in response.Results)
            {
                movie.IsFavourite = favourites.Contains(movie.Id);
            }

            return response;
        }
    }
}
