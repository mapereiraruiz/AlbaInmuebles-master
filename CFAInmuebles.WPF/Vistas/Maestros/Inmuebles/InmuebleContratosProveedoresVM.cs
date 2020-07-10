using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace CFAInmuebles.WPF
{
    public class InmuebleContratosProveedoresVM : ObservableViewModel, IPageViewModel
    {
        public Inmuebles entity;
        protected new ICommand _modifyCommand;

        private ContratosProveedores _selectedItem;
        private HomeInmueblesVM baseVM;
        public InmuebleContratosProveedoresVM(HomeInmueblesVM baseVM, Inmuebles entity = null)
        {
            this.entity = entity;
            this.baseVM = baseVM;
        }
        public string Name
        {
            get
            {
                return "Inmueble Contratos Proveedores";
            }
        }

        public ContratosProveedores SelectedItem
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
                    _modifyCommand = new RelayCommand(p => ModifyData((ContratosProveedores)p));
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
                ContratosProveedores = db.ContratosProveedores.Where(m => m.FechaEliminacion == null && inmuebles.Contains(m.IdInmueble)).ToList();
                Trazabilidad("Maestros", "Inmuebles", entity.Inmueble, "Consulta", "Mantenimiento Inmuebles Contratos Proveedores");
            }
        }

        protected void ModifyData(ContratosProveedores contrato)
        {
            HomeContratoProveedor ventana = new HomeContratoProveedor();

            HomeContratoProveedorVM datacontext = new HomeContratoProveedorVM();
            (ventana as Window).DataContext = datacontext;

            var viewmodel = new FichaContratoProveedorVM(datacontext, contrato);
            datacontext.CurrentPageViewModel = viewmodel;
            ventana.Show();
        }
    }
}