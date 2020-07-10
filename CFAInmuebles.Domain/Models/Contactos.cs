using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CFAInmuebles.Domain.Models
{
    [Table("Contacto")]
    public partial class Contactos
    {

        public override string ToString()
        {
            return Contacto;
        }


        [Key]
        public int IdContacto { get; set; }
        [Column("NIF")]
        [StringLength(20)]
        public string Nif { get; set; }
        public int IdFichero { get; set; }
        public int IdTipoFichero { get; set; }
        [Required]
        [StringLength(150)]
        public string Contacto { get; set; }
        [StringLength(13)]
        public string Telefono1 { get; set; }
        [StringLength(13)]
        public string Telefono2 { get; set; }
        [StringLength(13)]
        public string Telefono3 { get; set; }
        [StringLength(13)]
        public string Fax { get; set; }
        [StringLength(50)]
        public string Mail { get; set; }
        [Column("mailing01")]
        public bool? Mailing1 { get; set; }
        [Column("mailing02")]
        public bool? Mailing2 { get; set; }
        [Column("mailing03")]
        public bool? Mailing3 { get; set; }
        [Column("mailing04")]
        public bool? Mailing4 { get; set; }
        [Column("mailing05")]
        public bool? Mailing5 { get; set; }
        [Column("mailing06")]
        public bool? Mailing6 { get; set; }
        [Column("mailing07")]
        public bool? Mailing7 { get; set; }
        [Column("mailing08")]
        public bool? Mailing8 { get; set; }
        [Column("mailing09")]
        public bool? Mailing9 { get; set; }
        [Column("mailing10")]
        public bool? Mailing10 { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? FechaEliminacion { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime FechaSistema { get; set; }
        public int? IdUsuario { get; set; }

        [ForeignKey(nameof(IdUsuario))]
        [InverseProperty(nameof(Usuarios.Contactos))]
        public virtual Usuarios IdUsuarioNavigation { get; set; }

        [ForeignKey(nameof(IdTipoFichero))]
        [InverseProperty(nameof(TipoFichero.Contactos))]
        public virtual TipoFichero IdTipoFicheroNavigation { get; set; }

        [NotMapped]
        public string TipoFicheroDescripcion { get; set; }

    }
}
