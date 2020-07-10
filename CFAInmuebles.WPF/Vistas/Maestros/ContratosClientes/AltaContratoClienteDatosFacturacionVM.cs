using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;

namespace CFAInmuebles.WPF
{
    public class AltaContratoClienteDatosFacturacionVM : ObservableViewModel, IPageViewModel, IDataErrorInfo
    {
        public ContratosClientes entity;
        public Facturacion entityFacturacion;

        private HomeContratoClienteVM baseVM;
        private DateTime _fechacontrato;
        private ModalidadFactura _modalidadfactura;
        private ConceptoFacturacion _conceptofacturacion;
        private ConceptoFacturacion _conceptofacturacionimprimir;
        private string _codigoagrupacion;
        private string _importe;


        public AltaContratoClienteDatosFacturacionVM(HomeContratoClienteVM baseVM, ContratosClientes entity = null)
        {
            this.entity = entity;
            this.baseVM = baseVM;
            this.entityFacturacion = new Facturacion();
        }

        public int MaxCodigo
        {
            get { return 10; }
        }
        public string Name
        {
            get
            {
                return "Alta Contratos Clientes Datos Facturación";
            }
        } 
        public DateTime FechaContrato
        {
            get {
                CheckValidationState("FechaContrato", _fechacontrato); 
                return _fechacontrato; }
            set
            {
                if (_fechacontrato != value)
                {
                    _fechacontrato = value;                  
                    CheckValidationState("FechaAlta", _fechacontrato);
                    entityFacturacion.Fecha = FechaContrato;
                    RaisePropertyChanged("FechaContrato");
                }
            }
        }

        public string CodigoAgrupacion
        {
            get
            {
                return _codigoagrupacion;
            }
            set
            {
                if (_codigoagrupacion != value)
                {
                    _codigoagrupacion = value; 
                    entityFacturacion.CodigoAgrupacion  = CodigoAgrupacion;
                    RaisePropertyChanged("CodigoAgrupacion");
                }
            }
        }

        public string Importe
        {
            get
            {
                CheckValidationState("Importe", _fechacontrato);
                return _importe;
            }
            set
            {
                if (_importe != value)
                {
                    _importe = value;
                    CheckValidationState("Importe", _importe);
                    if (decimal.TryParse(Importe, out decimal numValue))
                        entityFacturacion.Importe = numValue;
                    RaisePropertyChanged("Importe");
                }
            }
        }

        private List<ConceptoFacturacion> _conceptosfacturacioncontrato;
        public List<ConceptoFacturacion> ConceptosFacturacionContrato
        {

            get { return _conceptosfacturacioncontrato; }
            set
            {
                if (_conceptosfacturacioncontrato != value)
                {
                    _conceptosfacturacioncontrato = value;
                    RaisePropertyChanged("ConceptosFacturacionContrato");
                }
            }
        }
        public new ConceptoFacturacion ConceptoFacturacion
        {

            get {
                CheckValidationState("ConceptoFacturacion", _conceptofacturacion); 
                return _conceptofacturacion; }
            set
            {
                if (_conceptofacturacion != value)
                {
                    _conceptofacturacion = value;
                    CheckValidationState("ConceptoFacturacion", _conceptofacturacion);
                    entityFacturacion.IdConceptoFacturacionNavigation = _conceptofacturacion;
                    RaisePropertyChanged("ConceptoFacturacion");
                }
            }
        }

        public ConceptoFacturacion ConceptoFacturacionImprimir
        {

            get
            {
                CheckValidationState("ConceptoFacturacionImprimir", _conceptofacturacionimprimir);
                return _conceptofacturacionimprimir;
            }
            set
            {
                if (_conceptofacturacionimprimir != value)
                {
                    _conceptofacturacionimprimir = value;
                    CheckValidationState("ConceptoFacturacionImprimir", _conceptofacturacionimprimir);
                    entityFacturacion.IdConceptoFacturacionImprimirNavigation = _conceptofacturacionimprimir;
                    RaisePropertyChanged("ConceptoFacturacionImprimir");
                }
            }
        }

        public new ModalidadFactura ModalidadFactura
        {

            get { return _modalidadfactura; }
            set
            {
                if (_modalidadfactura != value)
                {
                    _modalidadfactura = value;

                    entityFacturacion.IdModalidadFacturaNavigation = _modalidadfactura;


                    RaisePropertyChanged("ModalidadFactura");
                }
            }
        }

        public Visibility MostrarBotones
        {
            get
            {
                if (entity.IdContratoCliente == 0)
                {
                    return Visibility.Collapsed;
                }
                return Visibility.Visible;
            }
        }

        protected override void LoadData()
        {
            base.LoadData();

            ConceptosFacturacion = db.ConceptoFacturacion.Where(m => m.FechaEliminacion == null).ToList();
            ModalidadFacturas = db.ModalidadFactura.ToList();

            FechaContrato = DateTime.Now;

            if (entity.IdContratoCliente > 0)
                Facturas = db.Facturacion.Where(m => m.FechaEliminacion == null && m.IdContratoCliente == entity.IdContratoCliente).ToList();
        }
        protected override void ModifyData()
        {
            base.ModifyData();

            if (Errors.Count == 0)
            {
                var model = new Facturacion();
                var contrato = db.ContratosClientes.Find(entity.IdContratoCliente);
                model.IdConceptoFacturacionNavigation = ConceptoFacturacion;
                model.IdConceptoFacturacionImprimirNavigation = ConceptoFacturacionImprimir;
                model.IdContratoClienteNavigation = contrato;
                model.IdModalidadFacturaNavigation = ModalidadFactura;
                model.Importe = decimal.Parse(Importe);
                model.Fecha = FechaContrato;
                model.FechaSistema = DateTime.Now;
                model.IdUsuarioNavigation = UserId;
                db.Facturacion.Add(model);
                db.SaveChanges();
                Trazabilidad("Maestros", "Contratos Clientes", model.IdConceptoFacturacionNavigation.Conceptofacturacion1, "Alta", "Ficha Facturación");
                Facturas = db.Facturacion.Where(m => m.FechaEliminacion == null && m.IdContratoCliente == entity.IdContratoCliente).ToList();

            }
        }

        protected bool CheckValidationState<T>(string propertyName, T proposedValue)
        {
            if (propertyName == "Importe")
            {
                if (!String.IsNullOrEmpty(proposedValue as String) && !decimal.TryParse(proposedValue as String, out decimal numValue))
                {
                    SetError(propertyName, "El campo Importe debe ser numérico."); 
                    return false;
                }
                else
                {
                    SetError(propertyName, String.Empty);
                    return true;
                }
            }

            if (propertyName == "ConceptoFacturacion")
            {
                if (proposedValue == null)
                {
                    SetError(propertyName, "El campo Concepto Facturación es obligatorio.");
                    return false;
                }
                else
                {
                    SetError(propertyName, String.Empty);
                    return true;
                }
            }

            if (propertyName == "ConceptoFacturacionImprimir")
            {

                if (proposedValue == null)
                {
                    SetError(propertyName, "El campo Concepto Facturación Imprimir es obligatorio.");
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
