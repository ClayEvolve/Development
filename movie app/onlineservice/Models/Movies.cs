using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace onlineservice.Models
{
    public class Movies
    {
        public int Id { get; set; }
        public string Title { get; set; } = String.Empty;
        public string Year { get; set; } = String.Empty;
        public string imdbID { get; set; } = String.Empty;
        public string Type { get; set; } = String.Empty;
        public string Poster { get; set; } = String.Empty;
    }
}