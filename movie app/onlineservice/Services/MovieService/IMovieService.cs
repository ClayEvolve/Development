using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using onlineservice.Dto.Movie;

namespace onlineservice.Services.MovieService
{
    public interface IMovieService
    {
        public Task<ServiceResponse<List<MovieRequestDto>>> MovieGetAllFromSource(string searchPattern);
        public Task<ServiceResponse<List<MovieRequestDto>>> MovieGetAll();
        public  Task<ServiceResponse<MovieRequestDto>> MovieGetById(int id);
        public  Task<ServiceResponse<List<MovieRequestDto>>> MovieAddItem(MovieResponseDto item);
        public  Task<ServiceResponse<MovieRequestDto>> MovieUpdateItem(MovieUpdateDto item);
    }
}