using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using onlineservice.Dto.Character;



namespace onlineservice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CharacterController : ControllerBase
    {

        private readonly ICharacterService _characterService;
        public CharacterController(ICharacterService characterService)
        {
            _characterService = characterService;
        }
        
        [HttpGet("AllCharacters")]
        public async Task<ActionResult<ServiceResponse<List<RequestDto>>>> GetAll()
        {
            return Ok(await _characterService.GetAll());
        }

       

        [HttpGet("SingleCharacter/{id}")]
        public async Task<ActionResult<ServiceResponse<RequestDto>>> GetSingle(int id)
        {
            return Ok(await _characterService.GetById(id));
        }
        
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<ResponseDto>>>> AddCharacter(ResponseDto item)
        {
            return Ok(await _characterService.AddItem(item));
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<RequestDto>>> UpdateCharacter(UpdateDto item)
        {

            var response = await _characterService.UpdateItem(item);

            if(response.Data == null)
            {
                return NotFound(response);
            }

            return Ok(response);
        } 

    }
}