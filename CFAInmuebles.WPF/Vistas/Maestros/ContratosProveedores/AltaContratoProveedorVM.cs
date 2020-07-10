using CFAInmuebles.Domain.Models;
using System;
using System.Linq;
using System.ComponentModel;
using System.Windows;
using System.Reflection;
using System.Collections.Generic;
using CFAInmuebles.Infrastructure.Data;

namespace CFAInmuebles.WPF
{
    public class AltaContratoProveedorVM : ObservableViewModel, IPageViewModel, IDataErrorInfo
    {
        public ContratosProveedores entity;

        private HomeContratoProveedorVM baseVM;     
        private Inmuebles _inmueble;
        private Periodicidad _periodicidadcoste;
        private Periodicidad _periodicidadrevision;
        private ContratosProveedores _contratoproveedor;
        private string _referenciacontrato;
        private string _servicio;
        private string _costeanual;
        private string _importefactura;
        private DateTime? _fecha;
        private DateTime? _fechabaja;
        private DateTime? _fechapreaviso;
        private DateTime? _fechavencimiento;
        private string _archivodigital;
        private bool _selectedItem;
        

        public AltaContratoProveedorVM(HomeContratoProveedorVM baseVM, ContratosProveedores entidad = null)
        {
            entity = new ContratosProveedores();

            if (entidad != null)
            {
                var properties = typeof(ContratosProveedores).GetProperties(BindingFlags.Instance | BindingFlags.Public)
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
                return "Alta Contratos Proveedores";
            }
        }

        public int MaxReferencia
        {
            get { return 50;   }
          
        }

        public int MaxArchivoDigital
        {
            get { return 200; }
        }
        public new Inmuebles Inmueble
        {
            get {
                CheckValidationState("Inmueble", _inmueble); 
                return _inmueble; }
            set
            {
                if (_inmueble != value)
                {
                    CheckValidationState("Inmueble", _inmueble);
                    _inmueble = value;
                    entity.IdInmuebleNavigation = _inmueble;
                    RaisePropertyChanged("Inmueble");
                }
            }
        }

        public new Periodicidad PeriodicidadCoste
        {
            get
            {
                return _periodicidadcoste;
            }
            set
            {
                if (_periodicidadcoste != value)
                {
                    _periodicidadcoste = value;
                    entity.IdPeriodicidadCosteNavigation = _periodicidadcoste;
                    RaisePropertyChanged("PeriodicidadCoste");
                }
            }
        }
        public new Periodicidad PeriodicidadRevision
        {
            get
            {
                return _periodicidadrevision;
            }
            set
            {
                if (_periodicidadrevision != value)
                {
                    _periodicidadrevision = value;
                    entity.IdPeriodicidadRevisionNavigation = _periodicidadrevision;
                    RaisePropertyChanged("PeriodicidadRevision");
                }
            }
        }


        public new ContratosProveedores ContratoProveedor
        {
            get
            {
                CheckValidationState("ContratoProveedor", _contratoproveedor);
                return _contratoproveedor;
            }
            set
            {
                if (_contratoproveedor != value)
                {
                    CheckValidationState("ContratoProveedor", _contratoproveedor);
                    _contratoproveedor = value;
                    entity.NombreProveedor = _contratoproveedor.NombreProveedor;
                    RaisePropertyChanged("ContratoProveedor");
                }
            }
        }

        public string ReferenciaContrato
        {
            get {
                CheckValidationState("ReferenciaContrato", _referenciacontrato); 
                return _referenciacontrato; }
            set
            {
                if (_referenciacontrato != value)
                {
                    CheckValidationState("ReferenciaContrato", _referenciacontrato);
                    _referenciacontrato = value;
                    entity.ReferenciaContrato = _referenciacontrato;
                    RaisePropertyChanged("ReferenciaContrato");
                }
            }
        }

        public string Servicio
        {
            get { return _servicio; }
            set
            {
                if (_servicio != value)
                {
                    _servicio = value;
                    entity.Servicio = _servicio;
                    RaisePropertyChanged("Servicio");
                }
            }
        }

        public string CosteAnual
        {
            get
            {
                CheckValidationState("CosteAnual", _costeanual);
                return _costeanual;
            }
            set
            {
                if (_costeanual != value)
                {
                    CheckValidationState("CosteAnual", _costeanual);
                    _costeanual = value;

                    if (decimal.TryParse(CosteAnual, out decimal numValue))
                        entity.CosteAnual = decimal.Parse(CosteAnual);
                    RaisePropertyChanged("CosteAnual");
                }
            }
        }

        public string ImporteFactura
        {
            get
            {
                CheckValidationState("ImporteFactura", _importefactura);
                return _importefactura;
            }
            set
            {
                if (_importefactura != value)
                {
                    CheckValidationState("ImporteFactura", _importefactura);
                    _importefactura = value;

                    if (decimal.TryParse(ImporteFactura, out decimal numValue))
                        entity.ImporteFactura = decimal.Parse(ImporteFactura);
                    RaisePropertyChanged("ImporteFactura");
                }
            }
        }

