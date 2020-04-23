using System;
using System.Collections.Generic;

namespace VENTAS.Models
{
    public partial class TipoDocumento
    {
        public TipoDocumento()
        {
            Clientes = new HashSet<Cliente>();
        }

        public int Id { get; set; }
        public string Tipo { get; set; }
        public string DescripcionLarga { get; set; }
        public string DescripcionCorta { get; set; }
        public int Lo { get; set; }
        public int T { get; set; }
        public int C { get; set; }
        public int E { get; set; }

        public virtual ICollection<Cliente> Clientes { get; set; }
    }
}
