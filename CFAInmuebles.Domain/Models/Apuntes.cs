using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CFAInmuebles.Domain.Models
{
    public partial class Apuntes
    {
        public Apuntes()
        {

        }

        public override string ToString()
        {
            return Codigo.ToString();
        }


        [Key]
 
        public int Codigo { get; set; }

        public string ConceptoTexto { get; set; }

        public int? Asiento { get; set; }
        public short? Apunte { get; set; }

        [StringLength(16)]
        public string Subcuenta{ get; set; }

        [StringLength(16)]
        public string Contrapartida { get; set; }

        public decimal? EDebe { get; set; }

        public decimal? EHaber { get; set; }

        public DateTime? Fecha { get; set; }



    }
}
