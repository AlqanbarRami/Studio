namespace MoviesApi.Domain.Models.Authenticate
{
    public class Authenticate : IAuthenticate
    {
        public string Username { get ; set ; }
        public string Password { get ; set; }
    }
}
