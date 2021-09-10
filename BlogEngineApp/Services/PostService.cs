using BlogEngineApp.Models;
using BlogEngineApp.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace BlogEngineApp.Services
{
    public class PostService : IPostService
    {

        public readonly IGenericRepository _repository;

       
        public PostService( IGenericRepository repository) {

            _repository = repository;
            
        }

        public List<Post> getPosts()
        {
            return _repository.GetPostAll();
        }

        //Se retornaran los post pendientes es decir lo que no estan aprobados y tampoco rechazados
        public List<Post> getPostsPendientes() {

            return _repository.getPostsPendientes();
        }

        public List<Post> getPostsAprobados()
        {
            return _repository.getPostsAprobados();

        }

        public Post getPost(int id) {

            return _repository.getPostById(id);

        }

        //Reemplaza todo el contenido del post por el texto ingresado
        public Post ModificarCompletamenteElPost(int id, string text)
        {
            Post post = getPost(id);

            if (post != null) {

                Archivo archivo = post.IdArchivosNavigation;

                //Creo un nuevo archivo con el texto ingresado
                string RutaProyecto = Directory.GetCurrentDirectory();
                //ruta de donde se guardara el nuevo archivo
                string rutaNueva = Path.Combine(RutaProyecto + "\\Archivos\\" + archivo.Id + ".txt");

                //Borro el archivo ubicado en la carpeta local del proyecto y tambien actualizo el post con su archivo
                File.Delete(_repository.actualizarContenidoPostCompletamente(post,archivo, rutaNueva));

                using (FileStream fs = File.Create(rutaNueva))
                {
                    byte[] info = new UTF8Encoding(true).GetBytes(text);
                    
                    fs.Write(info, 0, info.Length);
                }
            }

            return post;
          
        }




        //Se aprueba o se rechaza el post
        public Tuple<Post,bool> AprobarPost(int id, string decision) {

            return _repository.AprobarPost(id, decision);
        }



        public  Post CrearPost(IFormFile file,int userId)
        {
            
            Archivo archivo = _repository.crearArchivo(file);

            
            //Genero el archivo en la carpeta local del proyecto
            using (var stream = File.Create(archivo.Ubicacion))
            {
                file.CopyToAsync(stream);
            }
            return _repository.crearPost(userId, archivo.Id);
        }


        public void EliminarPost(int id)
        {

          
            _repository.eliminarPost(id);

           

        }


    }
}
