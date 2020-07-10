using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CFAInmuebles.Domain.Models
{
    [Table("HistoricoContratoProveedor")]
    public partial class HistoricoContratosProveedores
    {
        public override string ToString()
        {
            return ReferenciaContrato;
        }

        [Key]
        public int IdHistoricoContratoProveedor { get; set; }
        public int IdContratoProveedor { get; set; }
        public int IdInmueble { get; set; }
        [StringLength(50)]
        [Column("NumContrato")]
        public string ReferenciaContrato { get; set; }
        [StringLength(100)]
        public string Servicio { get; set; }
        [Column("coste", TypeName = "decimal(30, 2)")]
        public decimal? CosteAnual { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? FechaVencimiento { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? FechaPreaviso { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? Fecha { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? FechaBaja { get; set; }
        public int? IdPeriodicidadCoste { get; set; }

        public int? IdPeriodicidadRevision { get; set; }

        [Column(TypeName = "decimal(30, 2)")]
        public decimal? ImporteFactura { get; set; }
        public int? IdInmuebleCentroCoste { get; set; }
        public int? PeriodicidadRevision { get; set; }
        [StringLength(200)]
        public string ArchivoDigital { get; set; }
        [StringLength(100)]
        public string Varios1 { get; set; }
        [StringLength(100)]
        public string Varios2 { get; set; }
        [StringLength(100)]
        public string Varios3 { get; set; }
        [StringLength(100)]
        public string Varios4 { get; set; }
        [StringLength(100)]
        public string Varios5 { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime FechaSistema { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? FechaEliminacion { get; set; }
        public int? IdUsuario { get; set; }

        [ForeignKey(nameof(IdContratoProveedor))]
        [InverseProperty(nameof(ContratosProveedores.HistoricoContratosProveedores))]
        public virtual ContratosProveedores IdContratoProveedorNavigation { get; set; }

        [ForeignKey(nameof(IdInmuebleCentroCoste))]
        [InverseProperty(nameof(InmuebleCentroCoste.HistoricoContratosProveedores))]
        public virtual InmuebleCentroCoste IdInmuebleCentroCosteNavigation { get; set; }
        [ForeignKey(nameof(IdInmueble))]
        [InverseProperty(nameof(Inmuebles.HistoricoContratosProveedores))]
        public virtual Inmuebles IdInmuebleNavigation { get; set; }

        [ForeignKey(nameof(IdPeriodicidadCoste))]
        [InverseProperty(nameof(Periodicidad.HistoricoContratosProveedoresCoste))]
        public virtual Periodicidad IdPeriodicidadCosteNavigation { get; set; }

        [ForeignKey(nameof(IdPeriodicidadRevision))]
        [InverseProperty(nameof(Periodicidad.HistoricoContratosProveedoresRevision))]
        public virtual Periodicidad IdPeriodicidadRevisionNavigation { get; set; }


        [ForeignKey(nameof(IdUsuario))]
        [InverseProperty(nameof(Usuarios.HistoricoContratosProveedores))]
        public virtual Usuarios IdUsuarioNavigation { get; set; }
    }
}
