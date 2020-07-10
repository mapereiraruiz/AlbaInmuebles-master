using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace CFAInmuebles.WPF
{
    public class ProveedorContactosVM : ObservableViewModel, IPageViewModel
    {
        public Terceros entity;
        protected new ICommand _modifyCommand;

        private Contactos _selectedItem;

        private FichaProveedoresVM baseVM;

        public ProveedorContactosVM(FichaProveedoresVM baseVM, Terceros entity = null)
        {
            this.entity = entity;
            this.baseVM = baseVM;
        }

        public string Name
        {
            get
            {
                return "Proveedor Contactos";
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

            if (entity != null)
            {
                Contactos = db.Contactos.Where(m => m.FechaEliminacion == null && m.IdFichero == entity.Numero && m.IdTipoFicheroNavigation.Valor == "Proveedor").ToList();
                Trazabilidad("Maestros", "Proveedores", entity.Nombre, "Consulta", "Mantenimiento Proveedores");
            }
        }

        protected void ModifyData(Contactos entity)
        {
            var viewmodel = PageViewModels.Where(m => m.Name == "Alta Contacto Proveedor").FirstOrDefault();
            viewmodel = new AltaContactoVM(baseVM, this.entity, entity);
            baseVM.CurrentPageViewModel = viewmodel;
        }
    }
}