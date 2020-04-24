using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VENTAS.RequestDTO
{
  
    public class UndMedidaDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

    }

    public class UnidadCreacionDTO
    {
        [Required(ErrorMessage = "Ingrese nombre de la categoria")]
        public string Nombre { get; set; }
        [MaxLength(150,ErrorMessage ="Máximo 150 caracteres")]
        public string Descripcion { get; set; }

    }
}
