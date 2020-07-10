using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CFAInmuebles.Domain.Models
{
    public partial class Swift
    {
        public Swift()
        {
            ContratosClientes = new HashSet<ContratosClientes>();
            Empresas = new HashSet<Empresas>();
            HistoricoEmpresa = new HashSet<HistoricoEmpresas>();
            HistoricoContratosClientes = new HashSet<HistoricoContratosClientes>();
        }

        public override string ToString()
        {
            return Swift1;
        }

        [Key]
        public int IdSwift { get; set; }
        [Column("Swift")]
        [StringLength(50)]
        [Required]
        public string Swift1 { get; set; }
        [StringLength(200)]
        public string Banco { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? FechaEliminacion { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime FechaSistema { get; set; }
        public int? IdUsuario { get; set; }

        [ForeignKey(nameof(IdUsuario))]
        [InverseProperty(nameof(Usuarios.Swift))]
        public virtual Usuarios IdUsuarioNavigation { get; set; }
        [InverseProperty("IdSwiftNavigation")]
        public virtual ICollection<ContratosClientes> ContratosClientes { get; set; }

        [InverseProperty("IdSwiftNavigation")]
        public virtual ICollection<Empresas> Empresas { get; set; }

        [InverseProperty("IdSwiftNavigation")]
        public virtual ICollection<HistoricoEmpresas> HistoricoEmpresa { get; set; }

        [InverseProperty("IdSwiftNavigation")]
        public virtual ICollection<HistoricoContratosClientes> HistoricoContratosClientes { get; set; }
    }
}
