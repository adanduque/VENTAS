using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VENTAS.RequestDTO
{
    public class CategoriaDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

    }

    public class CategoriaCreacionDTO
    {
        [Required (ErrorMessage ="Ingrese nombre de la categoria")]
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

    }
}
