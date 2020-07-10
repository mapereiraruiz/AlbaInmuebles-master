using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CFAInmuebles.Domain.Models
{
    [Table("Empresa")]
    public partial class Empresas
    {
        public Empresas()
        {
          
            Gastos = new HashSet<Gastos>();
            HistoricoFacturacion = new HashSet<HistoricoFacturacion>();
            HistoricoSeguros = new HashSet<HistoricoSeguros>();
            HistoricoTasaciones = new HashSet<HistoricoTasaciones>();
            Inmuebles = new HashSet<Inmuebles>();          
            TipoIva = new HashSet<TipoIva>();
            ConceptoFacturacion = new HashSet<ConceptoFacturacion>();
            HistoricoEmpresa = new HashSet<HistoricoEmpresas>();
            HistoricoInmueble = new HashSet<HistoricoInmuebles>();
            ContratosClientes = new HashSet<ContratosClientes>();
        }

        public override string ToString()
        {
            return Empresa;
        }

        [Key]
        public int IdEmpresa { get; set; }
        [Required]
        [StringLength(100)]
        public string Empresa { get; set; }
        [Required]
        [StringLength(9)]
        public string Nif { get; set; }
        [Required]
        [StringLength(250)]
        public string Direccion { get; set; }
        [Required]
        [StringLength(20)]
        public string CuentaBancaria { get; set; }
        [StringLength(10)]
        public string CuentaContable { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? FechaRenovacionSeguro { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? FechaRenovacionResponsabilidadCivil { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? FechaTasacionExterna { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? FechaUltimaFacturacionMensual { get; set; }
        [StringLength(50)]
        public string PolizaSeguro { get; set; }
        public int EjercicioFacturacion { get; set; }
        [Column("CodigoSEPA")]
        [StringLength(50)]
        public string CodigoSepa { get; set; }
        public int? IdSwift { get; set; }
        [Column("FechaIAE", TypeName = "smalldatetime")]
        public DateTime? FechaIae { get; set; }
        [Column("ImporteIAE", TypeName = "decimal(30, 2)")]
        public decimal? ImporteIae { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime FechaSistema { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? FechaEliminacion { get; set; }
        public int? IdUsuario { get; set; }
        [Column(TypeName = "image")]
        public byte[] LogoEmpresa { get; set; }
        [StringLength(500)]
        public string PieFactura { get; set; }
        [StringLength(500)]
        public string LateralFactura { get; set; }

        [StringLength(3)]
        public string EmpresaALTAI { get; set; }

        [ForeignKey(nameof(IdUsuario))]
        [InverseProperty(nameof(Usuarios.Empresas))]
        public virtual Usuarios IdUsuarioNavigation { get; set; }

        [ForeignKey(nameof(IdSwift))]
        [InverseProperty(nameof(Swift.Empresas))]
        public virtual Swift IdSwiftNavigation { get; set; }


        public virtual ICollection<Gastos> Gastos { get; set; }
        [InverseProperty("IdEmpresaNavigation")]
        public virtual ICollection<HistoricoFacturacion> HistoricoFacturacion { get; set; }
        [InverseProperty("IdEmpresaNavigation")]
        public virtual ICollection<HistoricoSeguros> HistoricoSeguros { get; set; }
        [InverseProperty("IdEmpresaNavigation")]
        public virtual ICollection<HistoricoTasaciones> HistoricoTasaciones { get; set; }
        [InverseProperty("IdEmpresaNavigation")]
        public virtual ICollection<Inmuebles> Inmuebles { get; set; }

        [InverseProperty("IdEmpresaNavigation")]
        public virtual ICollection<TipoIva> TipoIva { get; set; }

        [InverseProperty("IdEmpresaNavigation")]
        public virtual ICollection<ConceptoFacturacion> ConceptoFacturacion { get; set; }

        [InverseProperty("IdEmpresaNavigation")]
        public virtual ICollection<HistoricoEmpresas> HistoricoEmpresa { get; set; }

        [InverseProperty("IdEmpresaNavigation")]
        public virtual ICollection<HistoricoInmuebles> HistoricoInmueble { get; set; }

        [InverseProperty("IdEmpresaNavigation")]
        public virtual ICollection<ContratosClientes> ContratosClientes { get; set; }
    }
}
