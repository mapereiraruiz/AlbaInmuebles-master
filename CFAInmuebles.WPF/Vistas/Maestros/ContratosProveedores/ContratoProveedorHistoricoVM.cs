using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace CFAInmuebles.WPF
{
    public class ContratoProveedorHistoricoVM : ObservableViewModel, IPageViewModel
    {
        public ContratosProveedores entity;

        protected new ICommand _modifyCommand;
       

        private FichaContratoProveedorVM baseVM;
        private HistoricoContratosProveedores _selectedItem;
        public ContratoProveedorHistoricoVM(FichaContratoProveedorVM baseVM, ContratosProveedores entity = null)
        {
            this.entity = entity;
            this.baseVM = baseVM;
        }
        public string Name
        {
            get
            {
                return "Contrato Proveedor Histórico Modificaciones";
            }
        }

        public HistoricoContratosProveedores SelectedItem
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
                    _modifyCommand = new RelayCommand(p => ModifyData((HistoricoContratosProveedores)p));
                }
                return _modifyCommand;
            }
        }
        protected override void LoadData()
        {
            base.LoadData();

            if (entity.IdContratoProveedor > 0)
            {
                HistoricoContratosProveedores = db.HistoricoContratosProveedores.Where(m => m.FechaEliminacion == null &&  m.IdContratoProveedor == entity.IdContratoProveedor).OrderByDescending(m => m.IdHistoricoContratoProveedor).ToList();

                Trazabilidad("Maestros", "Contratos Proveedores", entity.ReferenciaContrato.ToString(), "Consulta", "Mantenimiento Contratos Proveedores Histórico Modificaciones");
            }
        }

        protected void ModifyData(HistoricoContratosProveedores historico)
        {
            //var viewmodel = PageViewModels.Where(m => m.Name == "Ficha Contrato Proveedor Histórico").FirstOrDefault();
            //viewmodel = new FichaContratoProveedorHistoricoVM(baseVM, this.entity, historico);
            //baseVM.CurrentPageViewModel = viewmodel;
        }
    }
}
