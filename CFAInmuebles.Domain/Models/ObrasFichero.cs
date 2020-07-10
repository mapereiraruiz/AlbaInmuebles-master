using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CFAInmuebles.Domain.Models
{
    public partial class ObrasFichero
    {
        [Key]
        public int IdObraFichero { get; set; }
        public int IdHistorialObra { get; set; }
        public int? IdFichero { get; set; }
        public int? IdTipoFichero { get; set; }

        [ForeignKey(nameof(IdHistorialObra))]
        [InverseProperty(nameof(HistorialObra.ObrasFichero))]
        public virtual HistorialObra IdHistorialObraNavigation { get; set; }
        [ForeignKey(nameof(IdTipoFichero))]
        [InverseProperty(nameof(TipoFichero.ObrasFichero))]
        public virtual TipoFichero IdTipoFicheroNavigation { get; set; }

        [NotMapped]
        public string TipoObraDescripcion { get; set; }
    }
}
