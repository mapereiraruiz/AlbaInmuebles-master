using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CFAInmuebles.Domain.Models
{
    public partial class ConceptoFacturacion
    {
        public ConceptoFacturacion()
        {
            ContratosClientes = new HashSet<ContratosClientes>();
            Facturacion = new HashSet<Facturacion>();
            FacturacionImprimir = new HashSet<Facturacion>();
            HistoricoConceptoFacturacion = new HashSet<HistoricoConceptoFacturacion>();
            Inmuebles = new HashSet<Inmuebles>();
            HistoricoInmueble = new HashSet<HistoricoInmuebles>();
            HistoricoContratosClientes = new HashSet<HistoricoContratosClientes>();
        }

        public override string ToString()
        {
            return Conceptofacturacion1;
        }


        [Key]
        public int IdConceptoFacturacion { get; set; }
        [Column("IdTipoIVA")]
        public int IdTipoIva { get; set; }
        [Required]
        [Column("conceptofacturacion")]
        [StringLength(200)]
        public string Conceptofacturacion1 { get; set; }
        public int IdTipoConcepto { get; set; }
        [Required]
        [StringLength(10)]
        public string CuentaContable { get; set; }
        [Column("revisableIPC")]
        public bool RevisableIpc { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? FechaEliminacion { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime FechaSistema { get; set; }
        public int? IdConceptoFacturacionPadre { get; set; }
        public int? IdEmpresa { get; set; }
        public int? IdCatalogarConcepto { get; set; }
        [Column("cuentaGasto")]
        [StringLength(50)]
        public string CuentaGasto { get; set; }
        public int? IdUsuario { get; set; }

        [ForeignKey(nameof(IdCatalogarConcepto))]
        [InverseProperty(nameof(CatalogarConcepto.ConceptoFacturacion))]
        public virtual CatalogarConcepto IdCatalogarConceptoNavigation { get; set; }
        [ForeignKey(nameof(IdConceptoFacturacionPadre))]
        [InverseProperty(nameof(ConceptoFacturacionPadre.ConceptoFacturacion))]
        public virtual ConceptoFacturacionPadre IdConceptoFacturacionPadreNavigation { get; set; }
        [ForeignKey(nameof(IdTipoConcepto))]
        [InverseProperty(nameof(TipoConcepto.ConceptoFacturacion))]
        public virtual TipoConcepto IdTipoConceptoNavigation { get; set; }
        [ForeignKey(nameof(IdTipoIva))]
        [InverseProperty(nameof(TipoIva.ConceptoFacturacion))]
        public virtual TipoIva IdTipoIvaNavigation { get; set; }
        [ForeignKey(nameof(IdUsuario))]
        [InverseProperty(nameof(Usuarios.ConceptoFacturacion))]
        public virtual Usuarios IdUsuarioNavigation { get; set; }
        [InverseProperty("IdConceptoFacturacionNavigation")]
        public virtual ICollection<ContratosClientes> ContratosClientes { get; set; }
        [InverseProperty("IdConceptoFacturacionNavigation")]
        public virtual ICollection<Facturacion> Facturacion { get; set; }

        [InverseProperty("IdConceptoFacturacionImprimirNavigation")]
        public virtual ICollection<Facturacion> FacturacionImprimir { get; set; }

        [InverseProperty("IdConceptoFacturacionNavigation")]
        public virtual ICollection<HistoricoConceptoFacturacion> HistoricoConceptoFacturacion { get; set; }
        [InverseProperty("IdConceptoFacturacionNavigation")]
        public virtual ICollection<Inmuebles> Inmuebles { get; set; }

        [ForeignKey(nameof(IdEmpresa))]
        [InverseProperty(nameof(Empresas.ConceptoFacturacion))]
        public virtual Empresas IdEmpresaNavigation { get; set; }

        [InverseProperty("IdConceptoFacturacionNavigation")]
        public virtual ICollection<HistoricoInmuebles> HistoricoInmueble { get; set; }

        [InverseProperty("IdConceptoFacturacionNavigation")]
        public virtual ICollection<HistoricoContratosClientes> HistoricoContratosClientes { get; set; }
    }
}
