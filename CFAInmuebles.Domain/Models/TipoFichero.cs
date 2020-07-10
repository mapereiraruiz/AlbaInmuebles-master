using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CFAInmuebles.Domain.Models
{
    public partial class TipoFichero
    {
        public TipoFichero()
        {
            HistorialObra = new HashSet<HistorialObra>();
            Incidencias = new HashSet<Incidencias>();
            Licencias = new HashSet<Licencias>();
            Mantenimientos = new HashSet<Mantenimientos>();
            ObrasFichero = new HashSet<ObrasFichero>();
            TareaPeriodica = new HashSet<TareaPeriodica>();
            Tareas = new HashSet<Tareas>();
            Contactos = new HashSet<Contactos>();
        }

        public override string ToString()
        {
            return Valor;
        }

        [Key]
        public int IdTipoFichero { get; set; }
        [StringLength(150)]
        public string Valor { get; set; }

        [InverseProperty("IdTipoFicheroNavigation")]
        public virtual ICollection<HistorialObra> HistorialObra { get; set; }
        [InverseProperty("IdTipoFicheroNavigation")]
        public virtual ICollection<Incidencias> Incidencias { get; set; }
        [InverseProperty("IdTipoFicheroNavigation")]
        public virtual ICollection<Licencias> Licencias { get; set; }
        [InverseProperty("IdTipoFicheroNavigation")]
        public virtual ICollection<Mantenimientos> Mantenimientos { get; set; }
        [InverseProperty("IdTipoFicheroNavigation")]
        public virtual ICollection<ObrasFichero> ObrasFichero { get; set; }
        [InverseProperty("IdTipoFicheroNavigation")]
        public virtual ICollection<TareaPeriodica> TareaPeriodica { get; set; }
        [InverseProperty("IdTipoFicheroNavigation")]
        //public virtual ICollection<Contactos> Contactos { get; set; }
        //[InverseProperty("IdTipoFicheroNavigation")]
        public virtual ICollection<Tareas> Tareas { get; set; }

        [InverseProperty("IdTipoFicheroNavigation")]
        public virtual ICollection<Contactos> Contactos { get; set; }
    }
}
