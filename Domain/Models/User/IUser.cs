using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MoviesApi.Domain.Models
{

  interface IUser
  {
    public int UserId { get; set; }
    public string Role { get; set; }
        public string FilmStudioId { get; set; }
        public FilmStudio FilmStudio { get; set; }

        public string Password { get; set; }

    public string Token { get; set; }
  }
}