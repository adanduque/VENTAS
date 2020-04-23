using System;
using System.Collections.Generic;

namespace VENTAS.Models
{
    public partial class VentaDetalle
    {
        public long Id { get; set; }
        public decimal Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal PrecioTotal { get; set; }
        public long VentaId { get; set; }
        public long ArticuloId { get; set; }

        public virtual Articulo Articulo { get; set; }
        public virtual Venta Venta { get; set; }
    }
}
