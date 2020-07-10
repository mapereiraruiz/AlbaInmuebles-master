using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CFAInmuebles.Domain.Models
{
    public partial class TipoOrigen
    {
        public TipoOrigen()
        {
            Normas = new HashSet<Normas>();
        }

        public override string ToString()
        {
            return Valor;
        }

        [Key]
        public int IdTipoOrigen { get; set; }
        [StringLength(150)]
        public string Valor { get; set; }

        [InverseProperty("IdTipoOrigenNavigation")]
        public virtual ICollection<Normas> Normas { get; set; }
    }
}
