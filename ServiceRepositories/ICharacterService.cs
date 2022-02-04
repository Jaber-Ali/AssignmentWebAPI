using AssignmentWebAPI.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssignmentWebAPI.ServiceRepositories
{
    /// <summary>
    /// repo service pattren
    /// </summary>
    public interface ICharacterService 
    {
        public Task<IEnumerable<Character>> GetAllCharactersAsync();
        public Task<Character> GetSpecificCharacterAsync(int id);
        public Task<Character> AddCharacterAsync(Character character);
        public Task UpdateCharacterAsync(Character character);
        public Task DeleteCharacterAsync(int id);
        public bool CharacterExists(int id);

    }
}
