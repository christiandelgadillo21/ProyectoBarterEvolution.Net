using System;
using System.Collections.Generic;

namespace BarterEvolution.Infrastructure
{
    public partial class UsuariosSistema
    {
        public UsuariosSistema()
        {
            Contratos = new HashSet<Contratos>();
            Ventas = new HashSet<Ventas>();
        }

        public int CedulaUsuario { get; set; }
        public string IdDocumento { get; set; }
        public string IdUsuario { get; set; }
        public string Nombreusuario1 { get; set; }
        public string Nombreusuario2 { get; set; }
        public string Apellidousuario1 { get; set; }
        public string Apellidousuario2 { get; set; }
        public string Email { get; set; }
        public string Usuario { get; set; }
        public string Clave { get; set; }

        public virtual TipoDocumento IdDocumentoNavigation { get; set; }
        public virtual TipoUsuario IdUsuarioNavigation { get; set; }
        public virtual ICollection<Contratos> Contratos { get; set; }
        public virtual ICollection<Ventas> Ventas { get; set; }
    }
}
