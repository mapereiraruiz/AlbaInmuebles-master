using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CFAInmuebles.Domain.Models
{
    [Table("FormaPago")]
    public partial class FormasDePago
    {
        public FormasDePago()
        {
            ContratosClientes = new HashSet<ContratosClientes>();
            HistoricoContratosClientes = new HashSet<HistoricoContratosClientes>();
        }

        public override string ToString()
        {
            return FormaPago;
        }

        [Key]
        public int IdFormaPago { get; set; }
        [Required]
        [StringLength(150)]
        public string FormaPago { get; set; }
        [Column("codigoFormaPago")]
        public int CodigoFormaPago { get; set; }
        [Column("otroDato")]
        [StringLength(150)]
        public string OtroDato { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? FechaEliminacion { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime FechaSistema { get; set; }
        public int? IdUsuario { get; set; }

        [ForeignKey(nameof(IdUsuario))]
        [InverseProperty(nameof(Usuarios.FormasDePago))]
        public virtual Usuarios IdUsuarioNavigation { get; set; }
        [InverseProperty("IdFormaPagoNavigation")]
        public virtual ICollection<ContratosClientes> ContratosClientes { get; set; }

        [InverseProperty("IdFormaPagoNavigation")]
        public virtual ICollection<HistoricoContratosClientes> HistoricoContratosClientes { get; set; }
    }
}
