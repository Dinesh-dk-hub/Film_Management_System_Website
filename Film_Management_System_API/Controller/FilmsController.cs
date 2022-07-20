using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Film_Management_System_API.Models;
using Film_Management_System_API.Infrastructure;

namespace Film_Management_System_API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmsController : ControllerBase
    {
        /*private readonly IMovieRepository movieRepository;

        public FilmsController(IMovieRepository movieRepository)
        {
            this.movieRepository= movieRepository;
        }*/

        private readonly MoviesContext _context;

        public FilmsController(MoviesContext context)
        {
            _context = context;
        }


        // GET: api/Films
       
        [HttpGet]
        
        public async Task<ActionResult<IEnumerable<Film>>> GetFilms()
        {
            if (_context.Films == null)
            {
                return NotFound();
            }
            return await _context.Films.ToListAsync();
        }


       // GET: api/Films/5
        [HttpGet("{id}")]
        
        public async Task<ActionResult<Film>> GetFilm(decimal id)
        {
            if (_context.Films == null)
            {
                return NotFound();
            }
            var film = await _context.Films.FindAsync(id);

            if (film == null)
            {
                return NotFound();
            }

            return film;
        }
        
        [HttpGet("{lang}")]
       
        public ActionResult<string> GetFilmByLanguage(string lang) 
        {
            
           var fil  = _context.Films
     .FromSqlRaw("EXECUTE dbo.spGetFilmByLanguage")
     .ToList();
            return Ok(fil);
        }
        /* [HttpGet("{Language}")]


        /*[HttpGet("{search}")]
        public async Task<ActionResult<IEnumerable<Film>>> Search(string name, Film? fil)

        {
            try
            {
                var result = await movieRepository.Search(name, fil);

                if (result.Any())
                {
                    return Ok(result);
                }

                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }*/

        // PUT: api/Films/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Route("ModifyFilmsById")]
        [HttpPut("{id}")]
        
        public async Task<IActionResult> PutFilm(decimal id, Film film)
        {
            if (id != film.FilmId)
            {
                return BadRequest();
            }

            _context.Entry(film).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FilmExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Films

        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Route("PostFilmById")]
        [HttpPost]
    
        public async Task<ActionResult<Film>> PostFilm(Film film)
        {
          if (_context.Films == null)
          {
              return Problem("Entity set 'MoviesContext.Films'  is null.");
          }
            _context.Films.Add(film);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFilm", new { id = film.FilmId }, film);
        }

        // DELETE: api/Films/5
        [Route("DeleteFilmById")]
        [HttpDelete("{id}")]
       
        public async Task<IActionResult> DeleteFilm(decimal id)
        {
            if (_context.Films == null)
            {
                return NotFound();
            }
            var film = await _context.Films.FindAsync(id);
            if (film == null)
            {
                return NotFound();
            }

            _context.Films.Remove(film);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FilmExists(decimal id)
        {
            return (_context.Films?.Any(e => e.FilmId == id)).GetValueOrDefault();
        }
    }
}
