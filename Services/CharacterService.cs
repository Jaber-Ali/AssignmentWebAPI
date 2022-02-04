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
        /// <summary>
        /// Reads All Characters from db
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Character>> GetAllCharactersAsync()
        {
            return await _context.Characters.Include(c => c.Movies).ToListAsync();
        }
        /// <summary>
        /// Reads Character by id from db
        /// </summary>
        /// <returns></returns>
        public async Task<Character> GetSpecificCharacterAsync(int id)
        {
            return await _context.Characters.FindAsync(id);
        }
        /// <summary>
        /// Adds character to db
        /// </summary>
        /// <param name="character">character</param>
        /// <returns>returns added character</returns>
        public async Task<Character> AddCharacterAsync(Character character)
        {
            _context.Characters.Add(character);
            await _context.SaveChangesAsync();
            return character;
        }
        /// <summary>
        /// this update character and save changes
        /// </summary>
        /// <param name="character">character to be updated</param>
        /// <returns></returns>
        public async Task UpdateCharacterAsync(Character character)
        {
            _context.Entry(character).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        /// <summary>
        /// deletes character from db
        /// </summary>
        /// <param name="id">character id</param>
        /// <returns></returns>
        public async Task DeleteCharacterAsync(int id)
        {
            var character = await _context.Characters.FindAsync(id);
            _context.Remove(character);
            await _context.SaveChangesAsync();
        }
        /// <summary>
        /// validation if CharacterExists in db
        /// </summary>
        /// <param name="id">character id</param>
        /// <returns>return true if CharacterExists </returns>
        public bool CharacterExists(int id)
        {
            return _context.Characters.Any(c => c.Id == id);
        }

        
    }
}

