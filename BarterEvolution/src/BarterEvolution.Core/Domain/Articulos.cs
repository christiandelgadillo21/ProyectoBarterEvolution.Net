using System;
using System.Collections.Generic;

namespace BarterEvolution.Infrastructure
{
    public partial class Articulos
    {
        public Articulos()
        {
            Contratos = new HashSet<Contratos>();
            Inventario = new HashSet<Inventario>();
            LegalidadArticulos = new HashSet<LegalidadArticulos>();
            Ventas = new HashSet<Ventas>();
        }

        public string IdArticulo { get; set; }
        public string NombreArticulo { get; set; }
        public string Serie { get; set; }
        public string Modelo { get; set; }
        public string Marca { get; set; }
        public int PrecioUnitario { get; set; }
        public string Descripcion { get; set; }
        public string Genero { get; set; }
        public string IdCategoria { get; set; }
        public string IdEstadoArticulo { get; set; }

        public virtual Genero GeneroNavigation { get; set; }
        public virtual Categorias IdCategoriaNavigation { get; set; }
        public virtual EstadoArticulos IdEstadoArticuloNavigation { get; set; }
        public virtual ICollection<Contratos> Contratos { get; set; }
        public virtual ICollection<Inventario> Inventario { get; set; }
        public virtual ICollection<LegalidadArticulos> LegalidadArticulos { get; set; }
        public virtual ICollection<Ventas> Ventas { get; set; }
    }
}
