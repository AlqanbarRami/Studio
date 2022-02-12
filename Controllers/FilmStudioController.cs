using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MoviesApi.Domain.Models;
using MoviesApi.Domain.Models.RegisterFilmStudio;
using MoviesApi.Domain.Models.User;
using MoviesApi.Domain.Repositories;
using MoviesApi.Domain.Repositories.UserRepository;
using MoviesApi.Resources;

namespace MoviesApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class FilmStudioController : Controller
    {
        private readonly IFilmStudioRepository iFilmStudioRepository;
        private readonly IUserRepository userRepository;
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;
        public FilmStudioController(IFilmStudioRepository iFilmStudioRepository, IUserRepository userRepository, IMapper mapper, UserManager<User> userManager)
        {
            this.iFilmStudioRepository = iFilmStudioRepository;
            this.userRepository = userRepository;
            this.mapper = mapper;
            this.userManager = userManager;
        
        }

        [Route("register")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterFilmStudio registerFilmStudio)
        {          
            try
            {  
                var countStudio = await iFilmStudioRepository.GetList();
                int studioId = countStudio.Count() ;

                var newStudio = new User
                {
                    Role = "filmstudio",
                    UserName = registerFilmStudio.Username,
                    Password = registerFilmStudio.Password,
                    FilmStudioId = studioId.ToString()
                };
                var AddUser = await userManager.CreateAsync(newStudio, newStudio.Password);
                if (AddUser.Succeeded)
                {
                    FilmStudio filmStudio = await iFilmStudioRepository.AddNewFilmStudio(registerFilmStudio);
                    var resources = mapper.Map<FilmStudio, StudioRegister>(filmStudio);
                    return Ok(resources);
                }
                else
                {
                    return BadRequest();
                }


            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }


        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<FilmStudio>>> GetList()
        {
            var userName = User.Identity.Name;
            var user = userRepository.GetByUserName(userName);
           
            try
            {
                var result = await iFilmStudioRepository.GetList();
                var data = mapper.Map<StudioResource[]>(result);

                if (user == null || user.Role == "filmstudio")
                {
                    return Ok(data);
                }          
                var dataForAdmin = mapper.Map<StudioRegister[]>(result);
                return Ok(dataForAdmin);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<FilmStudio>>> Get(int id)
        {
            var userName = User.Identity.Name;
            var user = userRepository.GetByUserName(userName);

            try
            {
                var result = await iFilmStudioRepository.GetFilmStudioById(id);
                var dataForAdmin = mapper.Map<StudioRegister[]>(result);
                var newRes = mapper.Map<StudioResource[]>(result);
                if (result != null)
                {
                    if (user == null)
                    {
                        return Ok(newRes);
                    }
                    else
                    {
                        if (user.Role == "admin")
                        {
                            return Ok(dataForAdmin);
                        }
                        else
                        {
                            if (user.Role == "filmstudio" && user.FilmStudioId == id.ToString())
                            {
                                return Ok(dataForAdmin);
                            }
                            if (user.Role == "filmstudio" && user.FilmStudioId != id.ToString())
                            {

                                return Ok(newRes);
                            }

                        }
                    }
                }
                else
                {
                    return NotFound("No Filmstudio with this id");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
            return Ok();
           
            
        }


    }
}
