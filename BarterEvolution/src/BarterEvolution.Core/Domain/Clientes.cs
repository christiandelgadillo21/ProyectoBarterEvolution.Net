using System;
using System.Collections.Generic;

namespace BarterEvolution.Infrastructure
{
    public partial class Clientes
    {
        public Clientes()
        {
            Contratos = new HashSet<Contratos>();
            LegalidadArticulos = new HashSet<LegalidadArticulos>();
            Ventas = new HashSet<Ventas>();
        }

        public int CedulaCliente { get; set; }
        public string IdDocumento { get; set; }
        public string IdTipocliente { get; set; }
        public string Nombrecliente1 { get; set; }
        public string Nombrecliente2 { get; set; }
        public string Apellidocliente1 { get; set; }
        public string Apellidocliente2 { get; set; }
        public string Genero { get; set; }
        public long? TelefonoMovil { get; set; }
        public string Email { get; set; }
        public string DireccionResidencia { get; set; }
        public string Ciudad { get; set; }
        public int IdLocalidad { get; set; }

        public virtual Genero GeneroNavigation { get; set; }
        public virtual TipoDocumento IdDocumentoNavigation { get; set; }
        public virtual Localidad IdLocalidadNavigation { get; set; }
        public virtual TipoCliente IdTipoclienteNavigation { get; set; }
        public virtual ICollection<Contratos> Contratos { get; set; }
        public virtual ICollection<LegalidadArticulos> LegalidadArticulos { get; set; }
        public virtual ICollection<Ventas> Ventas { get; set; }
    }
}
