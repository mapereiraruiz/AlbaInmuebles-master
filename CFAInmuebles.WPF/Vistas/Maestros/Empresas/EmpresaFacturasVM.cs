using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace CFAInmuebles.WPF
{
    public class EmpresaFacturasVM : ObservableViewModel, IPageViewModel
    {
        public Empresas entity;

        protected new ICommand _modifyCommand;

        private FichaEmpresasVM baseVM;
        private Facturacion _selectedItem;

        public EmpresaFacturasVM(FichaEmpresasVM baseVM, Empresas entity = null)
        {
            this.entity = entity;
            this.baseVM = baseVM;
        }
        public string Name
        {
            get
            {
                return "Empresa Facturas";
            }
        }

        public Facturacion SelectedItem
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
                    _modifyCommand = new RelayCommand(p => ModifyData((Facturacion)p));
                }
                return _modifyCommand;
            }
        }
        protected override void LoadData()
        {
            base.LoadData();

            if (entity.IdEmpresa > 0)
            {

                var inmuebles = db.Inmuebles.Where(m => m.FechaEliminacion == null && m.IdEmpresa == entity.IdEmpresa).Select(m => m.IdInmueble).ToList();

                var contratos = db.ContratosClientes.Where(m => m.FechaEliminacion == null && inmuebles.Contains(m.IdInmueble)).ToList();


                //Comprobamos que contratos puede ver el usuario
                if (!UserId.Administrador)
                {
                    var inmueble = db.UsuarioInmueble.Where(m => m.IdUsuario == UserId.IdUsuario).Select(m => m.IdInmueble).ToList();
                    contratos = contratos.Where(m => inmueble.Contains(m.IdInmueble)).ToList();
                }

                var contratosInmueble = contratos.Select(m => m.IdContratoCliente).ToList();

                Facturas = db.Facturacion.Where(m => m.FechaEliminacion == null && contratosInmueble.Contains(m.IdContratoCliente)).ToList();
            }
        }
        protected void ModifyData(Facturacion factura)
        {
            var viewmodel = PageViewModels.Where(m => m.Name == "Ficha Empresa Facturación").FirstOrDefault();
            viewmodel = new FichaEmpresaFacturacionVM(baseVM, this.entity, factura);
            baseVM.CurrentPageViewModel = viewmodel;
        }
    }
}
