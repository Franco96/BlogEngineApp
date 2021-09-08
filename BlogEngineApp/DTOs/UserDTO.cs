using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogEngineApp.DTOs
{
    public class UserDTO
    {

        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public int Role { get; set; }


        //public List<UserDTO> UserOrder { get; set; }

    }
}
