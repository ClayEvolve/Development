using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using onlineservice.Dto.MovieWatched;



namespace onlineservice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovieWatchedController : ControllerBase
    {

        private readonly IMovieWatchedService _service;
        public MovieWatchedController(IMovieWatchedService service)
        {
            _service = service;
        }

        [HttpGet("AllMoviesWatched")]
        public async Task<ActionResult<ServiceResponse<List<MovieWatchedRequestDto>>>> GetAll()
        {
            return Ok(await _service.MovieWatchedGetAll());
        }

        [HttpGet("SingleMoviesWatched/{id}")]
        public async Task<ActionResult<ServiceResponse<MovieWatchedRequestDto>>> GetSingle(int id)
        {
            return Ok(await _service.MovieWatchedGetById(id));
        }
        
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<MovieWatchedResponseDto>>>> AddMovieWatched(MovieWatchedResponseDto item)
        {
            return Ok(await _service.MovieWatchedAddItem(item));
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<MovieWatchedRequestDto>>> UpdateMovieWatched(MovieWatchedUpdateDto item)
        {

            var response = await _service.MovieWatchedUpdateItem(item);

            if(response.Data == null)
            {
                return NotFound(response);
            }

            return Ok(response);
        } 

    }
}