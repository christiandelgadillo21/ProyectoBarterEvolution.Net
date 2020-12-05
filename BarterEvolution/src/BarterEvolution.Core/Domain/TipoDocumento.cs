using System;
using System.Collections.Generic;

namespace BarterEvolution.Infrastructure
{
    public partial class TipoDocumento
    {
        public TipoDocumento()
        {
            Clientes = new HashSet<Clientes>();
            UsuariosSistema = new HashSet<UsuariosSistema>();
        }

        public string IdTipoDocumento { get; set; }
        public string TipoDocumento1 { get; set; }

        public virtual ICollection<Clientes> Clientes { get; set; }
        public virtual ICollection<UsuariosSistema> UsuariosSistema { get; set; }
    }
}
