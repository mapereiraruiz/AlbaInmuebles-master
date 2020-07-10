using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CFAInmuebles.Domain.Models
{
    [Table("HistoricoContratoCliente")]
    public partial class HistoricoContratosClientes
    {

        public override string ToString()
        {
            return NumeroContrato.ToString();
        }

        [Key]
        public int IdHistoricoContratoCliente { get; set; }
        public int IdContratoCliente { get; set; }
        [Column("IdTipoIPC")]
        public int? IdTipoIpc { get; set; }
        public int? IdFormaPago { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? FechaAlta { get; set; }
        [Column("NIF")]
        [StringLength(20)]
        public string Nif { get; set; }
        public int IdInmueble { get; set; }
        public int? IdConceptoFacturacion { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime FechaInicioFacturacion { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? FechaUltimaFacturacion { get; set; }
        [Column("NumContrato")]
        public int NumeroContrato { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime FechaContrato { get; set; }
        public int? IdSwift { get; set; }
       
       
        [StringLength(250)]
        public string DireccionEnvio { get; set; }
        [StringLength(50)]
        public string ArchivoDigital { get; set; }
        [StringLength(50)]
        public string CuentaFianza { get; set; }

        [StringLength(50)]
        public string CuentaBancaria { get; set; }

        [Column(TypeName = "decimal(30, 2)")]
        public decimal? ImporteAval { get; set; }
        [StringLength(100)]
        public string Comentario { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? FechaVencimiento { get; set; }
        [StringLength(50)]
        public string AgruparContrato { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime FechaSistema { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? FechaEliminacion { get; set; }
        public int? IdUsuario { get; set; }

        [ForeignKey(nameof(IdContratoCliente))]
        [InverseProperty(nameof(ContratosClientes.HistoricoContratosClientes))]
        public virtual ContratosClientes IdContratoClienteNavigation { get; set; }

        [ForeignKey(nameof(IdUsuario))]
        [InverseProperty(nameof(Usuarios.HistoricoContratosClientes))]
        public virtual Usuarios IdUsuarioNavigation { get; set; }

        
        [ForeignKey(nameof(IdConceptoFacturacion))]
        [InverseProperty(nameof(ConceptoFacturacion.HistoricoContratosClientes))]
        public virtual ConceptoFacturacion IdConceptoFacturacionNavigation { get; set; }
        [ForeignKey(nameof(IdFormaPago))]
        [InverseProperty(nameof(FormasDePago.HistoricoContratosClientes))]
        public virtual FormasDePago IdFormaPagoNavigation { get; set; }
        [ForeignKey(nameof(IdInmueble))]
        [InverseProperty(nameof(Inmuebles.HistoricoContratosClientes))]
        public virtual Inmuebles IdInmuebleNavigation { get; set; }
        [ForeignKey(nameof(IdSwift))]
        [InverseProperty(nameof(Swift.HistoricoContratosClientes))]
        public virtual Swift IdSwiftNavigation { get; set; }
        [ForeignKey(nameof(IdTipoIpc))]
        [InverseProperty(nameof(TipoIpc.HistoricoContratosClientes))]
        public virtual TipoIpc IdTipoIpcNavigation { get; set; }

      

    }
}
