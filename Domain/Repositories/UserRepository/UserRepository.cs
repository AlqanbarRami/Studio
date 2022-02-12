using Microsoft.EntityFrameworkCore;
using MoviesApi.Domain.Models.User;
using MoviesApi.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesApi.Domain.Repositories.UserRepository
{
    public class UserRepository : IUserRepository
    {

        private AppDbContext appDbContext;

        public UserRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

    
 
 
        public IEnumerable<User> GetAllUsers()
        {
            var users = appDbContext.Users;
            return users;
        }

        public User GetByUserName(string username)
        {
            return  appDbContext.Users.FirstOrDefault(u=>u.UserName == username);
        }
    }
}
