using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CFAInmuebles.Domain.Models
{
    [Table("HistoricoEmpresa")]
    public partial class HistoricoEmpresas
    {
        public override string ToString()
        {
            return Empresa;
        }

        [Key]
        public int IdHistoricoEmpresa { get; set; }
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
        [StringLength(50)]
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

        [ForeignKey(nameof(IdEmpresa))]
        [InverseProperty(nameof(Empresas.HistoricoEmpresa))]
        public virtual Empresas IdEmpresaNavigation { get; set; }

        [ForeignKey(nameof(IdUsuario))]
        [InverseProperty(nameof(Usuarios.HistoricoEmpresas))]
        public virtual Usuarios IdUsuarioNavigation { get; set; }
        
        [ForeignKey(nameof(IdSwift))]
        [InverseProperty(nameof(Swift.HistoricoEmpresa))]
        public virtual Swift IdSwiftNavigation { get; set; }
    }
}
