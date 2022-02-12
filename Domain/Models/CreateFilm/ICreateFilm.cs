using System;
using System.ComponentModel.DataAnnotations;

namespace MoviesApi.Domain.Models
{
  public interface ICreateFilm
  {
    public string Name { get; set; }
    public string ReleaseDate { get; set; }
    public string Country { get; set; }
    public string Director { get; set; }
    public int NumberOfCopies { get; set; }
  }
}