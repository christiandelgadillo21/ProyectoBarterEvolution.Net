using System;
using System.Collections.Generic;

namespace BarterEvolution.Infrastructure
{
    public partial class Genero
    {
        public Genero()
        {
            Articulos = new HashSet<Articulos>();
            Clientes = new HashSet<Clientes>();
        }

        public string IdGenero { get; set; }
        public string NombreGenero { get; set; }

        public virtual ICollection<Articulos> Articulos { get; set; }
        public virtual ICollection<Clientes> Clientes { get; set; }
    }
}
