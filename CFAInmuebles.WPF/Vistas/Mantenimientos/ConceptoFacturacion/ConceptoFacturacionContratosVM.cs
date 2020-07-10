using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace CFAInmuebles.WPF
{
    public class ConceptoFacturacionContratosVM : ObservableViewModel, IPageViewModel
    {
        public ConceptoFacturacion entity;

        protected new ICommand _modifyCommand;

        private HomeConceptoFacturacionVM baseVM;
        private ContratosClientes _selectedItem;

        public ConceptoFacturacionContratosVM(HomeConceptoFacturacionVM baseVM, ConceptoFacturacion entity = null)
        {
            this.entity = entity;
            this.baseVM = baseVM;
        }

        public string Name
        {
            get
            {
                return "Contratos Clientes Concepto de Facturación";
            }
        }

        public ContratosClientes SelectedItem
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
                    _modifyCommand = new RelayCommand(p => ModifyData((ContratosClientes)p));
                }
                return _modifyCommand;
            }
        }

        protected override void LoadData()
        {
            base.LoadData();

            if (entity.IdConceptoFacturacion > 0)
            {
                var facturas = db.Facturacion.Where(m => m.FechaEliminacion == null && m.IdConceptoFacturacion == entity.IdConceptoFacturacion).Select(m => m.IdContratoCliente).ToList();
                ContratosClientes = db.ContratosClientes.Where(m => m.FechaEliminacion == null && facturas.Contains(m.IdContratoCliente)).ToList();

                //Comprobamos que contratos puede ver el usuario según el inmueble
                if (!UserId.Administrador)
                {
                    var inmuebles = db.UsuarioInmueble.Where(m => m.IdUsuario == UserId.IdUsuario).Select(m => m.IdInmueble).ToList();
                    ContratosClientes = ContratosClientes.Where(m => inmuebles.Contains(m.IdInmueble)).ToList();
                }
            }
        }

        protected void ModifyData(ContratosClientes contrato)
        {
            HomeContratoCliente ventana = new HomeContratoCliente();

            HomeContratoClienteVM datacontext = new HomeContratoClienteVM();
            (ventana as Window).DataContext = datacontext;

            var viewmodel = new FichaContratoClienteVM(datacontext, contrato);
            datacontext.CurrentPageViewModel = viewmodel;
            ventana.Show();

        }
    }
}