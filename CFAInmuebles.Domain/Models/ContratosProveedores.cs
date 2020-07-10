using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CFAInmuebles.Domain.Models
{
    [Table("ContratoProveedor")]
    public partial class ContratosProveedores
    {
        public ContratosProveedores()
        {
            Mantenimientos = new HashSet<Mantenimientos>();
            HistoricoContratosProveedores = new HashSet<HistoricoContratosProveedores>();
            Incidencias = new HashSet<Incidencias>();
            HistorialObra = new HashSet<HistorialObra>();
        }

        public override string ToString()
        {
            return NombreProveedor;
        }

        [Key]
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

        [StringLength(20)]
        public string NIF { get; set; }

        [StringLength(150)]
        public string NombreProveedor { get; set; }

        [ForeignKey(nameof(IdInmuebleCentroCoste))]
        [InverseProperty(nameof(InmuebleCentroCoste.ContratosProveedores))]
        public virtual InmuebleCentroCoste IdInmuebleCentroCosteNavigation { get; set; }
        [ForeignKey(nameof(IdInmueble))]
        [InverseProperty(nameof(Inmuebles.ContratosProveedores))]
        public virtual Inmuebles IdInmuebleNavigation { get; set; }
       
        [ForeignKey(nameof(IdUsuario))]
        [InverseProperty(nameof(Usuarios.ContratosProveedores))]
        public virtual Usuarios IdUsuarioNavigation { get; set; }

        [ForeignKey(nameof(IdPeriodicidadCoste))]
        [InverseProperty(nameof(Periodicidad.ContratosProveedoresCoste))]
        public virtual Periodicidad IdPeriodicidadCosteNavigation { get; set; }

        [ForeignKey(nameof(IdPeriodicidadRevision))]
        [InverseProperty(nameof(Periodicidad.ContratosProveedoresRevision))]
        public virtual Periodicidad IdPeriodicidadRevisionNavigation { get; set; }

        [InverseProperty("IdContratoProveedorNavigation")]
        public virtual ICollection<Mantenimientos> Mantenimientos { get; set; }
        
        [InverseProperty("IdContratoProveedorNavigation")]
        public virtual ICollection<HistoricoContratosProveedores> HistoricoContratosProveedores { get; set; }

        [InverseProperty("IdContratoProveedorNavigation")]
        public virtual ICollection<Incidencias> Incidencias { get; set; }

        [InverseProperty("IdContratoProveedorNavigation")]
        public virtual ICollection<HistorialObra> HistorialObra { get; set; }
    }
}
