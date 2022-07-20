using Film_Management_System_API.Models;

namespace Film_Management_System_API.Infrastructure
{
    public interface IMovieRepository
    {
        Task<IEnumerable<Film>> Search(string name, Film? fil);
    }
}
