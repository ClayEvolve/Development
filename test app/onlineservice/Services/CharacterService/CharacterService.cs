using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using onlineservice.Dto.Character;

namespace onlineservice.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {

        private static List<Character> list = new List<Character>{
                new Character(),
                new Character{Id = 1, Name = "Connor"}
        };
         private readonly IMapper _mapper;
         private readonly DataContext _context;
        public CharacterService(IMapper mapper, DataContext context)
        {
         _context = context;
          _mapper = mapper;
        }
        
         public async Task<ServiceResponse<List<RequestDto>>> GetAll()
         {
            var response = new ServiceResponse<List<RequestDto>>();
            var dbCharacters = await _context.Characters.ToListAsync();
            response.Data = dbCharacters.Select(p => _mapper.Map<RequestDto>(p)).ToList();
            return response;
         }
         public async Task<ServiceResponse<RequestDto>> GetById(int id)
         {
            var response = new ServiceResponse<RequestDto>();
            var result = _context.Characters.FirstOrDefaultAsync(p => p.Id == id);

            //response.Data = result;
            response.Data = _mapper.Map<RequestDto>(result);
            return response;
            
         }
         public async Task<ServiceResponse<List<RequestDto>>> AddItem(ResponseDto item)
         {
            var response = new ServiceResponse<List<RequestDto>>();
            var record =  _mapper.Map<Character>(item);
            record.Id = list.Max(p=> p.Id) + 1;
            list.Add(record);
            
            response.Data = list.Select(p => _mapper.Map<RequestDto>(p)).ToList();
            return response;
         }

         public async Task<ServiceResponse<RequestDto>> UpdateItem(UpdateDto item)
         {
            var response = new ServiceResponse<RequestDto>();   
            try
            {
               
               var record =  list.FirstOrDefault(p => p.Id == item.Id);

               if(record == null){throw new Exception("Id '"+ item.Id +"' does not exist.");}
               record.Name = item.Name;
               record.Strength = item.Strength;
               record.Class = item.Class;
               
               response.Data = _mapper.Map<RequestDto>(record);
               //return response;

            }
            catch(Exception ex)
            {
               response.Success = false;
               response.Message = ex.Message;
            }

            return response;
            
         }

    }
}