        public DateTime? Fecha
        {
            get { return _fecha; }
            set
            {
                if (_fecha != value)
                {
                    _fecha = value;
                    entity.Fecha = _fecha;
                    RaisePropertyChanged("Fecha");
                }
            }
        }

        public DateTime? FechaBaja
        {
            get { return _fechabaja; }
            set
            {
                if (_fechabaja != value)
                {
                    _fechabaja = value;
                    entity.FechaBaja = _fechabaja;
                    RaisePropertyChanged("FechaBaja");
                }
            }
        }

        public DateTime? FechaPreaviso
        {
            get { return _fechapreaviso; }
            set
            {
                if (_fechapreaviso != value)
                {
                    _fechapreaviso = value;
                    entity.FechaPreaviso = _fechapreaviso;
                    RaisePropertyChanged("FechaPreaviso");
                }
            }
        }

        public DateTime? FechaVencimiento
        {
            get { return _fechavencimiento; }
            set
            {
                if (_fechavencimiento != value)
                {
                    _fechavencimiento = value;
                    entity.FechaVencimiento = _fechavencimiento;
                    RaisePropertyChanged("FechaVencimiento");
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
                    entity.ArchivoDigital = _archivodigital;
                    RaisePropertyChanged("ArchivoDigital");
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
                if (entity.IdContratoProveedor == 0)
                {
                    return Visibility.Collapsed;
                }
                return Visibility.Visible;
            }
        }

        protected override void LoadData()
        {
            base.LoadData();

            Inmuebles = db.Inmuebles.Where(m => m.FechaEliminacion == null).ToList();
            PeriodicidadCostes = db.Periodicidad.ToList();
            PeriodicidadRevisiones = db.Periodicidad.ToList();
            InmueblesCentrosCostes = db.InmuebleCentroCoste.ToList();
            ContratosProveedores = db.ContratosProveedores.Where(m => m.FechaEliminacion == null).DistinctBy(m => m.NombreProveedor).ToList();

            if (entity.IdContratoProveedor > 0)
            {
                Inmueble = entity.IdInmuebleNavigation;     
                ReferenciaContrato = entity.ReferenciaContrato;
                ContratoProveedor = ContratosProveedores.Where(m => m.NombreProveedor == entity.NombreProveedor).FirstOrDefault();
                Servicio = entity.Servicio;
                CosteAnual = entity.CosteAnual?.ToString();
                PeriodicidadCoste = entity.IdPeriodicidadCosteNavigation;
                PeriodicidadRevision = entity.IdPeriodicidadRevisionNavigation;
                ImporteFactura = entity.ImporteFactura?.ToString();
                InmuebleCentroCoste = entity.IdInmuebleCentroCosteNavigation;
                Fecha = entity.Fecha;
                FechaBaja = entity.FechaBaja;
                FechaPreaviso = entity.FechaPreaviso;
                FechaVencimiento = entity.FechaVencimiento;
                ArchivoDigital = entity.ArchivoDigital;
                Trazabilidad("Maestros", "Contratos Proveedores", entity.ReferenciaContrato, "Consulta", "Ficha Contratos Proveedores");
            }
         

            //Comprobamos que inmuebles puede ver el usuario
            if (!UserId.Administrador)
            {
                var inmueble = db.UsuarioInmueble.Where(m => m.IdUsuario == UserId.IdUsuario).Select(m => m.IdInmueble).ToList();             
                Inmuebles = Inmuebles.Where(m => inmueble.Contains(m.IdInmueble)).ToList();
            }

        }
        protected override void ModifyData()
        {
            base.ModifyData();
            string accion = "Update";

            if (baseVM.Errors.Count == 0)
            {
                entity.FechaSistema = DateTime.Now;
                entity.IdUsuarioNavigation = UserId;

                var model = db.ContratosProveedores.Find(entity?.IdContratoProveedor);

                if (model == null)
                {
                    db.ContratosProveedores.Add(entity);
                    accion = "Alta";
                }
                else 
                {
                    var newEntity = db.ContratosProveedores.ApplyValues(entity, entity.IdContratoProveedor);
                }

                db.SaveChanges();

                //Grabamos cambios
                if (accion == "Update")
                {
                    var historico = new HistoricoContratosProveedores();
                    historico.IdContratoProveedorNavigation = model;
                    historico.ArchivoDigital = entity.ArchivoDigital;
                    historico.CosteAnual = entity.CosteAnual;
                    historico.Fecha = entity.Fecha;
                    historico.FechaBaja = entity.FechaBaja;
                    historico.FechaEliminacion = entity.FechaEliminacion;
                    historico.FechaPreaviso = entity.FechaPreaviso;
                    historico.FechaVencimiento = entity.FechaVencimiento;           
                    historico.FechaSistema = DateTime.Now;
                    historico.IdUsuarioNavigation = entity.IdUsuarioNavigation;
                    historico.IdInmuebleNavigation = entity.IdInmuebleNavigation;
                    historico.IdInmuebleCentroCosteNavigation = entity.IdInmuebleCentroCosteNavigation;
                    historico.IdPeriodicidadRevisionNavigation = entity.IdPeriodicidadRevisionNavigation;
                    historico.IdPeriodicidadCosteNavigation = entity.IdPeriodicidadCosteNavigation;
                    historico.ImporteFactura = entity.ImporteFactura;
                    historico.PeriodicidadRevision = entity.PeriodicidadRevision;
                    historico.ReferenciaContrato = entity.ReferenciaContrato;
                    historico.Servicio = entity.Servicio;
                    historico.Varios1 = entity.Varios1;
                    historico.Varios2 = entity.Varios2;
                    historico.Varios3 = entity.Varios3;
                    historico.Varios4 = entity.Varios4;
                    historico.Varios5 = entity.Varios5;

                    db.HistoricoContratosProveedores.Add(historico);
                    db.SaveChanges();
                }
                Trazabilidad("Maestros", "Contratos Proveedores", entity.ReferenciaContrato, accion, "Ficha Contratos Proveedores");
                baseVM.ChangePageCommand.Execute(baseVM.PageViewModels.Where(m => m.Name == "Mantenimiento Contratos Proveedores").FirstOrDefault());
            }
        }
        protected override void DeleteData()
        {
            base.DeleteData();

            var model = db.ContratosProveedores.Find(entity.IdContratoProveedor);

            //Comprobamos que no tenga ninguna entidad relacionada

            var mantenimientos = db.Mantenimientos.Where(m => m.IdContratoProveedor == entity.IdContratoProveedor).Any();

            if (!mantenimientos)
            {
                var viewmodel = PageViewModels.Where(m => m.Name == "Eliminar Contratos Proveedores").FirstOrDefault();
                viewmodel = new DeleteContratoProveedorVM(baseVM, entity);
                baseVM.CurrentPageViewModel = viewmodel;
            }
            else
            {
                Mensaje = "Desvincule los Mantenimientos vinculados a este Contrato Proveedor para poder eliminarlo." + Environment.NewLine;
            }
        }
        protected override void VolverListado()
        {
            base.VolverListado();
            baseVM.ChangePageCommand.Execute(baseVM.PageViewModels.Where(m => m.Name == "Mantenimiento Contratos Proveedores").FirstOrDefault());
        }
        protected bool CheckValidationState<T>(string propertyName, T proposedValue)
        {
            baseVM.CheckValidationState(propertyName, proposedValue);

            if (propertyName == "Inmueble")
            {
                if (Mensaje != null)
                    Mensaje = Mensaje.Replace("* El campo Inmueble es obligatorio. ", "");

                if (proposedValue == null)
                {
                    SetError(propertyName, "*");
                    Mensaje += "* El campo Inmueble es obligatorio. ";
                    return false;
                }
                else
                {
                    SetError(propertyName, String.Empty);
                    return true;
                }
            }

            if (propertyName == "ContratoProveedor")
            {
                if (Mensaje != null)
                    Mensaje = Mensaje.Replace("* El campo Proveedor es obligatorio. ", "");

                if (proposedValue == null)
                {
                    SetError(propertyName, "*");
                    Mensaje += "* El campo Proveedor es obligatorio. ";
                    return false;
                }
                else
                {
                    SetError(propertyName, String.Empty);
                    return true;
                }
            }

            if (propertyName == "ReferenciaContrato")
            {
                if (Mensaje != null)
                    Mensaje = Mensaje.Replace("* El campo Referencia Contrato es obligatorio. ", "");

                if (proposedValue == null)
                {
                    SetError(propertyName, "*");
                    Mensaje += "* El campo Referencia Contrato es obligatorio. ";
                    return false;
                }
                else
                {
                    SetError(propertyName, String.Empty);
                    return true;
                }
            }

            if (propertyName == "CosteAnual")
            {
                if (Mensaje != null)
                    Mensaje = Mensaje.Replace("* El campo Coste Anual debe ser numérico. ", "");

                if (!String.IsNullOrEmpty(proposedValue as String) && !decimal.TryParse(proposedValue as String, out decimal numValue))
                {
                    SetError(propertyName, "*");
                    Mensaje += "* El campo Coste Anual debe ser numérico. ";
                    return false;
                }

                else
                {
                    SetError(propertyName, String.Empty);
                    return true;
                }
            }

            if (propertyName == "ImporteFactura")
            {
                if (Mensaje != null)
                    Mensaje = Mensaje.Replace("* El campo Importe Factura debe ser numérico. ", "");

                if (!String.IsNullOrEmpty(proposedValue as String) && !decimal.TryParse(proposedValue as String, out decimal numValue))
                {
                    SetError(propertyName, "*");
                    Mensaje += "* El campo Importe Factura debe ser numérico. ";
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

