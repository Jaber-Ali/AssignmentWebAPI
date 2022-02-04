using AssignmentWebAPI.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssignmentWebAPI.ServiceRepositories
{
    public interface IMovieService 
    {
        public Task<IEnumerable<Character>> GetCharacterInMovieAsync(int id);
        public Task<IEnumerable<Movie>> GetAllMoviesAsync();
        public Task<Movie> GetSpecificMovieAsync(int id);
        public Task<Movie> AddMovieAsync(Movie movie);
        public Task UpdateMovieAsync(Movie movie);
        public Task DeleteMovieAsync(int id);
        public bool MovieExists(int id);

      
    }
}
