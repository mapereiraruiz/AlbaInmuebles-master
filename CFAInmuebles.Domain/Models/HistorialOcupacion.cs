using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CFAInmuebles.Domain.Models
{
    public partial class HistorialOcupacion
    {
     
        public override string ToString()
        {
            return IdHistorialOcupacion.ToString();
        }

        [Key]
        public int IdHistorialOcupacion{ get; set; }
        public int? IdArticulo { get; set; }
        public int? IdContratoCliente { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? FechaAlta { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? FechaBaja { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime FechaSistema { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? FechaEliminacion { get; set; }
        public int? IdUsuario { get; set; }

        [ForeignKey(nameof(IdArticulo))]
        [InverseProperty(nameof(Articulos.HistorialOcupacion))]
        public virtual Articulos IdArticuloNavigation { get; set; }
        [ForeignKey(nameof(IdContratoCliente))]
        [InverseProperty(nameof(ContratosClientes.HistorialOcupacion))]
        public virtual ContratosClientes IdContratoClienteNavigation { get; set; }
        [ForeignKey(nameof(IdUsuario))]
        [InverseProperty(nameof(Usuarios.HistorialOcupacion))]
        public virtual Usuarios IdUsuarioNavigation { get; set; }

    }
}
