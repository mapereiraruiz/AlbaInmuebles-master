using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CFAInmuebles.Domain.Models
{
    public partial class Terceros
    {
        public Terceros()
        {

        }

        public override string ToString()
        {
            return Nombre;
        }


        [Key]
 
        [StringLength(16)]
        public string Cuenta { get; set; }

        [StringLength(16)]
        public string Codigo { get; set; }

        [StringLength(20)]
        public string NIF { get; set; }

        [StringLength(255)]
        public string Nombre { get; set; }

        [StringLength(2)]
        public string Sigla { get; set; }

        [StringLength(255)]
        public string Domicilio { get; set; }

        public int? Numero { get; set; }

        [StringLength(10)]
        public string Escalera { get; set; }

        public int? Piso { get; set; }

        [StringLength(10)]
        public string Puerta { get; set; }

        [StringLength(40)]
        public string Poblacion{ get; set; }

        [StringLength(20)]
        public string Provincia { get; set; }

        [StringLength(10)]
        public string CodigoPostal { get; set; }

        [NotMapped]
        public string Direccion
        {
            get
            {
                return Sigla + " "+  Domicilio + " " + Numero.ToString();
            }
        }

    }
}
