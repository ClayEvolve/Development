using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using onlineservice.Dto.Character;

namespace onlineservice.Services.CharacterService
{
    public interface ICharacterService
    {
        public Task<ServiceResponse<List<RequestDto>>> GetAll();
        public  Task<ServiceResponse<RequestDto>> GetById(int id);
        public  Task<ServiceResponse<List<RequestDto>>> AddItem(ResponseDto item);
        public  Task<ServiceResponse<RequestDto>> UpdateItem(UpdateDto item);
    }
}