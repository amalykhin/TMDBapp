﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TMDBapp.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        [JsonPropertyName("vote_average")]
        public double Rating { get; set; }
        [JsonPropertyName("poster_path")]
        public string PosterPath { get; set; }
    }
}
