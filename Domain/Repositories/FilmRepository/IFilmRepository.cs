using MoviesApi.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoviesApi.Domain.Repositories
{
    public interface IFilmRepository
    {
        Task<IEnumerable<Film>> GetAllMoviesAsync();
        Task<Film> GetFilmInfoByIdAsync(int id);

        void UpdateFilm(Film film);
        void Add(Film film);
    }
}
