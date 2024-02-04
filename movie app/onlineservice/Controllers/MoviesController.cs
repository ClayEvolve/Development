using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using onlineservice.Dto.Movie;



namespace onlineservice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovieController : ControllerBase
    {

        private readonly IMovieService _service;
        public MovieController(IMovieService service)
        {
            _service = service;
        }
        [HttpPost("AllMoviesFromImDb/{searchPattern}")]
        public async Task<ActionResult<ServiceResponse<List<MovieRequestDto>>>> MovieGetAllFromSoure(string searchPattern)
        {
            
            var response = await _service.MovieGetAllFromSource(searchPattern);

            if(response.Data == null)
            {
                return NotFound(response);
            }

            return Ok(response);
        }

        [HttpGet("AllMovies")]
        public async Task<ActionResult<ServiceResponse<List<MovieRequestDto>>>> MovieGetAll()
        {
            return Ok(await _service.MovieGetAll());
        }

       

        [HttpGet("SingleMovie/{id}")]
        public async Task<ActionResult<ServiceResponse<MovieRequestDto>>> MovieGetSingle(int id)
        {
            return Ok(await _service.MovieGetById(id));
        }
        
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<MovieResponseDto>>>> MovieAddMovie(MovieResponseDto item)
        {
            return Ok(await _service.MovieAddItem(item));
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<MovieRequestDto>>> MovieUpdateMovie(MovieUpdateDto item)
        {

            var response = await _service.MovieUpdateItem(item);

            if(response.Data == null)
            {
                return NotFound(response);
            }

            return Ok(response);
        } 

        
    }
}