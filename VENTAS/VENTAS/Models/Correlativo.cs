using System;
using System.Collections.Generic;

namespace VENTAS.Models
{
    public partial class Correlativo
    {
        public int Id { get; set; }
        public string Serie { get; set; }
        public string Valor { get; set; }
        public int ComprobanteId { get; set; }

        public virtual Comprobante Comprobante { get; set; }
    }
}
