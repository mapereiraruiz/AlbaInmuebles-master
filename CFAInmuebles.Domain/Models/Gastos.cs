using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CFAInmuebles.Domain.Models
{
    [Table("Gasto")]
    public partial class Gastos
    {
        public override string ToString()
        {
            return Gasto;
        }

        [Key]
        public int IdGasto { get; set; }
        [Required]
        [Column("gasto")]
        [StringLength(150)]
        public string Gasto { get; set; }
        public int? IdTipoGasto { get; set; }
        public int? IdEmpresa { get; set; }
        [Column("cuentacontable")]
        [StringLength(10)]
        public string Cuentacontable { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? FechaEliminacion { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime FechaSistema { get; set; }
        [Column("em2")]
        public bool? Em2 { get; set; }
        [Column("grupo")]
        [StringLength(50)]
        public string Grupo { get; set; }
        public int? IdUsuario { get; set; }

        [ForeignKey(nameof(IdEmpresa))]
        [InverseProperty(nameof(Empresas.Gastos))]
        public virtual Empresas IdEmpresaNavigation { get; set; }
        [ForeignKey(nameof(IdTipoGasto))]
        [InverseProperty(nameof(TipoGasto.Gastos))]
        public virtual TipoGasto IdTipoGastoNavigation { get; set; }
        [ForeignKey(nameof(IdUsuario))]
        [InverseProperty(nameof(Usuarios.Gastos))]
        public virtual Usuarios IdUsuarioNavigation { get; set; }
    }
}
