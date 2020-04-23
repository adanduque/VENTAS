using System;
using System.Collections.Generic;

namespace VENTAS.Models
{
    public partial class Cliente
    {
        public Cliente()
        {
            Ventas = new HashSet<Venta>();
        }

        public long Id { get; set; }
        public string NombreRazonS { get; set; }
        public string Direccion { get; set; }
        public string Email { get; set; }
        public int TipoDocumentoId { get; set; }
        public string NumDocumento { get; set; }
        public DateTime? FechaNacimiento { get; set; }

        public virtual TipoDocumento TipoDocumento { get; set; }
        public virtual ICollection<Venta> Ventas { get; set; }
    }
}
