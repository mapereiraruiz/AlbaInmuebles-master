using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CFAInmuebles.Domain.Models
{
    public partial class TipoLicencia
    {
        public TipoLicencia()
        {
            Licencias = new HashSet<Licencias>();
        }

        public override string ToString()
        {
            return Valor;
        }


        [Key]
        public int IdTipoLicencia { get; set; }
        [Required]
        [StringLength(150)]
        public string Valor { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? FechaEliminacion { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime FechaSistema { get; set; }
        public int? IdUsuario { get; set; }

        [ForeignKey(nameof(IdUsuario))]
        [InverseProperty(nameof(Usuarios.TipoLicencia))]
        public virtual Usuarios IdUsuarioNavigation { get; set; }
        [InverseProperty("IdTipoLicenciaNavigation")]
        public virtual ICollection<Licencias> Licencias { get; set; }
    }
}
