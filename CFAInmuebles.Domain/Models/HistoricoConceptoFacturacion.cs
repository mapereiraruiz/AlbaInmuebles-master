using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CFAInmuebles.Domain.Models
{
    public partial class HistoricoConceptoFacturacion
    {
        [Key]
        public int IdHistoricoConceptoFacturacion { get; set; }
        public int IdHistoricoFacturacion { get; set; }
        public int IdConceptoFacturacion { get; set; }
        [Required]
        [StringLength(200)]
        public string ConceptoFacturacion { get; set; }
        [Column(TypeName = "decimal(30, 2)")]
        public decimal? Importe { get; set; }
        [StringLength(50)]
        public string CodigoAgrupacion { get; set; }
        [Column(TypeName = "decimal(30, 2)")]
        public decimal? IvaConcepto { get; set; }
        [Column(TypeName = "decimal(30, 2)")]
        public decimal? IvaPorcentaje { get; set; }
        public int? IdCatalogarConcepto { get; set; }
        public int? IdTipoConcepto { get; set; }

        [ForeignKey(nameof(IdCatalogarConcepto))]
        [InverseProperty(nameof(CatalogarConcepto.HistoricoConceptoFacturacion))]
        public virtual CatalogarConcepto IdCatalogarConceptoNavigation { get; set; }
        [ForeignKey(nameof(IdConceptoFacturacion))]
        [InverseProperty("HistoricoConceptoFacturacion")]
        public virtual ConceptoFacturacion IdConceptoFacturacionNavigation { get; set; }
        [ForeignKey(nameof(IdHistoricoFacturacion))]
        [InverseProperty(nameof(HistoricoFacturacion.HistoricoConceptoFacturacion))]
        public virtual HistoricoFacturacion IdHistoricoFacturacionNavigation { get; set; }
        [ForeignKey(nameof(IdTipoConcepto))]
        [InverseProperty(nameof(TipoConcepto.HistoricoConceptoFacturacion))]
        public virtual TipoConcepto IdTipoConceptoNavigation { get; set; }
    }
}
