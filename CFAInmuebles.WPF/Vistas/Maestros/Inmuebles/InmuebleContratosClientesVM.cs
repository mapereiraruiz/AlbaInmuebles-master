using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace CFAInmuebles.WPF
{
    public class InmuebleContratosClientesVM : ObservableViewModel, IPageViewModel
    {
        public Inmuebles entity;
        protected new ICommand _modifyCommand;

        private ContratosClientes _selectedItem;
        private HomeInmueblesVM baseVM;
        public InmuebleContratosClientesVM(HomeInmueblesVM baseVM, Inmuebles entity = null)
        {
            this.entity = entity;
            this.baseVM = baseVM;
        }
        public string Name
        {
            get
            {
                return "Inmueble Contratos Clientes";
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

            if (entity.IdInmueble > 0)
            {
                var inmuebles = db.Inmuebles.Where(m => m.FechaEliminacion == null && m.IdInmueble == entity.IdInmueble).Select(m => m.IdInmueble).ToList();
                ContratosClientes = db.ContratosClientes.Where(m => m.FechaEliminacion == null && inmuebles.Contains(m.IdInmueble)).ToList();
                Trazabilidad("Maestros", "Inmuebles", entity.Inmueble, "Consulta", "Mantenimiento Inmuebles Contratos Clientes");

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
