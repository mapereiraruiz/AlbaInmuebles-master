using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CFAInmuebles.Domain.Models
{
    public partial class TipoPlanta
    {
        public TipoPlanta()
        {
            Articulos = new HashSet<Articulos>();
            HistoricoArticulo = new HashSet<HistoricoArticulos>();
        }

        public override string ToString()
        {
            return Valor;
        }

        [Key]
        public int IdPlanta { get; set; }
        [StringLength(150)]
        public string Valor { get; set; }

        [InverseProperty("IdPlantaNavigation")]
        public virtual ICollection<Articulos> Articulos { get; set; }

        [InverseProperty("IdPlantaNavigation")]
        public virtual ICollection<HistoricoArticulos> HistoricoArticulo { get; set; }
    }
}
