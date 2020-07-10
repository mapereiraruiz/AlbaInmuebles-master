using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace CFAInmuebles.WPF
{
    public class EmpresaContratosClientesVM : ObservableViewModel, IPageViewModel
    {
        public Empresas entity;

        protected new ICommand _modifyCommand;

        private HomeEmpresasVM baseVM;
        private ContratosClientes _selectedItem;
        public EmpresaContratosClientesVM(HomeEmpresasVM baseVM, Empresas entity = null)
        {
            this.entity = entity;
            this.baseVM = baseVM;
        }
        public string Name
        {
            get
            {
                return "Empresa Contratos Clientes";
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
        protected override void LoadData()
        {
            base.LoadData();
            if (entity.IdEmpresa > 0)
            {
                var inmuebles = db.Inmuebles.Where(m => m.FechaEliminacion == null && m.IdEmpresa == entity.IdEmpresa).Select(m => m.IdInmueble).ToList();
                ContratosClientes = db.ContratosClientes.Where(m => m.FechaEliminacion == null && inmuebles.Contains(m.IdInmueble)).ToList();
                Trazabilidad("Maestros", "Empresas", entity.Empresa, "Consulta", "Mantenimiento Empresas Contratos Clientes");

                //Comprobamos que contratos puede ver el usuario según el inmueble
                if (!UserId.Administrador)
                {
                    var inmueble = db.UsuarioInmueble.Where(m => m.IdUsuario == UserId.IdUsuario).Select(m => m.IdInmueble).ToList();
                    ContratosClientes = ContratosClientes.Where(m => inmueble.Contains(m.IdInmueble)).ToList();
                }
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
    }
}
