using CFAInmuebles.Domain.Models;
using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.ComponentModel;
using System.Reflection;
using System.Collections.Generic;
using CFAInmuebles.Infrastructure.Data;
using DevExpress.Xpf.RichEdit;
using DevExpress.XtraRichEdit;

namespace CFAInmuebles.WPF
{
    public partial class CustomRtfToContentConverter : RtfToContentConverter
    {
        protected override object ConvertToTargetType(RichEditControl control)
        {
            return control.Document.IsEmpty ? "" : base.ConvertToTargetType(control);
        }
    }
    public class AltaEmpresasVM : ObservableViewModel, IPageViewModel, IDataErrorInfo
    {
        public Empresas entity;

        protected ICommand _openDialogCommand;

        private HomeEmpresasVM baseVM;
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
        private Swift _swift;
        private string _polizaseguro;
        private byte[] _logoempresa;
        private string _piefactura;
        private string _lateralfactura;
        private bool _selectedItem;
        public virtual object DataBaseDocumentSource { get; set; }

        public AltaEmpresasVM(HomeEmpresasVM baseVM, Empresas entidad = null)
        {
            entity = new Empresas();

            if (entidad != null)
            {
                var properties = typeof(Empresas).GetProperties(BindingFlags.Instance | BindingFlags.Public)
                  .Where(p => !(p.PropertyType.IsInterface && p.PropertyType.IsGenericType &&
                                p.PropertyType.GetGenericTypeDefinition() == typeof(ICollection<>))
                               && p.CanWrite).ToList();
                properties.ForEach(p =>
                {
                    p.GetValue(entidad);
                    p.SetValue(entity, p.GetValue(entidad));
                });
            }

            this.baseVM = baseVM;
        }

        public string Name
        {
            get
            {
                return "Alta Empresas";
            }
        }

        public int MaxEmpresa
        {
            get { return 100; }
        }
        public int MaxNif
        {
            get { return 9; }
        }
        public int MaxDireccion
        {
            get { return 250; }
        }
        public int MaxCuentaBancaria
        {
            get { return 20; }
        }
        public int MaxCuentaContable
        {
            get { return 10; }
        }
        public int MaxPolizaSeguro
        {
            get { return 50; }
        }
        public int MaxCodigoSEPA
        {
            get { return 50; }
        }

        public string LateralFactura
        {
            get
            {
                return _lateralfactura;
            }
            set
            {
                if (_lateralfactura != value)
                {

                    _lateralfactura = value;
                    entity.LateralFactura = value;
                    RaisePropertyChanged("LateralFactura");
                }
            }
        }
        public string PieFactura
        {
            get
            {
                return _piefactura;
            }
            set
            {
                if (_piefactura != value)
                {

                    _piefactura = value;
                    entity.PieFactura = value;
                    RaisePropertyChanged("PieFactura");
                }
            }
        }
        public new string Empresa
        {
            get {
                CheckValidationState("Empresa", _empresa); 
                return _empresa; }
            set
            {
                if (_empresa != value)
                {
                    CheckValidationState("Empresa", _empresa);
                    _empresa = value;
                    entity.Empresa = _empresa;
                    RaisePropertyChanged("Empresa");
                }
            }
        }

        public string Nif
        {
            get {
                CheckValidationState("Nif", _nif); 
                return _nif; }
            set
            {
                if (_nif != value)
                {
                    CheckValidationState("Nif", _nif);
                    _nif = value;
                    entity.Nif = _nif;
                    RaisePropertyChanged("Nif");
                }
            }
        }

        public string Direccion
        {
            get {
                CheckValidationState("Direccion", _direccion);
                return _direccion; }
            set
            {
                if (_direccion != value)
                {
                    CheckValidationState("Direccion", _direccion);
                    _direccion = value;
                    entity.Direccion = _direccion;
                    RaisePropertyChanged("Direccion");
                }
            }
        }

        public string CuentaBancaria
        {
            get {
                CheckValidationState("CuentaBancaria", _cuentabancaria); 
                return _cuentabancaria; }
            set
            {
                if (_cuentabancaria != value)
                {
                    CheckValidationState("CuentaBancaria", _cuentabancaria);
                    _cuentabancaria = value;
                    entity.CuentaBancaria = _cuentabancaria;
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
                    entity.CuentaContable = _cuentacontable;
                    RaisePropertyChanged("CuentaContable");
                }
            }
        }

