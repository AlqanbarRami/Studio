using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MoviesApi.Domain.Models
{

    interface IUserRegister
    {
      public bool IsAdmin { get; set; }
      public string Username { get; set; }
      public string Password { get; set; }
    }
}