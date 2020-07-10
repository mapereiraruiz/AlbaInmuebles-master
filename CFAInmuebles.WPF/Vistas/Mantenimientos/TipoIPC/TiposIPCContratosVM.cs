using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace CFAInmuebles.WPF
{
    public class TiposIPCContratosVM : ObservableViewModel, IPageViewModel
    {
        public TipoIpc entity;
     
        protected new ICommand _modifyCommand;

        private HomeTipoIPCVM baseVM;
        private ContratosClientes _selectedItem;

        public TiposIPCContratosVM(HomeTipoIPCVM baseVM, TipoIpc entity = null)
        {
            this.entity = entity;
            this.baseVM = baseVM; 
        }

        public string Name
        {
            get
            {
                return "Contratos Clientes Tipo IPC";
            }
        }

        public ContratosClientes SelectedItem
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
                    _modifyCommand = new RelayCommand(p => ModifyData((ContratosClientes)p));
                }
                return _modifyCommand;
            }
        }

        protected void ModifyData(ContratosClientes contrato)
        {
            HomeContratoCliente ventana = new HomeContratoCliente();
           
            HomeContratoClienteVM datacontext = new HomeContratoClienteVM();
            (ventana as Window).DataContext = datacontext;

            var viewmodel = new FichaContratoClienteVM(datacontext, contrato);       
            datacontext.CurrentPageViewModel = viewmodel;         
            ventana.Show();

        }
        protected override void LoadData()
        {
            base.LoadData();

            if (entity.IdTipoIpc > 0)
            {
                ContratosClientes = db.ContratosClientes.Where(m => m.FechaEliminacion == null && m.IdTipoIpc == entity.IdTipoIpc).ToList();

                //Comprobamos que contratos puede ver el usuario según el inmueble
                if (!UserId.Administrador)
                {
                    var inmuebles = db.UsuarioInmueble.Where(m => m.IdUsuario == UserId.IdUsuario).Select(m => m.IdInmueble).ToList();
                    ContratosClientes = ContratosClientes.Where(m => inmuebles.Contains(m.IdInmueble)).ToList();
                }
            }
        }
    }
}
