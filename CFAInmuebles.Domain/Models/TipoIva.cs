using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CFAInmuebles.Domain.Models
{
    [Table("TipoIVA")]
    public partial class TipoIva
    {
        public TipoIva()
        {
            ConceptoFacturacion = new HashSet<ConceptoFacturacion>();
        }

        public override string ToString()
        {
            return TipoIva1;
        }

        [Key]
        [Column("IdTipoIVA")]
        public int IdTipoIva { get; set; }
        [Column("tipoIVA")]
        [StringLength(50)]
        [Required]
        public string TipoIva1 { get; set; }
        [Column("cuentaContable")]
        [StringLength(10)]
        public string CuentaContable { get; set; }
        [Column("IVA")]
        public int? Iva { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? FechaEliminacion { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime FechaSistema { get; set; }
        public int? IdUsuario { get; set; }
        public int? IdEmpresa { get; set; }

        [ForeignKey(nameof(IdEmpresa))]
        [InverseProperty(nameof(Empresas.TipoIva))]
        public virtual Empresas IdEmpresaNavigation { get; set; }
        [ForeignKey(nameof(IdUsuario))]
        [InverseProperty(nameof(Usuarios.TipoIva))]
        public virtual Usuarios IdUsuarioNavigation { get; set; }
        [InverseProperty("IdTipoIvaNavigation")]
        public virtual ICollection<ConceptoFacturacion> ConceptoFacturacion { get; set; }
    }
}
