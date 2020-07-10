using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CFAInmuebles.Domain.Models
{
    public partial class TipoObra
    {
        public TipoObra()
        {
            HistorialObra = new HashSet<HistorialObra>();
        }

        public override string ToString()
        {
            return Valor;
        }

        [Key]
        public int IdTipoObra { get; set; }
        [StringLength(150)]
        public string Valor { get; set; }

        [InverseProperty("IdTipoObraNavigation")]
        public virtual ICollection<HistorialObra> HistorialObra { get; set; }
    }
}
