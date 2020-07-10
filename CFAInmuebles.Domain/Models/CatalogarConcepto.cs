using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CFAInmuebles.Domain.Models
{
    public partial class CatalogarConcepto
    {
        public CatalogarConcepto()
        {
            ConceptoFacturacion = new HashSet<ConceptoFacturacion>();
            HistoricoConceptoFacturacion = new HashSet<HistoricoConceptoFacturacion>();
        }

        public override string ToString()
        {
            return Valor;
        }


        [Key]
        public int IdCatalogarConcepto { get; set; }
        [StringLength(150)]
        public string Valor { get; set; }

        [InverseProperty("IdCatalogarConceptoNavigation")]
        public virtual ICollection<ConceptoFacturacion> ConceptoFacturacion { get; set; }
        [InverseProperty("IdCatalogarConceptoNavigation")]
        public virtual ICollection<HistoricoConceptoFacturacion> HistoricoConceptoFacturacion { get; set; }
    }
}
