using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace CFAInmuebles.WPF
{
    public class ContratoClienteArticulosVM : ObservableViewModel, IPageViewModel
    {
        public ContratosClientes entity;

        protected new ICommand _modifyCommand;

        private HomeContratoClienteVM baseVM;
        private Articulos _selectedItem;
        public ContratoClienteArticulosVM(HomeContratoClienteVM baseVM, ContratosClientes entity = null)
        {
            this.entity = entity;
            this.baseVM = baseVM;
        }
        public string Name
        {
            get
            {
                return "Contratos Clientes Artículos";
            }
        }
        public Articulos SelectedItem
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
                    _modifyCommand = new RelayCommand(p => ModifyData((Articulos)p));
                }
                return _modifyCommand;
            }
        }
        protected override void LoadData()
        {
            base.LoadData();

            if (entity.IdContratoCliente > 0)
            {
                var inmuebles = db.Inmuebles.Where(m => m.FechaEliminacion == null && m.IdInmueble == entity.IdInmueble).Select(m => m.IdInmueble).ToList();

                //ContratosClientes = db.ContratosClientes.Where(m => m.FechaEliminacion == null && m.IdInmueble == entity.IdInmueble).ToList();

                Articulos = db.Articulos.Where(m => m.FechaEliminacion == null && inmuebles.Contains(m.IdInmueble)).ToList();
                Trazabilidad("Maestros", "Contratos Clientes", entity.NumeroContrato.ToString() , "Consulta", "Mantenimiento Contratos Clientes Artículos");
            }        
        }
        protected void ModifyData(Articulos articulo)
        {
            HomeArticulos ventana = new HomeArticulos();

            HomeArticulosVM datacontext = new HomeArticulosVM();
            (ventana as Window).DataContext = datacontext;

            var viewmodel = new FichaArticulosVM(datacontext, articulo);
            datacontext.CurrentPageViewModel = viewmodel;
            ventana.Show();
        }
    }
}
