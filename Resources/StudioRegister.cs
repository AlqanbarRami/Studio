using MoviesApi.Domain.Models;
using System.Collections.Generic;

namespace MoviesApi.Resources
{
    public class StudioRegister 
    {
        public int FilmStudioId { get ; set ; }
        public string Name { get ; set ; }
        public string City { get; set ; }
        public List<FilmCopy> RentedFilmCopies { get ; set; }
       
    }
}
