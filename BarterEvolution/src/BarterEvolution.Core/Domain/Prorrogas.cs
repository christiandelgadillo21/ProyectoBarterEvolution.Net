using System;
using System.Collections.Generic;

namespace BarterEvolution.Infrastructure
{
    public partial class Prorrogas
    {
        public Prorrogas()
        {
            Contratos = new HashSet<Contratos>();
        }

        public string NoProrroga { get; set; }
        public string NoContrato { get; set; }
        public DateTime FechaInicioProrroga { get; set; }
        public DateTime FechaVencimientoProrroga { get; set; }
        public int MesesAPagar { get; set; }
        public int ValorMes { get; set; }
        public int DiasVencidos { get; set; }

        public virtual Contratos NoContratoNavigation { get; set; }
        public virtual ICollection<Contratos> Contratos { get; set; }
    }
}
