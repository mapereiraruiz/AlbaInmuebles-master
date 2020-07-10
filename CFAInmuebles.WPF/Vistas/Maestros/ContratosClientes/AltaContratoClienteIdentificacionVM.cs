using CFAInmuebles.Domain.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace CFAInmuebles.WPF
{
    public class AltaContratoClienteIdentificacionVM : ObservableViewModel, IPageViewModel, IDataErrorInfo
    {
        public ContratosClientes entity;

        private HomeContratoClienteVM baseVM;
        private AltaContratoClienteVM ficha;
        private DateTime? _fechaalta;
        private DateTime? _fechainiciofacturacion;
        private DateTime? _fechaultimafacturacion;
        private string _cuentabancaria;
        private string _direccionenvio;
        private string _archivodigital;
        private string _agruparcontrato;
        private string _numerocontrato;
        private TipoIpc _tipoipc;
        private Swift _swift;
        private FormasDePago _formapago;
        protected ICommand _openDialogCommand;

        public AltaContratoClienteIdentificacionVM(HomeContratoClienteVM baseVM, ContratosClientes entity = null)
        {
            this.entity = entity;
            this.baseVM = baseVM;
            ficha = new AltaContratoClienteVM(baseVM, entity);
        }
        public string Name
        {
            get
            {
                return "Alta Contratos Clientes Identificación";
            }
        } 
        public int MaxCuentaBancaria
        {
            get { return 20; }
        }
        public int MaxDireccion
        {
            get { return 250; }
        }
        public int MaxArchivoDigital
        {
            get { return 50; }
        }
        public int MaxAgruparContrato
        {
            get { return 50; }
        }

        public ICommand OpenDialogCommand
        {
            get
            {
                if (_openDialogCommand == null)
                {
                    _openDialogCommand = new RelayCommand(p => OpenDialog());
                }
                return _openDialogCommand;
            }
        }
        public new TipoIpc TipoIpc
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
                    entity.IdTipoIpcNavigation = _tipoipc;
                    RaisePropertyChanged("TipoIpc");
                }
            }
        }
        public new Swift Swift
        {
            get
            {
                return _swift;
            }
            set
            {
                if (_swift != value)
                {
                    _swift = value;
                    entity.IdSwiftNavigation = _swift;
                    RaisePropertyChanged("Swift");
                }
            }
        }
        public new FormasDePago FormaDePago
        {
            get
            {
                return _formapago;
            }
            set
            {
                if (_formapago != value)
                {
                    _formapago = value;
                    entity.IdFormaPagoNavigation = _formapago;
                    RaisePropertyChanged("FormaDePago");
                }
            }
        }
        public DateTime? FechaAlta
        {
            get {
                CheckValidationState("FechaAlta", _fechaalta); 
                return _fechaalta; }
            set
            {
                if (_fechaalta != value)
                {
                    _fechaalta = value;
                    CheckValidationState("FechaAlta", _fechaalta);

                    if (FechaAlta != null)
                        entity.FechaAlta = FechaAlta ?? DateTime.Now;
                    RaisePropertyChanged("FechaAlta");
                }
            }
        }
        public string CuentaBancaria
        {
            get { return _cuentabancaria; }
            set
            {
                if (_cuentabancaria != value)
                {
                    _cuentabancaria = value;
                    entity.CuentaBancaria = CuentaBancaria;
                    RaisePropertyChanged("CuentaBancaria");
                }
            }
        }
        public string DireccionEnvio
        {
            get { return _direccionenvio; }
            set
            {
                if (_direccionenvio != value)
                {
                    _direccionenvio = value;
                    entity.DireccionEnvio = DireccionEnvio;
                    RaisePropertyChanged("DireccionEnvio");
                }
            }
        }

        public string NumeroContrato
        {
            get
            {
                CheckValidationState("NumeroContrato", _numerocontrato);
                return _numerocontrato;
            }
            set
            {
                if (_numerocontrato != value)
                {
                    _numerocontrato = value;
                    CheckValidationState("NumeroContrato", _numerocontrato);
                    if (int.TryParse(NumeroContrato, out int numValue))
                        entity.NumeroContrato = numValue;
                    RaisePropertyChanged("NumeroContrato");
                }
            }
        }

        public string ArchivoDigital
        {
            get { return _archivodigital; }
            set
            {
                if (_archivodigital != value)
                {
                    _archivodigital = value;
                    entity.ArchivoDigital = ArchivoDigital;
                    RaisePropertyChanged("ArchivoDigital");
                }
            }
        }

        public string AgruparContrato
        {
            get { return _agruparcontrato; }
            set
            {
                if (_agruparcontrato != value)
                {
                    _agruparcontrato = value;
                    entity.AgruparContrato = AgruparContrato;
                    RaisePropertyChanged("AgruparContrato");
                }
            }
        }


        public DateTime? FechaInicioFacturacion
        {
            get {
                CheckValidationState("FechaInicioFacturacion", _fechainiciofacturacion); 
                return _fechainiciofacturacion; }
            set
            {
                if (_fechainiciofacturacion != value)
                {
                    CheckValidationState("FechaInicioFacturacion", _fechainiciofacturacion);
                    _fechainiciofacturacion = value;

                    if (FechaInicioFacturacion != null)
                        entity.FechaInicioFacturacion = FechaInicioFacturacion ?? DateTime.Now;

                    RaisePropertyChanged("FechaInicioFacturacion");
                }
            }
        }

        public DateTime? FechaUltimaFacturacion
        {
            get { return _fechaultimafacturacion; }
            set
            {
                if (_fechaultimafacturacion != value)
                {
                    _fechaultimafacturacion = value;
                    entity.FechaUltimaFacturacion = FechaUltimaFacturacion;
                    RaisePropertyChanged("FechaUltimaFacturacion");
                }
            }
        }
        protected override void LoadData()
        {
            base.LoadData();

            TipoIpcs = db.TipoIpc.Where(m => m.FechaEliminacion == null).ToList();
            FormasDePago = db.FormasDePago.Where(m => m.FechaEliminacion == null).ToList();
            Swifts = db.Swift.Where(m => m.FechaEliminacion == null).ToList();

            if (entity != null)
            {
                TipoIpc = entity.IdTipoIpcNavigation;
                FormaDePago = entity.IdFormaPagoNavigation;
                Swift = entity.IdSwiftNavigation;
                FechaAlta = entity.FechaAlta;
                FechaInicioFacturacion = entity.FechaInicioFacturacion;
                FechaUltimaFacturacion = entity.FechaUltimaFacturacion;
                ArchivoDigital = entity.ArchivoDigital;
                DireccionEnvio = entity.DireccionEnvio;
                CuentaBancaria = entity.CuentaBancaria;
                AgruparContrato = entity.AgruparContrato;
                NumeroContrato = entity.NumeroContrato.ToString();
                
                if (FechaAlta?.ToShortDateString() == "01/01/0001")
                    FechaAlta = DateTime.Now;

                if (FechaInicioFacturacion?.ToShortDateString() == "01/01/0001")
                    FechaInicioFacturacion = DateTime.Now;


            }
        }
        protected void OpenDialog()
        {
            var of = new OpenFileDialog();
            of.Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg|All files (*.*)|*.*";
            var res = of.ShowDialog();
            if (res.HasValue)
            {
                ArchivoDigital = of.FileName;
            }
        }
        protected bool CheckValidationState<T>(string propertyName, T proposedValue)
        {
            baseVM.CheckValidationState(propertyName, proposedValue);

            if (propertyName == "NumeroContrato")
            {
                if (proposedValue == null)
                {
                    SetError(propertyName, "El campo Referencia Contrato es obligatorio");
                    return false;
                }


                if (!String.IsNullOrEmpty(proposedValue as String) && !int.TryParse(proposedValue as String, out int numValue))
                {
                    SetError(propertyName, "El campo Referencia Contrato debe ser numérico.");
                    return false;
                }
                else
                {
                    SetError(propertyName, String.Empty);
                    return true;
                }
            }

            if (propertyName == "FechaAlta")
            {
                if (proposedValue == null)
                {
                    SetError(propertyName, "El campo Fecha Alta es obligatorio");
                    return false;
                }

                else
                {
                    SetError(propertyName, String.Empty);
                    return true;
                }
            }

            if (propertyName == "FechaInicioFacturacion")
            {
                if (proposedValue == null)
                {
                    SetError(propertyName, "El campo Fecha Inicio Facturación es obligatorio");
                    return false;
                }

                else
                {
                    SetError(propertyName, String.Empty);
                    return true;
                }
            }

            return true;
        }
    }
}
