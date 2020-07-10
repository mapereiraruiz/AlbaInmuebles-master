using CFAInmuebles.Domain.Models;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace CFAInmuebles.WPF
{
    public class FichaEmpresaHistoricoVM : ObservableViewModel, IPageViewModel, IDataErrorInfo
	{
        public HistoricoEmpresas entity;
		public Empresas entitybase;

		private FichaEmpresasVM baseVM;

        private string _empresa;
        private string _nif;
        private string _direccion;
        private string _cuentabancaria;
        private string _cuentacontable;
        private string _ejercicio;
        private DateTime? _fechaultimafacturacionmensual;
        private string _codigosepa;
        private DateTime? _fechaiae;
        private string _importeiae;
        private int? _swift;
        private string _polizaseguro;
        private byte[] _logoempresa;
        private string _piefactura;
        private string _lateralfactura;

        private bool _selectedItem;

		public FichaEmpresaHistoricoVM(FichaEmpresasVM baseVM, Empresas entitybase, HistoricoEmpresas entity = null)
		{
            this.entity = entity ?? new HistoricoEmpresas();         
            this.entitybase = entitybase;
			this.baseVM = baseVM;
		}


		public string Name
        {
            get
            {
                 return "Ficha Empresa Histórico";
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

        public string Nif
        {
            get
            {
                return _nif;
            }
            set
            {
                if (_nif != value)
                {
                    _nif = value;
                    RaisePropertyChanged("Nif");
                }
            }
        }

        public string Direccion
        {
            get
            {
                return _direccion;
            }
            set
            {
                if (_direccion != value)
                {
                    _direccion = value;
                    RaisePropertyChanged("Direccion");
                }
            }
        }

        public string CuentaBancaria
        {
            get
            {
                return _cuentabancaria;
            }
            set
            {
                if (_cuentabancaria != value)
                {
                    _cuentabancaria = value;
                    RaisePropertyChanged("CuentaBancaria");
                }
            }
        }

        public string CuentaContable
        {
            get { return _cuentacontable; }
            set
            {
                if (_cuentacontable != value)
                {
                    _cuentacontable = value;
                    RaisePropertyChanged("CuentaContable");
                }
            }
        }

        public string EjercicioFacturacion
        {
            get
            {
                return _ejercicio;
            }
            set
            {
                if (_ejercicio != value)
                {
                    _ejercicio = value;
                    RaisePropertyChanged("EjercicioFacturacion");
                }
            }
        }

        public string CodigoSepa
        {
            get { return _codigosepa; }
            set
            {
                if (_codigosepa != value)
                {
                    _codigosepa = value;
                    RaisePropertyChanged("CodigoSepa");
                }
            }
        }


        public DateTime? FechaUltimaFacturacionMensual
        {
            get { return _fechaultimafacturacionmensual; }
            set
            {
                if (_fechaultimafacturacionmensual != value)
                {
                    _fechaultimafacturacionmensual = value;
                    RaisePropertyChanged("FechaUltimaFacturacionMensual");
                }
            }
        }

        public DateTime? FechaIae
        {
            get { return _fechaiae; }
            set
            {
                if (_fechaiae != value)
                {
                    _fechaiae = value;
                    RaisePropertyChanged("FechaIae");
                }
            }
        }

        public string ImporteIae
        {
            get
            {
                return _importeiae;
            }
            set
            {
                if (_importeiae != value)
                {
                    _importeiae = value;
                    RaisePropertyChanged("ImporteIae");
                }
            }
        }

        public new int? Swift
        {
            get { return _swift; }
            set
            {
                if (_swift != value)
                {
                    _swift = value;
                    RaisePropertyChanged("Swift");
                }
            }
        }

        public string PolizaSeguro
        {
            get { return _polizaseguro; }
            set
            {
                if (_polizaseguro != value)
                {
                    _polizaseguro = value;
                    RaisePropertyChanged("PolizaSeguro");
                }
            }
        }

        public byte[] LogoEmpresa
        {
            get { return _logoempresa; }
            set
            {
                if (_logoempresa != value)
                {
                    _logoempresa = value;
                    RaisePropertyChanged("LogoEmpresa");
                }
            }
        }

        public Visibility MostrarBotones
        {
            get
            {
				if (entity.IdEmpresa == 0)
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

            TipoFicheros = db.TipoFichero.ToList();

			if (entity.IdHistoricoEmpresa > 0)
			{
                Empresa = entity.Empresa;
                Nif = entity.Nif;
                Direccion = entity.Direccion;
                CuentaBancaria = entity.CuentaBancaria;
                CuentaContable = entity.CuentaContable;
                EjercicioFacturacion = entity.EjercicioFacturacion.ToString();
                FechaUltimaFacturacionMensual = entity.FechaUltimaFacturacionMensual;
                CodigoSepa = entity.CodigoSepa;
                FechaIae = entity.FechaIae;
                ImporteIae = entity.ImporteIae?.ToString() ?? String.Empty;
                Swift = entity.IdSwift;
                PolizaSeguro = entity.PolizaSeguro;
                LogoEmpresa = entity.LogoEmpresa;

                Trazabilidad("Maestros", "Empresas", entity.Empresa, "Consulta", "Mantenimiento Empresa Histórico");
			}
        }

        protected override void VolverListado()
        {
			base.VolverListado();
			var viewmodel = baseVM.PageViewModels.Where(m => m.Name == "Empresa Histórico Modificación").FirstOrDefault();
			viewmodel = new EmpresaHistoricoVM(baseVM, entitybase);
			baseVM.CurrentPageViewModel = viewmodel;
		}
	}
}
