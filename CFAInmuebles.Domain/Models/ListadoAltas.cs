using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CFAInmuebles.Domain.Models
{
    public partial class ListadoAltas
    {
        
    
        public string Inmueble { get; set; }
        public string NumContrato { get; set; }
        
        public string NombreCliente { get; set; }
        public string Articulo { get; set; }
        public DateTime FechaAlta { get; set; }
        
    }
}
