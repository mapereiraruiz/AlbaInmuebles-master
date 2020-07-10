using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CFAInmuebles.Domain.Models
{
    public partial class VentaParcialInmueble
    {
        public VentaParcialInmueble()
        {
            
        }

        [Key]
        public int IdVentaParcialInmueble { get; set; }
        public int IdInmueble { get; set; }
        [StringLength(250)]
        public string Comentario { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? FechaVenta { get; set; }

        [ForeignKey(nameof(IdInmueble))]
        [InverseProperty(nameof(Inmuebles.VentaParcialInmuebles))]
        public virtual Inmuebles IdInmuebleNavigation { get; set; }

    }
}
