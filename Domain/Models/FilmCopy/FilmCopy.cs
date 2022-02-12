
using MoviesApi.Domain;

namespace MoviesApi.Domain.Models
{
    public class FilmCopy : IFilmCopy
    {
        public int FilmCopyId { get; set ; }
        public int FilmId { get ; set ; }
        public int StudioId { get; set; }
        public bool RentedOut { get ; set ; } 
        
    }
}
