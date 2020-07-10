using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CFAInmuebles.Domain.Models
{
    [Table("ContratoCliente")]
    public partial class ContratosClientes
    {
        public ContratosClientes()
        {
            DatosImprimirFactura = new HashSet<DatosImprimirFactura>();
            Facturacion = new HashSet<Facturacion>();
            HistoricoFacturacion = new HashSet<HistoricoFacturacion>();
            HistoricoFacturacionBanco = new HashSet<HistoricoFacturacionBanco>();
            HistorialOcupacion = new HashSet<HistorialOcupacion>();
            HistoricoContratosClientes = new HashSet<HistoricoContratosClientes>();
            ContratosClientesBajaTemporal = new HashSet<ContratosClientesBajaTemporal>();
        }

        public override string ToString()
        {
            return NombreCliente;
        }


        [Key]
        public int IdContratoCliente { get; set; }
        [Column("IdTipoIPC")]
        public int? IdTipoIpc { get; set; }
        public int? IdFormaPago { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? FechaAlta { get; set; }

        public string NIF { get; set; }
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

        public int? IdEmpresa{ get; set; }
        
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

        [StringLength(150)]
        public string NombreCliente { get; set; }

        [ForeignKey(nameof(IdConceptoFacturacion))]
        [InverseProperty(nameof(ConceptoFacturacion.ContratosClientes))]
        public virtual ConceptoFacturacion IdConceptoFacturacionNavigation { get; set; }
        [ForeignKey(nameof(IdFormaPago))]
        [InverseProperty(nameof(FormasDePago.ContratosClientes))]
        public virtual FormasDePago IdFormaPagoNavigation { get; set; }
        [ForeignKey(nameof(IdInmueble))]
        [InverseProperty(nameof(Inmuebles.ContratosClientes))]
        public virtual Inmuebles IdInmuebleNavigation { get; set; }
        [ForeignKey(nameof(IdSwift))]
        [InverseProperty(nameof(Swift.ContratosClientes))]
        public virtual Swift IdSwiftNavigation { get; set; }
        [ForeignKey(nameof(IdTipoIpc))]
        [InverseProperty(nameof(TipoIpc.ContratosClientes))]
        public virtual TipoIpc IdTipoIpcNavigation { get; set; }

        [ForeignKey(nameof(IdEmpresa))]
        [InverseProperty(nameof(Empresas.ContratosClientes))]
        public virtual Empresas IdEmpresaNavigation { get; set; }

        [ForeignKey(nameof(IdUsuario))]
        [InverseProperty(nameof(Usuarios.ContratosClientes))]
        public virtual Usuarios IdUsuarioNavigation { get; set; }
        [InverseProperty("IdContratoClienteNavigation")]
        public virtual ICollection<DatosImprimirFactura> DatosImprimirFactura { get; set; }
        [InverseProperty("IdContratoClienteNavigation")]
        public virtual ICollection<Facturacion> Facturacion { get; set; }
        [InverseProperty("IdContratoClienteNavigation")]
        public virtual ICollection<HistoricoFacturacion> HistoricoFacturacion { get; set; }
        [InverseProperty("IdContratoclienteNavigation")]
        public virtual ICollection<HistoricoFacturacionBanco> HistoricoFacturacionBanco { get; set; }

        [InverseProperty("IdContratoClienteNavigation")]
        public virtual ICollection<HistorialOcupacion> HistorialOcupacion { get; set; }

        [InverseProperty("IdContratoClienteNavigation")]
        public virtual ICollection<HistoricoContratosClientes> HistoricoContratosClientes { get; set; }

        [InverseProperty("IdContratoClienteNavigation")]
        public virtual ICollection<ContratosClientesBajaTemporal> ContratosClientesBajaTemporal { get; set; }
    }
}
