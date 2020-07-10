using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CFAInmuebles.Domain.Models
{
    public partial class TipoInstalacion
    {
        public TipoInstalacion()
        {
            Mantenimientos = new HashSet<Mantenimientos>();
            Normas = new HashSet<Normas>();
        }

        public override string ToString()
        {
            return Valor;
        }

        [Key]
        public int IdTipoInstalacion { get; set; }
        [Required]
        [StringLength(150)]
        public string Valor { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? FechaEliminacion { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime FechaSistema { get; set; }
        public int? IdUsuario { get; set; }

        [ForeignKey(nameof(IdUsuario))]
        [InverseProperty(nameof(Usuarios.TipoInstalacion))]
        public virtual Usuarios IdUsuarioNavigation { get; set; }

        public virtual ICollection<Mantenimientos> Mantenimientos { get; set; }
        [InverseProperty("IdTipoInstalacionNavigation")]
        public virtual ICollection<Normas> Normas { get; set; }
    }
}
