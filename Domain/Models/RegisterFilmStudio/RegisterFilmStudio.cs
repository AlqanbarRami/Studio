using System.ComponentModel.DataAnnotations;

namespace MoviesApi.Domain.Models.RegisterFilmStudio
{
    public class RegisterFilmStudio : IRegisterFilmStudio
    {
        [Required]
        public string FilmStudioCity { get; set; }
        [Required]
        public string FilmStudioName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Username { get; set; }
    }
}
