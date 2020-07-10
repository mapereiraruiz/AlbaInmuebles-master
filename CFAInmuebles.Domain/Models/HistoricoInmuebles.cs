using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CFAInmuebles.Domain.Models
{
    [Table("HistoricoInmueble")]
    public partial class HistoricoInmuebles
    {
        public override string ToString()
        {
            return Inmueble;
        }

        [Key]
        public int IdHistoricoInmueble { get; set; }
        public int IdInmueble { get; set; }
        public int IdEmpresa { get; set; }
        public int IdProvincia { get; set; }
        public int IdTipoInmueble { get; set; }
        public int? IdTipoArticulo { get; set; }
        [Required]
        [StringLength(100)]
        public string Inmueble { get; set; }
        [Required]
        [StringLength(100)]
        public string Calle { get; set; }
        [Required]
        [StringLength(100)]
        public string Municipio { get; set; }
        [Column("codigoPostal")]
        [StringLength(5)]
        public string CodigoPostal { get; set; }
        public double SuperficieRegistralS { get; set; }
        public double SuperficieCatastralS { get; set; }
        public double SuperficieAlbaS { get; set; }
        public double SuperficieRegistralB { get; set; }
        public double SuperficieCatastralB { get; set; }
        public double SuperficieAlbaB { get; set; }
        public double SuperficieParcela { get; set; }
        [Column("numPlazaRegistral")]
        public int? NumPlazaRegistral { get; set; }
        [Column("numPlazaCatastral")]
        public int? NumPlazaCatastral { get; set; }
        [Column("numPlazaAlba")]
        public int? NumPlazaAlba { get; set; }
        public int? IdInmuebleCentroCoste { get; set; }
        public int? IdLicencia { get; set; }
        public bool UsoPropio { get; set; }
        [Column("abreviatura")]
        [StringLength(50)]
        public string Abreviatura { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime FechaCompra { get; set; }
        [Column("importeCompra", TypeName = "decimal(30, 2)")]
        public decimal? ImporteCompra { get; set; }
        [Column("historialIncidencia")]
        [StringLength(250)]
        public string HistorialIncidencia { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? FechaVenta { get; set; }
        [Column("importeVenta", TypeName = "decimal(30, 2)")]
        public decimal? ImporteVenta { get; set; }
        
        [Column("valorSuelo", TypeName = "decimal(30, 2)")]
        public decimal? ValorSuelo { get; set; }
        [Column("valorEdificio", TypeName = "decimal(30, 2)")]
        public decimal? ValorEdificio { get; set; }
        [Column("valorSueloAEAT", TypeName = "decimal(30, 2)")]
        public decimal? ValorSueloAeat { get; set; }
        [Column("valorEdificioAEAT", TypeName = "decimal(30, 2)")]
        public decimal? ValorEdificioAeat { get; set; }
        [Column("FechaAEAT", TypeName = "smalldatetime")]
        public DateTime? FechaAeat { get; set; }
        [Column("tasaCapitalizacion")]
        public double? TasaCapitalizacion { get; set; }
        [Column("valorPlazaGaraje", TypeName = "decimal(30, 2)")]
        public decimal? ValorPlazaGaraje { get; set; }
        [Column("archivoDigital")]
        [StringLength(50)]
        public string ArchivoDigital { get; set; }
        [Column("tasaBasuras")]
        public bool TasaBasuras { get; set; }
        [StringLength(100)]
        public string ServiciosConserjeria { get; set; }
        [StringLength(100)]
        public string ServiciosVigilancia24h { get; set; }
        [StringLength(100)]
        public string ServiciosPersonalMantenimiento { get; set; }
        [StringLength(100)]
        public string ServiciosRecepcion { get; set; }
        [StringLength(100)]
        public string ServiciosPaqueteria { get; set; }
        [StringLength(100)]
        public string InstalacionesAireAcondicionado { get; set; }
        [StringLength(100)]
        public string InstalacionesCalefaccion { get; set; }
        [StringLength(100)]
        public string InstalacionesAscensores { get; set; }
        [StringLength(100)]
        public string InstalacionesIncendios { get; set; }
        [StringLength(100)]
        public string InstalacionesControlAccesos { get; set; }
        [StringLength(100)]
        public string InstalacionesCCTV { get; set; }
        [StringLength(100)]
        public string InstalacionesIluminacion { get; set; }
        [StringLength(100)]
        public string InstalacionesPCI { get; set; }
        [StringLength(100)]
        public string InstalacionesScanner { get; set; }
        [StringLength(500)]
        public string GeneralDescripcion { get; set; }
        [StringLength(100)]
        public string GeneralUso { get; set; }
        [StringLength(100)]
        public string GeneralSuperficie { get; set; }
        [StringLength(100)]
        public string GeneralAparcamiento { get; set; }
        [StringLength(100)]
        public string GeneralNumeroPlazas { get; set; }
        [StringLength(100)]
        public string CalidadesFalsosTechos { get; set; }
        [StringLength(100)]
        public string CalidadesSuelosTecnicos { get; set; }

        [StringLength(100)]
        public string CalidadesPavimentos { get; set; }
        [StringLength(100)]
        public string CalidadesFachada { get; set; }
        [StringLength(100)]
        public string CalidadesCarpinterias { get; set; }
        [StringLength(100)]
        public string CalidadesRevestimientos { get; set; }
        [StringLength(100)]
        public string CalidadesAseosAdaptados { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime FechaSistema { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? FechaEliminacion { get; set; }
        public int? IdUsuario { get; set; }
        [StringLength(150)]
        public string ReferenciaCatastralGeneral { get; set; }
        public int? IdConceptoFacturacion { get; set; }

        [ForeignKey(nameof(IdInmueble))]
        [InverseProperty(nameof(Inmuebles.HistoricoInmueble))]
        public virtual Inmuebles IdInmuebleNavigation { get; set; }

        [ForeignKey(nameof(IdUsuario))]
        [InverseProperty(nameof(Usuarios.HistoricoInmuebles))]
        public virtual Usuarios IdUsuarioNavigation { get; set; }

        [ForeignKey(nameof(IdTipoArticulo))]
        [InverseProperty(nameof(TipoArticulos.HistoricoInmueble))]
        public virtual TipoArticulos IdTipoArticuloNavigation { get; set; }

        [ForeignKey(nameof(IdConceptoFacturacion))]
        [InverseProperty(nameof(ConceptoFacturacion.HistoricoInmueble))]
        public virtual ConceptoFacturacion IdConceptoFacturacionNavigation { get; set; }
        [ForeignKey(nameof(IdEmpresa))]
        [InverseProperty(nameof(Empresas.HistoricoInmueble))]
        public virtual Empresas IdEmpresaNavigation { get; set; }
        [ForeignKey(nameof(IdInmuebleCentroCoste))]
        [InverseProperty(nameof(InmuebleCentroCoste.HistoricoInmueble))]
        public virtual InmuebleCentroCoste IdInmuebleCentroCosteNavigation { get; set; }
        [ForeignKey(nameof(IdProvincia))]
        [InverseProperty(nameof(Provincias.HistoricoInmueble))]
        public virtual Provincias IdProvinciaNavigation { get; set; }
        [ForeignKey(nameof(IdTipoInmueble))]
        [InverseProperty(nameof(TipoInmuebles.HistoricoInmueble))]
        public virtual TipoInmuebles IdTipoInmuebleNavigation { get; set; }
    }
}
