using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CFAInmuebles.Domain.Models
{
    public partial class UsuarioInmueble
    {
        [Key]
        [Column("IdUsuarioInmueble")]
        public int IdUsuarioInmueble { get; set; }
        public int IdUsuario { get; set; }
        public int IdInmueble { get; set; }

        [ForeignKey(nameof(IdInmueble))]
        public virtual Inmuebles IdInmuebleNavigation { get; set; }
        [ForeignKey(nameof(IdUsuario))]
        public virtual Usuarios IdUsuarioNavigation { get; set; }
    }
}
