using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoviesApi.Domain.Models;
using MoviesApi.Domain.Models.CreateFilm;
using MoviesApi.Domain.Repositories;
using MoviesApi.Domain.Repositories.FilmCopyRepository;
using MoviesApi.Domain.Repositories.UserRepository;
using MoviesApi.Resources;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Identity;
using MoviesApi.Domain.Models.User;
using System.Linq;

namespace MoviesApi.Controllers
{
    [Route("api/films")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class FilmController : Controller
    {
        private readonly IFilmRepository filmRepository;
        private readonly IUserRepository userRepository;
        private readonly IFilmCopyRepository filmCopyRepository;
        private readonly IMapper mapper;

        public FilmController( IFilmRepository filmRepository, IMapper mapper, IUserRepository userRepository, IFilmCopyRepository filmCopyRepository)
        {
            this.filmRepository = filmRepository;
            this.mapper = mapper;
            this.userRepository = userRepository;
            this.filmCopyRepository = filmCopyRepository;
 
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Film>>> GetFilms()
        {
            var userName = User.Identity.Name;
            var user = userRepository.GetByUserName(userName);
            if (user == null) {
                var data2 = await filmRepository.GetAllMoviesAsync();
                var resu = mapper.Map<FilmNUsers[]>(data2);
                return Ok(resu);
            }
            if (user.Role == "admin" || user.Role == "filmstudio")
            {
                var data = await filmRepository.GetAllMoviesAsync();
                return Ok(data);
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("return")]
        [AllowAnonymous]
        public async Task<ActionResult<Film>> Return(int id, string studioid)
        {
            var userName = User.Identity.Name;
            var user = userRepository.GetByUserName(userName);
            var film = filmRepository.GetFilmInfoByIdAsync(id);
            if (user == null ||  user.FilmStudioId != studioid)
            {
                return StatusCode(401);
            }
  
            if( user != null && user.FilmStudioId == studioid)
            {
                if (film == null)
                {
                    return StatusCode(409);
                }

                var copyToBack = await filmCopyRepository.GetCopy(id);
                if(copyToBack == null)
                {
                    return StatusCode(409);
                }
                else
                {
                    FilmCopy filmCopy = new FilmCopy
                    {
                        FilmCopyId = copyToBack.FilmCopyId,
                        FilmId = copyToBack.FilmId,
                        RentedOut = false,
                        StudioId = 0

                    };

                    filmCopyRepository.ReturnFilm(copyToBack, filmCopy);
                    return Ok();
                }
            }

            return Ok();
        }

        [HttpPost]
        [Route("rent")]
        public async Task<ActionResult<Film>> BorrowFilm(int id, string studioid)
        {
            var userName = User.Identity.Name;
            var user = userRepository.GetByUserName(userName);
            var film = filmRepository.GetFilmInfoByIdAsync(id);

            if (user == null || (user.Role != "admin" && user.FilmStudioId != studioid))
            {
                return StatusCode(401);
            }
            if (user.FilmStudioId == studioid || user.Role == "admin")
            {
                if (film == null)
                {
                    return StatusCode(409);
                }

                if (await filmCopyRepository.CheckBorrow(int.Parse(studioid), id) != null)
                {
                    return StatusCode(403);
                }
                var copy = await filmCopyRepository.findCopy(id);
                if (copy == null)
                {
                    return StatusCode(409);
                }
                else
                {
                    
                    FilmCopy filmToChange = new FilmCopy
                    {
                        FilmCopyId = copy.FilmCopyId,
                        FilmId = copy.FilmId,
                        RentedOut = true,
                        StudioId = int.Parse(studioid)
                    };

                    filmCopyRepository.Borrow(copy, filmToChange);
                    return Ok();
                }
            }
            return Ok();
        }
            
       



        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Film>> GetFilmInfo(int id)
        {
            var film = await filmRepository.GetFilmInfoByIdAsync(id);
            if (film == null)
            {
                return NotFound("Film not found");
            }
            var userName = User.Identity.Name;
            var user = userRepository.GetByUserName(userName);
            if (user == null)
            {
                var data = mapper.Map<FilmNUsers>(film);
                return Ok(data);
            }
            else
            {
                return Ok(film);
            }


        }

      
        [Route("{id}")]
        [HttpPatch]
        [AllowAnonymous]
        public async Task<ActionResult<Film>> UpdateFilm(int id, [FromBody] Film filmFromUser)
        {
                var userName = User.Identity.Name;
                var user = userRepository.GetByUserName(userName);
                var oldOne = await filmRepository.GetFilmInfoByIdAsync(id);

                if(oldOne == null)
                {
                    return NotFound("Not found");

                }
                else if (user==null || user.Role != "admin")
                {
                    return StatusCode(401);
                }
                else {
                JsonPatchDocument<Film> patchDoc = new JsonPatchDocument<Film>();
                if(filmFromUser.Name != null)
                {
                 patchDoc.Replace(e => e.Name, filmFromUser.Name);
                }
                if(filmFromUser.Country != null)
                {
                 patchDoc.Replace(e => e.Country, filmFromUser.Country);
                }
                if(filmFromUser.ReleaseDate != null)
                {
                patchDoc.Replace(e => e.ReleaseDate, filmFromUser.ReleaseDate);
                }
                if(filmFromUser.Director != null)
                {
                 patchDoc.Replace(e => e.Director, filmFromUser.Director);
                }                    
                patchDoc.ApplyTo(oldOne,ModelState);
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                filmRepository.UpdateFilm(oldOne);
               
                return Ok(oldOne);
           
                  }     

        }

        [Route("{id}")]
        [HttpPut]
        public async Task<ActionResult<Film>> UpdateCopy(int id, [FromBody] JsonPatchDocument<Film> film)
        {
            var userName = User.Identity.Name;
            var user = userRepository.GetByUserName(userName);
            var oldOne = await filmRepository.GetFilmInfoByIdAsync(id);
            if (user == null || user.Role != "admin")
            {
                return StatusCode(401);
            }
            else
            {
                if(oldOne == null )
                {
                    return NotFound("Film not found");
                }
                else
                {
                    film.ApplyTo(oldOne, ModelState);
                    if (!ModelState.IsValid)
                    {
                        return BadRequest(ModelState);
                    }
                    else
                    {
                        for(int i = 0; i< oldOne.FilmCopies.Count(); i++)
                        {
                            filmCopyRepository.Update(oldOne.FilmCopies, id, oldOne.FilmCopies[i].FilmCopyId);
                        }

                    }
                }
               
                var obj = await filmRepository.GetFilmInfoByIdAsync(oldOne.FilmId);
                return Ok(obj);

            }
            
        }

        [HttpPut]
        public async Task<ActionResult<Film>> PutFilm([FromBody] CreateFilm createFilm)
        {
            var userName = User.Identity.Name;
            var user =   userRepository.GetByUserName(userName);
            if (user.Role != "admin")
            {
                return  Unauthorized("Only Admins");
            }
            else
            {
                var film = new Film
                {
                    Name = createFilm.Name,
                    ReleaseDate = createFilm.ReleaseDate,
                    Country = createFilm.Country,
                    Director = createFilm.Director

                };
                filmRepository.Add(film);
                filmCopyRepository.AddCopies(createFilm.NumberOfCopies, film.FilmId);        
                var films = await filmRepository.GetFilmInfoByIdAsync(film.FilmId);
        
                
                
               
                return Ok(films);
            }
           
        }

    }
}
