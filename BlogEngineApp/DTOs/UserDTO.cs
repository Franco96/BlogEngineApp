using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlogEngineApp.DTOs
{
    public class UserDTO
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "El campo UserName es requerido")]
        [StringLength(50, MinimumLength = 3,
        ErrorMessage = "UserName deberia tener un minimo de 3 characteres y un maximo de 50 characteres")]
        public string UserName { get; set; }
        
        [Required(ErrorMessage = "El campo Password es requerido")]
        [StringLength(50, MinimumLength = 3,
        ErrorMessage = "Password deberia tener un minimo de 3 characteres y un maximo de 50 characteres")]
        public string Password { get; set; }

        [Required(ErrorMessage = "El campo Role es requerido")]
        [Range(1, 2 ,ErrorMessage = "Para Role solo se admite el valor entero 1 o 2 equivale a editor(1) o escritor(2)")]
        public int Role { get; set; }


    }
}
