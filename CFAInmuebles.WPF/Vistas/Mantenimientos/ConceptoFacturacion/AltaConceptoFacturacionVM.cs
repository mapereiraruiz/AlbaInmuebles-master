using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace CFAInmuebles.WPF
{
    public class AltaConceptoFacturacionVM : ObservableViewModel, IPageViewModel, IDataErrorInfo
    {
        public ConceptoFacturacion entity;

        protected ICommand _desvincularCommand;

        private HomeConceptoFacturacionVM baseVM;
        private TipoConcepto _tipoconcepto;
        private TipoIva _tipoiva;
        private string _conceptofacturacion1;
        private string _cuentacontable;
        private bool _revisableipc;
        private string _cuentagasto;
        private bool _selectedItem;

        public AltaConceptoFacturacionVM(HomeConceptoFacturacionVM baseVM, ConceptoFacturacion entidad = null)
        {
            entity = new ConceptoFacturacion();

            if (entidad != null)
                {

                var properties = typeof(ConceptoFacturacion).GetProperties(BindingFlags.Instance | BindingFlags.Public)
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
                return "Alta Concepto de Facturación";
            }
        }

        public int MaxConceptoFacturacion
        {
            get
            {
                return 200;
            }
        }
        public int MaxCuentaContable
        {
            get
            {
                return 10;
            }
        }
        public int MaxCuentaGasto
        {
            get
            {
                return 50;
            }
        }
        public string Conceptofacturacion1
        {
            get {
                CheckValidationState("Conceptofacturacion1", _conceptofacturacion1); 
                return _conceptofacturacion1; }
            set
            {
                if (_conceptofacturacion1 != value)
                {
                    CheckValidationState("Conceptofacturacion1", _conceptofacturacion1);
                    _conceptofacturacion1 = value;
                    entity.Conceptofacturacion1 = _conceptofacturacion1;
                    RaisePropertyChanged("Conceptofacturacion1");
                }
            }
        }

       
        public new TipoConcepto TipoConcepto
        {
            get
            {
                CheckValidationState("TipoConcepto", _tipoconcepto); 
                return _tipoconcepto;
            }
            set
            {
                if (_tipoconcepto != value)
                {
                    CheckValidationState("TipoConcepto", _tipoconcepto);
                    _tipoconcepto = value;
                    entity.IdTipoConceptoNavigation = _tipoconcepto;
                    RaisePropertyChanged("TipoConcepto");
                }
            }
        }

        public new TipoIva TipoIva
        {
            get
            {
                CheckValidationState("TipoIva", _tipoiva);
                return _tipoiva;
            }
            set
            {
                if (_tipoiva != value)
                {
                    CheckValidationState("TipoIva", _tipoiva);
                    _tipoiva = value;
                    entity.IdTipoIvaNavigation = _tipoiva;
                    RaisePropertyChanged("TipoIva");
                }
            }
        }

        public string CuentaContable
        {
            get
            {
                CheckValidationState("CuentaContable", _cuentacontable);
                return _cuentacontable;
            }
            set
            {
                if (_cuentacontable != value)
                {
                    CheckValidationState("CuentaContable", _cuentacontable);
                    _cuentacontable = value;
                    entity.CuentaContable = _cuentacontable;
                    RaisePropertyChanged("CuentaContable");
                }
            }
        }

        public bool RevisableIpc
        {
            get { return _revisableipc; }
            set
            {
                if (_revisableipc != value)
                {
                    _revisableipc = value;
                    entity.RevisableIpc = _revisableipc;
                    RaisePropertyChanged("RevisableIpc");
                }
            }
        }

        public string CuentaGasto
        {
            get { return _cuentagasto; }
            set
            {
                if (_cuentagasto != value)
                {
                    _cuentagasto = value;
                    entity.CuentaGasto = _cuentagasto;
                    RaisePropertyChanged("CuentaGasto");
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
                if (entity.IdConceptoFacturacion == 0)
                {
                    return Visibility.Collapsed;
                }
                return Visibility.Visible;
            }
        }

        public ICommand DesvincularCommand
        {
            get
            {
                if (_desvincularCommand == null)
                {
                    _desvincularCommand = new RelayCommand(p => DesvincularContratos());
                }
                return _desvincularCommand;
            }
        }

        protected override void LoadData()
        {
            base.LoadData();

            TipoConceptos = db.TipoConcepto.ToList();
            TipoIvas = db.TipoIva.ToList();
            CatalogarConceptos = db.CatalogarConcepto.ToList();
            Empresas = db.Empresas.Where(m => m.FechaEliminacion == null).ToList();

            if (entity.IdConceptoFacturacion > 0)
            {
                Conceptofacturacion1 = entity.Conceptofacturacion1;
                CuentaContable = entity.CuentaContable;
                RevisableIpc = entity.RevisableIpc;
                CuentaGasto = entity.CuentaGasto;
                TipoConcepto = entity.IdTipoConceptoNavigation;
                TipoIva = entity.IdTipoIvaNavigation;
                CatalogarConcepto = entity.IdCatalogarConceptoNavigation;
                Empresa = entity.IdEmpresaNavigation;
                Trazabilidad("Mantenimientos", "Concepto Facturación", Conceptofacturacion1, "Consulta", "Ficha Concepto Facturación");
            }
        }

        protected override void ModifyData()
        {
            base.ModifyData();
            string accion = "Update";

            if (Errors.Count == 0)
            {

                var model = db.ConceptoFacturacion.Find(entity?.IdConceptoFacturacion);

                if (model == null)
                {
                    model = new ConceptoFacturacion();
                    db.ConceptoFacturacion.Add(model);
                    accion = "Alta";
                }

                model.Conceptofacturacion1 = Conceptofacturacion1;
                model.CuentaContable = CuentaContable;
                model.CuentaGasto = CuentaGasto;
                model.RevisableIpc = RevisableIpc;
                model.IdTipoConceptoNavigation = TipoConcepto;
                model.IdTipoIvaNavigation = TipoIva;
                model.IdCatalogarConceptoNavigation = CatalogarConcepto;
                model.FechaSistema = DateTime.Now;
                model.IdUsuarioNavigation = UserId;
                model.IdEmpresaNavigation = Empresa;

                db.SaveChanges();
                Trazabilidad("Mantenimientos", "Concepto Facturación", Conceptofacturacion1, accion, "Ficha Concepto Facturación");
                baseVM.ChangePageCommand.Execute(baseVM.PageViewModels.Where(m => m.Name == "Mantenimiento Concepto de Facturación").FirstOrDefault());
            }
        }

        protected override void DeleteData()
        {
            base.DeleteData();

            if (entity != null)
            {
                var model = db.ConceptoFacturacion.Find(entity.IdConceptoFacturacion);

                //Comprobamos que no tenga ninguna entidad obligatoria relacionada
                var contratos = db.ContratosClientes.Where(m => m.IdConceptoFacturacion == entity.IdConceptoFacturacion && m.FechaEliminacion == null).Any();
                var facturacion = db.Facturacion.Where(m => m.IdConceptoFacturacion == entity.IdConceptoFacturacion && m.FechaEliminacion == null).Any();
                var historicofacturacion = db.HistoricoConceptoFacturacion.Where(m => m.IdConceptoFacturacion == entity.IdConceptoFacturacion).Any();
                var inmuebles = db.Inmuebles.Where(m => m.IdConceptoFacturacion == entity.IdConceptoFacturacion && m.FechaEliminacion == null).Any();

                if (!contratos && !facturacion && !historicofacturacion && !inmuebles)
                {
                    var viewmodel = PageViewModels.Where(m => m.Name == "Eliminar Concepto de Facturación").FirstOrDefault();
                    viewmodel = new DeleteConceptoFacturacionVM(baseVM, entity);
                    baseVM.CurrentPageViewModel = viewmodel;
                }
                else
                {
                    Mensaje = String.Empty;

                    if (contratos)
                        Mensaje = "Desvincule los Contratos vinculados a este Concepto de Facturación para poder eliminarlo." + Environment.NewLine;
                    if (facturacion)
                        Mensaje += "Desvincule las Facturas vinculadas a este Concepto de Facturación para poder eliminarlo." + Environment.NewLine;
                    if (historicofacturacion)
                        Mensaje += "Desvincule los Históricos de Facturación vinculados a este Concepto de Facturación para poder eliminarlo." + Environment.NewLine;
                    if (inmuebles)
                        Mensaje += "Desvincule los Inmuebles vinculados a este Concepto de Facturación para poder eliminarlo.";
                }
            }
        }

        protected void DesvincularContratos()
        {
           
        }

        protected override void VolverListado()
        {
            base.VolverListado();
            baseVM.ChangePageCommand.Execute(baseVM.PageViewModels.Where(m => m.Name == "Mantenimiento Concepto de Facturación").FirstOrDefault());
        }

        protected bool CheckValidationState<T>(string propertyName, T proposedValue)
        {
            if (propertyName == "Conceptofacturacion1")
            {
                var existe = db.ConceptoFacturacion.Where(m => m.Conceptofacturacion1 == proposedValue as String && m.FechaEliminacion == null).FirstOrDefault();

                if (String.IsNullOrEmpty(proposedValue as String))
                {
                    SetError(propertyName, "El campo Concepto Facturación es obligatorio.");
                    return false;
                }

                else if (existe != null && existe.IdConceptoFacturacion != entity.IdConceptoFacturacion)
                {
                    SetError(propertyName, "Ya existe un Concepto Facturación con ese valor.");
                    return false;
                }

                else
                {
                    SetError(propertyName, String.Empty);
                    return true;
                }
            }

            if (propertyName == "TipoConcepto")
            {

                if (proposedValue == null)
                {
                    SetError(propertyName, "El campo Tipo Concepto es obligatorio.");
                    return false;
                }

                else
                {
                    SetError(propertyName, String.Empty);
                    return true;
                }
            }

            if (propertyName ==  "TipoIva")
            {

                if (proposedValue == null)
                {
                    SetError(propertyName, "El campo Tipo Iva es obligatorio.");
                    return false;
                }

                else
                {
                    SetError(propertyName, String.Empty);
                    return true;
                }
            }

            if (propertyName == "CuentaContable")
            {

                if (String.IsNullOrEmpty(proposedValue as String))
                {
                    SetError(propertyName, "El campo Cuenta Contable es obligatorio.");
                    return false;
                }
 
                else
                {
                    SetError(propertyName, String.Empty);
                    return true;
                }
            }

            if (propertyName == "Incremento")
            {

                if (proposedValue != null && !decimal.TryParse(proposedValue as String, out decimal numValue))
                {
                    SetError(propertyName, "El campo Incremento debe ser numérico.");
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

