using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CFAInmuebles.Domain.Models
{
    public partial class Periodicidad
    {
        public Periodicidad()
        {
            ContratosProveedoresCoste = new HashSet<ContratosProveedores>();
            ContratosProveedoresRevision = new HashSet<ContratosProveedores>();
            TareaPeriodica = new HashSet<TareaPeriodica>();
            HistoricoContratosProveedoresCoste = new HashSet<HistoricoContratosProveedores>();
            HistoricoContratosProveedoresRevision = new HashSet<HistoricoContratosProveedores>();
        }

        public override string ToString()
        {
            return Valor;
        }

        [Key]
        public int IdPeriodicidad{ get; set; }
        [StringLength(150)]
        public string Valor { get; set; }

        [InverseProperty("IdPeriodicidadCosteNavigation")]
        public virtual ICollection<ContratosProveedores> ContratosProveedoresCoste { get; set; }

        [InverseProperty("IdPeriodicidadRevisionNavigation")]
        public virtual ICollection<ContratosProveedores> ContratosProveedoresRevision { get; set; }

        [InverseProperty("IdPeriodicidadNavigation")]
        public virtual ICollection<TareaPeriodica> TareaPeriodica { get; set; }

        [InverseProperty("IdPeriodicidadCosteNavigation")]
        public virtual ICollection<HistoricoContratosProveedores> HistoricoContratosProveedoresCoste { get; set; }

        [InverseProperty("IdPeriodicidadRevisionNavigation")]
        public virtual ICollection<HistoricoContratosProveedores> HistoricoContratosProveedoresRevision { get; set; }



    }
}
