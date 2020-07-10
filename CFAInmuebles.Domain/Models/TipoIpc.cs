using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CFAInmuebles.Domain.Models
{
    [Table("TipoIPC")]
    public partial class TipoIpc
    {
        public TipoIpc()
        {
            ContratosClientes = new HashSet<ContratosClientes>();
            HistoricoContratosClientes = new HashSet<HistoricoContratosClientes>();
        }

        public override string ToString()
        {
            return Ipc;
        }

        [Key]
        [Column("IdTipoIPC")]
        public int IdTipoIpc { get; set; }

        [Required]
        [Column("ipc")]       
        [StringLength(100)]
        public string Ipc { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? FechaUltimaActualizacion { get; set; }
        [Column("cambiaripc")]
        public bool Cambiaripc { get; set; }
        [Column("IdTipoIPC_Destino")]
        public int? IdTipoIpcDestino { get; set; }
        [Column(TypeName = "decimal(30, 2)")]
        public decimal? Incremento { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? FechaComunicacionSubida { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? FechaFacturacion { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? FechaProximaRevision { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? FechaEliminacion { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? FechaSistema { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? FechaInicioFacturacion { get; set; }
        public int? IdUsuario { get; set; }

        [ForeignKey(nameof(IdUsuario))]
        [InverseProperty(nameof(Usuarios.TipoIpc))]
        public virtual Usuarios IdUsuarioNavigation { get; set; }
        [InverseProperty("IdTipoIpcNavigation")]
        public virtual ICollection<ContratosClientes> ContratosClientes { get; set; }

        [InverseProperty("IdTipoIpcNavigation")]
        public virtual ICollection<HistoricoContratosClientes> HistoricoContratosClientes { get; set; }
    }
}
