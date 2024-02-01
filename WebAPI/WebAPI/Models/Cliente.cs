using System;
using System.Collections.Generic;

namespace WebAPI.Models
{
    public partial class Cliente
    {
        public Cliente()
        {
            Tarjetacreditos = new HashSet<Tarjetacredito>();
        }

        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }

        public virtual ICollection<Tarjetacredito> Tarjetacreditos { get; set; }
    }
}
