using System;

namespace MoviesApi.Domain.Models
{
  public interface IFilmCopy
  {
    public int FilmCopyId { get; set; }
    public int FilmId { get; set; }
    public int StudioId { get; set; }
    public bool RentedOut { get; set; }
   
  }
}