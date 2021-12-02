using Microsoft.Extensions.Configuration;
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
        private readonly string apiKey;

        public TMDBMovieService(ITMDBApiService api, IFavouritesRepository favRepo, IConfiguration config)
        {
            this.apiKey = config["ApiKey"];
            (this.api, this.favRepo) = (api, favRepo);
        }

        public void AddFavourite(int movieId, string userId) 
            => favRepo.AddFavourite(movieId, userId);

        public PaginatedResponse<Movie> GetByGenre(string userId, int genreId, int page, string sortDirection = "desc")
        {
            var response = api.GetByGenre(apiKey, sortDirection, genreId, page).Result;
            response.SortDirection = sortDirection;
            return PopulateFavourites(userId, response);
        }

        public MovieDetails GetDetails(int movieId)
            => api.GetDetails(apiKey, movieId).Result;

        public IEnumerable<Genre> GetGenres()
            => api.GetGenres(apiKey).Result.Genres;

        public PaginatedResponse<Movie> GetMostPopular(string userId, int? page)
        {
            var response = api.GetMostPopular(apiKey, page).Result;
            return PopulateFavourites(userId, response);
        }

        public PaginatedResponse<Movie> GetTopRated(string userId, int page = 1, string sortDirection = "desc", int? totalPages = null)
        {
            PaginatedResponse<Movie> response = null;
            if (sortDirection.Equals("asc"))
            {
                if (totalPages is null)
                {
                    response = api.GetTopRated(apiKey, page).Result;
                    totalPages = response.TotalPages;
                }
                response = api.GetTopRated(apiKey, totalPages + 1 - page).Result;
                response.Page = page;
            } else
            {
                response = api.GetTopRated(apiKey, page).Result;
            }
            response.SortDirection = sortDirection;
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
