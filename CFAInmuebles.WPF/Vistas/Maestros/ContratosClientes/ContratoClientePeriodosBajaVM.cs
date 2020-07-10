using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace CFAInmuebles.WPF
{
    public class ContratoClientePeriodosBajaVM : ObservableViewModel, IPageViewModel
    {
        public ContratosClientes entity;
        protected new ICommand _modifyCommand;

        private FichaContratoClienteVM baseVM;

        private ContratosClientesBajaTemporal _selectedItem;
        public ContratoClientePeriodosBajaVM(FichaContratoClienteVM baseVM, ContratosClientes entity = null)
        {
            this.entity = entity;
            this.baseVM = baseVM;
        }
        public string Name
        {
            get
            {
                return "Contratos Clientes Periodos Baja";
            }
        }

        public ContratosClientesBajaTemporal SelectedItem
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
                    _modifyCommand = new RelayCommand(p => ModifyData((ContratosClientesBajaTemporal)p));
                }
                return _modifyCommand;
            }
        }


        protected override void LoadData()
        {
            base.LoadData();

            if (entity != null)
            {
                ContratosClientesBajasTemporal = db.ContratosClientesBajaTemporal.Where(m => m.IdContratoCliente == entity.IdContratoCliente).ToList();
                Trazabilidad("Maestros", "Contratos Clientes", entity.IdContratoCliente.ToString(), "Consulta", "Mantenimiento Contratos Clientes Periodos Baja");
            }
        }
        protected void ModifyData(ContratosClientesBajaTemporal entity)
        {
            var viewmodel = PageViewModels.Where(m => m.Name == "Alta Periodo Baja Contrato Cliente").FirstOrDefault();
            viewmodel = new AltaPeriodoBajaContratoClienteVM(baseVM, this.entity, entity);
            baseVM.CurrentPageViewModel = viewmodel;
        }
    }
}
