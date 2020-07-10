using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CFAInmuebles.Domain.Models
{
    public partial class HistoricoFacturacionBanco
    {
        [Key]
        public int IdHistoricoFacturacionBanco { get; set; }
        public int? IdHistoricoFacturacion { get; set; }
        public int? IdContratocliente { get; set; }
        [Column("IBAN")]
        [StringLength(50)]
        public string Iban { get; set; }
        [Column("SWIFT")]
        [StringLength(50)]
        public string Swift { get; set; }
        [StringLength(50)]
        public string Nombre { get; set; }

        [ForeignKey(nameof(IdContratocliente))]
        [InverseProperty(nameof(ContratosClientes.HistoricoFacturacionBanco))]
        public virtual ContratosClientes IdContratoclienteNavigation { get; set; }
        [ForeignKey(nameof(IdHistoricoFacturacion))]
        [InverseProperty(nameof(HistoricoFacturacion.HistoricoFacturacionBanco))]
        public virtual HistoricoFacturacion IdHistoricoFacturacionNavigation { get; set; }
    }
}
