using Microsoft.EntityFrameworkCore;
using MoviesApi.Domain.Models;
using MoviesApi.Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesApi.Domain.Repositories.FilmCopyRepository
{
    public class FilmCopyRepository : IFilmCopyRepository
    {
        private readonly AppDbContext context;

        public FilmCopyRepository(AppDbContext context)
        {
            this.context = context;
        }

        public void AddCopies(int number, int filmId)
        {
            for (int i= 0; i < number; i++){
                context.FilmCopies.Add(new FilmCopy
                {
                    FilmId = filmId,
                    RentedOut = false
                });
                context.SaveChanges();
                
            }
            
           
        }

        public void Borrow(FilmCopy filmCopy, FilmCopy newInfo)
        {
            context.Entry(filmCopy).CurrentValues.SetValues(newInfo);
            context.SaveChanges();
            
        }

        public async Task<FilmCopy> CheckBorrow(int studioId, int filmId)
        {
            var find = await context.FilmCopies.Where(f => f.RentedOut == true && f.StudioId == studioId && f.FilmId == filmId ).FirstOrDefaultAsync();
            if(find == null)
            {
                return null;
            }
            else
            {
                return find;
            }
        }

        public async Task<FilmCopy> GetCopy(int filmid)
        {
            var copy = await context.FilmCopies.Where(f => f.FilmId == filmid && f.RentedOut == true).FirstOrDefaultAsync();
            if (copy == null)
            {
                return null;
            }
            else
            {
                return copy;
            }
        }
        public async Task<FilmCopy> findCopy(int filmid)
        {
            var copy =  await context.FilmCopies.Where(f => f.FilmId == filmid && f.RentedOut == false).FirstOrDefaultAsync();
            if(copy == null)
            {
                return null;
            }
            else
            {
                return copy;
            }
        }

        public bool ReturnFilm(FilmCopy CopyToBack, FilmCopy NewEdit)
        {
          
            context.Entry(CopyToBack).CurrentValues.SetValues(NewEdit);
            if(context.SaveChanges() > 1)
            {
                return true ;
            }
            else
            {
                return false;
            }
            

        }

        public void Update(List<FilmCopy> filmCopy, int filmId, int filmCopyId)
        {
           var item = context.FilmCopies.Where(f => f.FilmId == filmId && f.FilmCopyId == filmCopyId).FirstOrDefault();

            if ( item != null)
            {
                for(int i = 0; i<filmCopy.Count(); i++)
                {
                    //      context.FilmCopies.Update(filmCopy[i]);
                    filmCopy[i].FilmId = filmId;
                    context.Entry(item).CurrentValues.SetValues(filmCopy[i]);
                    context.SaveChanges();
                }
                     
            }
            context.SaveChanges();
        }

        public async Task<IEnumerable<FilmCopy>> GetMyRents(int id)
        {
            return await context.FilmCopies.Where(i => i.StudioId == id).ToArrayAsync();
        }
    }
}
