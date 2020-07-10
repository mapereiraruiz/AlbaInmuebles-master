using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CFAInmuebles.Domain.Models
{
    [Table("HistoricoTasacion")]
    public partial class HistoricoTasaciones
    {
        public HistoricoTasaciones()
        {
            SuperficieTasacion = new HashSet<SuperficieTasacion>();
        }

        [Key]
        public int IdHistoricoTasacion { get; set; }
        public int IdEmpresa { get; set; }
        public int IdInmueble { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? FechaTasacion { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? FechaIncorporacion { get; set; }
        [Column(TypeName = "decimal(30, 2)")]
        public decimal? Importe { get; set; }
        [StringLength(200)]
        public string ArchivoDigital { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime FechaSistema { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? FechaEliminacion { get; set; }
        public int? IdUsuario { get; set; }

        [ForeignKey(nameof(IdEmpresa))]
        [InverseProperty(nameof(Empresas.HistoricoTasaciones))]
        public virtual Empresas IdEmpresaNavigation { get; set; }
        [ForeignKey(nameof(IdInmueble))]
        [InverseProperty(nameof(Inmuebles.HistoricoTasaciones))]
        public virtual Inmuebles IdInmuebleNavigation { get; set; }
        [ForeignKey(nameof(IdUsuario))]
        [InverseProperty(nameof(Usuarios.HistoricoTasaciones))]
        public virtual Usuarios IdUsuarioNavigation { get; set; }
        [InverseProperty("IdTasacionNavigation")]
        public virtual ICollection<SuperficieTasacion> SuperficieTasacion { get; set; }
    }
}
