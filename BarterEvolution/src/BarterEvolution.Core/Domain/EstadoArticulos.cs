using System;
using System.Collections.Generic;

namespace BarterEvolution.Infrastructure
{
    public partial class EstadoArticulos
    {
        public EstadoArticulos()
        {
            Articulos = new HashSet<Articulos>();
            Inventario = new HashSet<Inventario>();
            Ventas = new HashSet<Ventas>();
        }

        public string IdEstadoArticulo { get; set; }
        public string NombreEstadoart { get; set; }

        public virtual ICollection<Articulos> Articulos { get; set; }
        public virtual ICollection<Inventario> Inventario { get; set; }
        public virtual ICollection<Ventas> Ventas { get; set; }
    }
}
