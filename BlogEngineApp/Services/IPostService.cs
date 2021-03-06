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

        public List<Post> getPosts();

        public Post getPost(int id);

        public List<Post> getPostsPendientes();

        public List<Post> getPostsAprobados();

        public Post ModificarCompletamenteElPost(int id, string text);

        public Tuple<Post, bool> AprobarPost(int id, string desicion);

        public Post CrearPost(IFormFile file,int userId);

        public void EliminarPost(int id);


    }
}
