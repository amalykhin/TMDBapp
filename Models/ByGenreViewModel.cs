using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TMDBapp.Models
{
    public class ByGenreViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public PaginatedResponse<Movie> Movies { get; set; }
    }
}
