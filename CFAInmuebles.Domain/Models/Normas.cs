using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CFAInmuebles.Domain.Models
{
    public partial class Normas
    {
        public Normas()
        {
            Mantenimientos = new HashSet<Mantenimientos>();
        }

        public override string ToString()
        {
            return TextoDescriptivo;
        }

        [Key]
        public int IdNorma { get; set; }
        [Required]
        [StringLength(250)]
        public string TextoDescriptivo { get; set; }
        public int? IdTipoInstalacion { get; set; }
        public int? IdTipoOrigen { get; set; }

        [StringLength(150)]
        public string ArchivoDigital { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime FechaSistema { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? FechaEliminacion { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? FechaVigencia { get; set; }
        public int? IdUsuario { get; set; }

        [ForeignKey(nameof(IdTipoInstalacion))]
        [InverseProperty(nameof(TipoInstalacion.Normas))]
        public virtual TipoInstalacion IdTipoInstalacionNavigation { get; set; }
        [ForeignKey(nameof(IdUsuario))]
        [InverseProperty(nameof(Usuarios.Normas))]
        public virtual Usuarios IdUsuarioNavigation { get; set; }

        [ForeignKey(nameof(IdTipoOrigen))]
        [InverseProperty(nameof(TipoOrigen.Normas))]
        public virtual TipoOrigen IdTipoOrigenNavigation { get; set; }

        [InverseProperty("IdNormaNavigation")]
        public virtual ICollection<Mantenimientos> Mantenimientos { get; set; }
    }
}
