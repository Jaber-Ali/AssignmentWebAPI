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
using AssignmentWebAPI.Models.DTO.Franchise;
using AssignmentWebAPI.Models.DTO.Character;
using AssignmentWebAPI.Models.DTO.Movie;
using System.Net.Mime;

namespace AssignmentWebAPI.Controllers
{
    [Route("api/v1/Franchises")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class FranchiseController : ControllerBase
    {
        private readonly IFranchiseService _franchiseService;
        private readonly IMovieService _movieService;
        private readonly IMapper _mapper;
        public FranchiseController(IFranchiseService franchiseService, IMapper mapper,  IMovieService movieService)
        {
            _franchiseService = franchiseService;
           // _characterService = characterService;
            _movieService = movieService;
            _mapper = mapper;
        }
        /// <summary>
        /// Reads all franchises
        /// </summary>
        /// <returns></returns>
        // GET: api/Franchise
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FranchiseReadDTO>>> GetAllFranchises()
        {
            return _mapper.Map<List<FranchiseReadDTO>>(await _franchiseService.GetAllFranchisesAsync());
        
        }
        /// <summary>
        /// Reads franchise by given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        // GET: api/Franchise/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Franchise>> GetFranchiseById(int id)
        {
            var franchise = await _franchiseService.GetSpecificFranchiseAsync(id);
            if (franchise == null)
            {
                return NotFound();
            }

            var franciseData = _mapper.Map<FranchiseReadDTO>(franchise);
            return Ok(franciseData);
        }
        /// <summary>
        /// Reads all characters in franchise by given Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}/characters")]
        public async Task<ActionResult<IEnumerable<CharacterReadDTO>>> GetAllCharactersInFranchise(int id)
        {
            if (!_franchiseService.FranchiseExists(id))
            {
                return NotFound();
            }

            var franchiseCharacter = await _franchiseService.GetAllCharactersInFranchiseAsync(id);
            var data = _mapper.Map<IEnumerable<CharacterReadDTO>>(franchiseCharacter);
            return Ok(data);
        }
        /// <summary>
        /// Read all movies in franchise by given Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}/movies")]
        public async Task<ActionResult<IEnumerable<MovieReadDTO>>> GetAllMoviesInFranchise(int id)
        {
            if (!_franchiseService.FranchiseExists(id))
            {
                return NotFound();
            }

            var franchiseMovie = await _franchiseService.GetAllMoviesInFranchiseAsync(id);

            var data = _mapper.Map<IEnumerable<MovieReadDTO>>(franchiseMovie);
            return Ok(data);
        }
        /// <summary>
        /// Adds franchise to db
        /// </summary>
        /// <param name="franchise"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        // POST: api/Franchise
        [HttpPost]
        public async Task<ActionResult<Franchise>> PostFranchise(FranchiseCreateDTO franchise)
        {
            Franchise domainFranchise = _mapper.Map<Franchise>(franchise);
            domainFranchise = await _franchiseService.AddFranchiseAsync(domainFranchise);

            return CreatedAtAction("GetFranchiseById", new { id = domainFranchise.Id },
                  _mapper.Map<FranchiseReadDTO>(domainFranchise));
        }

        /// <summary>
        /// Updates franchise 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="franchise"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        // PUT: api/Franchise/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFranchise(int id, FranchiseEditDTO franchise)
        {
            if (id != franchise.Id)
            {
                return BadRequest();
            }

                if (!_franchiseService.FranchiseExists(id))
                {
                    return NotFound();
                }

            Franchise domainFranchise = _mapper.Map<Franchise>(franchise);
            await _franchiseService.UpdateFranchiseAsync(domainFranchise);

            return NoContent();
        }
        /// <summary>
        /// Updates movies in franchise by taking 2 integers
        /// </summary>
        /// <param name="movieId">movieId</param>
        /// <param name="franchiseId">franchiseId</param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut("{movieId}/movies/{franchiseId}", Name = ("GetMovieById"))]
        public async Task<ActionResult<IEnumerable<MovieReadDTO>>> UpdateMoviesInFranchise(int movieId, int franchiseId)
        {
            if (!_movieService.MovieExists(movieId) || !_franchiseService.FranchiseExists(franchiseId))
            {
                return NotFound();
            }

            Movie domainMovie = await _movieService.GetSpecificMovieAsync(movieId);
            domainMovie.FranchiseId = franchiseId;
            try
            {
                await _movieService.UpdateMovieAsync(domainMovie);
            }
            catch (KeyNotFoundException)
            {
                return BadRequest();
            }
            return CreatedAtAction("GetMovieById",
                   new { id = domainMovie.Id },
                    _mapper.Map<MovieReadDTO>(domainMovie));
        }
        /// <summary>
        /// Deletes movie from franchise
        /// </summary>
        /// <param name="movieId">movieId</param>
        /// <param name="franchiseId">franchiseId</param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{movieId}/delete/{franchiseId}")]
        public async Task<ActionResult<MovieReadDTO>> DeleteMovieFromFranchise(int movieId, int franchiseId)
        {
            if (!_movieService.MovieExists(movieId) || !_franchiseService.FranchiseExists(franchiseId))
            {
                return NotFound();
            }

            Movie domainMovie = await _movieService.GetSpecificMovieAsync(movieId);
            //Franchise domaninFrancise = await _franchiseService.GetSpecificFranchiseAsync(franchiseId);
            domainMovie.FranchiseId = null;
            try
            {
                await _movieService.UpdateMovieAsync(domainMovie);
            }
            catch (KeyNotFoundException)
            {
                return BadRequest();
            }
            return CreatedAtAction("GetMovieById",
                  new { id = domainMovie.Id },
                   _mapper.Map<MovieReadDTO>(domainMovie));
           
        }
        /// <summary>
        /// Deletes franchise by given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        // DELETE: api/Franchise/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFranchise(int id)
        {
            if (!_franchiseService.FranchiseExists(id))
            {
                return NotFound();
            }
            Franchise franchise = await _franchiseService.GetSpecificFranchiseAsync(id);

            foreach (Movie movie in await _movieService.GetAllMoviesAsync())
            {
                if (movie.FranchiseId == franchise.Id)
                {
                    movie.FranchiseId = null;
                }
            }

            await _franchiseService.DeleteFranchiseAsync(id);

            return NoContent();
        }
        /// <summary>
        /// This is a read movie by given id that has been used in this class
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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


    }

}
