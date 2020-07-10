using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace CFAInmuebles.WPF
{
    public class InmuebleCentroCostesVM : ObservableViewModel, IPageViewModel
    {
        public Inmuebles entity;
        protected new ICommand _modifyCommand;

        private InmuebleCentroCoste _selectedItem;
        private HomeInmueblesVM baseVM;
        public InmuebleCentroCostesVM(HomeInmueblesVM baseVM, Inmuebles entity = null)
        {
            this.entity = entity;
            this.baseVM = baseVM;

        }
        public string Name
        {
            get
            {
                return "Inmueble Centro Costes";
            }
        }
        public InmuebleCentroCoste SelectedItem
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
                    _modifyCommand = new RelayCommand(p => ModifyData((InmuebleCentroCoste)p));
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
                InmueblesCentrosCostes = db.InmuebleCentroCoste.ToList();
                Trazabilidad("Maestros", "Inmuebles", entity.Inmueble, "Consulta", "Mantenimiento Inmuebles Centros Coste");
            }
        }

        protected void ModifyData(InmuebleCentroCoste entidad)
        {
        }
    }
}