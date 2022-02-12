using System.ComponentModel.DataAnnotations;

namespace MoviesApi.Domain.Models.Authenticate
{
    public interface IAuthenticate
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
