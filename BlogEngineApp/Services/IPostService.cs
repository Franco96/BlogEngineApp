using BlogEngineApp.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogEngineApp.Services
{
    public interface IPostService
    {
        public Post CrearPost(IFormFile file);

        public Post EliminarPost(int id);
    }
}
