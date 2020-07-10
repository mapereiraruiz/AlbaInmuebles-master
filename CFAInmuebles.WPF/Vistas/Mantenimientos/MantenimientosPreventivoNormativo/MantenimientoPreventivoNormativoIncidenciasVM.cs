using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace CFAInmuebles.WPF
{
    public class MantenimientoPreventivoNormativoIncidenciasVM : ObservableViewModel, IPageViewModel
    {
        public Mantenimientos entity;
        
        protected new ICommand _modifyCommand;

        private Incidencias _selectedItem;

        private HomePreventivoNormativoVM baseVM;
        public MantenimientoPreventivoNormativoIncidenciasVM(HomePreventivoNormativoVM baseVM, Mantenimientos entity = null)
        {
            this.entity = entity;
            this.baseVM = baseVM;
        }
        public string Name
        {
            get
            {
                return "Mantenimiento Preventivo Normativo Incidencias";
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

            if (entity.IdMantenimiento > 0)
            {
                Incidencias = db.Incidencias.Where(m => m.FechaEliminacion == null && m.IdFichero == entity.IdMantenimiento && m.IdTipoFicheroNavigation.Valor == "Mantenimiento").ToList();
                Trazabilidad("Mantenimientos", "Mantenimiento Preventivo Normativo", entity.IdMantenimiento.ToString(), "Consulta", "Mantenimiento Perventivo Normativo Incidencias");

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
