using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VENTAS.RequestDTO
{
    public class ArticuloDTO
    {
        public long Id { get; set; }
        public string Nombre { get; set; }
        public decimal Stock { get; set; }
        public decimal Precio { get; set; }
        public string Imagen { get; set; }
        public virtual CategoriaDTO Categoria { get; set; }
        public virtual UndMedidaDTO UndMedida { get; set; }
    }
    public class ArticuloCreacionDTO
    {
        [Required(ErrorMessage ="Ingrese nombre del articulo")]
        public string Nombre { get; set; }
        [Range(1, 10000000)]
        public decimal Stock { get; set; }
        [Range(1, 10000000)]
        public decimal Precio { get; set; }
        public string Imagen { get; set; }
        [Range(1, 10000)]
        public int CategoriaId { get; set; }
        [Range(1, 10000)]
        public int UndMedidaId { get; set; }
    }
}
