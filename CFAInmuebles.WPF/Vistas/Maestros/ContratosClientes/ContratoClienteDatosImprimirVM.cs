using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace CFAInmuebles.WPF
{
    public class ContratoClienteDatosImprimirVM : ObservableViewModel, IPageViewModel
    {
        public ContratosClientes entity;

        protected new ICommand _modifyCommand;

        private FichaContratoClienteVM baseVM;

        private DatosImprimirFactura _selectedItem;

        public ContratoClienteDatosImprimirVM(FichaContratoClienteVM baseVM, ContratosClientes entity = null)
        {
            this.entity = entity;
            this.baseVM = baseVM;
        }
        public string Name
        {
            get
            {
                return "Contratos Clientes Datos Imprimir";
            }
        }

        public DatosImprimirFactura SelectedItem
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
                    _modifyCommand = new RelayCommand(p => ModifyData((DatosImprimirFactura)p));
                }
                return _modifyCommand;
            }
        }

        protected override void LoadData()
        {
            base.LoadData();

            if (entity.IdContratoCliente > 0)
            {
                ContratosClienteDatosImprimir = db.DatosImprimirFactura.Where(m => m.IdContratoCliente == entity.IdContratoCliente).ToList();
                Trazabilidad("Maestros", "Contratos Clientes", entity.NumeroContrato.ToString(), "Consulta", "Mantenimiento Contratos Datos Imprimir");
            }
        }

        protected void ModifyData(DatosImprimirFactura entity)
        {
            var viewmodel = PageViewModels.Where(m => m.Name == "Alta Datos Imprimir Contrato Cliente").FirstOrDefault();
            viewmodel = new AltaDatosImprimirContratoClienteVM(baseVM, this.entity, entity);
            baseVM.CurrentPageViewModel = viewmodel;
        }
    }
}
