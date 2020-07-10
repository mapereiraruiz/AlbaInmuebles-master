using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CFAInmuebles.Domain.Models
{
    public partial class DatosImprimirFactura
    {
        public DatosImprimirFactura()
        {
            HistoricoFacturacion = new HashSet<HistoricoFacturacion>();
        }

        [Key]
        public int IdDatosImprimir { get; set; }
        public int IdContratoCliente { get; set; }
        [StringLength(200)]
        public string Nota { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? FechaInicio { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? FechaFin { get; set; }

        [ForeignKey(nameof(IdContratoCliente))]
        [InverseProperty(nameof(ContratosClientes.DatosImprimirFactura))]
        public virtual ContratosClientes IdContratoClienteNavigation { get; set; }
        [InverseProperty("IdDatoImprimirNavigation")]
        public virtual ICollection<HistoricoFacturacion> HistoricoFacturacion { get; set; }
    }
}
