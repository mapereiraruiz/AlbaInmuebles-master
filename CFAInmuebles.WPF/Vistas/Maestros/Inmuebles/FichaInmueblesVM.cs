using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CFAInmuebles.WPF
{
    public class FichaInmueblesVM : ObservableViewModel, IPageViewModel
    {
        public Inmuebles entity;
        private HomeInmueblesVM baseVM;
        public FichaInmueblesVM(HomeInmueblesVM baseVM, Inmuebles entidad = null)
        {
            entity =  new Inmuebles() ;

            if (entidad != null)
            {
                var properties = typeof(Inmuebles).GetProperties(BindingFlags.Instance | BindingFlags.Public)
               .Where(p => !(p.PropertyType.IsInterface && p.PropertyType.IsGenericType &&
                             p.PropertyType.GetGenericTypeDefinition() == typeof(ICollection<>))
                            && p.CanWrite).ToList();

                properties.ForEach(p =>
                {
                    p.GetValue(entidad);
                    p.SetValue(entity, p.GetValue(entidad));
                });
            }

            this.baseVM = baseVM;
            PageViewModels.Add(new AltaInmueblesVM(this.baseVM, entity));
            PageViewModels.Add(new AltaInmueblesJuridicoVM(this.baseVM, entity));
            PageViewModels.Add(new AltaInmueblesAdministracionVM(this.baseVM, entity));
            PageViewModels.Add(new AltaInmueblesArchivoDigitalVM(this.baseVM, entity));
            PageViewModels.Add(new AltaInmueblesDatosWebVM(this.baseVM, entity));
            PageViewModels.Add(new InmuebleObrasVM(this, entity));
            PageViewModels.Add(new AltaInmuebleObrasVM(this, null));
            PageViewModels.Add(new InmuebleArticulosVM(this.baseVM, entity));
            PageViewModels.Add(new InmuebleTareasVM(this.baseVM, entity));
            PageViewModels.Add(new InmuebleIncidenciasVM(this.baseVM, entity));
            PageViewModels.Add(new InmuebleContratosClientesVM(this.baseVM, entity));
            PageViewModels.Add(new InmuebleContratosProveedoresVM(this.baseVM, entity));
            PageViewModels.Add(new InmuebleReferenciaCatastralVM(this, entity));
            PageViewModels.Add(new AltaInmuebleReferenciaCatastralVM(this, null));
            PageViewModels.Add(new InmuebleLicenciasVM(this, entity));
            PageViewModels.Add(new AltaInmuebleLicenciasVM(this, null));
            PageViewModels.Add(new InmuebleHistoricoSegurosVM(this.baseVM, entity));
            PageViewModels.Add(new InmuebleHistoricoTasacionesVM(this.baseVM, entity));
            PageViewModels.Add(new InmuebleMantenimientoPreventivoVM(this.baseVM, entity));
            PageViewModels.Add(new InmuebleMantenimientoNormativoVM(this.baseVM, entity));
            PageViewModels.Add(new InmuebleCentroCostesVM(this.baseVM, entity));
            PageViewModels.Add(new InmuebleContactosVM(this, entity));
            PageViewModels.Add(new AltaContactoInmuebleVM(this, null));
            PageViewModels.Add(new InmuebleVentasParcialesVM(this, entity));
            PageViewModels.Add(new AltaVentaParcialInmuebleVM(this, null));
            PageViewModels.Add(new InmuebleHistoricoVM(this,  entity));
            CurrentPageViewModel = PageViewModels[0];
        }

        public string Name
        {
            get
            {
                return "Ficha Inmuebles";
            }
        }
        public IPageViewModel AltaInmuebles
        {
            get { return Acceso("Alta Inmuebles", false); }
        }

        public IPageViewModel AltaInmueblesJuridico
        {
            get { return Acceso("Alta Inmuebles Jurídico", false); }
        }

        public IPageViewModel AltaInmueblesAdministracion
        {
            get { return Acceso("Alta Inmuebles Administración", false); }
        }

        public IPageViewModel AltaInmueblesArchivoDigital
        {
            get { return Acceso("Alta Inmuebles Archivo Digital", false); }
        }

        public IPageViewModel AltaInmueblesDatosWeb
        {
            get { return Acceso("Alta Inmuebles Datos Web", false); }
        }

        public IPageViewModel InmuebleObras
        {
            get { return Acceso("Inmueble Obras", false); }
        }

        public IPageViewModel AltaInmuebleObras
        {
            get { return Acceso("Alta Inmueble Obras", false); }
        }

        public IPageViewModel InmuebleArticulos
        {
            get { return Acceso("Inmueble Artículos", false); }
        }

        public IPageViewModel InmuebleTareas
        {
            get { return Acceso("Inmueble Tareas", false); }
        }
        public IPageViewModel InmuebleIncidencias
        {
            get { return Acceso("Inmueble Incidencias", false); }
        }

        public IPageViewModel InmuebleContratosClientes
        {
            get { return Acceso("Inmueble Contratos Clientes", false); }
        }

        public IPageViewModel InmuebleContratosProveedores
        {
            get { return Acceso("Inmueble Contratos Proveedores", false); }
        }

        public IPageViewModel InmuebleReferenciaCatastral
        {
            get { return Acceso("Inmueble Referencia Catastral", false); }
        }

        public IPageViewModel AltaInmuebleReferenciaCatastral
        {
            get { return Acceso("Alta Inmueble Referencia Catastral", false); }
        }

        public IPageViewModel InmuebleLicencias
        {
            get { return Acceso("Inmueble Licencias", false); }
        }

        public IPageViewModel AltaInmuebleLicencias
        {
            get { return Acceso("Alta Inmueble Licencias", false); }
        }

        public IPageViewModel InmuebleHistoricoSeguros
        {
            get { return Acceso("Inmueble Histórico Seguros", false); }
        }

        public IPageViewModel InmuebleHistoricoTasaciones
        {
            get { return Acceso("Inmueble Histórico Tasaciones", false); }
        }

        public IPageViewModel InmuebleMantenimientoPreventivo
        {
            get { return Acceso("Inmueble Mantenimiento Preventivo", false); }
        }

        public IPageViewModel InmuebleMantenimientoNormativo
        {
            get { return Acceso("Inmueble Mantenimiento Normativo", false); }
        }

        public IPageViewModel InmuebleCentroCostes
        {
            get { return Acceso("Inmueble Centro Costes", false); }
        }

        public IPageViewModel InmuebleContactos
        {
            get { return Acceso("Inmueble Contactos", false); }
        }

        public IPageViewModel AltaContactoInmueble
        {
            get { return Acceso("Alta Contacto Inmueble", false); }
        }

        public IPageViewModel InmuebleVentasParciales
        {
            get { return Acceso("Inmueble Ventas Parciales", false); }
        }

        public IPageViewModel AltaInmuebleVentaParcial
        {
            get { return Acceso("Alta Venta Parcial Inmueble", false); }
        }

        public IPageViewModel InmuebleHistorico
        {
            get { return Acceso("Inmueble Histórico Modificaciones", false); }
        }

        public IPageViewModel Acceso(string viewModel, bool AdminRequired)
        {
            if (AdminRequired)
                return PageViewModels.Where(m => m.Name == "Ficha Inmuebles").FirstOrDefault();

            return PageViewModels.Where(m => m.Name == viewModel).FirstOrDefault();
        }

    }
}
