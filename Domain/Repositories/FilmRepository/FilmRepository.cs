using Microsoft.EntityFrameworkCore;
using MoviesApi.Domain.Models;
using MoviesApi.Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesApi.Domain.Repositories.FilmRepository
{
    public class FilmRepository : IFilmRepository
    {
        private readonly AppDbContext context;

        public FilmRepository(AppDbContext context)
        {
            this.context = context;
        }

        public void Add(Film film)
        {
            context.Add(film);
            context.SaveChanges();
        }

        public async Task<IEnumerable<Film>> GetAllMoviesAsync()
        {
            return await context.Films.Include(f=>f.FilmCopies).ToArrayAsync(); 
        }

        public async Task<Film> GetFilmInfoByIdAsync(int id)
        {
            return  await context.Films.Include(i=>i.FilmCopies).FirstOrDefaultAsync(f => f.FilmId == id);
        }

        public void UpdateFilm(Film film)
        {
            context.Films.Update(film);
            context.SaveChanges();
        }
    }
}
