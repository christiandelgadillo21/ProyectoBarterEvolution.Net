using System;
using System.Collections.Generic;

namespace BarterEvolution.Infrastructure
{
    public partial class CondicionLegalidad
    {
        public CondicionLegalidad()
        {
            LegalidadArticulos = new HashSet<LegalidadArticulos>();
        }

        public string CondicionLegalidad1 { get; set; }
        public string NombreCondicion { get; set; }

        public virtual ICollection<LegalidadArticulos> LegalidadArticulos { get; set; }
    }
}
