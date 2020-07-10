using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CFAInmuebles.Domain.Models
{
    public partial class Usuarios
    {
        public override string ToString()
        {
            return NombreUsuario;
        }

        public Usuarios()
        {
            Articulos = new HashSet<Articulos>();
            ConceptoFacturacion = new HashSet<ConceptoFacturacion>();
            Contactos = new HashSet<Contactos>();
            ContratosClientes = new HashSet<ContratosClientes>();
            ContratosProveedores = new HashSet<ContratosProveedores>();
            Empresas = new HashSet<Empresas>();
            Facturacion = new HashSet<Facturacion>();
            FormasDePago = new HashSet<FormasDePago>();
            Gastos = new HashSet<Gastos>();
            HistorialObra = new HashSet<HistorialObra>();
            HistoricoSeguros = new HashSet<HistoricoSeguros>();
            HistoricoTasaciones = new HashSet<HistoricoTasaciones>();
            Incidencias = new HashSet<Incidencias>();
            Inmuebles = new HashSet<Inmuebles>();
            Licencias = new HashSet<Licencias>();
            Mantenimientos = new HashSet<Mantenimientos>();
            Normas = new HashSet<Normas>();
            Provincias = new HashSet<Provincias>();
            ReferenciasCatastrales = new HashSet<ReferenciasCatastrales>();
            Swift = new HashSet<Swift>();
            TareaPeriodica = new HashSet<TareaPeriodica>();
            TareaPeriodicaAsignada = new HashSet<TareaPeriodica>();
            TareaPeriodicaResponsable = new HashSet<TareaPeriodica>();
            Tareas = new HashSet<Tareas>();
            TipoArticulos = new HashSet<TipoArticulos>();
            TipoInmuebles = new HashSet<TipoInmuebles>();
            TipoIpc = new HashSet<TipoIpc>();
            TipoIva = new HashSet<TipoIva>();
            TipoLicencia = new HashSet<TipoLicencia>();
            TipoInstalacion = new HashSet<TipoInstalacion>();
            HistorialOcupacion = new HashSet<HistorialOcupacion>();
            HistoricoEmpresas = new HashSet<HistoricoEmpresas>();
            HistoricoInmuebles = new HashSet<HistoricoInmuebles>();
            HistoricoContratosClientes = new HashSet<HistoricoContratosClientes>();
            HistoricoContratosProveedores = new HashSet<HistoricoContratosProveedores>();
            HistoricoArticulo = new HashSet<HistoricoArticulos>();
        }


        [Key]
        public int IdUsuario { get; set; }
        [Required]
        [StringLength(100)]
        public string NombreUsuario { get; set; }
        [StringLength(100)]
        public string Email { get; set; }
        public bool Administrador { get; set; }
        public int? PermisosBloque1 { get; set; }
        public int? PermisosBloque2 { get; set; }
        public int? PermisosBloque3 { get; set; }
        public int? PermisosBloque4 { get; set; }
        public int? PermisosBloque5 { get; set; }
        public int? PermisosBloque6 { get; set; }
        public int? PermisosBloque7 { get; set; }
        public int? PermisosBloque8 { get; set; }
        public int? PermisosBloque9 { get; set; }
        public int? PermisosBloque { get; set; }

        [InverseProperty("IdUsuarioNavigation")]
        public virtual ICollection<Articulos> Articulos { get; set; }
       
        [InverseProperty("IdUsuarioNavigation")]
        public virtual ICollection<ConceptoFacturacion> ConceptoFacturacion { get; set; }
        [InverseProperty("IdUsuarioNavigation")]
        public virtual ICollection<Contactos> Contactos { get; set; }
        [InverseProperty("IdUsuarioNavigation")]
        public virtual ICollection<ContratosClientes> ContratosClientes { get; set; }
        [InverseProperty("IdUsuarioNavigation")]
        public virtual ICollection<ContratosProveedores> ContratosProveedores { get; set; }
        [InverseProperty("IdUsuarioNavigation")]
        public virtual ICollection<Empresas> Empresas { get; set; }
        [InverseProperty("IdUsuarioNavigation")]
        public virtual ICollection<Facturacion> Facturacion { get; set; }
        [InverseProperty("IdUsuarioNavigation")]
        public virtual ICollection<FormasDePago> FormasDePago { get; set; }
        [InverseProperty("IdUsuarioNavigation")]
        public virtual ICollection<Gastos> Gastos { get; set; }
        [InverseProperty("IdUsuarioNavigation")]
        public virtual ICollection<HistorialObra> HistorialObra { get; set; }
        [InverseProperty("IdUsuarioNavigation")]
        public virtual ICollection<HistoricoSeguros> HistoricoSeguros { get; set; }
        [InverseProperty("IdUsuarioNavigation")]
        public virtual ICollection<HistoricoTasaciones> HistoricoTasaciones { get; set; }
        [InverseProperty("IdUsuarioNavigation")]
        public virtual ICollection<Incidencias> Incidencias { get; set; }
        [InverseProperty("IdUsuarioNavigation")]
        public virtual ICollection<Inmuebles> Inmuebles { get; set; }
        [InverseProperty("IdUsuarioNavigation")]
        public virtual ICollection<Licencias> Licencias { get; set; }
        [InverseProperty("IdUsuarioNavigation")]
        public virtual ICollection<Mantenimientos> Mantenimientos { get; set; }
        [InverseProperty("IdUsuarioNavigation")]
        public virtual ICollection<Normas> Normas { get; set; }
        
        [InverseProperty("IdUsuarioNavigation")]
        public virtual ICollection<Provincias> Provincias { get; set; }
        [InverseProperty("IdUsuarioNavigation")]
        public virtual ICollection<ReferenciasCatastrales> ReferenciasCatastrales { get; set; }
        [InverseProperty("IdUsuarioNavigation")]
        public virtual ICollection<Swift> Swift { get; set; }
        [InverseProperty("IdUsuarioNavigation")]
        public virtual ICollection<TareaPeriodica> TareaPeriodica { get; set; }
        [InverseProperty("IdUsuarioNavigation")]
        public virtual ICollection<Tareas> Tareas { get; set; }
        [InverseProperty("IdUsuarioNavigation")]
        public virtual ICollection<TipoArticulos> TipoArticulos { get; set; }
        [InverseProperty("IdUsuarioNavigation")]
        public virtual ICollection<TipoInmuebles> TipoInmuebles { get; set; }
        [InverseProperty("IdUsuarioNavigation")]
        public virtual ICollection<TipoIpc> TipoIpc { get; set; }
        [InverseProperty("IdUsuarioNavigation")]

        public virtual ICollection<TipoLicencia> TipoLicencia { get; set; }
        [InverseProperty("IdUsuarioNavigation")]
        public virtual ICollection<TipoInstalacion> TipoInstalacion { get; set; }
        [InverseProperty("IdUsuarioNavigation")]

        public virtual ICollection<TipoIva> TipoIva { get; set; }

        [InverseProperty("IdUsuarioNavigation")]
        public virtual ICollection<HistorialOcupacion> HistorialOcupacion { get; set; }

        [InverseProperty("IdUsuarioAsignadoNavigation")]
        public virtual ICollection<TareaPeriodica> TareaPeriodicaAsignada { get; set; }

        [InverseProperty("IdUsuarioResponsableNavigation")]
        public virtual ICollection<TareaPeriodica> TareaPeriodicaResponsable { get; set; }

        [InverseProperty("IdUsuarioNavigation")]
        public virtual ICollection<HistoricoEmpresas> HistoricoEmpresas { get; set; }

        [InverseProperty("IdUsuarioNavigation")]
        public virtual ICollection<HistoricoInmuebles> HistoricoInmuebles { get; set; }

        [InverseProperty("IdUsuarioNavigation")]
        public virtual ICollection<HistoricoContratosClientes> HistoricoContratosClientes { get; set; }
       
        [InverseProperty("IdUsuarioNavigation")]
        public virtual ICollection<HistoricoContratosProveedores> HistoricoContratosProveedores { get; set; }

        [InverseProperty("IdUsuarioNavigation")]
        public virtual ICollection<HistoricoArticulos> HistoricoArticulo { get; set; }

    }
}
