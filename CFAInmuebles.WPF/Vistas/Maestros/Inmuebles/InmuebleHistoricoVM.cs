using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace CFAInmuebles.WPF
{
    public class InmuebleHistoricoVM : ObservableViewModel, IPageViewModel
    {
        public Inmuebles entity;

        protected new ICommand _modifyCommand;

        private FichaInmueblesVM baseVM;
        private HistoricoInmuebles _selectedItem;
        public InmuebleHistoricoVM(FichaInmueblesVM baseVM, Inmuebles entity = null)
        {
            this.entity = entity;
            this.baseVM = baseVM;
        }
        public string Name
        {
            get
            {
                return "Inmueble Histórico Modificaciones";
            }
        }

        public HistoricoInmuebles SelectedItem
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
                    _modifyCommand = new RelayCommand(p => ModifyData((HistoricoInmuebles)p));
                }
                return _modifyCommand;
            }
        }
        protected override void LoadData()
        {
            base.LoadData();

            if (entity.IdInmueble > 0)
            {
                HistoricoInmuebles = db.HistoricoInmuebles.Where(m => m.FechaEliminacion == null &&  m.IdInmueble == entity.IdInmueble).OrderByDescending(m => m.IdHistoricoInmueble).ToList();

                Trazabilidad("Maestros", "Inmuebles", entity.Inmueble, "Consulta", "Mantenimiento Inmuebles Histórico Modificaciones");
            }
        }

        protected void ModifyData(HistoricoInmuebles historico)
        {
            //var viewmodel = PageViewModels.Where(m => m.Name == "Ficha Inmueble Histórico").FirstOrDefault();
            //viewmodel = new FichaInmuebleHistoricoVM(baseVM, this.entity, historico);
            //baseVM.CurrentPageViewModel = viewmodel;
        }
    }
}
