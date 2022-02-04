using AssignmentWebAPI.Models;
using AssignmentWebAPI.Models.Domain;
using Microsoft.EntityFrameworkCore;
using AssignmentWebAPI.ServiceRepositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssignmentWebAPI.Services
{
    public class MovieService : IMovieService
    {
        private readonly MovieCharacterDbContext _context;
        public MovieService(MovieCharacterDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Movie>> GetAllMoviesAsync()
        {
            return await _context.Movies.Include(m => m.Characters).ToListAsync();
        }
        public async Task<Movie> GetSpecificMovieAsync(int id)
        {
            return await _context.Movies.Where(c => c.Id == id).Include(m => m.Characters).FirstAsync();
        }
        public async Task<IEnumerable<Character>> GetCharacterInMovieAsync(int id)
        {
            var movie = await _context.Movies.Where(m => m.Id == id)
                .Include(c => c.Characters)
                .Select(c => c.Characters).ToListAsync();

            List<Character> listOfCharacters = new();

            foreach (var characters in movie)
            {
                foreach (var character in characters)
                {
                    listOfCharacters.Add(character);
                }
            }
            return listOfCharacters.Distinct();

        }
        public async Task<Movie> AddMovieAsync(Movie movie)
        {
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();
            return movie;
        }
        public async Task UpdateMovieAsync(Movie movie)
        {
            _context.Entry(movie).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteMovieAsync(int id)
        {
            var Movie = await _context.Movies.FindAsync(id);
            _context.Remove(Movie);
            await _context.SaveChangesAsync();
        }
        public bool MovieExists(int id)
        {
            return _context.Movies.Any(m => m.Id == id);
        }
      
      
    }
}
