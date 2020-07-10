using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CFAInmuebles.Domain.Models
{
    public partial class Incidencias
    {
        [Key]
        public int IdIncidencia { get; set; }
        public int IdFichero { get; set; }
        public int IdContratoProveedor { get; set; }
        public int IdTipoFichero { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime FechaIncidencia { get; set; }
        [Required]
        [StringLength(200)]
        public string Incidencia { get; set; }
        [StringLength(500)]
        public string Descripcion { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? FechaResolucion { get; set; }
        [StringLength(500)]
        public string DescripcionResolucion { get; set; }
        public bool Finalizada { get; set; }
        [StringLength(200)]
        public string Servicio { get; set; }
        public bool Programado { get; set; }
        [StringLength(200)]
        public string Afecta { get; set; }
        [Column("LPD")]
        public string Lpd { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? FechaEliminacion { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime FechaSistema { get; set; }
        public int? IdUsuario { get; set; }

        [ForeignKey(nameof(IdTipoFichero))]
        [InverseProperty(nameof(TipoFichero.Incidencias))]
        public virtual TipoFichero IdTipoFicheroNavigation { get; set; }
        [ForeignKey(nameof(IdUsuario))]
        [InverseProperty(nameof(Usuarios.Incidencias))]
        public virtual Usuarios IdUsuarioNavigation { get; set; }

        [ForeignKey(nameof(IdContratoProveedor))]
        [InverseProperty(nameof(ContratosProveedores.Incidencias))]

        public virtual ContratosProveedores IdContratoProveedorNavigation { get; set; }

        [NotMapped]
        public string TipoIncidenciaDescripcion { get; set; }
    }
}
