using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CFAInmuebles.Domain.Models
{
    public partial class ReferenciasCatastrales
    {

        public override string ToString()
        {
            return ReferenciaCatastral;
        }

        [Key]
        public int IdReferenciaCatastral { get; set; }
        public int? IdInmueble { get; set; }
        [StringLength(50)]
        public string ReferenciaCatastral { get; set; }
        [StringLength(50)]
        public string DescripcionFinca { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? ValorSuelo { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? ValorConstruccion { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? ValorCatastral { get; set; }
        [StringLength(50)]
        public string TipoPago { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? FechaUltimoPago { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? ImporteUltimoPago { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? ImporteAnual { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? FechaBaja { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? FechaEliminacion { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime FechaSistema { get; set; }
        public int? IdUsuario { get; set; }

        [ForeignKey(nameof(IdInmueble))]
        [InverseProperty(nameof(Inmuebles.ReferenciasCatastrales))]
        public virtual Inmuebles IdInmuebleNavigation { get; set; }
        [ForeignKey(nameof(IdUsuario))]
        [InverseProperty(nameof(Usuarios.ReferenciasCatastrales))]
        public virtual Usuarios IdUsuarioNavigation { get; set; }
    }
}
