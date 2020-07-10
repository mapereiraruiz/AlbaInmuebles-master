using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CFAInmuebles.Domain.Models
{
    [Table("TipoArticulo")]
    public partial class TipoArticulos
    {
        public TipoArticulos()
        {
            Articulos = new HashSet<Articulos>();
            HistoricoFacturacionSuperficie = new HashSet<HistoricoFacturacionSuperficie>();
            HistoricoArticulo = new HashSet<HistoricoArticulos>();
            HistoricoInmueble = new HashSet<HistoricoInmuebles>();
            Inmuebles = new HashSet<Inmuebles>();
        }

        public override string ToString()
        {
            return Tipoarticulo;
        }

        [Key]
        public int IdTipoArticulo { get; set; }
        [Required]
        [Column("tipoarticulo")]
        [StringLength(50)]
        public string Tipoarticulo { get; set; }
        [Column("alquilable")]
        public bool Alquilable { get; set; }
        [Column("coeficienteHomogeneizacion")]
        public double CoeficienteHomogeneizacion { get; set; }
        public int IdTipoMedida { get; set; }
        public int IdTipoConceptoArticulo { get; set; }
        public int IdClasificacion { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? FechaEliminacion { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime FechaSistema { get; set; }
        public int? IdUsuario { get; set; }

        [ForeignKey(nameof(IdClasificacion))]
        [InverseProperty(nameof(Clasificacion.TipoArticulos))]
        public virtual Clasificacion IdClasificacionNavigation { get; set; }
        [ForeignKey(nameof(IdTipoConceptoArticulo))]
        [InverseProperty(nameof(TipoConceptoArticulo.TipoArticulos))]
        public virtual TipoConceptoArticulo IdTipoConceptoArticuloNavigation { get; set; }
        [ForeignKey(nameof(IdTipoMedida))]
        [InverseProperty(nameof(TipoMedida.TipoArticulos))]
        public virtual TipoMedida IdTipoMedidaNavigation { get; set; }
        [ForeignKey(nameof(IdUsuario))]
        [InverseProperty(nameof(Usuarios.TipoArticulos))]
        public virtual Usuarios IdUsuarioNavigation { get; set; }
        [InverseProperty("IdTipoArticuloNavigation")]
        public virtual ICollection<Articulos> Articulos { get; set; }
        [InverseProperty("IdTipoArticuloNavigation")]
        public virtual ICollection<HistoricoFacturacionSuperficie> HistoricoFacturacionSuperficie { get; set; }
        [InverseProperty("IdTipoArticuloNavigation")]
        public virtual ICollection<HistoricoArticulos> HistoricoArticulo { get; set; }

        [InverseProperty("IdTipoArticuloNavigation")]
        public virtual ICollection<HistoricoInmuebles> HistoricoInmueble { get; set; }

        [InverseProperty("IdTipoArticuloNavigation")]
        public virtual ICollection<Inmuebles> Inmuebles { get; set; }
    }
}
