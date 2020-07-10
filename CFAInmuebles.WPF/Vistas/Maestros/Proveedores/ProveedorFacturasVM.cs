using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace CFAInmuebles.WPF
{
    public class ProveedorFacturasVM : ObservableViewModel, IPageViewModel
    {
        public Terceros entity;

        private FichaProveedoresVM baseVM;
        protected new ICommand _modifyCommand;

        private Facturacion _selectedItem;
        public ProveedorFacturasVM(FichaProveedoresVM baseVM, Terceros entity = null)
        {
            this.entity = entity;
            this.baseVM = baseVM;
        }
        public string Name
        {
            get
            {
                return "Proveedor Facturas";
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

            if (entity != null)
            {
                var inmuebles = db.Inmuebles.Where(m => m.FechaEliminacion == null).Select(m => m.IdInmueble).ToList();
                var contratos = db.ContratosClientes.Where(m => m.FechaEliminacion == null && inmuebles.Contains(m.IdInmueble)).Select(m => m.IdContratoCliente).ToList();

                Facturas = db.Facturacion.Where(m => m.FechaEliminacion == null && contratos.Contains(m.IdContratoCliente)).ToList();

                Trazabilidad("Maestros", "Proveedores", entity.Nombre, "Consulta", "Mantenimiento Proveedores Facturas");

            }
        }

        protected void ModifyData(Facturacion factura)
        {
            var viewmodel = PageViewModels.Where(m => m.Name == "Ficha Proveedor Facturación").FirstOrDefault();
            viewmodel = new FichaProveedorFacturacionVM(baseVM, this.entity, factura);
            baseVM.CurrentPageViewModel = viewmodel;
        }
    }
}
