using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CFAInmuebles.Domain.Models
{
    public partial class Empleados
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Apellido1 { get; set; }
        public string Apelldo2 { get; set; }
        public int Edad { get; set; }
        public DateTime FechaAlta { get; set; }
        public int DepartamentoId { get; set; }
        [Column("DNI")]
        public string Dni { get; set; }

        [ForeignKey(nameof(DepartamentoId))]
        [InverseProperty(nameof(Departamentos.Empleados))]
        public virtual Departamentos Departamento { get; set; }
    }
}
