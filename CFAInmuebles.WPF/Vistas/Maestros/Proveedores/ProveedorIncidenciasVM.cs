using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace CFAInmuebles.WPF
{
    public class ProveedorIncidenciasVM : ObservableViewModel, IPageViewModel
    {
        public Terceros entity;
        private HomeProveedoresVM baseVM;
        
        protected new ICommand _modifyCommand;

        private Incidencias _selectedItem;


        public ProveedorIncidenciasVM(HomeProveedoresVM baseVM, Terceros entity = null)
        {
            this.entity = entity;
            this.baseVM = baseVM;
        }

        public string Name
        {
            get
            {
                return "Proveedor Incidencias";
            }
        }

        public Incidencias SelectedItem
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
                    _modifyCommand = new RelayCommand(p => ModifyData((Incidencias)p));
                }
                return _modifyCommand;
            }
        }
        protected override void LoadData()
        {
            base.LoadData();

            if (entity != null)
            {
                Incidencias = db.Incidencias.Where(m => m.FechaEliminacion == null && m.IdTipoFicheroNavigation.Valor == "Proveedor" && m.IdFichero == entity.Numero).ToList();
                Trazabilidad("Maestros", "Proveedores", entity.Nombre, "Consulta", "Mantenimiento Proveedores Incidencias");
            }
        }

        protected void ModifyData(Incidencias entity)
        {
            HomeIncidencias ventana = new HomeIncidencias();

            HomeIncidenciasVM datacontext = new HomeIncidenciasVM();
            (ventana as Window).DataContext = datacontext;

            var viewmodel = new FichaIncidenciasVM(datacontext, entity);
            datacontext.CurrentPageViewModel = viewmodel;
            ventana.Show();
        }
    }
}
