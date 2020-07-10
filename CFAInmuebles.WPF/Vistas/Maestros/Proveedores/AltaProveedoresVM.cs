using CFAInmuebles.Domain.Models;
using CFAInmuebles.Infrastructure.Data;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CFAInmuebles.WPF
{
    public class AltaProveedoresVM : ObservableViewModel, IPageViewModel, IDataErrorInfo
    {
        public Terceros entity;
        private HomeProveedoresVM baseVM;

        private string _proveedor1;
        private string _servicio;
        private string _cuentacontable;
        private string _nif;

        private bool _selectedItem;

        public AltaProveedoresVM(HomeProveedoresVM baseVM, Terceros entity = null)
        {
            this.entity = entity;
            this.baseVM = baseVM;
        }

        public string Name
        {
            get
            {
                 return "Alta Proveedores";
            }
        }

        public string Proveedor1
        {
            get
            {
                return _proveedor1;
            }
            set
            {
                if (_proveedor1 != value)
                {
                    _proveedor1 = value;
                    RaisePropertyChanged("Proveedor1");
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

        public string CuentaContable
        {
            get { return _cuentacontable; }
            set
            {
                if (_cuentacontable != value)
                {
                    _cuentacontable = value;
                    RaisePropertyChanged("CuentaContable");
                }
            }
        }

        public string Nif
        {
            get { return _nif; }
            set
            {
                if (_nif != value)
                {
                    _nif = value;
                    RaisePropertyChanged("Nif");
                }
            }
        }

        public bool SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                RaisePropertyChanged("SelectedItem");
            }
        }

        public Visibility MostrarBotones
        {
            get
            {
                if (entity != null)
                {
                    return Visibility.Collapsed;
                }
                return Visibility.Visible;
            }
        }


        protected override void LoadData()
        {
            base.LoadData();

            var searchEmpresas = db.Empresas.Where(m => m.FechaEliminacion == null).AsQueryable();
            Empresas = searchEmpresas.ToList();


            if (entity != null)
            {
                Proveedor1 = entity.Nombre;
             //   Servicio = entity.Servicio;
                CuentaContable = entity.Cuenta;
                Nif = entity.NIF;
                Trazabilidad("Maestros", "Proveedores", Proveedor1, "Consulta", "Ficha Proveedores");
            }
        }

        protected override void VolverListado()
        {
            base.VolverListado();
            baseVM.ChangePageCommand.Execute(baseVM.PageViewModels.Where(m => m.Name == "Mantenimiento Proveedores").FirstOrDefault());
        }

    }
}
