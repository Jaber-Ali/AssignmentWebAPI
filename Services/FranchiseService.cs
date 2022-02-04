using AssignmentWebAPI.Models;
using AssignmentWebAPI.Models.Domain;
using Microsoft.EntityFrameworkCore;
using AssignmentWebAPI.ServiceRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssignmentWebAPI.Services
{
    public class FranchiseService : IFranchiseService
    {
        /// <summary>
        /// all data linked to MovieCharacterDbContext
        /// </summary>
        private readonly MovieCharacterDbContext _context;
        public FranchiseService(MovieCharacterDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Reads All Francises from franchis db
        /// </summary>
        /// <returns>franchise</returns>
        public async Task<IEnumerable<Franchise>> GetAllFranchisesAsync()
        {
            return await _context.Franchises.Include(f => f.Movies).ToListAsync();
        }
        /// <summary>
        /// Reads franchise by id
        /// </summary>
        /// <param name="id">franchise id</param>
        /// <returns></returns>
        public async Task<Franchise> GetSpecificFranchiseAsync(int id)
        {
            return await _context.Franchises.FindAsync(id);
        }
        /// <summary>
        /// updates francise
        /// </summary>
        /// <param name="franchise">francise</param>
        /// <returns></returns>
        public async Task UpdateFranchiseAsync(Franchise franchise)
        {
            _context.Entry(franchise).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// adds Franchise to db
        /// </summary>
        /// <param name="franchise"></param>
        /// <returns></returns>
        public async Task<Franchise> AddFranchiseAsync(Franchise franchise)
        {
            _context.Franchises.Add(franchise);
            await _context.SaveChangesAsync();
            return franchise;
        }
        /// <summary>
        /// delete franchise
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteFranchiseAsync(int id)
        {
            var franchise = await _context.Franchises.FindAsync(id);
            _context.Franchises.Remove(franchise);
            await _context.SaveChangesAsync();
        }
        /// <summary>
        /// validation for farnchise if exist
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public bool FranchiseExists(int id)
        {
            return _context.Franchises.Any(c => c.Id == id);
        }
        /// <summary>
        /// this returns all the characters in franchises
        /// </summary>
        /// <param name="id">franchise id</param>
        /// <returns>list of characters</returns>

        public async Task<IEnumerable<Character>> GetAllCharactersInFranchiseAsync(int id)
        {
            return await _context.Movies.Where(m => m.FranchiseId == id).SelectMany(x => x.Characters).Distinct().ToListAsync();
        }
        /// <summary>
        /// this returns all the moives in franchises
        /// </summary>
        /// <param name="id">franchise id</param>
        /// <returns>List of movies</returns>

        public async Task<IEnumerable<Movie>> GetAllMoviesInFranchiseAsync(int id)
        {
            return await _context.Movies
                 .Where(m => m.FranchiseId == id)
                 .ToListAsync();
        }

    }
}
