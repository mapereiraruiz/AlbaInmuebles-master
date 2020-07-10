using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace CFAInmuebles.WPF
{
    public class ContratoClienteContactosVM : ObservableViewModel, IPageViewModel
    {
        public ContratosClientes entity;

        protected new ICommand _modifyCommand;

        private FichaContratoClienteVM baseVM;
        private Contactos _selectedItem;
        private string _importeaval;
        public ContratoClienteContactosVM(FichaContratoClienteVM baseVM, ContratosClientes entity = null)
        {
            this.entity = entity;
            this.baseVM = baseVM;
        }
        public string Name
        {
            get
            {
                return "Contratos Clientes Contactos";
            }
        }


        public string ImporteAval
        {
            get { return _importeaval; }
            set
            {
                if (_importeaval != value)
                {
                    _importeaval = value;                  
                    RaisePropertyChanged("ImporteAval");
                }
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

            if (entity.IdContratoCliente > 0)
            {
                Contactos = db.Contactos.Where(m => m.FechaEliminacion == null && m.IdFichero == entity.IdContratoCliente && m.IdTipoFicheroNavigation.Valor == "Contrato Cliente").ToList();
            }
        }

        protected void ModifyData(Contactos entity)
        {
            var viewmodel = PageViewModels.Where(m => m.Name == "Alta Contacto Contrato Cliente").FirstOrDefault();
            viewmodel = new AltaContactoContratoClienteVM(baseVM, this.entity, entity);
            baseVM.CurrentPageViewModel = viewmodel;
        }
    }
}
