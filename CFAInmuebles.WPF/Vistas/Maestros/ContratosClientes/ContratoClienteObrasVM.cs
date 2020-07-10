using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace CFAInmuebles.WPF
{
    public class ContratoClienteObrasVM : ObservableViewModel, IPageViewModel
    {
        public ContratosClientes entity;
        
        protected new ICommand _modifyCommand;

        private HomeContratoClienteVM baseVM;
        private HistorialObra _selectedItem;
        public ContratoClienteObrasVM(HomeContratoClienteVM baseVM, ContratosClientes entity = null)
        {
            this.entity = entity;
            this.baseVM = baseVM;
        }
        public string Name
        {
            get
            {
                return "Contratos Clientes Obras";
            }
        }

        public HistorialObra SelectedItem
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
                    _modifyCommand = new RelayCommand(p => ModifyData((HistorialObra)p));
                }
                return _modifyCommand;
            }
        }
        protected override void LoadData()
        {
            base.LoadData();

            if (entity.IdContratoCliente > 0)
            {
                var ficheroobras = db.ObrasFichero.Where(m => m.IdFichero == entity.IdContratoCliente && m.IdTipoFicheroNavigation.Valor == "Contrato Cliente").Select(m => m.IdHistorialObra).ToList();
                Obras = db.HistorialObra.Where(m => m.FechaEliminacion == null && ficheroobras.Contains(m.IdHistorialObra)).ToList();
                Trazabilidad("Maestros", "Contratos Clientes", entity.NumeroContrato.ToString(), "Consulta", "Mantenimiento Contratos Clientes Obras");
            }
        }

        protected void ModifyData(HistorialObra entity)
        {
            HomeObra ventana = new HomeObra();

            HomeObraVM datacontext = new HomeObraVM();
            (ventana as Window).DataContext = datacontext;

            var viewmodel = new FichaObraVM(datacontext, entity);
            datacontext.CurrentPageViewModel = viewmodel;
            ventana.Show();
        }
    }
}
