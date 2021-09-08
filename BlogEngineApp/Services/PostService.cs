using BlogEngineApp.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        public  Post CrearPost(IFormFile file)
        {

            double taminio = file.Length;
            taminio = Math.Round(taminio, 2);

            Archivo archivo = new Archivo();
           
            archivo.Extension = Path.GetExtension(file.FileName).Substring(1);
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


            _context.Add(post);

            _context.SaveChanges();


            return post;
        }


        public Post EliminarPost(int id)
        {

            Post post = _context.Posts.Find(id);


            if (post != null)
            {

                Archivo archivo = _context.Archivos.Find(post.IdArchivos);

                String ruta = archivo.Ubicacion;
                //Borro el archivo ubicado en la carpeta local del proyecto
                File.Delete(ruta);

                
                //Elimino el post
                _context.Remove(post);
                //Elimino el archivo que contenia el post
                _context.Remove(archivo);

                _context.SaveChanges();
            }

            return post;

        }


    }
}
