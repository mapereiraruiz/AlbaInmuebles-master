using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CFAInmuebles.Domain.Models
{
    public partial class ConceptoFacturacionPadre
    {
        public ConceptoFacturacionPadre()
        {
            ConceptoFacturacion = new HashSet<ConceptoFacturacion>();
        }

        public override string ToString()
        {
            return Valor;
        }


        [Key]
        public int IdConceptoFacturacionPadre { get; set; }
        [StringLength(150)]
        public string Valor { get; set; }

        [InverseProperty("IdConceptoFacturacionPadreNavigation")]
        public virtual ICollection<ConceptoFacturacion> ConceptoFacturacion { get; set; }
    }
}
