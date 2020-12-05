using System;
using System.Collections.Generic;

namespace BarterEvolution.Infrastructure
{
    public partial class TipoCliente
    {
        public TipoCliente()
        {
            Clientes = new HashSet<Clientes>();
        }

        public string IdTipocliente { get; set; }
        public string NombreTipocliente { get; set; }

        public virtual ICollection<Clientes> Clientes { get; set; }
    }
}
