using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CFAInmuebles.Domain.Models
{
    [Table("HistoricoArticulo")]
    public partial class HistoricoArticulos
    {
       

        public override string ToString()
        {
            return Articulo;
        }


        [Key]
        public int IdHistoricoArticulo { get; set; }
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

        [Column(TypeName = "smalldatetime")]
        public DateTime? FechaVenta { get; set; }
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
        public DateTime FechaSistema { get; set; }
        public int? IdUsuario { get; set; }

        [ForeignKey(nameof(IdInmueble))]
        [InverseProperty(nameof(Inmuebles.HistoricoArticulo))]
        public virtual Inmuebles IdInmuebleNavigation { get; set; }
        [ForeignKey(nameof(IdPlanta))]
        [InverseProperty(nameof(TipoPlanta.HistoricoArticulo))]
        public virtual TipoPlanta IdPlantaNavigation { get; set; }
        [ForeignKey(nameof(IdTipoArticulo))]
        [InverseProperty(nameof(TipoArticulos.HistoricoArticulo))]
        public virtual TipoArticulos IdTipoArticuloNavigation { get; set; }
        [ForeignKey(nameof(IdUsuario))]
        [InverseProperty(nameof(Usuarios.HistoricoArticulo))]
        public virtual Usuarios IdUsuarioNavigation { get; set; }
        [ForeignKey(nameof(IdArticulo))]
        [InverseProperty(nameof(Articulos.HistoricoArticulo))]
        public virtual Articulos IdArticuloNavigation { get; set; }
       
    }
}
