using BlogEngineApp.Models;
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
    public class PostService:IPostService
    {

        public readonly BlogDBContext _context;

        public readonly User user;


        public PostService(BlogDBContext context) {

            _context = context;

            //Se simula que este usuario esta logueado con datos mockeados
            user = _context.Users.Find(5);

        }

        public List<Post> getPosts()
        {
            return _context.Posts.Include(u => u.IdArchivosNavigation).ToList();
        }

        public Post getPost(int id) {

           
            return _context.Posts.Include(u => u.IdArchivosNavigation).FirstOrDefault(x => x.Id == id); 

        }

        //Reemplaza todo el contenido del post por el texto ingresado
        public Post ModificarCompletamenteElPost(int id, string text)
        {
            Post post = getPost(id);

            if (post != null) {

                Archivo archivo = post.IdArchivosNavigation;
                //Borro el archivo ubicado en la carpeta local del proyecto
                File.Delete(archivo.Ubicacion);

                //Creo un nuevo archivo con el texto ingresado
                string RutaProyecto = Directory.GetCurrentDirectory();
                //ruta de donde se guardaran los archivos localmente
                string ruta = Path.Combine(RutaProyecto + "\\Archivos\\" + archivo.Id +".txt");
                
                using (FileStream fs = File.Create(ruta))
                {
                    byte[] info = new UTF8Encoding(true).GetBytes(text);
                    // Add some information to the file.
                    fs.Write(info, 0, info.Length);
                }

                archivo.Ubicacion = ruta;
                archivo.Extension = ".txt";
                post.SubmitDate = DateTime.Now;

                _context.Update(archivo);
                _context.Update(post);

                _context.SaveChanges();


            }


            return post;

        }

        //Se aprueba o se rechaza el post
        public Tuple<Post,bool> AprobarPost(int id, string desicion) {

            Post post = getPost(id);
            bool bienIngresada = true;

            if (post != null)
            {
                if (desicion == "approve")
                {
                    post.Approval = true;
                    post.Rechazado = false;
                }
                else if (desicion == "reject")
                {
                    post.Rechazado = true;
                    post.Approval = false;

                }
                else bienIngresada = false;


            _context.Update(post);
            _context.SaveChanges();

            }

            

            return new Tuple<Post, bool>(post,bienIngresada);
        }

        public  Post CrearPost(IFormFile file)
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

            //ruta de donde se guardaran los archivos localmente
            string ruta = Path.Combine(RutaProyecto + "\\Archivos\\" + archivo.Id + Path.GetExtension(file.FileName));


            //Genero el archivo en la carpeta local del proyecto
            using (var stream = File.Create(ruta))
            {
                file.CopyToAsync(stream);
            }

            archivo.Ubicacion = ruta;
            archivo.Nombre = ""+archivo.Id;

            Post post = new Post();
            post.IdUser = user.Id;
            post.IdArchivos = archivo.Id;
            post.SubmitDate = DateTime.Now;
            post.Approval = false;
            post.Rechazado = false;


            _context.Add(post);

            _context.SaveChanges();


            return post;
        }


        public void EliminarPost(int id)
        {

            Post post = _context.Posts.Include(u => u.IdArchivosNavigation).FirstOrDefault(x => x.Id == id);


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


    }
}
