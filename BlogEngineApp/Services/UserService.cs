using BlogEngineApp.Models;
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

        public readonly BlogDBContext _context;

        public UserService(BlogDBContext context) {

            _context = context;

        }

        public async Task<List<User>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }
    }
}
