using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace CFAInmuebles.WPF
{
    public class ContratoClienteFacturasVM : ObservableViewModel, IPageViewModel
    {
        public ContratosClientes entity;

        protected new ICommand _modifyCommand;

        private FichaContratoClienteVM baseVM;
        private Facturacion _selectedItem;

        public ContratoClienteFacturasVM(FichaContratoClienteVM baseVM, ContratosClientes entity = null)
        {
            this.entity = entity;
            this.baseVM = baseVM;
        }
        public string Name
        {
            get
            {
                return "Contratos Clientes Facturas";
            }
        }

        public Facturacion SelectedItem
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
                    _modifyCommand = new RelayCommand(p => ModifyData((Facturacion)p));
                }
                return _modifyCommand;
            }
        }
        protected override void LoadData()
        {
            base.LoadData();

            if (entity.IdContratoCliente > 0)
            {
                Facturas = db.Facturacion.Where(m => m.FechaEliminacion == null && m.IdContratoCliente == entity.IdContratoCliente).ToList();
                Trazabilidad("Maestros", "Contratos Clientes", entity.NumeroContrato.ToString(), "Consulta", "Mantenimiento Contratos Clientes Facturas");
            }
        }

        private void ModifyData(Facturacion factura)
        {
            var viewmodel = PageViewModels.Where(m => m.Name == "Ficha Contrato Cliente Facturación").FirstOrDefault();
            viewmodel = new FichaContratoClienteFacturacionVM(baseVM, this.entity, factura);
            baseVM.CurrentPageViewModel = viewmodel;
        }

    }
}
