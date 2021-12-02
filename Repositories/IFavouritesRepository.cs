using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMDBapp.Models;

namespace TMDBapp.Repositories
{
    public interface IFavouritesRepository
    {
        public void AddFavourite(int movieId, string userId);
        public void RemoveFavourite(int movieId, string userId);
        public IEnumerable<Favourite> GetFavourites(string userId);
    }
}
