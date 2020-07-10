using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace CFAInmuebles.WPF
{
    public class InmuebleLicenciasVM : ObservableViewModel, IPageViewModel
    {
        public Inmuebles entity;

        protected new ICommand _modifyCommand;

        private Licencias _selectedItem;
        private FichaInmueblesVM baseVM;
        public InmuebleLicenciasVM(FichaInmueblesVM baseVM, Inmuebles entity = null)
        {
            this.entity = entity;
            this.baseVM = baseVM;
    
        }
        public string Name
        {
            get
            {
                return "Inmueble Licencias";
            }
        }

        public Licencias SelectedItem
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
                    _modifyCommand = new RelayCommand(p => ModifyData((Licencias)p));
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
                Licencias = db.Licencias.Where(m => m.FechaEliminacion == null && inmuebles.Contains(m.IdFichero)).ToList();
                Trazabilidad("Maestros", "Inmuebles", entity.Inmueble, "Consulta", "Mantenimiento Inmuebles Licencias");
            }
        }

        protected void ModifyData(Licencias entity)
        {
            var viewmodel = PageViewModels.Where(m => m.Name == "Alta Inmueble Licencias").FirstOrDefault();
            viewmodel = new AltaInmuebleLicenciasVM(baseVM, this.entity, entity);
            baseVM.CurrentPageViewModel = viewmodel;
        }
    }
}