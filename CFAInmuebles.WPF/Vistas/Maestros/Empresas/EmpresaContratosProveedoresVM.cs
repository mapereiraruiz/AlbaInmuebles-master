using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace CFAInmuebles.WPF
{
    public class EmpresaContratosProveedoresVM : ObservableViewModel, IPageViewModel
    {
        public Empresas entity;

        protected new ICommand _modifyCommand;

        private HomeEmpresasVM baseVM;
        private ContratosProveedores _selectedItem;
        public EmpresaContratosProveedoresVM(HomeEmpresasVM baseVM, Empresas entity = null)
        {
            this.entity = entity;
            this.baseVM = baseVM;
        }
        public string Name
        {
            get
            {
                return "Empresa Contratos Proveedores";
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

            if (entity.IdEmpresa > 0)
            {
                var inmuebles = db.Inmuebles.Where(m => m.FechaEliminacion == null && m.IdEmpresa == entity.IdEmpresa).Select(m => m.IdInmueble).ToList();
                ContratosProveedores = db.ContratosProveedores.Where(m => m.FechaEliminacion == null && inmuebles.Contains(m.IdInmueble)).ToList();
                Trazabilidad("Maestros", "Empresas", entity.Empresa, "Consulta", "Mantenimiento Empresas Contratos Proveedores");

                //Comprobamos que contratos puede ver el usuario
                if (!UserId.Administrador)
                {
                    var inmueble = db.UsuarioInmueble.Where(m => m.IdUsuario == UserId.IdUsuario).Select(m => m.IdInmueble).ToList();
                    ContratosProveedores = ContratosProveedores.Where(m => inmueble.Contains(m.IdInmueble)).ToList();
                }
            }
        }

        protected void ModifyData(ContratosProveedores contrato)
        {
            HomeContratoProveedor ventana = new HomeContratoProveedor();

            HomeContratoProveedorVM datacontext = new HomeContratoProveedorVM();
            (ventana as Window).DataContext = datacontext;

            var viewmodel = new FichaContratoProveedorVM(datacontext, contrato);
            datacontext.CurrentPageViewModel = viewmodel;
            ventana.Show();
        }
    }
}