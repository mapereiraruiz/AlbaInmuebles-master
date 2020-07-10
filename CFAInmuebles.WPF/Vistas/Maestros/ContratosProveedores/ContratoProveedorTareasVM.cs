using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace CFAInmuebles.WPF
{
    public class ContratoProveedorTareasVM : ObservableViewModel, IPageViewModel
    {
        public ContratosProveedores entity;

        protected new ICommand _modifyCommand;

        private HomeContratoProveedorVM baseVM;
        private TareaPeriodica _selectedItem;
        public ContratoProveedorTareasVM(HomeContratoProveedorVM baseVM, ContratosProveedores entity = null)
        {
            this.entity = entity;
            this.baseVM = baseVM;
        }
        public string Name
        {
            get
            {
                return "Contratos Proveedores Tareas";
            }
        }
        public TareaPeriodica SelectedItem
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
                    _modifyCommand = new RelayCommand(p => ModifyData((TareaPeriodica)p));
                }
                return _modifyCommand;
            }
        }
        protected override void LoadData()
        {
            base.LoadData();

            if (entity.IdContratoProveedor > 0)
            {
                TareasPeriodicas = db.TareaPeriodica.Where(m => m.FechaEliminacion == null && m.IdTipoFicheroNavigation.Valor == "Contrato Proveedor"  && m.IdFichero == entity.IdContratoProveedor).ToList();
               
                Trazabilidad("Maestros", "Contratos Clientes", entity.ReferenciaContrato, "Consulta", "Mantenimiento Contratos Proveedores Tareas");
            }
        }
        protected void ModifyData(TareaPeriodica entity)
        {
            HomeTareaPeriodica ventana = new HomeTareaPeriodica();

            HomeTareaPeriodicaVM datacontext = new HomeTareaPeriodicaVM();
            (ventana as Window).DataContext = datacontext;

            var viewmodel = new FichaTareaPeriodicaVM(datacontext, entity);
            datacontext.CurrentPageViewModel = viewmodel;
            ventana.Show();
        }
    }
}
