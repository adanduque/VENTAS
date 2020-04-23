using System;
using System.Collections.Generic;

namespace VENTAS.Models
{
    public partial class Comprobante
    {
        public Comprobante()
        {
            Correlativoes = new HashSet<Correlativo>();
            Ventas = new HashSet<Venta>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<Correlativo> Correlativoes { get; set; }
        public virtual ICollection<Venta> Ventas { get; set; }
    }
}
