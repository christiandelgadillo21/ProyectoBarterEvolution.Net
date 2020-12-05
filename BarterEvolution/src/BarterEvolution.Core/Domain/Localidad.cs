using System;
using System.Collections.Generic;

namespace BarterEvolution.Infrastructure
{
    public partial class Localidad
    {
        public Localidad()
        {
            Clientes = new HashSet<Clientes>();
        }

        public int IdLocalidad { get; set; }
        public string NombreLocalidad { get; set; }

        public virtual ICollection<Clientes> Clientes { get; set; }
    }
}
