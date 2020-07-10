using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CFAInmuebles.Domain.Models
{
    [Table("Provincia")]
    public partial class Provincias
    {
        public Provincias()
        {
            Inmuebles = new HashSet<Inmuebles>();
            HistoricoInmueble = new HashSet<HistoricoInmuebles>();
        }

        public override string ToString()
        {
            return Provincia;
        }

        [Key]
        public int IdProvincia { get; set; }
        [Required]
        [StringLength(100)]
        public string Provincia { get; set; }
        [Column("conveniofianza")]
        public bool? Conveniofianza { get; set; }
        [Column("porcentajeDeposito")]
        public double? PorcentajeDeposito { get; set; }
        [Column("numeroConcierto")]
        [StringLength(10)]
        public string NumeroConcierto { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? FechaEliminacion { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime FechaSistema { get; set; }
        public int? IdUsuario { get; set; }

        [ForeignKey(nameof(IdUsuario))]
        [InverseProperty(nameof(Usuarios.Provincias))]
        public virtual Usuarios IdUsuarioNavigation { get; set; }
       
        [InverseProperty("IdProvinciaNavigation")]
        public virtual ICollection<Inmuebles> Inmuebles { get; set; }

        [InverseProperty("IdProvinciaNavigation")]
        public virtual ICollection<HistoricoInmuebles> HistoricoInmueble { get; set; }
    }
}
