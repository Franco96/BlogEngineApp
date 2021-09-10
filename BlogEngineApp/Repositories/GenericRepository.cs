using BlogEngineApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BlogEngineApp.Repositories
{
    public class GenericRepository : IGenericRepository
    {

        internal BlogDBContext _context;
        

        public GenericRepository(BlogDBContext context)
        {
            _context = context;
        }


        public List<Post> GetPostAll()
        {
            return _context.Posts.Include(u => u.IdArchivosNavigation).ToList();
        }

        public List<Post> getPostsPendientes() 
        {
            return _context.Posts.Include(u => u.IdArchivosNavigation).Where(x => x.Approval == false)
                   .Where(x => x.Rechazado == false).ToList();
        }

        public List<Post> getPostsAprobados() {
            return _context.Posts.Include(u => u.IdArchivosNavigation).Where(x => x.Approval == true)
                      .Where(x => x.Rechazado == false).ToList();
        }

        public Post getPostById(int id) 
        {
            return _context.Posts.Include(u => u.IdArchivosNavigation).FirstOrDefault(x => x.Id == id);
        }

        public string actualizarContenidoPostCompletamente(Post post, Archivo archivo ,String rutaNueva) {

            string rutaAnterior = archivo.Ubicacion;

        

            archivo.Ubicacion = rutaNueva;
            archivo.Extension = ".txt";
            post.SubmitDate = DateTime.Now;

            _context.Update(archivo);
            _context.Update(post);

            _context.SaveChanges();


            return rutaAnterior;
        }


        public Tuple<Post, bool> AprobarPost(int id ,string decision) {

            Post post = getPostById(id);
            bool bienIngresada = true;

            if (post != null)
            {
                if (decision == "approve")
                {
                    post.Approval = true;
                    post.Rechazado = false;
                }
                else if (decision == "reject")
                {
                    post.Rechazado = true;
                    post.Approval = false;

                }
                else bienIngresada = false;


                _context.Update(post);
                _context.SaveChanges();

            }



            return new Tuple<Post, bool>(post, bienIngresada);


        }

        public Archivo crearArchivo(IFormFile file) 
        {

            double taminio = file.Length;
            taminio = Math.Round(taminio, 2);

            Archivo archivo = new Archivo();

            archivo.Extension = Path.GetExtension(file.FileName);
            archivo.Tamanio = taminio;
            archivo.Ubicacion = "";
            archivo.Nombre = "";

            _context.Add(archivo);
            _context.SaveChanges();

            string RutaProyecto = Directory.GetCurrentDirectory();
            string ruta = Path.Combine(RutaProyecto + "\\Archivos\\" + archivo.Id + Path.GetExtension(file.FileName));
           
            archivo.Ubicacion = ruta;
            archivo.Nombre = "" + archivo.Id;
            _context.Update(archivo);
            _context.SaveChanges();


            return archivo;

        }


        public Post crearPost(int idUser, int idArchivo)
        {

            Post post = new Post();

            post.IdUser = idUser;
            post.IdArchivos = idArchivo;
            post.SubmitDate = DateTime.Now;
            post.Approval = false;
            post.Rechazado = false;


            _context.Add(post);

            _context.SaveChanges();


            return post;
        }


        public void eliminarPost(int id) {
            

            Post post = getPostById(id);

            if (post != null)
            {

                Archivo archivo = post.IdArchivosNavigation;

                //Borro el archivo ubicado en la carpeta local del proyecto
                File.Delete(archivo.Ubicacion);

                //Elimino el post
                _context.Remove(post);
                //Elimino el archivo que contenia el post
                _context.Remove(archivo);

                _context.SaveChanges();
            }


        }


        public List<User> GetUsersAll() 
        {
            return _context.Users.ToList();
        }

        public User GetUserById(int id) 
        {
            return _context.Users.Find(id);
        }

        public void crearUser(User user) {

            _context.Add(user);

            _context.SaveChanges();
        }
    }
}
