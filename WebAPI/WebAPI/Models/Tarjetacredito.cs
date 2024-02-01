using System;
using System.Collections.Generic;

namespace WebAPI.Models
{
    public partial class Tarjetacredito
    {
        public Tarjetacredito()
        {
            Movimientos = new HashSet<Movimiento>();
        }

        public int IdTarjeta { get; set; }
        public int? IdCliente { get; set; }
        public string? NumeroTarjeta { get; set; }
        public decimal? SaldoActual { get; set; }
        public decimal? SaldoDisponible { get; set; }
        public decimal? LimiteCredito { get; set; }
        public decimal? InteresBonificable { get; set; }

        public string? Nombre { get; set; }
        public string? Apellido { get; set; }

        public virtual Cliente? IdClienteNavigation { get; set; }
        public virtual ICollection<Movimiento> Movimientos { get; set; }
    }
}
