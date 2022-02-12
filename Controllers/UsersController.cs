using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MoviesApi.Domain.Models;
using MoviesApi.Domain.Models.Authenticate;
using MoviesApi.Domain.Models.User;
using MoviesApi.Domain.Models.UserRegister;
using MoviesApi.Domain.Repositories;
using MoviesApi.Domain.Repositories.UserRepository;

using MoviesApi.Resources;

namespace MoviesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {

        private readonly IUserRepository userRepository;
        private readonly IFilmStudioRepository filmStudioRepository;
        private readonly IMapper mapper;   
        private readonly UserManager<User> userManager;
        private readonly IConfiguration config;
        private readonly SignInManager<User> signInManager;

        public UsersController(SignInManager<User> signInManager,IFilmStudioRepository filmStudioRepository,IUserRepository userRepository, IMapper mapper, UserManager<User> userManager, IConfiguration config)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
            this.userManager = userManager;
            this.config = config;
            this.filmStudioRepository = filmStudioRepository;
            this.signInManager = signInManager;
            
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register([FromBody] UserRegister userRegister)
        {
            var us = userRepository.GetAllUsers();
            var totalUsers = us.Where(u=>u.Role !="filmstudio").Count()+1;
            try
            {
                if (userRegister.IsAdmin)
                {
                    var newUser = new User
                    {
                        UserId = totalUsers,
                        Role = "admin",
                        UserName = userRegister.Username,
                        Password = userRegister.Password

                    };
                 var AddUser = await userManager.CreateAsync(newUser, newUser.Password);
                    if (AddUser.Succeeded)
                    {
                        var sendObjBack =  userRepository.GetByUserName(userRegister.Username);
                        var resources = mapper.Map<User, AdminRegister>(sendObjBack);
                        return Ok(resources);
                    }
                    else
                    {
                        return BadRequest(AddUser);
                    }
                }

                return BadRequest();
            }
            catch(Exception e)
            {
                return BadRequest(e);
            }
              
        }

        [HttpPost("Authenticate")]
        public async Task<IActionResult> CreateToken([FromBody] Authenticate model)
        {
            if (ModelState.IsValid)
            {
                var user =  userRepository.GetByUserName(model.Username);

                if (user != null)
                {
                    var result = await signInManager.CheckPasswordSignInAsync(user,model.Password,false);

                    if (result.Succeeded)
                    {
                        // Create the Token
                        var claims = new[]
                        {
                         new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                          new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                          new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName)
            };

                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Tokens:Key"]));

                        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                        var token = new JwtSecurityToken(
                          config["Tokens:Issuer"],
                          config["Tokens:Audience"],
                          claims,
                          expires: DateTime.UtcNow.AddMinutes(60),
                          signingCredentials: creds);

                      
                        if(user.Role == "admin")
                        {
                            var results = new 
                            {
                                UserId = user.UserId,
                                Role = user.Role,
                                UserName = user.UserName,
                                token = new JwtSecurityTokenHandler().WriteToken(token),
                                expiration = token.ValidTo
                            };
                       
                            return Ok(results);
                        }
                        if(user.Role == "filmstudio")
                        {
                            var studio = filmStudioRepository.GetByIdAsync(int.Parse(user.FilmStudioId));
                        
                            var results = new
                            {
                                FilmStudioId = user.FilmStudioId,
                                Role = user.Role,
                                FilmStudio = new
                                {
                                    Name = studio.Name,
                                    City = studio.City
                                    
                                },
                                token = new JwtSecurityTokenHandler().WriteToken(token),
                                expiration = token.ValidTo
                            };
                            return Ok(results);
                        }

                        
                    }
                }
            }

            return BadRequest();
        }

  

    }

}
