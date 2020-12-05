using System;
using System.Collections.Generic;

namespace BarterEvolution.Infrastructure
{
    public partial class TipoUsuario
    {
        public TipoUsuario()
        {
            UsuariosSistema = new HashSet<UsuariosSistema>();
        }

        public string IdTipoUsuario { get; set; }
        public string NombreTipous { get; set; }

        public virtual ICollection<UsuariosSistema> UsuariosSistema { get; set; }
    }
}
