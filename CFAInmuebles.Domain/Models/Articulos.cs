using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CFAInmuebles.Domain.Models
{
    [Table("Articulo")]
    public partial class Articulos
    {
        public Articulos()
        {
            SuperficieTasacion = new HashSet<SuperficieTasacion>();
            HistorialOcupacion = new HashSet<HistorialOcupacion>();
            HistoricoArticulo = new HashSet<HistoricoArticulos>();
        }

        public override string ToString()
        {
            return Articulo;
        }


        [Key]
        public int IdArticulo { get; set; }
        public int IdInmueble { get; set; }
        public int IdTipoArticulo { get; set; }
        [Required]
        [StringLength(150)]
        public string Articulo { get; set; }
        public int IdPlanta { get; set; }
        public double? NumUnidad { get; set; }
        [StringLength(250)]
        public string HistorialObra { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? FechaBaja { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? FechaAlta { get; set; }
        public bool Alquilado { get; set; }
        [StringLength(500)]
        public string Planos { get; set; }
        [StringLength(50)]
        public string Estado { get; set; }
        [Column(TypeName = "decimal(30, 2)")]
        public decimal? ValorSuelo { get; set; }
        [Column(TypeName = "decimal(30, 2)")]
        public decimal? ValorEdificio { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? FechaEliminacion { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? FechaVenta { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime FechaSistema { get; set; }
        public int? IdUsuario { get; set; }

        [ForeignKey(nameof(IdInmueble))]
        [InverseProperty(nameof(Inmuebles.Articulos))]
        public virtual Inmuebles IdInmuebleNavigation { get; set; }
        [ForeignKey(nameof(IdPlanta))]
        [InverseProperty(nameof(TipoPlanta.Articulos))]
        public virtual TipoPlanta IdPlantaNavigation { get; set; }
        [ForeignKey(nameof(IdTipoArticulo))]
        [InverseProperty(nameof(TipoArticulos.Articulos))]
        public virtual TipoArticulos IdTipoArticuloNavigation { get; set; }
        [ForeignKey(nameof(IdUsuario))]
        [InverseProperty(nameof(Usuarios.Articulos))]
        public virtual Usuarios IdUsuarioNavigation { get; set; }
        [InverseProperty("IdArticuloNavigation")]

        public virtual ICollection<SuperficieTasacion> SuperficieTasacion { get; set; }

        [InverseProperty("IdArticuloNavigation")]
        public virtual ICollection<HistorialOcupacion> HistorialOcupacion { get; set; }

        [InverseProperty("IdArticuloNavigation")]
        public virtual ICollection<HistoricoArticulos> HistoricoArticulo { get; set; }
    }
}
