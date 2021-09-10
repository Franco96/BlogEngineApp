using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BlogEngineApp.Models;
using AutoMapper;
using BlogEngineApp.DTOs;
using BlogEngineApp.Services;

namespace BlogEngineApp.Controllers
{
    [ApiController]
    [Route("api")]
    public class UsersController : ControllerBase
    {

        
        private readonly IMapper _mapper;
        private  IUserService _userService { get; set; }
        public UsersController(IUserService userService, IMapper mapper)
        {
           
            _userService = userService;
            _mapper = mapper;
        }

        
        [HttpGet("ObtenerUsuarios")]
        public IActionResult GetUsers()
        {

            List<User> users = _userService.GetUsers();
            List<UserDTO> userDTOs = _mapper.Map<List<UserDTO>>(users);

             return Ok(userDTOs);

          
        }

        [HttpGet("ObtenerUsuario/{id}")]
        public IActionResult GetUser(int id)
        {
            User user = _userService.GetUser(id);

            if (user == null)
            {
                return NotFound("No existe un usuario que tenga el identificador ingresado");
            }

            return Ok(_mapper.Map<UserDTO>(user));
        }

        [HttpPost("CrearUsuario")]
        public IActionResult crearUsuario([FromBody] UserDTO userDTO) {


            if (!ModelState.IsValid)
            {
                return this.BadRequest();
            }

            User user = _mapper.Map<User>(userDTO);

            _userService.crearUser(user);

            return Ok(userDTO);
        }
       
    }

}
