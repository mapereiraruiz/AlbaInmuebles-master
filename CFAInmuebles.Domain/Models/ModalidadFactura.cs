using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CFAInmuebles.Domain.Models
{
    public partial class ModalidadFactura
    {
        public ModalidadFactura()
        {
            Facturacion = new HashSet<Facturacion>();
        }

        public override string ToString()
        {
            return Valor;
        }

        [Key]
        public int IdModalidadFactura { get; set; }
        [StringLength(250)]
        public string Valor { get; set; }

        [InverseProperty("IdModalidadFacturaNavigation")]
        public virtual ICollection<Facturacion> Facturacion { get; set; }
    }
}
