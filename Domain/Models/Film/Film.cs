using System;
using System.Collections.Generic;

namespace MoviesApi.Domain.Models
{
    public class Film : IFilm
    {
        public int FilmId { get; set; }
        public string Name { get; set; }
        public string ReleaseDate { get; set ; }
        public string Country { get; set ; }
        public string Director { get ; set ; }
        public List<FilmCopy> FilmCopies { get ; set ; }
    }
}
