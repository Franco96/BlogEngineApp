using BlogEngineApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogEngineApp.Services
{
    public interface IUserService
    {
        Task<List<User>> GetUsers();
    }
}
