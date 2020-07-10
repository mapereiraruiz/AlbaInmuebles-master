using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace CFAInmuebles.WPF
{
    public class InmuebleContactosVM : ObservableViewModel, IPageViewModel
    {
        public Inmuebles entity;

        protected new ICommand _modifyCommand;

        private FichaInmueblesVM baseVM;
        private Contactos _selectedItem;

        public InmuebleContactosVM(FichaInmueblesVM baseVM, Inmuebles entity = null)
        {
            this.entity = entity;
            this.baseVM = baseVM;
        }
        public string Name
        {
            get
            {
                return "Inmueble Contactos";
            }
        }

        public Contactos SelectedItem
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
                    _modifyCommand = new RelayCommand(p => ModifyData((Contactos)p));
                }
                return _modifyCommand;
            }
        }

        protected override void LoadData()
        {
            base.LoadData();

            if (entity.IdInmueble > 0)
            {
                Contactos = db.Contactos.Where(m => m.FechaEliminacion == null && m.IdFichero == entity.IdInmueble && m.IdTipoFicheroNavigation.Valor == "Inmueble").ToList();
                Trazabilidad("Maestros", "Inmuebles", entity.Inmueble, "Consulta", "Mantenimiento Inmuebles Contactos");

            }
        }

        protected void ModifyData(Contactos entity)
        {
            var viewmodel = PageViewModels.Where(m => m.Name == "Alta Contacto Inmueble").FirstOrDefault();
            viewmodel = new AltaContactoInmuebleVM(baseVM, this.entity, entity);
            baseVM.CurrentPageViewModel = viewmodel;
        }
    }
}
