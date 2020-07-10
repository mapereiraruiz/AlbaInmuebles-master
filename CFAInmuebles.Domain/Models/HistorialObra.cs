using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CFAInmuebles.Domain.Models
{
    public partial class HistorialObra
    {
        public HistorialObra()
        {
            ObrasFichero = new HashSet<ObrasFichero>();
        }

        public override string ToString()
        {
            return Descripcion;
        }

        [Key]
        public int IdHistorialObra { get; set; }
        public int IdTipoFichero { get; set; }
        public int? IdTipoObra { get; set; }
        public int IdFichero { get; set; }
        public int? IdContratoProveedor { get; set; }
        [StringLength(500)]
        public string Descripcion { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? FechaApertura { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? FechaCierre { get; set; }
        [StringLength(100)]
        public string NumeroExpediente { get; set; }
        [Column(TypeName = "decimal(30, 2)")]
        public decimal? PresupuestoAdjudicacion { get; set; }
        [Column(TypeName = "decimal(30, 2)")]
        public decimal? CosteFinal { get; set; }
        [Column(TypeName = "decimal(30, 2)")]
        public decimal? CosteAsumido { get; set; }
        [Column("DireccionFacultativaSINO")]
        public bool DireccionFacultativaSino { get; set; }
        [StringLength(100)]
        public string DireccionFacultativa { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime FechaSistema { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? FechaEliminacion { get; set; }
        public int? IdUsuario { get; set; }
        public bool? TieneLicencia { get; set; }

        [ForeignKey(nameof(IdContratoProveedor))]
        [InverseProperty(nameof(ContratosProveedores.HistorialObra))]
        public virtual ContratosProveedores IdContratoProveedorNavigation { get; set; }
        [ForeignKey(nameof(IdTipoFichero))]
        [InverseProperty(nameof(TipoFichero.HistorialObra))]
        public virtual TipoFichero IdTipoFicheroNavigation { get; set; }
        [ForeignKey(nameof(IdUsuario))]
        [InverseProperty(nameof(Usuarios.HistorialObra))]
        public virtual Usuarios IdUsuarioNavigation { get; set; }
        [InverseProperty("IdHistorialObraNavigation")]
        public virtual ICollection<ObrasFichero> ObrasFichero { get; set; }

        [ForeignKey(nameof(IdTipoObra))]
        [InverseProperty(nameof(TipoObra.HistorialObra))]
        public virtual TipoObra IdTipoObraNavigation { get; set; }

        [NotMapped]
        public string TipoObraDescripcion { get; set; }
    }
}
