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

        public async Task<PaginatedResponse<Movie>> GetByGenre(string userId, int genreId, int page, string sortDirection = "desc")
        {
            var response = await api.GetByGenre(apiKey, sortDirection, genreId, page);
            response.SortDirection = sortDirection;
            return PopulateFavourites(userId, response);
        }

        public async Task<MovieDetails> GetDetails(int movieId)
            => await api.GetDetails(apiKey, movieId);

        public async Task<IEnumerable<Genre>> GetGenres()
            => (await api.GetGenres(apiKey)).Genres;

        public async Task<PaginatedResponse<Movie>> GetMostPopular(string userId, int? page)
        {
            var response = await api.GetMostPopular(apiKey, page);
            return PopulateFavourites(userId, response);
        }

        public async Task<PaginatedResponse<Movie>> GetTopRated(string userId, int page = 1, string sortDirection = "desc", int? totalPages = null)
        {
            PaginatedResponse<Movie> response = null;
            if (sortDirection.Equals("asc"))
            {
                if (totalPages is null)
                {
                    response = await api.GetTopRated(apiKey, page);
                    totalPages = response.TotalPages;
                }
                response = await api.GetTopRated(apiKey, totalPages + 1 - page);
                response.Page = page;
            } else
            {
                response = await api.GetTopRated(apiKey, page);
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
