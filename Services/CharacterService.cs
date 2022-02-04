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
    public class CharacterService : ICharacterService
    {

        private readonly MovieCharacterDbContext _context;
        public CharacterService(MovieCharacterDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Character>> GetAllCharactersAsync()
        {
            return await _context.Characters.Include(c => c.Movies).ToListAsync();
        }
        public async Task<Character> GetSpecificCharacterAsync(int id)
        {
            return await _context.Characters.FindAsync(id);
        }
        public async Task<Character> AddCharacterAsync(Character character)
        {
            _context.Characters.Add(character);
            await _context.SaveChangesAsync();
            return character;
        }
        public async Task UpdateCharacterAsync(Character character)
        {
            _context.Entry(character).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public async Task DeleteCharacterAsync(int id)
        {
            var character = await _context.Characters.FindAsync(id);
            _context.Remove(character);
            await _context.SaveChangesAsync();
        }

        public bool CharacterExists(int id)
        {
            return _context.Characters.Any(c => c.Id == id);
        }

        
    }
}

