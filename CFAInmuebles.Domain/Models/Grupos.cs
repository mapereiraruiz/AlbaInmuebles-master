using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CFAInmuebles.Domain.Models
{
    public partial class Grupos
    {
        [Key]
        public int IdGrupo { get; set; }
        [Column("nombreGrupo")]
        [StringLength(500)]
        public string NombreGrupo { get; set; }
    }
}
