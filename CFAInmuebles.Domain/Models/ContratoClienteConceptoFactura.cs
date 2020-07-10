using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CFAInmuebles.Domain.Models
{
    public partial class ContratoClienteConceptoFactura
    {
        [Key]
        public int Id{ get; set; }
        public int IdContratoCliente { get; set; }
        public int IdConceptoFacturacion { get; set; }

        [ForeignKey(nameof(IdContratoCliente))]
        public virtual ContratosClientes IdContratoClienteNavigation { get; set; }
        [ForeignKey(nameof(IdConceptoFacturacion))]
        public virtual ConceptoFacturacion IdConceptoFacturacionNavigation { get; set; }
    }
}
