using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CFAInmuebles.Domain.Models
{
    public partial class Licencias
    {
        public override string ToString()
        {
            return Descripcion;
        }

        [Key]
        public int IdLicencia { get; set; }
        public int IdTipoLicencia { get; set; }
        [Required]
        [StringLength(250)]
        public string NumeroExpediente { get; set; }
        public int IdFichero { get; set; }
        public int IdTipoFichero { get; set; }
        [Required]
        public string Descripcion { get; set; }
        [StringLength(250)]
        public string Organismo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? FechaConcesion { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? FechaSolicitud { get; set; }
        [StringLength(250)]
        public string ArchivoDigital { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? FechaEliminacion { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime FechaSistema { get; set; }
        public int? IdUsuario { get; set; }

        [ForeignKey(nameof(IdTipoFichero))]
        [InverseProperty(nameof(TipoFichero.Licencias))]
        public virtual TipoFichero IdTipoFicheroNavigation { get; set; }


        [ForeignKey(nameof(IdTipoLicencia))]
        [InverseProperty(nameof(TipoLicencia.Licencias))]
        public virtual TipoLicencia IdTipoLicenciaNavigation { get; set; }


        [ForeignKey(nameof(IdUsuario))]
        [InverseProperty(nameof(Usuarios.Licencias))]
        public virtual Usuarios IdUsuarioNavigation { get; set; }

        [NotMapped]
        public string TipoLicenciaDescripcion { get; set; }
    }
}
