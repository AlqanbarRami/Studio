using System.Collections.Generic;
using System.Threading.Tasks;
using MoviesApi.Domain.Models;
using MoviesApi.Domain.Models.RegisterFilmStudio;

namespace MoviesApi.Domain.Repositories
{
    public interface IFilmStudioRepository
    {

        Task<IEnumerable<FilmStudio>> GetList();
        Task<IEnumerable<FilmStudio>> GetFilmStudioById(int id);
        Task<FilmStudio> AddNewFilmStudio(RegisterFilmStudio filmStudio);
        FilmStudio GetByIdAsync(int id);
       
    }
}
