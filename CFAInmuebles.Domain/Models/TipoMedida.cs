using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CFAInmuebles.Domain.Models
{
    public partial class TipoMedida
    {
        public TipoMedida()
        {
            TipoArticulos = new HashSet<TipoArticulos>();
        }

        public override string ToString()
        {
            return Valor;
        }

        [Key]
        public int IdTipoMedida { get; set; }
        [StringLength(150)]
        public string Valor { get; set; }

        [InverseProperty("IdTipoMedidaNavigation")]
        public virtual ICollection<TipoArticulos> TipoArticulos { get; set; }
    }
}
