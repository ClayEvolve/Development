using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using onlineservice.Dto.Movie;

namespace onlineservice.Models
{
    public class SourceApiResults
    {
        public List<MovieResponseDto>? Search { get; set; } 
        public string? totalResults { get; set; }
        public string? Response { get; set; }
    }
}