using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace CFAInmuebles.WPF
{
    public class ContratoClienteHistoricoVM : ObservableViewModel, IPageViewModel
    {
        public ContratosClientes entity;

        protected new ICommand _modifyCommand;
       

        private FichaContratoClienteVM baseVM;
        private HistoricoContratosClientes _selectedItem;
        public ContratoClienteHistoricoVM(FichaContratoClienteVM baseVM, ContratosClientes entity = null)
        {
            this.entity = entity;
            this.baseVM = baseVM;
        }
        public string Name
        {
            get
            {
                return "Contrato Cliente Histórico Modificaciones";
            }
        }

        public HistoricoContratosClientes SelectedItem
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
                    _modifyCommand = new RelayCommand(p => ModifyData((HistoricoContratosClientes)p));
                }
                return _modifyCommand;
            }
        }
        protected override void LoadData()
        {
            base.LoadData();

            if (entity.IdContratoCliente > 0)
            {
                HistoricoContratosClientes = db.HistoricoContratosClientes.Where(m => m.FechaEliminacion == null &&  m.IdContratoCliente == entity.IdContratoCliente).OrderByDescending(m => m.IdHistoricoContratoCliente).ToList();

                Trazabilidad("Maestros", "Contratos Clientes", entity.NumeroContrato.ToString(), "Consulta", "Mantenimiento Contratos Clientes Histórico Modificaciones");
            }
        }

        protected void ModifyData(HistoricoContratosClientes historico)
        {
            //var viewmodel = PageViewModels.Where(m => m.Name == "Ficha Contrato Cliente Histórico").FirstOrDefault();
            //viewmodel = new FichaContratoClienteHistoricoVM(baseVM, this.entity, historico);
            //baseVM.CurrentPageViewModel = viewmodel;
        }
    }
}
