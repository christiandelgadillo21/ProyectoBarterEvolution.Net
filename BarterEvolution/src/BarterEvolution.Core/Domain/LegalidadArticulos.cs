using System;
using System.Collections.Generic;

namespace BarterEvolution.Infrastructure
{
    public partial class LegalidadArticulos
    {
        public int IdLegalidad { get; set; }
        public string CondLegalidad { get; set; }
        public DateTime FechaLegalidad { get; set; }
        public int CedulaCliente { get; set; }
        public string DescripcionLegalidad { get; set; }
        public string IdArticulo { get; set; }

        public virtual Clientes CedulaClienteNavigation { get; set; }
        public virtual CondicionLegalidad CondLegalidadNavigation { get; set; }
        public virtual Articulos IdArticuloNavigation { get; set; }
    }
}
