using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CFAInmuebles.Domain.Models
{
    public partial class TipoConceptoArticulo
    {
        public TipoConceptoArticulo()
        {
            TipoArticulos = new HashSet<TipoArticulos>();
        }

        public override string ToString()
        {
            return Valor;
        }

        [Key]
        public int IdTipoConceptoArticulo { get; set; }
        [StringLength(150)]
        public string Valor { get; set; }

        [InverseProperty("IdTipoConceptoArticuloNavigation")]
        public virtual ICollection<TipoArticulos> TipoArticulos { get; set; }
    }
}
