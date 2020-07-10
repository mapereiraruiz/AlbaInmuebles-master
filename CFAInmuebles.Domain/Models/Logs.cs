using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CFAInmuebles.Domain.Models
{
    public partial class Logs
    {
        public Logs()
        {
            
        }
        public override string ToString()
        {
            return Descripcion;
        }


        [Key]
        public int IdLog { get; set; }

        [StringLength(100)]
        public string Bloque { get; set; }

        [StringLength(100)]
        public string Mantenimiento { get; set; }

        [StringLength(100)]
        public string Entidad { get; set; }

        [StringLength(100)]
        public string Accion { get; set; }

        [StringLength(100)]
        public string Usuario { get; set; }

        [StringLength(500)]
        public string Descripcion { get; set; }

        public DateTime? Fecha { get; set; }

    }
}

