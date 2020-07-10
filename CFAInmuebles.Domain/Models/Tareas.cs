using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CFAInmuebles.Domain.Models
{
    [Table("Tarea")]
    public partial class Tareas
    {
        public Tareas()
        {
            TareaPeriodica = new HashSet<TareaPeriodica>();
        }

        public override string ToString()
        {
            return Tarea;
        }

        [Key]
        public int IdTarea { get; set; }
        [Required]
        [StringLength(100)]
        public string Tarea { get; set; }
        public int IdTipoFichero { get; set; }
        [Required]
        [StringLength(150)]
        public string Descripcion { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? FechaEliminacion { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime FechaSistema { get; set; }
        public int? IdUsuario { get; set; }

        [ForeignKey(nameof(IdTipoFichero))]
        [InverseProperty(nameof(TipoFichero.Tareas))]
        public virtual TipoFichero IdTipoFicheroNavigation { get; set; }
        [ForeignKey(nameof(IdUsuario))]
        [InverseProperty(nameof(Usuarios.Tareas))]
        public virtual Usuarios IdUsuarioNavigation { get; set; }
        [InverseProperty("IdTareaNavigation")]
        public virtual ICollection<TareaPeriodica> TareaPeriodica { get; set; }
    }
}
