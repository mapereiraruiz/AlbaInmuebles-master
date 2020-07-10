using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CFAInmuebles.Domain.Models
{
    public partial class Mantenimientos
    {
        public override string ToString()
        {
            return Servicio;
        }

        [Key]
        public int IdMantenimiento { get; set; }
        public int IdFichero { get; set; }
        
        public int? IdTipoInstalacion { get; set; }
        public int? IdContratoProveedor { get; set; }
        public int IdTipoMantenimiento { get; set; }
        [StringLength(100)]
        public string Servicio { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? FechaServicio { get; set; }
        public int? IdPeriodicidadServicio { get; set; }
        public int? IdNorma { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime FechaSistema { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? FechaEliminacion { get; set; }
        public int? IdUsuario { get; set; }
        public int IdTipoFichero { get; set; }
        public bool? MedidasCorrectoras { get; set; }

        [ForeignKey(nameof(IdContratoProveedor))]
        [InverseProperty(nameof(ContratosProveedores.Mantenimientos))]
        public virtual ContratosProveedores IdContratoProveedorNavigation { get; set; }


        [ForeignKey(nameof(IdNorma))]
        [InverseProperty(nameof(Normas.Mantenimientos))]
        public virtual Normas IdNormaNavigation { get; set; }


        [ForeignKey(nameof(IdPeriodicidadServicio))]
        [InverseProperty(nameof(PeriodicidadServicio.Mantenimientos))]
        public virtual PeriodicidadServicio IdPeriodicidadServicioNavigation { get; set; }


        [ForeignKey(nameof(IdTipoFichero))]
        [InverseProperty(nameof(TipoFichero.Mantenimientos))]
        public virtual TipoFichero IdTipoFicheroNavigation { get; set; }


        [ForeignKey(nameof(IdTipoInstalacion))]
        [InverseProperty(nameof(TipoInstalacion.Mantenimientos))]
        public virtual TipoInstalacion IdTipoInstalacionNavigation { get; set; }


        [ForeignKey(nameof(IdUsuario))]
        [InverseProperty(nameof(Usuarios.Mantenimientos))]
        public virtual Usuarios IdUsuarioNavigation { get; set; }


        [ForeignKey(nameof(IdTipoMantenimiento))]
        [InverseProperty(nameof(TipoMantenimiento.Mantenimientos))]
        public virtual TipoMantenimiento IdTipoMantenimientoNavigation { get; set; }


        [NotMapped]
        public string TipoMantenimientoDescripcion { get; set; }
    }
}
