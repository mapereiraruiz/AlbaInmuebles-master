using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace CFAInmuebles.WPF
{
    public class ProveedorContratosVM : ObservableViewModel, IPageViewModel
    {
        public Terceros entity;

        protected new ICommand _modifyCommand;

        private HomeProveedoresVM baseVM;
        private ContratosProveedores _selectedItem;
        public ProveedorContratosVM(HomeProveedoresVM baseVM, Terceros entity = null)
        {
            this.entity = entity;
            this.baseVM = baseVM;
        }
        public string Name
        {
            get
            {
                return "Proveedor Contratos";
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

            if (entity != null)
            {
                ContratosProveedores = db.ContratosProveedores.Where(m => m.FechaEliminacion == null && m.IdContratoProveedor == entity.Numero).ToList();
                //Comprobamos que contratos puede ver el usuario en función del inmueble
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