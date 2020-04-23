using System;
using System.Collections.Generic;

namespace VENTAS.Models
{
    public partial class UndMedida
    {
        public UndMedida()
        {
            Articuloes = new HashSet<Articulo>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<Articulo> Articuloes { get; set; }
    }
}
