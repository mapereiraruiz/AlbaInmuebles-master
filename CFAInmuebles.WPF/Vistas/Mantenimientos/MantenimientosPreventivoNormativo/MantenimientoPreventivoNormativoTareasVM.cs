using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace CFAInmuebles.WPF
{
    public class MantenimientoPreventivoNormativoTareasVM : ObservableViewModel, IPageViewModel
    {
        public Mantenimientos entity;

        protected new ICommand _modifyCommand;

        private TareaPeriodica _selectedItem;

        private HomePreventivoNormativoVM baseVM;

        public MantenimientoPreventivoNormativoTareasVM(HomePreventivoNormativoVM baseVM, Mantenimientos entity = null)
        {
            this.entity = entity;
            this.baseVM = baseVM;
        }
        public string Name
        {
            get
            {
                return "Mantenimiento Preventivo Normativo Tareas";
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

            if (entity.IdMantenimiento > 0)
            {
                TareasPeriodicas = db.TareaPeriodica.Where(m => m.FechaEliminacion == null && m.IdTipoFicheroNavigation.Valor == "Mantenimiento" && m.IdFichero == entity.IdMantenimiento).ToList();
                
                Trazabilidad("Mantenimientos", "Mantenimiento Preventivo Normativo", entity.IdMantenimiento.ToString(), "Consulta", "Mantenimiento Perventivo Normativo Tareas Periódicas");

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
