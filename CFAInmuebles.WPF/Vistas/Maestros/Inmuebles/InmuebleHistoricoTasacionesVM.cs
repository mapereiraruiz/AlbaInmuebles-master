using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace CFAInmuebles.WPF
{
    public class InmuebleHistoricoTasacionesVM : ObservableViewModel, IPageViewModel
    {
        public Inmuebles entity;
        protected new ICommand _modifyCommand;

        private HistoricoTasaciones _selectedItem;
        private HomeInmueblesVM baseVM;
        public InmuebleHistoricoTasacionesVM(HomeInmueblesVM baseVM, Inmuebles entity = null)
        {
            this.entity = entity;
            this.baseVM = baseVM;
        }
        public string Name
        {
            get
            {
                return "Inmueble Histórico Tasaciones";
            }
        }

        public HistoricoTasaciones SelectedItem
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
                    _modifyCommand = new RelayCommand(p => ModifyData((HistoricoTasaciones)p));
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
                HistoricosTasaciones = db.HistoricoTasaciones.Where(m => m.FechaEliminacion == null && inmuebles.Contains(m.IdInmueble)).ToList();
                Trazabilidad("Maestros", "Inmuebles", entity.Inmueble, "Consulta", "Mantenimiento Inmuebles Histórico Tasaciones");
            }
        }

        protected void ModifyData(HistoricoTasaciones historico)
        {

        }
    }
}
