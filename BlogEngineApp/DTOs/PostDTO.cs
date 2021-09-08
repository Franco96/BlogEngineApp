using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogEngineApp.DTOs
{
    public class PostDTO
    {

        public int Id { get; set; }
        public int IdUser { get; set; }
        public int IdArchivos { get; set; }
        public DateTime SubmitDate { get; set; }
        public bool Approval { get; set; }

        public string PostText { get; set; }

    }
}
