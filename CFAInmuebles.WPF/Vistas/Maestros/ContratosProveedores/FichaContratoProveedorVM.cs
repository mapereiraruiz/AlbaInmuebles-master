using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CFAInmuebles.WPF
{
    public class FichaContratoProveedorVM : ObservableViewModel, IPageViewModel
    {
        public ContratosProveedores entity;
        private HomeContratoProveedorVM baseVM;
        public FichaContratoProveedorVM(HomeContratoProveedorVM baseVM, ContratosProveedores entidad = null)
        {

            entity = new ContratosProveedores();

            if (entidad != null)
            {
                var properties = typeof(ContratosProveedores).GetProperties(BindingFlags.Instance | BindingFlags.Public)
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
            PageViewModels.Add(new AltaContratoProveedorVM(this.baseVM, entity));
            PageViewModels.Add(new ContratoProveedorIncidenciasVM(this.baseVM, entity));
            PageViewModels.Add(new ContratoProveedorTareasVM(this.baseVM, entity));
            PageViewModels.Add(new ContratoProveedorHistoricoVM(this, entity));
            CurrentPageViewModel = PageViewModels[0];
        }

        public string Name
        {
            get
            {
                return "Ficha Contratos Proveedores";
            }
        }
        public IPageViewModel AltaContratoProveedor
        {
            get { return Acceso("Alta Contratos Proveedores", false); }
        }
        public IPageViewModel ContratoProveedorIncidencias
        {
            get { return Acceso("Contratos Proveedores Incidencias", false); }
        }
        public IPageViewModel ContratoProveedorTareas
        {
            get { return Acceso("Contratos Proveedores Tareas", false); }
        }
        public IPageViewModel ContratoProveedorHistorico
        {
            get { return Acceso("Contrato Proveedor Histórico Modificaciones", false); }
        }
        public IPageViewModel Acceso(string viewModel, bool AdminRequired)
        {
            if (AdminRequired)
                return PageViewModels.Where(m => m.Name == "Ficha Contratos Proveedores").FirstOrDefault();

            return PageViewModels.Where(m => m.Name == viewModel).FirstOrDefault();
        }

    }
}
