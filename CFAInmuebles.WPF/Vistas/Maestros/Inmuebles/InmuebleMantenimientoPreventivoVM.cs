using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace CFAInmuebles.WPF
{
    public class InmuebleMantenimientoPreventivoVM : ObservableViewModel, IPageViewModel
    {
        public Inmuebles entity;
        protected new ICommand _modifyCommand;

        private Mantenimientos _selectedItem;
        private HomeInmueblesVM baseVM;
        public InmuebleMantenimientoPreventivoVM(HomeInmueblesVM baseVM, Inmuebles entity = null)
        {
            this.entity = entity;
            this.baseVM = baseVM;
        }
        public string Name
        {
            get
            {
                return "Inmueble Mantenimiento Preventivo";
            }
        }

        public Mantenimientos SelectedItem
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
                    _modifyCommand = new RelayCommand(p => ModifyData((Mantenimientos)p));
                }
                return _modifyCommand;
            }
        }
        protected override void LoadData()
        {
            base.LoadData();

            if (entity.IdInmueble > 0)
            {
                Mantenimientos = db.Mantenimientos.Where(m => m.FechaEliminacion == null && m.IdTipoMantenimientoNavigation.Valor == "Preventivo" && m.IdFichero == entity.IdInmueble).ToList();
                Trazabilidad("Maestros", "Inmuebles", entity.Inmueble, "Consulta", "Mantenimiento Inmuebles Mantenimientos Preventivos");
            }
        }

        protected void ModifyData(Mantenimientos mantenimiento)
        {
            HomePreventivoNormativo ventana = new HomePreventivoNormativo();

            HomePreventivoNormativoVM datacontext = new HomePreventivoNormativoVM();
            (ventana as Window).DataContext = datacontext;

            var viewmodel = new FichaPreventivoNormativoVM(datacontext, mantenimiento);
            datacontext.CurrentPageViewModel = viewmodel;
            ventana.Show();
        }
    }
}
