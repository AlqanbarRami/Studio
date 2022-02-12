using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MoviesApi.Domain.Models.User
{
    public class User :IdentityUser,  IUser
    {
        public int UserId { get ; set; }
        public string Role { get ; set; }
        public string FilmStudioId { get ; set ; }
        public FilmStudio FilmStudio { get ; set ; }
        public string Token { get; set ; }
        [Required]
        public string Password { get ; set ; }
    }
}
