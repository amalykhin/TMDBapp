using Microsoft.VisualStudio.TestTools.UnitTesting;
using TMDBapp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using TMDBapp.Models;
using TMDBapp.Repositories;
using Microsoft.Extensions.Configuration;

namespace TMDBapp.Services.Tests
{
    [TestClass()]
    public class TMDBMovieServiceTests
    {
        static readonly Dictionary<string, string> config = new()
        {
            ["ApiKey"] = "",
        };

        static IConfiguration configStub { get; set; }
        static IFavouritesRepository emptyFavRepoStub { get; set; }

        [ClassInitialize]
        public static void GenerateDefaultStubs(TestContext context)
        {
            configStub = new ConfigurationBuilder()
                .AddInMemoryCollection(config)
                .Build();

            var favRepoStub = new Mock<IFavouritesRepository>();
            favRepoStub
                .Setup(x => x.GetFavourites(""))
                .Returns(new List<Favourite>());
            emptyFavRepoStub = favRepoStub.Object;
        }

        [TestMethod()]
        [DataRow("asc")]
        [DataRow("desc")]
        public void GetByGenre_SortDirection_PopulateSortDirection(string sortDirection)
        {
            var (genre, page) = (0, 0);
            var apiStub = new Mock<ITMDBApiService>();
            apiStub
                .Setup(x => x.GetByGenre(config["ApiKey"], sortDirection, genre, page))
                .Returns(Task.FromResult(new PaginatedResponse<Movie>
                {
                    Results = new List<Movie>(),
                }));
            var service = new TMDBMovieService(apiStub.Object, emptyFavRepoStub, configStub);

            var result = service.GetByGenre("", genre, page, sortDirection).Result;

            Assert.AreEqual(result.SortDirection, sortDirection);
        }

        [TestMethod]
        [DataRow(0)]
        [DataRow(1)]
        [DataRow(int.MaxValue)]
        public void GetByGenre_Genre_ReturnMoviesOfGenre(int genre)
        {
            var page = 0;
            var apiStub = new Mock<ITMDBApiService>();
            apiStub
                .Setup(x => x.GetByGenre(config["ApiKey"], "desc", genre, page))
                .Returns(Task.FromResult(new PaginatedResponse<Movie>
                {
                    Results = new List<Movie> { new Movie { Genres = new List<Genre> { new Genre { Id = genre } } } },
                }));
            var service = new TMDBMovieService(apiStub.Object, emptyFavRepoStub, configStub);

            var result = service.GetByGenre("", genre, page).Result;

            Assert.IsTrue(result.Results.All(x => x.Genres.All(y => y.Id == genre)));
        }

        [TestMethod]
        [DataRow(0)]
        [DataRow(1)]
        [DataRow(int.MaxValue)]
        public void GetByGenre_Page_ReturnMoviesOnPage(int page)
        {
            var genre = 0;
            var apiStub = new Mock<ITMDBApiService>();
            apiStub
                .Setup(x => x.GetByGenre(config["ApiKey"], "desc", genre, page))
                .Returns(Task.FromResult(new PaginatedResponse<Movie>
                {
                    Page = page,
                    Results = new List<Movie>(),
                }));
            var service = new TMDBMovieService(apiStub.Object, emptyFavRepoStub, configStub);

            var result = service.GetByGenre("", genre, page).Result;

            Assert.AreEqual(result.Page, page);
        }

        [TestMethod()]
        public void GetDetails_MovieId_GetCorrectMovieDetails()
        {
            var movieId = 2;
            var apiStub = new Mock<ITMDBApiService>();
            apiStub
                .Setup(x => x.GetDetails(config["ApiKey"], movieId))
                .Returns(Task.FromResult(new MovieDetails
                {
                    Id = movieId,
                }));
            var service = new TMDBMovieService(apiStub.Object, emptyFavRepoStub, configStub);

            var result = service.GetDetails(movieId).Result;

            Assert.AreEqual(result.Id, movieId);
        }

        [TestMethod]
        [DataRow(0)]
        [DataRow(1)]
        [DataRow(int.MaxValue)]
        public void GetMostPopular_Page_ReturnMoviesOnPage(int page)
        {
            var apiStub = new Mock<ITMDBApiService>();
            apiStub
                .Setup(x => x.GetMostPopular(config["ApiKey"], page))
                .Returns(Task.FromResult(new PaginatedResponse<Movie>
                {
                    Page = page,
                    Results = new List<Movie>(),
                }));
            var service = new TMDBMovieService(apiStub.Object, emptyFavRepoStub, configStub);

            var result = service.GetMostPopular("", page).Result;

            Assert.AreEqual(result.Page, page);
        }


        [TestMethod()]
        [DataRow("asc")]
        [DataRow("desc")]
        public void GetTopRated_SortDirection_PopulateSortDirection(string sortDirection)
        {
            var page = 1;
            var apiStub = new Mock<ITMDBApiService>();
            apiStub
                .Setup(x => x.GetTopRated(config["ApiKey"], page))
                .Returns(Task.FromResult(new PaginatedResponse<Movie>
                {
                    Results = new List<Movie>(),
                    TotalPages = page + 1,
                }));
            apiStub
                .Setup(x => x.GetTopRated(config["ApiKey"], page+1))
                .Returns(Task.FromResult(new PaginatedResponse<Movie>
                {
                    Results = new List<Movie>(),
                    TotalPages = page + 1,
                }));
            var service = new TMDBMovieService(apiStub.Object, emptyFavRepoStub, configStub);

            var result = service.GetTopRated("", page, sortDirection).Result;

            Assert.AreEqual(result.SortDirection, sortDirection);
        }
    }
}