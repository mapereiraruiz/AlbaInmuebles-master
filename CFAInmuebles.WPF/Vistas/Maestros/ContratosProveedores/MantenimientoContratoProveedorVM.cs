using CFAInmuebles.Domain.Models;
using System;
using System.Linq;
using CFAInmuebles.Infrastructure.Data;
using System.Windows.Input;
using System.Collections.Generic;

namespace CFAInmuebles.WPF
{
    public class MantenimientoContratoProveedorVM : ObservableViewModel, IPageViewModel
    {
        protected new ICommand _modifyCommand;

        private HomeContratoProveedorVM baseVM;
        private ContratosProveedores _selectedItem;
        private List<ContratosProveedores> _contratosproveedoresearch;
        private ContratosProveedores _proveedorsearch;
        private string _servicio;
        private bool? _eliminado;

        public string Name
        {
            get
            {
                return "Mantenimiento Contratos Proveedores";
            }
        }

        public MantenimientoContratoProveedorVM(HomeContratoProveedorVM baseVM)
        {
            this.baseVM = baseVM;
        }

        public bool? Eliminado
        {
            get { return _eliminado; }
            set
            {
                if (_eliminado != value)
                {
                    _eliminado = value;
                    RaisePropertyChanged("Eliminado");
                }
            }
        }

        public string Servicio
        {
            get { return _servicio; }
            set
            {
                if (_servicio != value)
                {
                    _servicio = value;
                    RaisePropertyChanged("Servicio");
                }
            }
        }

        public ContratosProveedores ContratoProveedorSearch
        {
            get { return _proveedorsearch; }
            set
            {
                _proveedorsearch = value;
                RaisePropertyChanged("ContratoProveedorSearch");
            }
        }

        public ContratosProveedores SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                RaisePropertyChanged("SelectedItem");
            }
        }

        public List<ContratosProveedores> ContratosProveedoresSearch
        {
            get { return _contratosproveedoresearch; }
            set
            {
                _contratosproveedoresearch = value;
                RaisePropertyChanged("ContratosProveedoresSearch");
            }
        }



        public new ICommand ModifyCommand
        {
            get
            {
                if (_modifyCommand == null)
                {
                    _modifyCommand = new RelayCommand(p => ModifyData((ContratosProveedores)p));
                }
                return _modifyCommand;
            }
        }

        protected override void LoadData()
        {
            base.LoadData();

            Inmuebles = db.Inmuebles.Where(m => m.FechaEliminacion == null).ToList();

            Empresas = db.Empresas.Where(m => m.FechaEliminacion == null).ToList();
            Empresas.Insert(0, new Empresas { Empresa = "Seleccione:" });
            
            ContratosProveedoresSearch = db.ContratosProveedores.Where(m => m.FechaEliminacion == null).DistinctBy(m => m.NombreProveedor).ToList();
            ContratosProveedoresSearch.Insert(0, new ContratosProveedores { NombreProveedor = "Seleccione:" });

            Trazabilidad("Maestros", "Contratos Proveedores", "", "Consulta", "Mantenimiento Contratos Proveedores");

            //Comprobamos que contratos puede ver el usuario en función del inmueble
            if (!UserId.Administrador)
            {
                var inmueble = db.UsuarioInmueble.Where(m => m.IdUsuario == UserId.IdUsuario).Select(m => m.IdInmueble).ToList();
                ContratosProveedores = ContratosProveedores.Where(m => inmueble.Contains(m.IdInmueble)).ToList();
                Inmuebles = Inmuebles.Where(m => inmueble.Contains(m.IdInmueble)).ToList();
            }

            Inmuebles.Insert(0, new Inmuebles { Inmueble = "Seleccione:" });
            SearchData();
        }
     
        private void ModifyData(ContratosProveedores entity)
        {
            var viewmodel = baseVM.PageViewModels.Where(m => m.Name == "Ficha Contratos Proveedores").FirstOrDefault();
            viewmodel = new FichaContratoProveedorVM(baseVM, entity);
            baseVM.CurrentPageViewModel = viewmodel;
        }

        protected override void SearchData()
        {
            base.SearchData();

            Trazabilidad("Maestros", "Contratos Proveedores", "", "Búsqueda", "Cadena de consulta: Empresa=" + Empresa?.Empresa + "&Inmueble=" + Inmueble?.Inmueble  + "&Proveedor=" + ContratoProveedorSearch?.NombreProveedor
                + "&Servicio=" + Servicio + "&Eliminado=" + Eliminado);

            var search = db.ContratosProveedores.AsQueryable();

            //Comprobamos que contratos puede ver el usuario en función del inmueble
            if (!UserId.Administrador)
            {
                var inmuebles = db.UsuarioInmueble.Where(m => m.IdUsuario == UserId.IdUsuario).Select(m => m.IdInmueble).ToList();
                search = search.Where(m => inmuebles.Contains(m.IdInmueble));
            }

            if (Empresa != null && Empresa.Empresa != "Seleccione:")
            {
                search = search.Where(m => m.IdInmuebleNavigation.IdEmpresaNavigation == Empresa);
            }

            if (Inmueble != null && Inmueble.Inmueble != "Seleccione:")
            {
                search = search.Where(m => m.IdInmuebleNavigation == Inmueble);
            }

            if (ContratoProveedorSearch != null && ContratoProveedorSearch.NombreProveedor != "Seleccione:")
            {
                search = search.Where(m => m.NombreProveedor == ContratoProveedorSearch.NombreProveedor);
            }

            if (!String.IsNullOrEmpty(Servicio))
            {
                search = search.Where(m => m.Servicio.Contains(Servicio));
            }

            if (Eliminado == true)
                search = search.Where(m => m.FechaEliminacion != null);
            else
                search = search.Where(m => m.FechaEliminacion == null);


            ContratosProveedores = search.ToList();
        }
    }
}

