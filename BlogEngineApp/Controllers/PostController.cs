using AutoMapper;
using BlogEngineApp.DTOs;
using BlogEngineApp.Models;
using BlogEngineApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlogEngineApp.Controllers
{
    [ApiController]
    [Route("api")]
    public class PostController : ControllerBase
    {

        private readonly IMapper _mapper;
        private IPostService _postService { get; set; }
        private IUserService _userService { get; set; }


        //se utiliza este usuario para simular que esta en sesion y poder controlar el tema de roles con sus respectivos accesos
        private User user { get; set; }


        public PostController(IPostService postService, IUserService userService, IMapper mapper) {

            _postService = postService;
            _userService = userService;
            _mapper = mapper;
            user = _userService.GetUser(5);
        
        }


      
        [HttpGet("ObtenerPosts")]
        public IActionResult ObtenerPosts()
        {

            //Si es usuario escritor no tiene acceso a ver post todos los post solo los aprobados
            if (user.Role == 2)
            {

                return Unauthorized("El usuario '" + user.UserName + "' en sesion no tiene acceso a ver todos los post porque no es editor");

            }

            List<Post> posts = _postService.getPosts();

            List<PostDTO> postsDTO = _mapper.Map<List<PostDTO>>(posts);

            return Ok(postsDTO);
        }

        [HttpGet("ObtenerPostsPendientes")]
        public IActionResult ObtenerPostsPendientes() {

            //Si es usuario escritor no tiene acceso a ver post pendientes a aprobar
            if (user.Role == 2)
            {

                return Unauthorized("El usuario '" + user.UserName + "' en sesion no tiene acceso a ver post pendientes a aprobar porque no es editor");

            }

            List<Post> posts = _postService.getPostsPendientes();

            List<PostDTO> postsDTO = _mapper.Map<List<PostDTO>>(posts);

            return Ok(postsDTO);
        }

        [HttpGet("ObtenerPostsAprobados")]
        public IActionResult ObtenerPostsAprobados()
        {
            List<Post> posts = _postService.getPostsAprobados();

            List<PostDTO> postsDTO = _mapper.Map<List<PostDTO>>(posts);

            return Ok(postsDTO);
        }


            [HttpGet("ObtenerPost/{id}")]
        public IActionResult Get(int id)
        {
            Post post = _postService.getPost(id);

            if (post == null)
            {
                return NotFound("No existe un post que tenga el identificador ingresado");
            }

            return Ok(_mapper.Map<PostDTO>(post));
        }

   
        [HttpPut("ModificarPost/{id}")]
        public IActionResult Put(int id, [FromForm] string text)
        {

            Post post = _postService.ModificarCompletamenteElPost(id, text);

            if (post == null)
            {

                return NotFound("No existe un post que tenga el identificador ingresado");
            }

            PostDTO postDTO = _mapper.Map<PostDTO>(post);

            return Ok(postDTO);
        }



        [HttpPut("AprobarPost/{id}")]
        public IActionResult Post(int id, [FromForm] string decision)
        {

            //Si es usuario escritor no tiene acceso a aprobar o rechazar post
            if (user.Role == 2)
            {

                return Unauthorized("El usuario '" + user.UserName + "' en sesion no tiene acceso a aprobar/rechazar post porque no es editor");

            }

            Tuple<Post, bool> tupla = _postService.AprobarPost(id, decision);

            if (tupla.Item1 == null)
            {

                return NotFound("No existe un post que tenga el identificador ingresado");
            }

            //Si se ingreso mal el string para la decision (approve or reject) devuelvo error
            if (tupla.Item2 == false )
            {

                return BadRequest("Se debe ingresar (approve o reject)");
            }
           
            PostDTO postDTO = _mapper.Map<PostDTO>(tupla.Item1);

            return Ok(postDTO);

        }



        [HttpPost("CrearPost")]
        public IActionResult CrearPost([FromForm] IFormFile file)
        {
            if (file == null) {

                return BadRequest("Se debe ingresar un archivo en el body como parametro");
            }

            Post post = _postService.CrearPost(file,5);


            PostDTO postDTO = _mapper.Map<PostDTO>(post);

            return Ok(postDTO);
        
        }


        
        [HttpDelete("EliminarPost/{id}")]
        public IActionResult EliminarPost(int id)
        {
            //Si es usuario escritor no tiene acceso a eliminar post
            if (user.Role == 2) {

                return Unauthorized("El usuario '"+user.UserName+ "' en sesion no tiene acceso a eliminar post porque no es editor");
                    
            }


            //Busco primero el post que se quiere eliminar para luego devolverlo 
            Post post = _postService.getPost(id);
           
            if (post == null) {

                return NotFound("No existe un post que tenga el identificador ingresado");
            }
            PostDTO postDTO = _mapper.Map<PostDTO>(post);

            //Lo elimino
            _postService.EliminarPost(id);

            return Ok(postDTO);
        }



    }
}
