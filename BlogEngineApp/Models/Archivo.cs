using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace BlogEngineApp.Models
{
    public partial class Archivo
    {
        public Archivo()
        {
            Posts = new HashSet<Post>();
        }

       
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Extension { get; set; }
        public double Tamanio { get; set; }
        public string Ubicacion { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}
