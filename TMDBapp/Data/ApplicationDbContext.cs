using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TMDBapp.Models;

namespace TMDBapp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Favourite> Favourites { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Favourite>()
                .ToTable("Favourites")
                .HasKey(f => new { f.MovieId, f.UserId });
        }
    }
}
