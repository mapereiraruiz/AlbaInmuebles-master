using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CFAInmuebles.Domain.Models
{
    public partial class TipoConcepto
    {
        public TipoConcepto()
        {
            ConceptoFacturacion = new HashSet<ConceptoFacturacion>();
            HistoricoConceptoFacturacion = new HashSet<HistoricoConceptoFacturacion>();
        }

        public override string ToString()
        {
            return Valor;
        }

        [Key]
        public int IdTipoConcepto { get; set; }
        [StringLength(150)]
        public string Valor { get; set; }

        
        public virtual ICollection<ConceptoFacturacion> ConceptoFacturacion { get; set; }
        [InverseProperty("IdTipoConceptoNavigation")]
        public virtual ICollection<HistoricoConceptoFacturacion> HistoricoConceptoFacturacion { get; set; }
    }
}
