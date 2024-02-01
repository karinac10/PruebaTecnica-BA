using System;
using System.Collections.Generic;

namespace WebAPI.Models
{
    public partial class Movimiento
    {
        public int IdMovimiento { get; set; }
        public int? IdTarjeta { get; set; }
        public DateTime? Fecha { get; set; }
        public string? Descripcion { get; set; }
        public decimal? Monto { get; set; }
        public string? Tipo { get; set; }

        public virtual Tarjetacredito? IdTarjetaNavigation { get; set; }
    }
}
