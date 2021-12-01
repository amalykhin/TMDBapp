using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TMDBapp.Models
{
    public class MovieDetails
    {
        public int Budget { get; set; }
        public IEnumerable<Genre> Genres { get; set; }
        public string Homepage { get; set; }
        [JsonPropertyName("original_language")]
        public string OriginalLanguage { get; set; }
        [JsonPropertyName("original_title")]
        public string OriginalTitle { get; set; }
        public string Overview { get; set; }
        public double Popularity { get; set; }
        [JsonPropertyName("production_companies")]
        public IEnumerable<ProductionCompany> ProductionCompanies { get; set; }
        [JsonPropertyName("release_date")]
        public DateTime ReleaseDate { get; set; }
        public int Runtime { get; set; }
        public int Revenue { get; set; }
        public string Status { get; set; }
        public string Tagline { get; set; }
        public string Title { get; set; }
        [JsonPropertyName("vote_average")]
        public double Rating { get; set; }
        [JsonPropertyName("poster_path")]
        public string PosterPath { get; set; }
    }
}
