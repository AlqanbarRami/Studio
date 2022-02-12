using MoviesApi.Domain.Models.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoviesApi.Domain.Repositories.UserRepository
{
    public interface IUserRepository
    {


        User GetByUserName(string username);
        IEnumerable<User> GetAllUsers();
      

    }
}
