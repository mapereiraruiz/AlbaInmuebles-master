using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CFAInmuebles.Domain.Models
{
    [Table("TipoInmueble")]
    public partial class TipoInmuebles
    {
        public TipoInmuebles()
        {
            Inmuebles = new HashSet<Inmuebles>();
            HistoricoInmueble = new HashSet<HistoricoInmuebles>();
        }

        public override string ToString()
        {
            return TipoInmueble;
        }

        [Key]
        public int IdTipoInmueble { get; set; }
        [Required]
        [Column("tipoInmueble")]
        [StringLength(50)]
        public string TipoInmueble { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? FechaEliminacion { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime FechaSistema { get; set; }
        public int? IdUsuario { get; set; }

        [ForeignKey(nameof(IdUsuario))]
        [InverseProperty(nameof(Usuarios.TipoInmuebles))]
        public virtual Usuarios IdUsuarioNavigation { get; set; }
        [InverseProperty("IdTipoInmuebleNavigation")]
        public virtual ICollection<Inmuebles> Inmuebles { get; set; }

        [InverseProperty("IdTipoInmuebleNavigation")]
        public virtual ICollection<HistoricoInmuebles> HistoricoInmueble { get; set; }
    }
}
