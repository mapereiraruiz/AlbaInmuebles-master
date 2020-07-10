using CFAInmuebles.Domain.Models;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace CFAInmuebles.WPF
{
    public class FichaArticuloFacturacionVM : ObservableViewModel, IPageViewModel, IDataErrorInfo
	{
        public Facturacion entity;
		public Articulos entitybase;

		private FichaArticulosVM baseVM;

        private decimal? _importe;
        private string _codigoagrupacion;
        private string _cliente;

        private bool _selectedItem;
		public FichaArticuloFacturacionVM(FichaArticulosVM baseVM, Articulos entitybase, Facturacion entity = null)
		{
            this.entity = entity ?? new Facturacion();         
            this.entitybase = entitybase;
			this.baseVM = baseVM;
		}
		public string Name
        {
            get
            {
                 return "Ficha Artículo Facturación";
            }
        }

        public decimal? Importe
        {
            get
            {
                return _importe;
            }
            set
            {
                if (_importe != value)
                {
                    _importe = value;
                    RaisePropertyChanged("Importe");
                }
            }
        }       
        public string CodigoAgrupacion
        {
            get { return _codigoagrupacion; }
            set
            {
                if (_codigoagrupacion != value)
                {
                    _codigoagrupacion = value;
                    RaisePropertyChanged("CodigoAgrupacion");
                }
            }
        }

        public string Cliente
        {
            get { return _cliente; }
            set
            {
                if (_cliente != value)
                {
                    _cliente = value;
                    RaisePropertyChanged("Cliente");
                }
            }
        }
        public Visibility MostrarBotones
        {
            get
            {
				if (entity.IdFacturacion == 0)
				{
					return Visibility.Collapsed;
                }
                return Visibility.Visible;
            }
        }

		public bool SelectedItem
		{
			get { return _selectedItem; }
			set
			{
				_selectedItem = value;
				RaisePropertyChanged("SelectedItem");
			}
		}

		protected override void LoadData()
        {
            base.LoadData();

            ContratosClientes = db.ContratosClientes.Where(m => m.FechaEliminacion == null).ToList();
            ConceptosFacturacion = db.ConceptoFacturacion.Where(m => m.FechaEliminacion == null).ToList();         
            ModalidadFacturas = db.ModalidadFactura.ToList();

            if (entity.IdFacturacion > 0)
			{
                Importe = entity.Importe;
                ContratoCliente = entity.IdContratoClienteNavigation;
                ConceptoFacturacion = entity.IdConceptoFacturacionNavigation;
                ModalidadFactura = entity.IdModalidadFacturaNavigation;
                CodigoAgrupacion = entity.CodigoAgrupacion;
                Cliente = entity.IdContratoClienteNavigation.NombreCliente;
                Trazabilidad("Maestros", "Empresas", entity.IdFacturacion.ToString(), "Consulta", "Mantenimiento Artículo Facturación");
			}
        }

        protected override void VolverListado()
        {
			base.VolverListado();
			var viewmodel = baseVM.PageViewModels.Where(m => m.Name == "Artículo Facturas").FirstOrDefault();
			viewmodel = new ArticuloFacturasVM(baseVM, entitybase);
			baseVM.CurrentPageViewModel = viewmodel;
		}
	}
}
