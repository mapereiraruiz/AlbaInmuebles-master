using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CFAInmuebles.Domain.Models
{
    public partial class PeriodicidadServicio
    {
        public PeriodicidadServicio()
        {
            Mantenimientos = new HashSet<Mantenimientos>();
        }

        public override string ToString()
        {
            return Valor;
        }

        [Key]
        public int IdPeriodicidadServicio { get; set; }
        [StringLength(150)]
        public string Valor { get; set; }

        [InverseProperty("IdPeriodicidadServicioNavigation")]
        public virtual ICollection<Mantenimientos> Mantenimientos { get; set; }
    }
}
