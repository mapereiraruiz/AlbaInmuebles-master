using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace CFAInmuebles.Domain.Models
{
    [Table("HistoricoSeguro")]
    public partial class HistoricoSeguros
    {
        public override string ToString()
        {
            if (FechaSeguro != null)
                return FechaSeguro.Value.ToShortDateString();
            else
                return string.Empty;
        }

        [NotMapped]
        public string NombreInmueble { get; set; }

        [Key]
        public int IdHistoricoSeguro { get; set; }
        public int IdEmpresa { get; set; }
        public int IdInmueble { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? FechaContable { get; set; }


        [Column(TypeName = "decimal(30, 2)")]
        public decimal? Continente { get; set; }
        [Column(TypeName = "decimal(30, 2)")]
        public decimal? Daños { get; set; }
        [Column(TypeName = "decimal(30, 2)")]
        public decimal? ResponsabilidadCivil { get; set; }
        [Column("PerdidaAlquiler", TypeName = "decimal(30, 2)")]
        public decimal? PerdidaAlquileres { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? FechaSeguro { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? FechaIncorporacion { get; set; }
        [StringLength(200)]
        public string ArchivoDigital { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? FechaEliminacion { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime FechaSistema { get; set; }
        public int? IdUsuario { get; set; }

        [ForeignKey(nameof(IdEmpresa))]
        [InverseProperty(nameof(Empresas.HistoricoSeguros))]
        public virtual Empresas IdEmpresaNavigation { get; set; }
        [ForeignKey(nameof(IdInmueble))]
        [InverseProperty(nameof(Inmuebles.HistoricoSeguros))]
        public virtual Inmuebles IdInmuebleNavigation { get; set; }
        [ForeignKey(nameof(IdUsuario))]
        [InverseProperty(nameof(Usuarios.HistoricoSeguros))]
        public virtual Usuarios IdUsuarioNavigation { get; set; }
    }
}
