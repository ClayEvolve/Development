using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace onlineservice.Dto.Character
{
    public class RequestDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = "test";
        public int Strength { get; set; } = 10;
        public RpgClass Class { get; set; } = RpgClass.Knight;    
    }
}