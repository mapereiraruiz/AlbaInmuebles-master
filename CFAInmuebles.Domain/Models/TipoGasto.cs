using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CFAInmuebles.Domain.Models
{
    public partial class TipoGasto
    {
        public TipoGasto()
        {
            Gastos = new HashSet<Gastos>();
        }

        public override string ToString()
        {
            return Valor;
        }

        [Key]
        public int IdTipoGasto { get; set; }
        [StringLength(250)]
        public string Valor { get; set; }

        [InverseProperty("IdTipoGastoNavigation")]
        public virtual ICollection<Gastos> Gastos { get; set; }
    }
}
