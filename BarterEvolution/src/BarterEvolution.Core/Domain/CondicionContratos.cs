using System;
using System.Collections.Generic;

namespace BarterEvolution.Infrastructure
{
    public partial class CondicionContratos
    {
        public CondicionContratos()
        {
            Contratos = new HashSet<Contratos>();
        }

        public string IdCondicionContrato { get; set; }
        public string NombreCondicioncon { get; set; }

        public virtual ICollection<Contratos> Contratos { get; set; }
    }
}
