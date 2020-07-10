using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace CFAInmuebles.WPF
{
    public class InmuebleObrasVM : ObservableViewModel, IPageViewModel
    {
        public Inmuebles entity;

        protected new ICommand _modifyCommand;

        private HistorialObra _selectedItem;

        private FichaInmueblesVM baseVM;
        public InmuebleObrasVM(FichaInmueblesVM baseVM, Inmuebles entity = null)
        {
            this.entity = entity;
            this.baseVM = baseVM;
    
        }
        public string Name
        {
            get
            {
                return "Inmueble Obras";
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

            if (entity.IdInmueble > 0)
            {
                var ficheroobras = db.ObrasFichero.Where(m => m.IdFichero == entity.IdInmueble && m.IdTipoFicheroNavigation.Valor == "Inmueble").Select(m => m.IdHistorialObra).ToList();
                Obras = db.HistorialObra.Where(m => m.FechaEliminacion == null && ficheroobras.Contains(m.IdHistorialObra)).ToList();
                Trazabilidad("Maestros", "Inmuebles", entity.Inmueble, "Consulta", "Mantenimiento Inmuebles Historial Obras");
            }
        }

        protected void ModifyData(HistorialObra entity)
        {
            var viewmodel = PageViewModels.Where(m => m.Name == "Alta Inmueble Obras").FirstOrDefault();
            viewmodel = new AltaInmuebleObrasVM(baseVM, this.entity, entity);
            baseVM.CurrentPageViewModel = viewmodel;
        }
    }
}