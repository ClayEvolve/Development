using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace onlineservice.Models
{
    public class Character
    {
        public int Id { get; set; }
        public string Name { get; set; } = "Clay";
        public int Strength { get; set; } = 10;
        public RpgClass Class { get; set; } = RpgClass.Knight;
    }
}