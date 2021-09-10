using BlogEngineApp.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogEngineApp.Repositories
{
    public interface IGenericRepository
    {

        public List<Post> GetPostAll();

        public List<Post> getPostsPendientes();

        public List<Post> getPostsAprobados();
        public Post getPostById(int id);

        public string actualizarContenidoPostCompletamente(Post post, Archivo archivo, String rutaNueva);

        public Tuple<Post, bool> AprobarPost(int id,string decision);

        public Archivo crearArchivo(IFormFile file);

        public Post crearPost(int idUser, int idArchivo);

        public void eliminarPost(int id);


        public List<User> GetUsersAll();

        public User GetUserById(int id);

        public void crearUser(User user);

    }
}
