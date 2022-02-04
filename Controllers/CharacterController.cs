using AssignmentWebAPI.Models.Domain;
using AssignmentWebAPI.Models.DTO.Character;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AssignmentWebAPI.ServiceRepositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

namespace AssignmentWebAPI.Controllers
{
    /// <summary>
    /// Characters controller with characterServices.
    /// </summary>

    [Route("api/v1/Characters")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterService _characterService;
        private readonly IMapper _mapper;

        public CharacterController(ICharacterService characterService, IMapper mapper)
        {
            _characterService = characterService;
            _mapper = mapper;
        }
        /// <summary>
        /// Reads all Characters
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CharacterReadDTO>>> GetCharacters()
        {
            var characterData = _mapper.Map<IEnumerable<CharacterReadDTO>>(await _characterService.GetAllCharactersAsync());
            return Ok(characterData);
        }
        /// <summary>
        /// Reads Character by given id
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<CharacterReadDTO>> GetCharacter(int id)
        {
            Character character = await _characterService.GetSpecificCharacterAsync(id);
            if (character == null)
            {
                return NotFound();
            }
            var data =_mapper.Map<CharacterReadDTO>(character);
            return Ok(data);
        }
        /// <summary>
        /// Adds character
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<ActionResult<Character>> PostCharacter(CharacterCreateDTO character)
        {
            Character domainCharacter = _mapper.Map<Character>(character);

            domainCharacter = await _characterService.AddCharacterAsync(domainCharacter);

            return CreatedAtAction("GetCharacter",
            new { id = domainCharacter.Id },
            _mapper.Map<CharacterReadDTO>(domainCharacter));

        }
        /// <summary>
        /// Updates character
        /// </summary>
        /// <param name="id"></param>
        /// <param name="character"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCharacter(int id, CharacterEditDTO character)
        {
            if(id != character.Id)
            {
                return BadRequest();

            }
            if (!_characterService.CharacterExists(id))
            {
                return NotFound();
            }

            Character domainCharacter = _mapper.Map<Character>(character);
            await _characterService.UpdateCharacterAsync(domainCharacter);

            return NoContent();
        }
        /// <summary>
        /// Delete Character
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCharacter(int id)
        {
            if (!_characterService.CharacterExists(id))
            {
                return NotFound();
            }
                
            await _characterService.DeleteCharacterAsync(id);
   
            return NoContent();
        }
    }
}
