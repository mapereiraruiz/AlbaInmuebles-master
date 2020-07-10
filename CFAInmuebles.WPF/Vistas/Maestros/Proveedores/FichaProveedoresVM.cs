using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CFAInmuebles.WPF
{
    public class FichaProveedoresVM : ObservableViewModel, IPageViewModel
    {
        public Terceros entity;
        private HomeProveedoresVM baseVM;

        public FichaProveedoresVM(HomeProveedoresVM baseVM, Terceros entity = null)
        {
            this.entity = entity ?? new Terceros();
            this.baseVM = baseVM;           
            PageViewModels.Add(new AltaProveedoresVM(this.baseVM, this.entity));
            PageViewModels.Add(new ProveedorContratosVM(baseVM, this.entity));
            PageViewModels.Add(new ProveedorFacturasVM(this, this.entity));
            PageViewModels.Add(new ProveedorTareasVM(baseVM, this.entity));
            PageViewModels.Add(new ProveedorIncidenciasVM(baseVM, this.entity));            
            PageViewModels.Add(new ProveedorContactosVM(this, this.entity));

            CurrentPageViewModel = PageViewModels[0];
        }

        public string Name
        {
            get
            {
                return "Ficha Proveedores";
            }
        }

        public IPageViewModel AltaProveedores
        {
            get { return Acceso("Alta Proveedores", false); }
        }

        public IPageViewModel ProveedorContactos
        {
            get { return Acceso("Proveedor Contactos", false); }
        }

        public IPageViewModel AltaContacto
        {
            get { return Acceso("Alta Contacto", false); }
        }

        public IPageViewModel ProveedorContratos
        {
            get { return Acceso("Proveedor Contratos", false); }
        }

        public IPageViewModel ProveedorFacturas
        {
            get { return Acceso("Proveedor Facturas", false); }
        }


        public IPageViewModel ProveedorTareas
        {
            get { return Acceso("Proveedor Tareas", false); }
        }

        public IPageViewModel ProveedorIncidencias
        {
            get { return Acceso("Proveedor Incidencias", false); }
        }



        public IPageViewModel Acceso(string viewModel, bool AdminRequired)
        {
            if (AdminRequired)
                return PageViewModels.Where(m => m.Name == "Ficha Proveedores").FirstOrDefault();

            return PageViewModels.Where(m => m.Name == viewModel).FirstOrDefault();
        }
    }
}
