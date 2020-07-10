using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace CFAInmuebles.WPF
{
    public class ContratoClienteHistorialOcupacionVM : ObservableViewModel, IPageViewModel
    {
        public ContratosClientes entity;

        private HomeContratoClienteVM baseVM;
        private HistorialOcupacion _selectedItem;
        public ContratoClienteHistorialOcupacionVM(HomeContratoClienteVM baseVM, ContratosClientes entity = null)
        {
            this.entity = entity;
            this.baseVM = baseVM;
        }
        public string Name
        {
            get
            {
                return "Contratos Clientes Historial Ocupación";
            }
        }

        public HistorialOcupacion SelectedItem
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
                    _modifyCommand = new RelayCommand(p => ModifyData((HistorialOcupacion)p));
                }
                return _modifyCommand;
            }
        }
        protected override void LoadData()
        {
            base.LoadData();

            if (entity.IdContratoCliente > 0)
            {
                HistorialOcupaciones = db.HistorialOcupacion.Where(m => m.FechaEliminacion == null && m.IdContratoCliente == entity.IdContratoCliente).ToList();
                Trazabilidad("Maestros", "Contratos Clientes", entity.NumeroContrato.ToString(), "Consulta", "Mantenimiento Contratos Clientes Historial Ocupación");
            }
        }
        protected void ModifyData(HistorialOcupacion entity)
        { }

        }
    }
