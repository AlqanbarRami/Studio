using Microsoft.EntityFrameworkCore;
using MoviesApi.Domain.Models;
using MoviesApi.Domain.Models.RegisterFilmStudio;
using MoviesApi.Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesApi.Domain.Repositories.FilmStudioRepository
{
    public class FilmStudioRepository : IFilmStudioRepository
    {

        private readonly AppDbContext context;

        public FilmStudioRepository(AppDbContext context)
        {
            this.context = context;
            
        }

       
        public async Task<FilmStudio> AddNewFilmStudio(RegisterFilmStudio filmStudio)
        {
    
            var company = new FilmStudio
            {
                    City = filmStudio.FilmStudioCity,
                    Name = filmStudio.FilmStudioName
                };
                context.FilmStudios.Add(company);
                await context.SaveChangesAsync();
            return company;               
     
        }

        public FilmStudio GetByIdAsync(int id)
        {
            return context.FilmStudios.FirstOrDefault(f => f.FilmStudioId == id);
        }

        public async Task<IEnumerable<FilmStudio>> GetFilmStudioById(int id)
        {

            var filmStudio = await context.FilmStudios.Include(f=>f.RentedFilmCopies).Where(i=>i.FilmStudioId==id).ToArrayAsync();
            if(filmStudio.Count() >= 1)
            {
                return filmStudio;
            }
            else
            {
                return null;
            }
        }

        public async Task<IEnumerable<FilmStudio>> GetList()
        {
            return await context.FilmStudios.Include(f=>f.RentedFilmCopies).ToArrayAsync();
        }


    }
}
