using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CFAInmuebles.Domain.Models
{
    public partial class InmuebleCentroCoste
    {
        public InmuebleCentroCoste()
        {
            ContratosProveedores = new HashSet<ContratosProveedores>();
            Inmuebles = new HashSet<Inmuebles>();
            HistoricoInmueble = new HashSet<HistoricoInmuebles>();
            HistoricoContratosProveedores = new HashSet<HistoricoContratosProveedores>();
        }

        public override string ToString()
        {
            return Valor;
        }

        [Key]
        public int IdInmuebleCentroCoste { get; set; }
        [StringLength(150)]
        public string Valor { get; set; }

        [InverseProperty("IdInmuebleCentroCosteNavigation")]
        public virtual ICollection<ContratosProveedores> ContratosProveedores { get; set; }
        [InverseProperty("IdInmuebleCentroCosteNavigation")]
        public virtual ICollection<Inmuebles> Inmuebles { get; set; }
        [InverseProperty("IdInmuebleCentroCosteNavigation")]
        public virtual ICollection<HistoricoInmuebles> HistoricoInmueble { get; set; }

        [InverseProperty("IdInmuebleCentroCosteNavigation")]
        public virtual ICollection<HistoricoContratosProveedores> HistoricoContratosProveedores { get; set; }
    }
}
