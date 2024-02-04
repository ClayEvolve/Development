using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using onlineservice.Dto.Movie;
using System.Text.Json;
using System.Diagnostics.Eventing.Reader;

namespace onlineservice.Services.MovieService
{
    public class MovieService : IMovieService
    {

         private readonly IMapper _mapper;
         private readonly DataContext _context;
        public MovieService(IMapper mapper, DataContext context)
        {
         _context = context;
          _mapper = mapper;
        }

         public async Task<ServiceResponse<List<MovieRequestDto>>> MovieGetAllFromSource(string searchPattern)
         {
            var response = new ServiceResponse<List<MovieRequestDto>>();
            try
            {
               var list = await MovieGetSourceMovies(searchPattern);
               response.Data = list;
            }
            catch(Exception ex)
            {
               response.Success = false;
               response.Message = ex.Message;
            }

            return response;
         }
         private async Task<List<MovieRequestDto>> MovieGetSourceMovies(string searchPattern)
         {
            List<MovieRequestDto> response = new List<MovieRequestDto>();
            var destinationList = new List<Movies>();
            var sourceList = new List<Movies>();
            var context = _context;
            try
            {
               using (var httpClient = new HttpClient())
               {
                  using (var httpResponse = await httpClient.GetAsync(@"http://www.omdbapi.com/?s="+ searchPattern + "&apikey=f7332a24"))
                  {
                     var apiResponse = await httpResponse.Content.ReadAsStringAsync();
                        

                     SourceApiResults ouput = JsonSerializer.Deserialize<SourceApiResults>(apiResponse);

                     if(ouput.Search.Count == 0)
                     {
                        throw new Exception("No Data for Search Pattern '"+ searchPattern +"'");
                     }

                     await context.Movies.ToListAsync();
                     
                     destinationList = context.Movies.Select(p =>  _mapper.Map<Movies>(p)).ToList();
                     sourceList = ouput?.Search.Select(p =>  _mapper.Map<Movies>(p)).ToList();

                     
                     int uniqueId = context.Movies.Count() == 0 ? 0  : context.Movies.Max(p => p.Id);
                     
                     foreach (var item in sourceList)
                     {
                        var record = context.Movies.FirstOrDefault(p=> p.imdbID == item.imdbID);

                        if(record == null)
                        {
                           uniqueId +=1;
                           item.Id = uniqueId;
                           context.Movies.Add(item);
                        }
                        else 
                        {
                           record.Id = record.Id; 
                           record.Title = item.Title;
                           record.Year = item.Year;
                           record.imdbID = item.imdbID;
                           record.Poster = item.Poster;      
                        }
                        context.SaveChanges();
                     }
                     response = sourceList.Select(p => _mapper.Map<MovieRequestDto>(p)).ToList(); 
                  }
               }
            }
            catch(Exception ex)
            {
               throw ex;
            }
            return response;
         }
        
         public async Task<ServiceResponse<List<MovieRequestDto>>> MovieGetAll()
         {
            var response = new ServiceResponse<List<MovieRequestDto>>();
            var db = await _context.Movies.ToListAsync();
            response.Data = db.Select(p => _mapper.Map<MovieRequestDto>(p)).ToList();
            return response;
         }
         public async Task<ServiceResponse<MovieRequestDto>> MovieGetById(int id)
         {
            var response = new ServiceResponse<MovieRequestDto>();
            var result = _context.Movies.FirstOrDefaultAsync(p => p.Id == id);

            //response.Data = result;
            response.Data = _mapper.Map<MovieRequestDto>(result);
            return response;
            
         }
         public async Task<ServiceResponse<List<MovieRequestDto>>> MovieAddItem(MovieResponseDto item)
         {
            var response = new ServiceResponse<List<MovieRequestDto>>();
            var record =  _mapper.Map<Movies>(item);
            record.Id = _context.Movies.Max(p=> p.Id) + 1;
            _context.Movies.Add(record);
            
            response.Data = _context.Movies.Select(p => _mapper.Map<MovieRequestDto>(p)).ToList();
            return response;
         }

         public async Task<ServiceResponse<MovieRequestDto>> MovieUpdateItem(MovieUpdateDto item)
         {
            var response = new ServiceResponse<MovieRequestDto>();   
            try
            {
               
               var record =  _context.Movies.FirstOrDefault(p => p.Id == item.Id);

               if(record == null){throw new Exception("Id '"+ item.Id +"' does not exist.");}
               
               record.Id = item.Id; 
               record.Title = item.Title;
               record.Year = item.Year;
               record.imdbID = item.imdbID;
               record.Poster = item.Poster;                    

               
               response.Data = _mapper.Map<MovieRequestDto>(record);
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