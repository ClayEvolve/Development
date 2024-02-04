using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace onlineservice.Dto.Character
{
    public class ResponseDto
    {
        public string Name { get; set; } = "Clay";
        public int Strength { get; set; } = 10;
        public RpgClass Class { get; set; } = RpgClass.Knight;

    }
}