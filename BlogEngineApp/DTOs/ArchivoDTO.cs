using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogEngineApp.DTOs
{
    public class ArchivoDTO
    {

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Extension { get; set; }
        public double Tamanio { get; set; }
        public string Ubicacion { get; set; }


    }
}
