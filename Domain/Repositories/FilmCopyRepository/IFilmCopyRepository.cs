using MoviesApi.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoviesApi.Domain.Repositories.FilmCopyRepository
{
    public interface IFilmCopyRepository
    {
        void AddCopies(int number, int filmId);
        void Update(List<FilmCopy> filmCopy, int filmId , int filmCopyId);
        Task<FilmCopy> findCopy(int filmid);
        Task<FilmCopy> CheckBorrow(int studioId, int filmId);
        bool ReturnFilm(FilmCopy CopyToBack, FilmCopy NewEdit);
        Task<FilmCopy> GetCopy(int filmid);
        void Borrow(FilmCopy filmCopy, FilmCopy newInfo);

        Task<IEnumerable<FilmCopy>> GetMyRents(int id);

    }
}
