using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace CFAInmuebles.WPF
{
    public class InmuebleVentasParcialesVM : ObservableViewModel, IPageViewModel
    {
        public Inmuebles entity;

        protected new ICommand _modifyCommand;

        private FichaInmueblesVM baseVM;
        private VentaParcialInmueble _selectedItem;

        public InmuebleVentasParcialesVM(FichaInmueblesVM baseVM, Inmuebles entity = null)
        {
            this.entity = entity;
            this.baseVM = baseVM;
        }
        public string Name
        {
            get
            {
                return "Inmueble Ventas Parciales";
            }
        }

        public VentaParcialInmueble SelectedItem
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
                    _modifyCommand = new RelayCommand(p => ModifyData((VentaParcialInmueble)p));
                }
                return _modifyCommand;
            }
        }

        protected override void LoadData()
        {
            base.LoadData();

            if (entity.IdInmueble > 0)
            {
                VentaParcialInmuebles = db.VentaParcialInmueble.Where(m => m.IdInmueble == entity.IdInmueble).ToList();
                Trazabilidad("Maestros", "Inmuebles", entity.Inmueble, "Consulta", "Mantenimiento Inmuebles Venta Parcial");

            }
        }

        protected void ModifyData(VentaParcialInmueble entity)
        {
            var viewmodel = PageViewModels.Where(m => m.Name == "Alta Venta Parcial Inmueble").FirstOrDefault();
            viewmodel = new AltaVentaParcialInmuebleVM(baseVM, this.entity, entity);
            baseVM.CurrentPageViewModel = viewmodel;
        }
    }
}
