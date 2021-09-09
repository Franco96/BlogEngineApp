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


        public PostController(IPostService postService, IMapper mapper) {

            _postService = postService;
            _mapper = mapper;
        
        }


      
        [HttpGet("ObtenerPosts")]
        public IActionResult ObtenerPosts()
        {
            List<Post> posts = _postService.getPosts();

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

            Post post = _postService.CrearPost(file);


            PostDTO postDTO = _mapper.Map<PostDTO>(post);

            return Ok(postDTO);
        
        }


        
        [HttpDelete("EliminarPost/{id}")]
        public IActionResult EliminarPost(int id)
        {
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
