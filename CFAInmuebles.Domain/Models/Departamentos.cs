using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CFAInmuebles.Domain.Models
{
    public partial class Departamentos
    {
        public Departamentos()
        {
            Empleados = new HashSet<Empleados>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(250)]
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        [InverseProperty("Departamento")]
        public virtual ICollection<Empleados> Empleados { get; set; }
    }
}
