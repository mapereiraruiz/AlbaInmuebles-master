using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CFAInmuebles.Domain.Models
{
    public partial class ContratosClientesBajaTemporal
    {
       
        [Key]
        public int IdBajaTemporal { get; set; }

        public int IdContratoCliente { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? FechaInicio { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? FechaFin { get; set; }

        [ForeignKey(nameof(IdContratoCliente))]
        [InverseProperty(nameof(ContratosClientes.ContratosClientesBajaTemporal))]
        public virtual ContratosClientes IdContratoClienteNavigation { get; set; }


    }
}
