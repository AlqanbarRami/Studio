using System.Collections.Generic;

namespace MoviesApi.Domain.Models
{
    public class FilmStudio : IFIilmStudio
    {
        public int FilmStudioId { get ; set ; }
        public string Name { get; set; }
        public string City { get ; set; }
        public List<FilmCopy> RentedFilmCopies { get; set ; }
    }
}
