using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CFAInmuebles.WPF
{
    public class FichaIncidenciasVM : ObservableViewModel, IPageViewModel
    {
        public Incidencias entity;
        private HomeIncidenciasVM baseVM;

        public FichaIncidenciasVM(HomeIncidenciasVM baseVM, Incidencias entity = null)
        {
            this.entity = entity ?? new Incidencias();
            this.baseVM = baseVM;

            PageViewModels.Add(new AltaIncidenciasVM(baseVM, this.entity));
            PageViewModels.Add(new IncidenciaInmueblesVM(baseVM, this.entity));
            PageViewModels.Add(new IncidenciaEmpresasVM(baseVM, this.entity));
            PageViewModels.Add(new IncidenciaContratosClientesVM(this.baseVM, this.entity));
            PageViewModels.Add(new IncidenciaContratosProveedoresVM(this.baseVM, this.entity));
            PageViewModels.Add(new IncidenciaArticulosVM(this.baseVM, this.entity));
            PageViewModels.Add(new IncidenciaObrasVM(this.baseVM, this.entity));
            PageViewModels.Add(new IncidenciaTareasVM(this.baseVM, this.entity));
            PageViewModels.Add(new IncidenciaMantenimientoPreventivoVM(this.baseVM, this.entity));
            PageViewModels.Add(new IncidenciaMantenimientoNormativoVM(this.baseVM, this.entity));

            CurrentPageViewModel = PageViewModels[0];
        }

        public string Name
        {
            get
            {
                return "Ficha Incidencias";
            }
        }

        public IPageViewModel AltaIncidencias
        {
            get { return Acceso("Alta Incidencias", false); }
        }

        public IPageViewModel IncidenciaInmuebles
        {
            get { return Acceso("Incidencia Inmuebles", false); }
        }

        public IPageViewModel IncidenciaEmpresas
        {
            get { return Acceso("Incidencia Empresas", false); }
        }

        public IPageViewModel IncidenciaContratosClientes
        {
            get { return Acceso("Incidencia Contratos Clientes", false); }
        }


        public IPageViewModel IncidenciaContratosProveedores
        {
            get { return Acceso("Incidencia Contratos Proveedores", false); }
        }

        public IPageViewModel IncidenciaArticulos
        {
            get { return Acceso("Incidencia Artículos", false); }
        }

        public IPageViewModel IncidenciaObras
        {
            get { return Acceso("Incidencia Obras", false); }
        }

        public IPageViewModel IncidenciaTareas
        {
            get { return Acceso("Incidencia Tareas", false); }
        }


        public IPageViewModel IncidenciaMantenimientoPreventivo
        {
            get { return Acceso("Incidencia Mantenimiento Preventivo", false); }
        }

        public IPageViewModel IncidenciaMantenimientoNormativo
        {
            get { return Acceso("Incidencia Mantenimiento Normativo", false); }
        }



        public IPageViewModel Acceso(string viewModel, bool AdminRequired)
        {
            if (AdminRequired)
                return PageViewModels.Where(m => m.Name == "Ficha Incidencias").FirstOrDefault();

            return PageViewModels.Where(m => m.Name == viewModel).FirstOrDefault();
        }
    }
}
