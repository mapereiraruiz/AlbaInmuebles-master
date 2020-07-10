using CFAInmuebles.Domain.Models;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Reflection;
using CFAInmuebles.Infrastructure.Data;

namespace CFAInmuebles.WPF
{
    public class AltaPreventivoNormativoVM : ObservableViewModel, IPageViewModel, IDataErrorInfo
    {
        public Mantenimientos entity;
        private HomePreventivoNormativoVM baseVM;


        private TipoMantenimiento _tipomantenimiento;
        private TipoInstalacion _tipoinstalacion;
        private ContratosProveedores _contratoproveedor;
        private PeriodicidadServicio _periodicidad;
        private string _servicio;
        private DateTime? _fechaservicio;
        private TipoFichero _tipofichero;
        private List<String> _idficherovalues;
        private String _idficherovalue;
        private bool? _medidascorrectoras;
 

        private bool _selectedItem;

        public AltaPreventivoNormativoVM(HomePreventivoNormativoVM baseVM, Mantenimientos entidad = null)
        {
            entity = new Mantenimientos();

            if (entidad != null)
            {

                var properties = typeof(Mantenimientos).GetProperties(BindingFlags.Instance | BindingFlags.Public)
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
                 return "Alta PreventivoNormativo";
            }
        }

        public int MaxServicio
        {
            get
            {
                return 100;
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

        public bool? MedidasCorrectoras
        {
            get { return _medidascorrectoras; }
            set
            {
                _medidascorrectoras = value;
                entity.MedidasCorrectoras = _medidascorrectoras;
        //       CheckValidationState("Proveedor", Proveedor);
                RaisePropertyChanged("Proveedor");
                RaisePropertyChanged("MedidasCorrectoras");
            }
        }

        public String IdFicheroValue
        {
            get
            {
                CheckValidationState("IdFicheroValue", _idficherovalue);
                return _idficherovalue;
            }
            set
            {
                if (_idficherovalue != value)
                {
                    CheckValidationState("IdFicheroValue", _idficherovalue);
                    _idficherovalue = value;

                    if (_idficherovalue != null)
                    {
                        switch (_tipofichero?.Valor)
                        {
                            case "Inmueble":
                                entity.IdFichero = db.Inmuebles.Where(m => m.Inmueble == _idficherovalue).FirstOrDefault().IdInmueble;
                                break;
                            case "Artículo":
                                entity.IdFichero = db.Articulos.Where(m => m.Articulo == _idficherovalue).FirstOrDefault().IdArticulo;
                                break;
                        }
                    }

                    RaisePropertyChanged("IdFicheroValue");
                }
            }
        }

        public new TipoFichero TipoFichero
        {
            get
            {
                CheckValidationState("TipoFichero", _tipofichero);
                return _tipofichero;
            }
            set
            {
                if (_tipofichero != value)
                {
                    CheckValidationState("TipoFichero", _tipofichero);
                    _tipofichero = value;
                    entity.IdTipoFicheroNavigation = _tipofichero;

                    switch (_tipofichero?.Valor)
                    {
                        case "Inmueble":
                            IdFicheroValues = db.Inmuebles.Where(m => m.FechaEliminacion == null).Select(m => m.Inmueble).ToList();
                            break;
                        case "Artículo":
                            IdFicheroValues = db.Articulos.Where(m => m.FechaEliminacion == null).Select(m => m.Articulo).ToList();
                            break;
                    }

                    RaisePropertyChanged("TipoFichero");
                }
            }
        }

        //public new Proveedor Proveedor
        //{
        //    get
        //    {
        //        CheckValidationState("Proveedor", _proveedor);
        //        return _proveedor;
        //    }
        //    set
        //    {
        //        if (_proveedor != value)
        //        {
        //            CheckValidationState("Proveedor", _proveedor);
        //            _proveedor = value;
        //            entity.IdProveedorNavigation = _proveedor;
        //            RaisePropertyChanged("Proveedor");
        //        }
        //    }
        //}

        public List<String> IdFicheroValues
        {

            get { return _idficherovalues; }
            set
            {
                if (_idficherovalues != value)
                {
                    _idficherovalues = value;
                    RaisePropertyChanged("IdFicheroValues");
                }
            }
        }



        public new TipoMantenimiento TipoMantenimiento
        {
            get
            {
                CheckValidationState("TipoMantenimiento", _tipomantenimiento);
                return _tipomantenimiento;
            }
            set
            {
                if (_tipomantenimiento != value)
                {
                    CheckValidationState("TipoMantenimiento", _tipomantenimiento);
                    _tipomantenimiento = value;
                    entity.IdTipoMantenimientoNavigation = _tipomantenimiento;
                    RaisePropertyChanged("TipoMantenimiento");
                }
            }
        }

        public new TipoInstalacion TipoInstalacion
        {
            get
            {
                return _tipoinstalacion;
            }
            set
            {
                if (_tipoinstalacion != value)
                {
                    _tipoinstalacion = value;
                    entity.IdTipoInstalacionNavigation = _tipoinstalacion;
                    RaisePropertyChanged("TipoInstalacion");
                }
            }
        }
        public new ContratosProveedores ContratoProveedor
        {
            get
            {
                return _contratoproveedor;
            }
            set
            {
                if (_contratoproveedor != value)
                {
                    _contratoproveedor = value;
                    entity.IdContratoProveedorNavigation = _contratoproveedor;
                    RaisePropertyChanged("ContratoProveedor");
                }
            }
        }
        public new PeriodicidadServicio Periodicidad
        {
            get
            {
                return _periodicidad;
            }
            set
            {
                if (_periodicidad != value)
                {
                    _periodicidad = value;
                    entity.IdPeriodicidadServicioNavigation = _periodicidad;
                    RaisePropertyChanged("Periodicidad");
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
        public DateTime? FechaServicio
        {
            get { return _fechaservicio; }
            set
            {
                if (_fechaservicio != value)
                {
                    _fechaservicio = value;
                    entity.FechaServicio = _fechaservicio;
                    RaisePropertyChanged("FechaServicio");
                }
            }
        }

        public Visibility MostrarBotones
        {
            get
            {
                if (entity.IdMantenimiento == 0)
                {
                    return Visibility.Collapsed;
                }
                return Visibility.Visible;
            }
        }


        protected override void LoadData()
        {
            base.LoadData();

            TipoFicheros = db.TipoFichero.Where(m => m.Valor == "Inmueble" || m.Valor == "Artículo").ToList();
            TiposLicencia = db.TipoLicencia.Where(m => m.FechaEliminacion == null).ToList();  
            TipoInstalaciones = db.TipoInstalacion.Where(m => m.FechaEliminacion == null).ToList();
            ContratosProveedores = db.ContratosProveedores.Where(m => m.FechaEliminacion == null).DistinctBy(m => m.NombreProveedor).ToList();
            TipoInstalaciones = db.TipoInstalacion.Where(m => m.FechaEliminacion == null).ToList();
            TipoMantenimientos = db.TipoMantenimiento.ToList();
            PeriodicidadesServicios = db.PeriodicidadServicio.ToList();
            Normas = db.Normas.Where(m => m.FechaEliminacion == null).ToList();

            if (entity.IdMantenimiento > 0)
            {
                switch (entity.IdTipoFicheroNavigation?.Valor)
                {
                    case "Inmueble":
                        IdFicheroValues = db.Inmuebles.Where(m => m.FechaEliminacion == null).Select(m => m.Inmueble).ToList();
                        IdFicheroValue = db.Inmuebles.Find(entity.IdFichero)?.Inmueble;
                        break;
                    case "Artículo":
                        IdFicheroValues = db.Articulos.Where(m => m.FechaEliminacion == null).Select(m => m.Articulo).ToList();
                        IdFicheroValue = db.Articulos.Find(entity.IdFichero)?.Articulo;
                        break;                  
                    case "Contrato Cliente":
                        IdFicheroValues = db.ContratosClientes.Where(m => m.FechaEliminacion == null).Select(m => m.NombreCliente).ToList();
                        IdFicheroValue = db.ContratosClientes.Find(entity.IdFichero)?.NombreCliente;
                        break;
                    case "Contrato Proveedor":
                        IdFicheroValues = db.ContratosProveedores.Where(m => m.FechaEliminacion == null).Distinct().Select(m => m.ReferenciaContrato).ToList();
                        IdFicheroValue = db.ContratosProveedores.Find(entity.IdFichero)?.ReferenciaContrato;
                        break;
                    case "Empresa":
                        IdFicheroValues = db.Empresas.Where(m => m.FechaEliminacion == null).Select(m => m.Empresa).ToList();
                        IdFicheroValue = db.Empresas.Find(entity.IdFichero)?.Empresa;
                        break;                  
                    case "Obra":
                        IdFicheroValues = db.HistorialObra.Where(m => m.FechaEliminacion == null).Select(m => m.Descripcion).ToList();
                        IdFicheroValue = db.HistorialObra.Find(entity.IdFichero)?.Descripcion;
                        break;
                    case "Incidencia":
                        IdFicheroValues = db.Incidencias.Where(m => m.FechaEliminacion == null).Select(m => m.Incidencia).ToList();
                        IdFicheroValue = db.Incidencias.Find(entity.IdFichero)?.Incidencia;
                        break;
                    case "Mantenimiento":
                        IdFicheroValues = db.Mantenimientos.Where(m => m.FechaEliminacion == null).Select(m => m.IdMantenimiento.ToString()).ToList();
                        IdFicheroValue = db.Mantenimientos.Find(entity.IdFichero)?.IdMantenimiento.ToString();
                        break;
                }

               
                TipoInstalacion = entity.IdTipoInstalacionNavigation;
                ContratoProveedor = entity.IdContratoProveedorNavigation;
                TipoMantenimiento = entity.IdTipoMantenimientoNavigation;
                Servicio = entity.Servicio;
                FechaServicio = entity.FechaServicio;
                PeriodicidadServicio = entity.IdPeriodicidadServicioNavigation;
                Norma = entity.IdNormaNavigation;
                TipoFichero = entity.IdTipoFicheroNavigation;
                MedidasCorrectoras = entity.MedidasCorrectoras;

                Trazabilidad("Mantenimientos", "Preventivo Normativo", Servicio, "Consulta", "Ficha Preventivo Normativo");
            }
            else
            {
                FechaServicio = DateTime.Now;
            }
        }

        protected override void ModifyData()
        {
            base.ModifyData();
            string accion = "Update";

            if (Errors.Count == 0)
            {
                var model = db.Mantenimientos.Find(entity?.IdMantenimiento);

                if (model == null)
                {
                    model = new Mantenimientos();
                    db.Mantenimientos.Add(model);
                    accion = "Alta";
                }

                model.IdFichero = entity.IdFichero;
                model.IdTipoInstalacionNavigation = TipoInstalacion;
                model.IdContratoProveedorNavigation = ContratoProveedor;
                model.IdTipoMantenimientoNavigation = TipoMantenimiento;
                model.Servicio = Servicio;
                model.FechaServicio = FechaServicio;
                model.IdPeriodicidadServicioNavigation = PeriodicidadServicio;
                model.IdNormaNavigation = Norma;
                model.IdTipoFicheroNavigation = TipoFichero;
                model.IdUsuarioNavigation = UserId;
                model.FechaSistema = DateTime.Now;
                model.MedidasCorrectoras = MedidasCorrectoras;
                db.SaveChanges();
                Trazabilidad("Mantenimientos", "Preventivo Normativo", Servicio, accion, "Ficha Preventivo Normativo");

                if (MedidasCorrectoras == true && accion == "Alta")
                {
                    //Damos de alta una incidencia
                    var incidencia = new Incidencias();
                    incidencia.IdFichero = model.IdMantenimiento;
                    incidencia.IdTipoFicheroNavigation = db.TipoFichero.Where(m => m.Valor == "Mantenimiento").FirstOrDefault();
                    incidencia.IdUsuarioNavigation = UserId;
                    incidencia.FechaSistema = DateTime.Now;
                    incidencia.Descripcion = "Alta incidencia desde Mantenimientos";
                    incidencia.Incidencia = model.Servicio ?? "Incidencia de Mantenimiento";
                    incidencia.FechaIncidencia = DateTime.Now;
                    incidencia.IdContratoProveedorNavigation = ContratoProveedor;
                    db.Incidencias.Add(incidencia);
                    db.SaveChanges();
                    Trazabilidad("Mantenimientos", "Preventivo Normativo", incidencia.Incidencia, accion, "Ficha Preventivo Normativo Alta Incidencia");
                }

                baseVM.ChangePageCommand.Execute(baseVM.PageViewModels.Where(m => m.Name == "Mantenimiento Preventivo y Normativo").FirstOrDefault());
            }
        }

        protected override void DeleteData()
        {
            base.DeleteData();

            if (entity != null)
            {
                var viewmodel = PageViewModels.Where(m => m.Name == "Eliminar PreventivoNormativo").FirstOrDefault();
                viewmodel = new DeletePreventivoNormativoVM(baseVM, entity);
                baseVM.CurrentPageViewModel = viewmodel;
            }
        }

        protected override void VolverListado()
        {
            base.VolverListado();
            baseVM.ChangePageCommand.Execute(baseVM.PageViewModels.Where(m => m.Name == "Mantenimiento Preventivo y Normativo").FirstOrDefault());
        }


        protected bool CheckValidationState<T>(string propertyName, T proposedValue)
        {
           
            if (propertyName == "TipoFichero")
            {
                if (proposedValue == null)
                {
                    SetError(propertyName, "El campo Tipo Fichero es obligatorio.");
                    return false;
                }
                else
                {
                    SetError(propertyName, String.Empty);
                    return true;
                }
            }

            if (propertyName == "IdFicheroValue")
            {
                if (String.IsNullOrEmpty(proposedValue as String))
                {
                    SetError(propertyName, "El campo Fichero es obligatorio.");                  
                    return false;
                }

                else
                {
                    SetError(propertyName, String.Empty);
                    return true;
                }
            }

            if (propertyName == "TipoMantenimiento")
            {
                if (proposedValue == null)
                {
                    SetError(propertyName, "El campo Tipo Mantenimiento es obligatorio.");
                    return false;
                }
                else
                {
                    SetError(propertyName, String.Empty);
                    return true;
                }
            }

            if (propertyName == "Proveedor")
            {
                if (_medidascorrectoras == true && proposedValue == null)
                {
                    SetError(propertyName, "El campo Proveedor es obligatorio si selecciona una medida correctora.");
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
