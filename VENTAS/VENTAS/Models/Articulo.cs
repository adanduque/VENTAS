using System;
using System.Collections.Generic;

namespace VENTAS.Models
{
    public partial class Articulo
    {
        public Articulo()
        {
            VentaDetalles = new HashSet<VentaDetalle>();
        }

        public long Id { get; set; }
        public string Nombre { get; set; }
        public decimal Stock { get; set; }
        public decimal Precio { get; set; }
        public string Imagen { get; set; }
        public int CategoriaId { get; set; }
        public int UndMedidaId { get; set; }

        public virtual Categoria Categoria { get; set; }
        public virtual UndMedida UndMedida { get; set; }
        public virtual ICollection<VentaDetalle> VentaDetalles { get; set; }
    }
}
