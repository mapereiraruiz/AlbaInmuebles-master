using CFAInmuebles.Domain.Models;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace CFAInmuebles.WPF
{
    public class FichaEmpresaFacturacionVM : ObservableViewModel, IPageViewModel, IDataErrorInfo
	{
        public Facturacion entity;
		public Empresas entitybase;

		private FichaEmpresasVM baseVM;

        private decimal? _importe;
        private string _modalidadfactura;
        private string _cliente;
        private string _codigoagrupacion;

        private bool _selectedItem;
		public FichaEmpresaFacturacionVM(FichaEmpresasVM baseVM, Empresas entitybase, Facturacion entity = null)
		{
            this.entity = entity ?? new Facturacion();         
            this.entitybase = entitybase;
			this.baseVM = baseVM;
		}
		public string Name
        {
            get
            {
                 return "Ficha Empresa Facturación";
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
                Trazabilidad("Maestros", "Empresas", entity.IdFacturacion.ToString(), "Consulta", "Mantenimiento Empresa Facturación");
			}
        }

        protected override void VolverListado()
        {
			base.VolverListado();
			var viewmodel = baseVM.PageViewModels.Where(m => m.Name == "Empresa Facturas").FirstOrDefault();
			viewmodel = new EmpresaFacturasVM(baseVM, entitybase);
			baseVM.CurrentPageViewModel = viewmodel;
		}
	}
}
