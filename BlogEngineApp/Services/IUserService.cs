using BlogEngineApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogEngineApp.Services
{
    public interface IUserService
    {
        public List<User> GetUsers();

        public User GetUser(int id);

        public void crearUser(User user);
    }
}
