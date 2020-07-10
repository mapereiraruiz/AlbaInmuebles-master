using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace CFAInmuebles.WPF
{
    public class InmuebleHistoricoSegurosVM : ObservableViewModel, IPageViewModel
    {
        public Inmuebles entity;
        protected new ICommand _modifyCommand;

        private HistoricoSeguros _selectedItem;
        private HomeInmueblesVM baseVM;
        public InmuebleHistoricoSegurosVM(HomeInmueblesVM baseVM, Inmuebles entity = null)
        {
            this.entity = entity;
            this.baseVM = baseVM;
        }
        public string Name
        {
            get
            {
                return "Inmueble Histórico Seguros";
            }
        }
        public HistoricoSeguros SelectedItem
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
                    _modifyCommand = new RelayCommand(p => ModifyData((HistoricoSeguros)p));
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
                HistoricosSeguros = db.HistoricoSeguros.Where(m => m.FechaEliminacion == null && inmuebles.Contains(m.IdInmueble)).ToList();
                Trazabilidad("Maestros", "Inmuebles", entity.Inmueble, "Consulta", "Mantenimiento Inmuebles Histórico Seguros");
            }
        }

        protected void ModifyData(HistoricoSeguros historico)
        {

        }
    }
}
