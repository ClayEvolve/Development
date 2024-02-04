using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using onlineservice.Models;
using onlineservice.Dto.Character;

namespace onlineservice
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Character, RequestDto>();
            CreateMap<ResponseDto, Character>();
        }

    }
}