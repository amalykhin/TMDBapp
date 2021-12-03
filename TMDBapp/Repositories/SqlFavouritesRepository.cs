using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMDBapp.Data;
using TMDBapp.Models;

namespace TMDBapp.Repositories
{
    public class SqlFavouritesRepository : IFavouritesRepository
    {
        private readonly ApplicationDbContext context;

        public SqlFavouritesRepository(ApplicationDbContext context)
            => this.context = context;

        public void AddFavourite(int movieId, string userId)
        {
            context.Favourites.Add(new Favourite
            {
                MovieId = movieId,
                UserId = userId,
            });

            context.SaveChanges();
        }

        public IEnumerable<Favourite> GetFavourites(string userId) 
            => context.Favourites
                .Where(f => userId == f.UserId)
                .AsNoTracking();

        public void RemoveFavourite(int movieId, string userId)
        {
            var fav = context.Favourites
                .Where(f => f.MovieId == movieId && f.UserId == userId)
                .FirstOrDefault();
            context.Favourites.Remove(fav);

            context.SaveChanges();
        }
    }
}
