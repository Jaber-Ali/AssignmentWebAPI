using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AssignmentWebAPI.Models;
using AssignmentWebAPI.Models.Domain;
using AssignmentWebAPI.ServiceRepositories;
using AutoMapper;
using AssignmentWebAPI.Models.DTO.Movie;
using System.Net.Mime;
using AssignmentWebAPI.Models.DTO.Character;
using System.Data;

namespace AssignmentWebAPI.Controllers
{
   
    [Route("api/v1/Movies")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Produces(MediaTypeNames.Application.Json)]
    public class MovieController : ControllerBase
    {
        
        private readonly IMovieService _movieService;
        private readonly ICharacterService _characterService;
        private readonly IMapper _mapper;
       
        public MovieController(IMovieService movieService, IMapper mapper, ICharacterService characterService)
        {
            _movieService = movieService;
            _characterService = characterService;
            _mapper = mapper;

        }
        /// <summary>
        /// Reads all movies
        /// </summary>
        /// <returns></returns>

        // GET: api/Movie
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieReadDTO>>> GetAllMovies()
        {
            var movieData = _mapper.Map<IEnumerable<MovieReadDTO>>(
                await _movieService.GetAllMoviesAsync());
            return Ok(movieData);
        }
        /// <summary>
        /// Reads moive by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/Movie/5
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
       // [ActionName("GetMovieById")]
        public async Task<ActionResult<MovieReadDTO>> GetMovieById(int id)
        {
            var movie = await _movieService.GetSpecificMovieAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            var movieData = _mapper.Map<MovieReadDTO>(movie);
            return Ok(movieData);
        }
        /// <summary>
        /// Adds moive
        /// </summary>
        /// <param name="movie"></param>
        /// <returns></returns>
        // POST: api/Movie
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<ActionResult<Movie>> PostMovie(MovieCreateDTO movie)
        {
            Movie domainMovie = _mapper.Map<Movie>(movie);

            domainMovie = await _movieService.AddMovieAsync(domainMovie);

            return CreatedAtAction("GetMovieById",
                   new { id = domainMovie.Id },
                    _mapper.Map<MovieCreateDTO>(domainMovie));

        }
        /// <summary>
        /// Update moive
        /// </summary>
        /// <param name="id"></param>
        /// <param name="movie"></param>
        /// <returns></returns>
        // PUT: api/Movie/5
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovie(int id, MovieEditDTO movie)
        {
            if (id != movie.Id)
            {
                return BadRequest();
            }
            if (!_movieService.MovieExists(id))
            {
                return NotFound();
            }
            Movie domainMovie = _mapper.Map<Movie>(movie);
            await _movieService.UpdateMovieAsync(domainMovie);
            return NoContent();

        }
        /// <summary>
        /// Reads characters in a movie
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}/characters")]
        public async Task<ActionResult<IEnumerable<CharacterReadDTO>>> GetCharactersInMovie(int id)
        {
            if (!_movieService.MovieExists(id)) 
            {
                return NotFound();
            } 
            var movieCharacters = await _movieService.GetCharacterInMovieAsync(id);
            var data = _mapper.Map<IEnumerable<CharacterReadDTO>>(movieCharacters);
            return Ok(data);
          
        }
        /// <summary>
        /// Updates character in movie by taking two integers
        /// </summary>
        /// <param name="movieId">movieId</param>
        /// <param name="characterId">characterId</param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut("{movieId}/update/{characterId}")]
        public async Task<ActionResult<IEnumerable<CharacterReadDTO>>> UpdateCharacterInMovie(int movieId, int characterId)
        {
            if (!_movieService.MovieExists(movieId) ||  !_characterService.CharacterExists(characterId))
            {
                return NotFound();
            }

            Movie domainMovie = await _movieService.GetSpecificMovieAsync(movieId);
            Character domainCharacter = await _characterService.GetSpecificCharacterAsync(characterId);

            if (domainMovie.Characters.Where(c => c.Id == characterId).Count() == 1) 
            {
                return NoContent();
            } 
            var characters = domainMovie.Characters;
            characters.Add(domainCharacter);
            try
            {
                await _movieService.UpdateMovieAsync(domainMovie);
            }
            catch (KeyNotFoundException)
            {
                return BadRequest();
            }
            var data = _mapper.Map<IEnumerable<CharacterReadDTO>>(characters);
            return Ok(data);
        }
        /// <summary>
        /// Deletes character from movie by taking two integers
        /// </summary>
        /// <param name="movieId">movieId</param>
        /// <param name="characterId">characterId</param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{movieId}/deleteCharacters/{characterId}")]
        public async Task<ActionResult<MovieReadDTO>> DeleteCharacterFromMovie(int movieId, int characterId)
        {
            if (!_movieService.MovieExists(movieId) || !_characterService.CharacterExists(characterId))
            {
                return NotFound();
            }

            Movie domainMovie = await _movieService.GetSpecificMovieAsync(movieId);
            Character domainCharacter = await _characterService.GetSpecificCharacterAsync(characterId);

            if (domainMovie.Characters.Where(c => c.Id == characterId).Count() == 0)
            {
                return NoContent();
            }
            var characters = domainMovie.Characters;
            characters.Remove(domainCharacter);
            domainMovie.Characters = characters;
            try
            {
                await _movieService.UpdateMovieAsync(domainMovie);
            }
            catch (KeyNotFoundException)
            {
                return BadRequest();
            }

            var data = _mapper.Map<IEnumerable<CharacterReadDTO>>(characters);
            return Ok(data);
        }
        /// <summary>
        /// Deletes movies from db
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/Movie/5
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            if (!_movieService.MovieExists(id))
            {
                return NotFound();
            }

            await _movieService.DeleteMovieAsync(id);

            return NoContent();
        }
    }
}
