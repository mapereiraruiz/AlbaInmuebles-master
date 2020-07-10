using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CFAInmuebles.Domain.Models
{
    public partial class GastosAnuales
    {
        [Key]
        public int IdGastoAnual { get; set; }
        [Column("codigo")]
        public int? Codigo { get; set; }
        [Column("gasto", TypeName = "decimal(18, 2)")]
        public decimal? Gasto { get; set; }
        [Column("capex", TypeName = "decimal(18, 2)")]
        public decimal? Capex { get; set; }
        [Column("superficie", TypeName = "decimal(18, 2)")]
        public decimal? Superficie { get; set; }
        [Column("em2", TypeName = "decimal(18, 2)")]
        public decimal? Em2 { get; set; }
        [Column("em2capex", TypeName = "decimal(18, 2)")]
        public decimal? Em2capex { get; set; }
        [Column("año")]
        public int? Año { get; set; }

        [ForeignKey(nameof(Codigo))]
        [InverseProperty(nameof(Inmuebles.GastosAnuales))]
        public virtual Inmuebles CodigoNavigation { get; set; }
    }
}
