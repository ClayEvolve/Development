using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using onlineservice.Models;
using onlineservice.Dto.Movie;
using onlineservice.Dto.MovieWatched;

namespace onlineservice
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Movies, MovieRequestDto>();
            CreateMap<MovieResponseDto, Movies>();
            CreateMap<SourceApiResults, MovieRequestDto>();
            CreateMap<MoviesWatched, MovieWatchedRequestDto>();
            CreateMap<MovieWatchedResponseDto, MoviesWatched>();
        }

    }
}