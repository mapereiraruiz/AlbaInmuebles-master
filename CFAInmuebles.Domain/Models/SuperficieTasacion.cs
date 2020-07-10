using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CFAInmuebles.Domain.Models
{
    public partial class SuperficieTasacion
    {
        public override string ToString()
        {
            return SumatorioSuperficie?.ToString();
        }

        [Key]
        public int IdSuperficie { get; set; }
        public int IdTasacion { get; set; }
        public int? IdInmueble { get; set; }
        public int? IdArticulo { get; set; }
        [Column(TypeName = "decimal(30, 2)")]
        public decimal? SumatorioSuperficie { get; set; }

        [ForeignKey(nameof(IdArticulo))]
        [InverseProperty(nameof(Articulos.SuperficieTasacion))]
        public virtual Articulos IdArticuloNavigation { get; set; }
        [ForeignKey(nameof(IdInmueble))]
        [InverseProperty(nameof(Inmuebles.SuperficieTasacion))]
        public virtual Inmuebles IdInmuebleNavigation { get; set; }
        [ForeignKey(nameof(IdTasacion))]
        [InverseProperty(nameof(HistoricoTasaciones.SuperficieTasacion))]
        public virtual HistoricoTasaciones IdTasacionNavigation { get; set; }
    }
}
