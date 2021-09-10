using BlogEngineApp.Models;
using BlogEngineApp.Repositories;
using BlogEngineApp.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogEngineApp.Services
{
    public class UserService : IUserService
    {

        public readonly IGenericRepository _repository;

        public UserService(IGenericRepository repository) {

            _repository = repository;

        }

        public List<User> GetUsers()
        {
            return _repository.GetUsersAll();
        }

        public User GetUser(int id) {

            return _repository.GetUserById(id);
        }


        public void crearUser(User user) {

            _repository.crearUser(user);
        }
    }
}
