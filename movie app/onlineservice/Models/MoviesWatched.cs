using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace onlineservice.Models
{
    public class MoviesWatched
    {
        public int Id { get; set; }
        public string User { get; set; } = string.Empty;
        public bool Indicator { get; set; } = true;
    }
}