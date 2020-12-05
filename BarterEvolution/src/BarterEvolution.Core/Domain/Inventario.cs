using System;
using System.Collections.Generic;

namespace BarterEvolution.Infrastructure
{
    public partial class Inventario
    {
        public string IdInventario { get; set; }
        public int Cantidad { get; set; }
        public string IdArticulo { get; set; }
        public string IdCondicionArticulo { get; set; }
        public string IdCategoria { get; set; }
        public string IdEstadoArticulo { get; set; }

        public virtual Articulos IdArticuloNavigation { get; set; }
        public virtual Categorias IdCategoriaNavigation { get; set; }
        public virtual CondicionArticulos IdCondicionArticuloNavigation { get; set; }
        public virtual EstadoArticulos IdEstadoArticuloNavigation { get; set; }
    }
}
