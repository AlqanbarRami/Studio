using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MoviesApi.Domain.Models
{

  interface IFilm
  {
    public int FilmId { get; set; }
    public string Name { get; set; }
    public string ReleaseDate { get; set; }
    public string Country { get; set; }
    public string Director { get; set; }
    public List<FilmCopy> FilmCopies { get; set; }
  }

}