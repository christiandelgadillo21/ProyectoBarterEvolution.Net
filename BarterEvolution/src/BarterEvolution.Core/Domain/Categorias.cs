using System;
using System.Collections.Generic;

namespace BarterEvolution.Infrastructure
{
    public partial class Categorias
    {
        public Categorias()
        {
            Articulos = new HashSet<Articulos>();
            Inventario = new HashSet<Inventario>();
        }

        public string IdCategoria { get; set; }
        public string NombreCategoria { get; set; }

        public virtual ICollection<Articulos> Articulos { get; set; }
        public virtual ICollection<Inventario> Inventario { get; set; }
    }
}