        public string EjercicioFacturacion
        {
            get {
                CheckValidationState("EjercicioFacturacion", _ejercicio); 
                return _ejercicio; }
            set
            {
                if (_ejercicio != value)
                {
                    CheckValidationState("EjercicioFacturacion", _ejercicio);
                    _ejercicio = value;

                    if (int.TryParse(EjercicioFacturacion, out int numValue))
                        entity.EjercicioFacturacion = int.Parse(EjercicioFacturacion);

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
                    entity.CodigoSepa = _codigosepa;
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
                    entity.FechaUltimaFacturacionMensual = _fechaultimafacturacionmensual;
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
                    entity.FechaIae = _fechaiae;
                    RaisePropertyChanged("FechaIae");
                }
            }
        }

        public string ImporteIae
        {
            get {
                CheckValidationState("ImporteIae", _importeiae);
                return _importeiae; }
            set
            {
                if (_importeiae != value)
                {
                    CheckValidationState("ImporteIae", _importeiae);
                    _importeiae = value;

                    if (decimal.TryParse(ImporteIae, out decimal numValue))
                        entity.ImporteIae = decimal.Parse(ImporteIae);

                    RaisePropertyChanged("ImporteIae");
                }
            }
        }

        public new Swift Swift
        {
            get { return _swift; }
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

        public string PolizaSeguro
        {
            get { return _polizaseguro; }
            set
            {
                if (_polizaseguro != value)
                {
                    _polizaseguro = value;
                    entity.PolizaSeguro = _polizaseguro;
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
                    entity.LogoEmpresa = _logoempresa;
                    RaisePropertyChanged("LogoEmpresa");
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

        public Visibility MostrarBotones
        {
            get
            {
                if (entity.IdEmpresa  == 0)
                {
                    return Visibility.Collapsed;
                }
                return Visibility.Visible;
            }
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

        bool modified;
        public void UpdateModified(bool Modified)
        {
            this.modified = Modified;
        }

        
        public void Save(string rtfText)
        {
            entity.PieFactura = rtfText;
            modified = false;
        }
        public bool CanSave(string rtfText)
        {
            return modified != false;
        }
        protected override void LoadData()
        {
            base.LoadData();
            Swifts = db.Swift.Where(m => m.FechaEliminacion == null).ToList();
            if (entity.IdEmpresa > 0)
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
                Swift = entity.IdSwiftNavigation;
                PolizaSeguro = entity.PolizaSeguro;
                LogoEmpresa = entity.LogoEmpresa;
                PieFactura = entity.PieFactura;
                LateralFactura = entity.LateralFactura;
                Trazabilidad("Maestros", "Empresas", Empresa, "Consulta", "Ficha Empresas");
            }
        }

        protected override void ModifyData()
        {
            base.ModifyData();
            string accion = "Update";

            if (Errors.Count == 0)
            {
                entity.FechaSistema = DateTime.Now;
                entity.IdUsuarioNavigation = UserId;

                var model = db.Empresas.Find(entity?.IdEmpresa);

                if (model == null)
                {
                    db.Empresas.Add(entity);
                    accion = "Alta";
                }
                else
                {
                    var newEntity = db.Empresas.ApplyValues(entity, entity.IdEmpresa);
                }

                db.SaveChanges();

                //Grabamos cambios
                if (accion == "Update")
                {
                    var historico = new HistoricoEmpresas();
                    historico.IdEmpresaNavigation = model;
                    historico.Empresa = entity.Empresa;
                    historico.Nif = entity.Nif;
                    historico.Direccion = entity.Direccion;
                    historico.CuentaBancaria = entity.CuentaBancaria;
                    historico.CuentaContable = entity.CuentaContable;
                    historico.EjercicioFacturacion = entity.EjercicioFacturacion;
                    historico.FechaUltimaFacturacionMensual = entity.FechaUltimaFacturacionMensual;
                    historico.CodigoSepa = entity.CodigoSepa;
                    historico.FechaIae = entity.FechaIae;
                    historico.ImporteIae = entity.ImporteIae;
                    historico.IdSwiftNavigation = entity.IdSwiftNavigation;
                    historico.PolizaSeguro = entity.PolizaSeguro;
                    historico.FechaSistema = DateTime.Now;
                    historico.IdUsuarioNavigation  = entity.IdUsuarioNavigation;
                    historico.LogoEmpresa = entity.LogoEmpresa;
                    db.HistoricoEmpresas.Add(historico);
                    db.SaveChanges();
                }

                Trazabilidad("Maestros", "Empresas", Empresa, accion, "Ficha Empresas");
                baseVM.ChangePageCommand.Execute(baseVM.PageViewModels.Where(m => m.Name == "Mantenimiento Empresas").FirstOrDefault());
            }
        }

        protected override void DeleteData()
        {
            base.DeleteData();

            var model = db.Empresas.Find(entity.IdEmpresa);

            //Comprobamos que no tenga ninguna entidad relacionada
          
            var gastos = db.Gastos.Where(m => m.FechaEliminacion == null && m.IdEmpresa == entity.IdEmpresa).Any();
            var historicofacturacion = db.HistoricoFacturacion.Where(m => m.IdEmpresa == entity.IdEmpresa).Any();
            var historicoseguros = db.HistoricoSeguros.Where(m => m.FechaEliminacion == null && m.IdEmpresa == entity.IdEmpresa).Any();
            var historicotasaciones = db.HistoricoTasaciones.Where(m => m.FechaEliminacion == null && m.IdEmpresa == entity.IdEmpresa).Any();
            var inmuebles = db.Inmuebles.Where(m => m.FechaEliminacion == null && m.IdEmpresa == entity.IdEmpresa).Any();     
            var tiposiva = db.TipoIva.Where(m => m.FechaEliminacion == null && m.IdEmpresa == entity.IdEmpresa).Any();

            if ( !gastos && !historicofacturacion && !historicoseguros && !historicotasaciones && !inmuebles  && !tiposiva)
            {
                var viewmodel = PageViewModels.Where(m => m.Name == "Eliminar Empresas").FirstOrDefault();
                viewmodel = new DeleteEmpresasVM(baseVM, entity);
                baseVM.CurrentPageViewModel = viewmodel;
            }
            else
            {
                Mensaje = String.Empty;

                if (gastos)
                    Mensaje += "Desvincule los Gastos vinculados a esta Empresa para poder eliminarla. ";
                if (historicofacturacion)
                    Mensaje += "Desvincule los Históricos de Facturación vinculados a esta Empresa para poder eliminarla. ";
                if (historicoseguros)
                    Mensaje += "Desvincule los Históricos de seguros vinculados a esta Empresa para poder eliminarla. ";
                if (historicotasaciones)
                    Mensaje += "Desvincule los Históricos de Tasaciones vinculados a esta Empresa para poder eliminarla. ";
                if (inmuebles)
                    Mensaje += "Desvincule los Inmuebles vinculados a esta Empresa para poder eliminarla. ";         
                if (tiposiva)
                    Mensaje += "Desvincule los Tipos de Iva vinculados a esta Empresa para poder eliminarla. ";
            }
        }
      
        protected override void VolverListado()
        {
            base.VolverListado();
            baseVM.ChangePageCommand.Execute(baseVM.PageViewModels.Where(m => m.Name == "Mantenimiento Empresas").FirstOrDefault());
        }

        protected void OpenDialog()
        {
            var of = new OpenFileDialog();
            of.Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg|All files (*.*)|*.*";
            var res = of.ShowDialog();
            if (res.HasValue)
            {
                BitmapImage imagen = new BitmapImage(new Uri(of.FileName));
                JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(imagen));
                using (MemoryStream ms = new MemoryStream())
                {
                    encoder.Save(ms);
                    LogoEmpresa = ms.ToArray();
                }
            }
        }

        protected bool CheckValidationState<T>(string propertyName, T proposedValue)
        {
            if (propertyName == "Empresa")
            {
                var existe = db.Empresas.Where(m => m.Empresa == proposedValue as String && m.FechaEliminacion == null).FirstOrDefault();
                
                if (Mensaje != null)
                {
                    Mensaje = Mensaje.Replace("* El campo Empresa es obligatorio. ", "");
                    Mensaje = Mensaje.Replace("* Ya existe una Empresa con ese valor. ", "");
                }

                if (String.IsNullOrEmpty(proposedValue as String))
                {
                    SetError(propertyName, "*");
                    Mensaje += "* El campo Empresa es obligatorio. ";
                    return false;
                }

                else if (existe != null && existe.IdEmpresa != entity.IdEmpresa)
                {
                    SetError(propertyName, "*");
                    Mensaje += "* Ya existe una Empresa con ese valor. ";
                    return false;
                }

                else
                {
                    SetError(propertyName, String.Empty);                   
                    return true;
                }
            }

            if (propertyName == "Nif")
            {
                if (Mensaje != null)
                {
                    Mensaje = Mensaje.Replace("* El campo Nif es obligatorio. ", "");
                    Mensaje = Mensaje.Replace("* El campo Nif no puede tener una longitud mayor a 9. ", "");
                }

                if (String.IsNullOrEmpty(proposedValue as String))
                {
                    SetError(propertyName, "*");
                    Mensaje += "* El campo Nif es obligatorio. ";
                    return false;
                }

                else if (proposedValue != null && (proposedValue as String).Length > 9)
                {
                    SetError(propertyName, "*");
                    Mensaje += "* El campo Nif no puede tener una longitud mayor a 9. ";
                    return false;
                }


                else
                {
                    SetError(propertyName, String.Empty);
                    Mensaje = Mensaje.Replace("* El campo Nif es obligatorio. ", "");
                    return true;
                }
            }

            if (propertyName == "Direccion")
            {
                if (String.IsNullOrEmpty(proposedValue as String))
                {
                    SetError(propertyName, "*");
                    Mensaje += "* El campo Dirección es obligatorio. ";
                    return false;
                }

                else
                {
                    SetError(propertyName, String.Empty);
                    Mensaje = Mensaje.Replace("* El campo Dirección es obligatorio. ", "");
                    return true;
                }
            }

            if (propertyName == "CuentaBancaria")
            {
                if (String.IsNullOrEmpty(proposedValue as String))
                {
                    SetError(propertyName, "*");
                    Mensaje += "* El campo Cuenta Bancaria es obligatorio. ";
                    return false;
                }

                else
                {
                    SetError(propertyName, String.Empty);
                    Mensaje = Mensaje.Replace("* El campo Cuenta Bancaria es obligatorio. ", "");
                    return true;
                }
            }

            if (propertyName == "EjercicioFacturacion")
            {
                if (Mensaje != null)
                {
                    Mensaje = Mensaje.Replace("* El campo Ejercicio Facturación debe ser un entero. ", "");
                    Mensaje = Mensaje.Replace("* El campo Ejercicio Facturación es obligatorio. ", "");
                }

                if (String.IsNullOrEmpty(proposedValue as String))
                {
                    SetError(propertyName, "*");
                    Mensaje += "* El campo Ejercicio Facturación es obligatorio. ";
                    return false;
                }

                else if (proposedValue != null && !int.TryParse(proposedValue as String, out int numValue))
                {
                    SetError(propertyName, "*");                  
                    Mensaje += "* El campo Ejercicio Facturación debe ser un entero. ";
                    return false;
                }

                else
                {
                    SetError(propertyName, String.Empty);
                    return true;
                }
            }

            if (propertyName == "ImporteIae")
            {
                if (!String.IsNullOrEmpty(proposedValue as String) && !decimal.TryParse(proposedValue as String, out decimal numValue))
                {
                    SetError(propertyName, "*");
                    Mensaje = Mensaje.Replace("* El campo Importe Iae debe ser numérico. ", "");
                    Mensaje += "* El campo Importe Iae debe ser numérico. ";
                    return false;
                }

                else
                {
                    Mensaje = Mensaje.Replace("* El campo Importe Iae debe ser numérico. ", "");
                    SetError(propertyName, String.Empty);
                    return true;
                }
            }

            return true;
        }
    } 
}

