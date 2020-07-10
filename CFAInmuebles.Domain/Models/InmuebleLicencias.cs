using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CFAInmuebles.Domain.Models
{
    public partial class InmuebleLicencias
    {
        public InmuebleLicencias()
        {
            Inmuebles = new HashSet<Inmuebles>();
        }

        [Key]
        public int IdInmuebleLicencia { get; set; }
        public int IdTipoLicencia { get; set; }
        [Required]
        [StringLength(250)]
        public string NumExpediente { get; set; }
        public int IdCliente { get; set; }
        [Required]
        public string Descripcion { get; set; }
        [Required]
        [StringLength(250)]
        public string TipoLicencia { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Fecha { get; set; }
        [Required]
        [Column("PDF")]
        [StringLength(250)]
        public string Pdf { get; set; }

        [ForeignKey(nameof(IdTipoLicencia))]
        [InverseProperty("InmuebleLicencias")]
        public virtual TipoLicencia IdTipoLicenciaNavigation { get; set; }
        [InverseProperty("IdInmuebleLicenciaNavigation")]
        public virtual ICollection<Inmuebles> Inmuebles { get; set; }
    }
}
