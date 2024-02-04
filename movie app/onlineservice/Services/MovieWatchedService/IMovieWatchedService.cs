using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using onlineservice.Dto.MovieWatched;

namespace onlineservice.Services.MovieWatchedService
{
    public interface IMovieWatchedService
    {

        public Task<ServiceResponse<List<MovieWatchedRequestDto>>> MovieWatchedGetAll();
        public  Task<ServiceResponse<MovieWatchedRequestDto>> MovieWatchedGetById(int id);
        public  Task<ServiceResponse<List<MovieWatchedRequestDto>>> MovieWatchedAddItem(MovieWatchedResponseDto item);
        public  Task<ServiceResponse<MovieWatchedRequestDto>> MovieWatchedUpdateItem(MovieWatchedUpdateDto item);
    }
}