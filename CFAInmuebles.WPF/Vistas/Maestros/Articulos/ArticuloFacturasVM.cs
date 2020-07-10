using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace CFAInmuebles.WPF
{
    public class ArticuloFacturasVM : ObservableViewModel, IPageViewModel
    {
        public Articulos entity;

        protected new ICommand _modifyCommand;

        private Facturacion _selectedItem;

        private FichaArticulosVM baseVM;
        public ArticuloFacturasVM(FichaArticulosVM baseVM, Articulos entity = null)
        {
            this.entity = entity;
            this.baseVM = baseVM;
        }
        public string Name
        {
            get
            {
                return "Artículo Facturas";
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

            if (entity.IdArticulo > 0)
            {
                var inmuebles = db.Inmuebles.Where(m => m.FechaEliminacion == null && m.IdInmueble == entity.IdInmueble).Select(m => m.IdInmueble).ToList();
                var contratos = db.ContratosClientes.Where(m => m.FechaEliminacion == null && inmuebles.Contains(m.IdInmueble)).Select(m => m.IdContratoCliente).ToList();
                Facturas = db.Facturacion.Where(m => m.FechaEliminacion == null && contratos.Contains(m.IdContratoCliente)).ToList();
                Trazabilidad("Maestros", "Artículos", entity.Articulo, "Consulta", "Mantenimiento Artículos Facturas");
            }
        }
        protected void ModifyData(Facturacion factura)
        {
            var viewmodel = PageViewModels.Where(m => m.Name == "Ficha Artículo Facturación").FirstOrDefault();
            viewmodel = new FichaArticuloFacturacionVM(baseVM, this.entity, factura);
            baseVM.CurrentPageViewModel = viewmodel;
        }
    }
}
