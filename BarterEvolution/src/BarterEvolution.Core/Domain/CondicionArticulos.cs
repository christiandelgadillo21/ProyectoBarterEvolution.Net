using System;
using System.Collections.Generic;

namespace BarterEvolution.Infrastructure
{
    public partial class CondicionArticulos
    {
        public CondicionArticulos()
        {
            Inventario = new HashSet<Inventario>();
        }

        public string IdCondicionArticulo { get; set; }
        public string NombreCondicionart { get; set; }

        public virtual ICollection<Inventario> Inventario { get; set; }
    }
}
