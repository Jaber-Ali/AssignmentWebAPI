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
        private readonly MovieCharacterDbContext _context;
        public FranchiseService(MovieCharacterDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Franchise>> GetAllFranchisesAsync()
        {
            return await _context.Franchises.Include(f => f.Movies).ToListAsync();
        }


        public async Task<Franchise> AddFranchiseAsync(Franchise franchise)
        {
            _context.Franchises.Add(franchise);
            await _context.SaveChangesAsync();
            return franchise;
        }

        public async Task DeleteFranchiseAsync(int id)
        {
            var franchise = await _context.Franchises.FindAsync(id);
            _context.Franchises.Remove(franchise);
            await _context.SaveChangesAsync();
        }


        public bool FranchiseExists(int id)
        {
            return _context.Franchises.Any(c => c.Id == id);
        }

      
        public async Task<IEnumerable<Character>> GetAllCharactersInFranchiseAsync(int id)
        {
            return await _context.Movies.Where(m => m.FranchiseId == id).SelectMany(x => x.Characters).Distinct().ToListAsync();
        }

        public async Task<IEnumerable<Movie>> GetAllMoviesInFranchiseAsync(int id)
        {
            return await _context.Movies
                 .Where(m => m.FranchiseId == id)
                 .ToListAsync();
        }

        public async Task<Franchise> GetSpecificFranchiseAsync(int id)
        {
            return await _context.Franchises.FindAsync(id);
        }

        public async Task UpdateFranchiseAsync(Franchise franchise)
        {
            _context.Entry(franchise).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

       
    }
}
