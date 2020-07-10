using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CFAInmuebles.WPF
{
    public class FichaEmpresasVM : ObservableViewModel, IPageViewModel
    {
        public Empresas entity;
        private HomeEmpresasVM baseVM;

        public FichaEmpresasVM(HomeEmpresasVM baseVM, Empresas entidad = null)
        {

            entity = new Empresas();

            if (entidad != null)
            {

                var properties = typeof(Empresas).GetProperties(BindingFlags.Instance | BindingFlags.Public)
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
            PageViewModels.Add(new AltaEmpresasVM(baseVM, entity));
            PageViewModels.Add(new EmpresaInmueblesVM(baseVM, entity));
            PageViewModels.Add(new EmpresaArticulosVM(baseVM, entity));
            PageViewModels.Add(new EmpresaContratosClientesVM(baseVM, entity));
            PageViewModels.Add(new EmpresaContratosProveedoresVM(baseVM, entity));
            PageViewModels.Add(new EmpresaFacturasVM(this, entity));
            PageViewModels.Add(new EmpresaObrasVM(this, entity));
            PageViewModels.Add(new AltaEmpresaObrasVM(this, null));
            PageViewModels.Add(new EmpresaTareasVM(baseVM, entity));
            PageViewModels.Add(new EmpresaIncidenciasVM(baseVM, entity));
            PageViewModels.Add(new EmpresaHistoricoVM(this, entity));
            CurrentPageViewModel = PageViewModels[0];
        }

        public string Name
        {
            get
            {
                return "Ficha Empresas";
            }
        }

        public IPageViewModel AltaEmpresas
        {
            get { return Acceso("Alta Empresas", false); }
        }

        public IPageViewModel EmpresaInmuebles
        {
            get { return Acceso("Empresa Inmuebles", false); }
        }


        public IPageViewModel EmpresaArticulos
        {
            get { return Acceso("Empresa Artículos", false); }
        }

        public IPageViewModel EmpresaContratosClientes
        {
            get { return Acceso("Empresa Contratos Clientes", false); }
        }

        public IPageViewModel EmpresaContratosProveedores
        {
            get { return Acceso("Empresa Contratos Proveedores", false); }
        }

        public IPageViewModel EmpresaFacturas
        {
            get { return Acceso("Empresa Facturas", false); }
        }

        public IPageViewModel EmpresaObras
        {
            get { return Acceso("Empresa Obras", false); }
        }

        public IPageViewModel AltaEmpresaObras
        {
            get { return Acceso("Alta Empresa Obras", false); }
        }

        public IPageViewModel EmpresaTareas
        {
            get { return Acceso("Empresa Tareas", false); }
        }

        public IPageViewModel EmpresaIncidencias
        {
            get { return Acceso("Empresa Incidencias", false); }
        }
        public IPageViewModel EmpresaHistorico
        {
            get { return Acceso("Empresa Histórico Modificaciones", false); }
        }

        public IPageViewModel Acceso(string viewModel, bool AdminRequired)
        {
            if (AdminRequired)
                return PageViewModels.Where(m => m.Name == "Ficha Empresas").FirstOrDefault();

            return PageViewModels.Where(m => m.Name == viewModel).FirstOrDefault();
        }
    }
}
