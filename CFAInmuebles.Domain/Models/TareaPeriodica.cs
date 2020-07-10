using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace CFAInmuebles.Domain.Models
{

    public partial class TareaPeriodica
    {
        
  
        public override string ToString()
        {
            return IdTareaNavigation?.Tarea;
        }


        [Key]
        public int IdTareaPeriodica { get; set; }
        public int IdTarea { get; set; }
        public int IdFichero { get; set; }
        public int IdTipoFichero { get; set; }

        public int? IdPeriodicidad { get; set; }
        [StringLength(250)]
        public string Propietario { get; set; }

        public int? AsignadaA { get; set; }
        public int IdResponsable { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? FechaInicio { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime FechaFin { get; set; }
        [Required]
        [StringLength(50)]
        public string Estado { get; set; }

       
        [Required]
        [StringLength(100)]
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public double? Porcentaje { get; set; }
        public bool Recurrente { get; set; }
        [StringLength(100)]
        public string TipoRecurrente { get; set; }
        [Column("FechaYHoraRecordatorio", TypeName = "smalldatetime")]
        public DateTime? FechaYhoraRecordatorio { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime FechaSistema { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? FechaEliminacion { get; set; }
        public int? IdUsuario { get; set; }

        [ForeignKey(nameof(IdTarea))]
        [InverseProperty(nameof(Tareas.TareaPeriodica))]
        public virtual Tareas IdTareaNavigation { get; set; }
        [ForeignKey(nameof(IdTipoFichero))]
        [InverseProperty(nameof(TipoFichero.TareaPeriodica))]
        public virtual TipoFichero IdTipoFicheroNavigation { get; set; }
        [ForeignKey(nameof(IdUsuario))]
        [InverseProperty(nameof(Usuarios.TareaPeriodica))]
        public virtual Usuarios IdUsuarioNavigation { get; set; }

        [ForeignKey(nameof(AsignadaA))]
        [InverseProperty(nameof(Usuarios.TareaPeriodicaAsignada))]
        public virtual Usuarios IdUsuarioAsignadoNavigation { get; set; }

        [ForeignKey(nameof(IdResponsable))]
        [InverseProperty(nameof(Usuarios.TareaPeriodicaResponsable))]
        public virtual Usuarios IdUsuarioResponsableNavigation { get; set; }

        [ForeignKey(nameof(IdPeriodicidad))]
        [InverseProperty(nameof(Periodicidad.TareaPeriodica))]
        public virtual Periodicidad IdPeriodicidadNavigation { get; set; }


        [NotMapped]
        public string TipoTareaDescripcion { get; set; }
    }
}
