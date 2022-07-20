using Film_Management_System_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Film_Management_System_API.Infrastructure
{
    public class FilmRepository : IMovieRepository
    {
        private readonly MoviesContext appDbContext;

        public FilmRepository(MoviesContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<IEnumerable<Film>> Search(string name, Film? fil)
        {
            IQueryable<Film> query = appDbContext.Films;

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(e => e.Title.Contains(name));

            }



            return await query.ToListAsync();
        }
    }
}
    
