using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CFAInmuebles.Domain.Models
{
    public partial class HistoricoFacturacion
    {
        public HistoricoFacturacion()
        {
            HistoricoConceptoFacturacion = new HashSet<HistoricoConceptoFacturacion>();
            HistoricoFacturacionBanco = new HashSet<HistoricoFacturacionBanco>();
            HistoricoFacturacionSuperficie = new HashSet<HistoricoFacturacionSuperficie>();
        }

        [Key]
        public int IdHistoricoFacturacion { get; set; }
        public int IdEmpresa { get; set; }
        public int IdInmueble { get; set; }
        public int IdContratoCliente { get; set; }
        public int IdCliente { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? FechaFactura { get; set; }
        public int? NumFactura { get; set; }
        [StringLength(150)]
        public string Cliente { get; set; }
        [StringLength(100)]
        public string Provincia { get; set; }
        [StringLength(6)]
        public string CodigoPostal { get; set; }
        [StringLength(50)]
        public string Municipio { get; set; }
        [StringLength(250)]
        public string Direccion { get; set; }
        [Column("NIF")]
        [StringLength(9)]
        public string Nif { get; set; }
        public int? IdDatoImprimir { get; set; }
        [StringLength(150)]
        public string FormaPago { get; set; }
        [Column(TypeName = "decimal(30, 2)")]
        public decimal? BaseImponible { get; set; }
        [Column("IVAFactura", TypeName = "decimal(30, 2)")]
        public decimal? Ivafactura { get; set; }
        [Column(TypeName = "decimal(30, 2)")]
        public decimal? TotalFactura { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? FechaAlta { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? FechaNovacion { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? FechaAnulacion { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? FechaSistema { get; set; }

       
        [ForeignKey(nameof(IdContratoCliente))]
        [InverseProperty(nameof(ContratosClientes.HistoricoFacturacion))]
        public virtual ContratosClientes IdContratoClienteNavigation { get; set; }
        [ForeignKey(nameof(IdDatoImprimir))]
        [InverseProperty(nameof(DatosImprimirFactura.HistoricoFacturacion))]
        public virtual DatosImprimirFactura IdDatoImprimirNavigation { get; set; }
        [ForeignKey(nameof(IdEmpresa))]
        [InverseProperty(nameof(Empresas.HistoricoFacturacion))]
        public virtual Empresas IdEmpresaNavigation { get; set; }
        [ForeignKey(nameof(IdInmueble))]
        [InverseProperty(nameof(Inmuebles.HistoricoFacturacion))]
        public virtual Inmuebles IdInmuebleNavigation { get; set; }
        [InverseProperty("IdHistoricoFacturacionNavigation")]
        public virtual ICollection<HistoricoConceptoFacturacion> HistoricoConceptoFacturacion { get; set; }
        [InverseProperty("IdHistoricoFacturacionNavigation")]
        public virtual ICollection<HistoricoFacturacionBanco> HistoricoFacturacionBanco { get; set; }
        [InverseProperty("IdHistoricoFacturacionNavigation")]
        public virtual ICollection<HistoricoFacturacionSuperficie> HistoricoFacturacionSuperficie { get; set; }
    }
}
