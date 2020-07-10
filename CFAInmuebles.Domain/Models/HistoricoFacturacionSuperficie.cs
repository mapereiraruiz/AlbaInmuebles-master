using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CFAInmuebles.Domain.Models
{
    public partial class HistoricoFacturacionSuperficie
    {
        [Key]
        public int IdHistoricoFacturacionSuperficie { get; set; }
        public int? IdHistoricoFacturacion { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? Fecha { get; set; }
        public int? NumFactura { get; set; }
        public int? IdTipoArticulo { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? NumUnidad { get; set; }

        [ForeignKey(nameof(IdHistoricoFacturacion))]
        [InverseProperty(nameof(HistoricoFacturacion.HistoricoFacturacionSuperficie))]
        public virtual HistoricoFacturacion IdHistoricoFacturacionNavigation { get; set; }
        [ForeignKey(nameof(IdTipoArticulo))]
        [InverseProperty(nameof(TipoArticulos.HistoricoFacturacionSuperficie))]
        public virtual TipoArticulos IdTipoArticuloNavigation { get; set; }
    }
}
