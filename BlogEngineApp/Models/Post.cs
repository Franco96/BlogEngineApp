using System;
using System.Collections.Generic;

#nullable disable

namespace BlogEngineApp.Models
{
    public partial class Post
    {
        public int Id { get; set; }
        public int IdUser { get; set; }
        public int IdArchivos { get; set; }
        public DateTime SubmitDate { get; set; }
        public bool Approval { get; set; }

        public virtual Archivo IdArchivosNavigation { get; set; }
        public virtual User IdUserNavigation { get; set; }
    }
}
