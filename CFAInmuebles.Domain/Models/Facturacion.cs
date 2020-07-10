using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CFAInmuebles.Domain.Models
{
    public partial class Facturacion
    {
        public override string ToString()
        {
            return IdContratoClienteNavigation?.NumeroContrato.ToString() ;
        }

        [Key]
        public int IdFacturacion { get; set; }
        public int IdContratoCliente { get; set; }
        public int IdConceptoFacturacion { get; set; }
        public int IdConceptoFacturacionImprimir { get; set; }
        [Column(TypeName = "decimal(30, 2)")]
        public decimal? Importe { get; set; }
        [StringLength(10)]
        public string CodigoAgrupacion { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? Fecha { get; set; }
        public int?  IdModalidadFactura { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? FechaEliminacion { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime FechaSistema { get; set; }
        public int? IdUsuario { get; set; }

        [ForeignKey(nameof(IdConceptoFacturacion))]
        [InverseProperty(nameof(ConceptoFacturacion.Facturacion))]
        public virtual ConceptoFacturacion IdConceptoFacturacionNavigation { get; set; }
        [ForeignKey(nameof(IdContratoCliente))]
        [InverseProperty(nameof(ContratosClientes.Facturacion))]
        public virtual ContratosClientes IdContratoClienteNavigation { get; set; }
        [ForeignKey(nameof(IdUsuario))]
        [InverseProperty(nameof(Usuarios.Facturacion))]
        public virtual Usuarios IdUsuarioNavigation { get; set; }

        [ForeignKey(nameof(IdModalidadFactura))]
        [InverseProperty(nameof(ModalidadFactura.Facturacion))]
        public virtual ModalidadFactura IdModalidadFacturaNavigation { get; set; }

        [ForeignKey(nameof(IdConceptoFacturacionImprimir))]
        [InverseProperty(nameof(ConceptoFacturacion.FacturacionImprimir))]
        public virtual ConceptoFacturacion IdConceptoFacturacionImprimirNavigation { get; set; }
    }
}
