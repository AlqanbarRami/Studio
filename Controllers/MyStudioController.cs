using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoviesApi.Domain.Models;
using MoviesApi.Domain.Repositories;
using MoviesApi.Domain.Repositories.FilmCopyRepository;
using MoviesApi.Domain.Repositories.UserRepository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoviesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class MyStudioController : Controller
    {
        private readonly IFilmRepository filmRepository;
        private readonly IUserRepository userRepository;
        private readonly IFilmCopyRepository filmCopyRepository;

        public MyStudioController(IFilmRepository filmRepository, IUserRepository userRepository, IFilmCopyRepository filmCopyRepository)
        {
            this.filmRepository = filmRepository;
            this.filmCopyRepository = filmCopyRepository;
            this.userRepository = userRepository;
        }

        [HttpGet]
        [Route("rentals")]
        public async Task<ActionResult<IEnumerable<FilmCopy>>> GetFilms()
        {

            var userName = User.Identity.Name;
            var user = userRepository.GetByUserName(userName);
            var films = await filmCopyRepository.GetMyRents(int.Parse(user.FilmStudioId));
            return Ok(films);
        }
    }
}
