using System;
using System.Collections.Generic;

namespace VENTAS.Models
{
    public partial class Venta
    {
        public Venta()
        {
            VentaDetalles = new HashSet<VentaDetalle>();
        }

        public long Id { get; set; }
        public string Serie { get; set; }
        public string Correlativo { get; set; }
        public decimal Total { get; set; }
        public long ClienteId { get; set; }
        public int ComprobanteId { get; set; }
        public DateTime FechaVenta { get; set; }

        public virtual Cliente Cliente { get; set; }
        public virtual Comprobante Comprobante { get; set; }
        public virtual ICollection<VentaDetalle> VentaDetalles { get; set; }
    }
}
