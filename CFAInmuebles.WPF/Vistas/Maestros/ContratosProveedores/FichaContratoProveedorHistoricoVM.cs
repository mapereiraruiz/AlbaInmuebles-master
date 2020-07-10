using CFAInmuebles.Domain.Models;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace CFAInmuebles.WPF
{
    public class FichaContratoProveedorHistoricoVM : ObservableViewModel, IPageViewModel, IDataErrorInfo
	{
        public HistoricoContratosProveedores entity;
		public ContratosProveedores entitybase;

		private FichaContratoProveedorVM baseVM;

        private string _empresa;
        private string _inmueble;
        private string _tipoipc;
        private string _cliente;

        private bool _selectedItem;

		public FichaContratoProveedorHistoricoVM(FichaContratoProveedorVM baseVM, ContratosProveedores entitybase, HistoricoContratosProveedores entity = null)
		{
            this.entity = entity ?? new HistoricoContratosProveedores();         
            this.entitybase = entitybase;
			this.baseVM = baseVM;
		}


		public string Name
        {
            get
            {
                 return "Ficha Contrato Proveedor Histórico";
            }
        }

        public new string Empresa
        {
            get
            {
                return _empresa;
            }
            set
            {
                if (_empresa != value)
                {
                    _empresa = value;
                    RaisePropertyChanged("Empresa");
                }
            }
        }

        public new string Inmueble
        {
            get
            {
                return _inmueble;
            }
            set
            {
                if (_inmueble != value)
                {
                    _inmueble = value;
                    RaisePropertyChanged("Inmueble");
                }
            }
        }

        public new string Cliente
        {
            get
            {
                return _cliente;
            }
            set
            {
                if (_cliente != value)
                {
                    _cliente = value;
                    RaisePropertyChanged("Cliente");
                }
            }
        }

        public new string TipoIpc
        {
            get
            {
                return _tipoipc;
            }
            set
            {
                if (_tipoipc != value)
                {
                    _tipoipc = value;
                    RaisePropertyChanged("TipoIpc");
                }
            }
        }
        public Visibility MostrarBotones
        {
            get
            {
				if (entity.IdHistoricoContratoProveedor == 0)
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

			if (entity.IdHistoricoContratoProveedor > 0)
			{
               
                Inmueble = db.Inmuebles.Find(entity.IdInmueble).Inmueble;
                Empresa = db.Inmuebles.Find(entity.IdInmueble).IdEmpresaNavigation.Empresa;

                Trazabilidad("Maestros", "Contratos Proveedores", entity.ReferenciaContrato.ToString(), "Consulta", "Mantenimiento Contrato Proveedor Histórico");
			}
        }

        protected override void VolverListado()
        {
			base.VolverListado();
			var viewmodel = baseVM.PageViewModels.Where(m => m.Name == "Contrato Proveedor Histórico Modificación").FirstOrDefault();
			viewmodel = new ContratoProveedorHistoricoVM(baseVM, entitybase);
			baseVM.CurrentPageViewModel = viewmodel;
		}
	}
}
