using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CFAInmuebles.Domain.Models
{
    public partial class TipoMantenimiento
    {
        public TipoMantenimiento()
        {
            Mantenimientos = new HashSet<Mantenimientos>();
        }

        public override string ToString()
        {
            return Valor;
        }

        [Key]
        public int IdTipoMantenimiento { get; set; }
        [StringLength(150)]
        public string Valor { get; set; }

        public virtual ICollection<Mantenimientos> Mantenimientos { get; set; }
    }
}
