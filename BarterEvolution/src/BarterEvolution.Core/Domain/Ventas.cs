using System;
using System.Collections.Generic;

namespace BarterEvolution.Infrastructure
{
    public partial class Ventas
    {
        public string NoFactura { get; set; }
        public DateTime FechaFactura { get; set; }
        public int PrecioUnitario { get; set; }
        public int Cantidad { get; set; }
        public int SubTotal { get; set; }
        public float Iva { get; set; }
        public int ValorTotal { get; set; }
        public int CedulaCliente { get; set; }
        public int CedulaUsuario { get; set; }
        public string IdArticulo { get; set; }
        public string IdEstadoArticulo { get; set; }

        public virtual Clientes CedulaClienteNavigation { get; set; }
        public virtual UsuariosSistema CedulaUsuarioNavigation { get; set; }
        public virtual Articulos IdArticuloNavigation { get; set; }
        public virtual EstadoArticulos IdEstadoArticuloNavigation { get; set; }
    }
}
