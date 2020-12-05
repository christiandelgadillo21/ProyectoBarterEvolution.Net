using System;
using System.Collections.Generic;

namespace BarterEvolution.Infrastructure
{
    public partial class Contratos
    {
        public Contratos()
        {
            Prorrogas = new HashSet<Prorrogas>();
        }

        public string NoContrato { get; set; }
        public int CedulaCliente { get; set; }
        public int CedulaUsuario { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public DateTime? FechaPago { get; set; }
        public int PlazoEstipulado { get; set; }
        public string IdArticulo { get; set; }
        public string IdCondicionContrato { get; set; }
        public string NoProrroga { get; set; }
        public int ValorContrato { get; set; }

        public virtual Clientes CedulaClienteNavigation { get; set; }
        public virtual UsuariosSistema CedulaUsuarioNavigation { get; set; }
        public virtual Articulos IdArticuloNavigation { get; set; }
        public virtual CondicionContratos IdCondicionContratoNavigation { get; set; }
        public virtual Prorrogas NoProrrogaNavigation { get; set; }
        public virtual ICollection<Prorrogas> Prorrogas { get; set; }
    }
}
