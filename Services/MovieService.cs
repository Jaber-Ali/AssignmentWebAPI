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
        /// <summary>
        /// Redas All movies from db
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Movie>> GetAllMoviesAsync()
        {
            return await _context.Movies.Include(m => m.Characters).ToListAsync();
        }
        /// <summary>
        /// Reads movie by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Movie> GetSpecificMovieAsync(int id)
        {
            return await _context.Movies.Where(c => c.Id == id).Include(m => m.Characters).FirstAsync();
        }
        /// <summary>
        /// this returns characters in a movie
        /// </summary>
        /// <param name="id">movie id</param>
        /// <returns>list of movies</returns>
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
        /// <summary>
        /// adds movie to db
        /// </summary>
        /// <param name="movie"></param>
        /// <returns></returns>
        public async Task<Movie> AddMovieAsync(Movie movie)
        {
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();
            return movie;
        }
        /// <summary>
        /// updates movie
        /// </summary>
        /// <param name="movie"></param>
        /// <returns></returns>
        public async Task UpdateMovieAsync(Movie movie)
        {
            _context.Entry(movie).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        /// <summary>
        /// delets movie
        /// </summary>
        /// <param name="id">moive id</param>
        /// <returns></returns>

        public async Task DeleteMovieAsync(int id)
        {
            var Movie = await _context.Movies.FindAsync(id);
            _context.Remove(Movie);
            await _context.SaveChangesAsync();
        }
        /// <summary>
        /// validation if moive exist
        /// </summary>
        /// <param name="id">moive id</param>
        /// <returns></returns>
        public bool MovieExists(int id)
        {
            return _context.Movies.Any(m => m.Id == id);
        }
      
      
    }
}
