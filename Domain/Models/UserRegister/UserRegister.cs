using System.ComponentModel.DataAnnotations;

namespace MoviesApi.Domain.Models.UserRegister
{
    public class UserRegister : IUserRegister
    {
        [Required]
        public bool IsAdmin { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get ; set; }
    }
}
