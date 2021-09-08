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


       
        // GET api/<PostController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<PostController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<PostController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }


        
        [HttpGet("ObtenerPosts")]
        public IEnumerable<string> ObtenerPosts()
        {
            return new string[] { "value1", "value2" };
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

            Post post = _postService.EliminarPost(id);

            if (post == null) {

                return NotFound("No existe un post que tenga el identificador ingresado");
            }

            PostDTO postDTO = _mapper.Map<PostDTO>(post);

            return Ok(postDTO);
        }



    }
}
