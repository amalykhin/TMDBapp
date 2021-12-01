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

        public IDictionary<string, IEnumerable<Movie>> GetByGenre()
        {
            throw new NotImplementedException();
        }

        public MovieDetails GetDetails(int movieId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Movie> GetMostPopular(int? page)
            =>  api.GetMostPopular(page).Result.Results;

        public IEnumerable<Movie> GetTopRated(int? page)
            => api.GetTopRated(page).Result.Results;
    }
}
