using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TMDBapp.Models
{
    public class PaginatedResponse<T>
    {
        public int Page { get; set; }
        public IEnumerable<T> Results { get; set; }
        public int TotalResults { get; set; }
        public int TotalPages { get; set; }
    }
}
