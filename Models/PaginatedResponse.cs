using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TMDBapp.Models
{
    public class PaginatedResponse<T>
    {
        public int Page { get; set; }
        public IEnumerable<T> Results { get; set; }
        [JsonPropertyName("total_results")]
        public int TotalResults { get; set; }
        [JsonPropertyName("total_pages")]
        public int TotalPages { get; set; }
        public string SortDirection { get; set; }
    }
}
