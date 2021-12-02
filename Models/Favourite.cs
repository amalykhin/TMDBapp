using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TMDBapp.Models
{
    public class Favourite
    {
        public int MovieId { get; set; }

        public string UserId { get; set; }
    }
}
