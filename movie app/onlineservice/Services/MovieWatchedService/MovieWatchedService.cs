using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using onlineservice.Dto.MovieWatched;
using System.Text.Json;
using System.Diagnostics.Eventing.Reader;

namespace onlineservice.Services.MovieWatchedService
{
    public class MovieWatchedService : IMovieWatchedService
    {

         private readonly IMapper _mapper;
         private readonly DataContext _context;
        public MovieWatchedService(IMapper mapper, DataContext context)
        {
         _context = context;
          _mapper = mapper;
        }
        
         public async Task<ServiceResponse<List<MovieWatchedRequestDto>>> MovieWatchedGetAll()
         {
            var response = new ServiceResponse<List<MovieWatchedRequestDto>>();
            var db = await _context.MoviesWatched.ToListAsync();
            response.Data = db.Select(p => _mapper.Map<MovieWatchedRequestDto>(p)).ToList();
            return response;
         }
         public async Task<ServiceResponse<MovieWatchedRequestDto>> MovieWatchedGetById(int id)
         {
            var response = new ServiceResponse<MovieWatchedRequestDto>();
            var result = _context.MoviesWatched.FirstOrDefaultAsync(p => p.Id == id);

            //response.Data = result;
            response.Data = _mapper.Map<MovieWatchedRequestDto>(result);
            return response;
            
         }
         public async Task<ServiceResponse<List<MovieWatchedRequestDto>>> MovieWatchedAddItem(MovieWatchedResponseDto item)
         {
            var response = new ServiceResponse<List<MovieWatchedRequestDto>>();
            var record =  _mapper.Map<MoviesWatched>(item);
            record.Id = _context.MoviesWatched.Max(p=> p.Id) + 1;
            _context.MoviesWatched.Add(record);
            
            response.Data = _context.Movies.Select(p => _mapper.Map<MovieWatchedRequestDto>(p)).ToList();
            return response;
         }

         public async Task<ServiceResponse<MovieWatchedRequestDto>> MovieWatchedUpdateItem(MovieWatchedUpdateDto item)
         {
            var response = new ServiceResponse<MovieWatchedRequestDto>();   
            try
            {
               
               var record =  _context.MoviesWatched.FirstOrDefault(p => p.Id == item.Id);

               if(record == null){throw new Exception("Id '"+ item.Id +"' does not exist.");}
               
               record.Id = item.Id; 
                              
               response.Data = _mapper.Map<MovieWatchedRequestDto>(record);

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