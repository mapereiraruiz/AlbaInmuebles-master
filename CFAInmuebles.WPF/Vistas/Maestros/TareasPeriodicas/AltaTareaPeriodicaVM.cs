using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;


namespace CFAInmuebles.WPF
{
    public class AltaTareaPeriodicaVM : ObservableViewModel, IPageViewModel, IDataErrorInfo
    {
        public TareaPeriodica entity;

        private HomeTareaPeriodicaVM baseVM;

        private Tareas _tarea;
        private TipoFichero _tipofichero;
        private Usuarios _asignadaa;
        private Usuarios _responsable;
        private string _propietario;     
        private DateTime? _fechainicio;
        private DateTime _fechafin;
        private string _estado;
        private string _titulo;
        private string _descripcion;
        private string _porcentaje;
        private bool _recurrente;
        private string _tiporecurrente;
        private DateTime? _fechayhorarecordatorio;
        private List<String> _idficherovalues;
        private String _idficherovalue;

        private bool _selectedItem;

        public AltaTareaPeriodicaVM(HomeTareaPeriodicaVM baseVM, TareaPeriodica entity = null)
        {
            this.entity = entity;
            this.baseVM = baseVM;
        }

        public string Name
        {
            get
            {
                 return "Alta Tarea Periódica";
            }
        }


        public string Propietario
        {
            get { return _propietario; }
            set
            {
                if (_propietario != value)
                {
                    _propietario = value;
                    RaisePropertyChanged("Propietario");
                }
            }
        }


        public DateTime? FechaInicio
        {
            get { return _fechainicio; }
            set
            {
                if (_fechainicio != value)
                {
                    _fechainicio = value;
                    RaisePropertyChanged("FechaInicio");
                }
            }
        }

        public DateTime FechaFin
        {
            get {
                CheckValidationState("Estado", _estado); 
                return _fechafin; }
            set
            {
                if (_fechafin != value)
                {
                    CheckValidationState("Estado", _estado);
                    _fechafin = value;
                    RaisePropertyChanged("FechaFin");
                }
            }
        }

       
        public string Porcentaje
        {
            get {
                CheckValidationState("Porcentaje", _porcentaje); 
                return _porcentaje; }
            set
            {
                if (_porcentaje != value)
                {
                    CheckValidationState("Porcentaje", _porcentaje);
                    _porcentaje = value;
                    RaisePropertyChanged("Porcentaje");
                }
            }
        }

        public bool Recurrente
        {
            get { return _recurrente; }
            set
            {
                if (_recurrente != value)
                {
                    _recurrente = value;
                    RaisePropertyChanged("Recurrente");
                }
            }
        }

        public string TipoRecurrente
        {
            get { return _tiporecurrente; }
            set
            {
                if (_tiporecurrente != value)
                {
                    _tiporecurrente = value;
                    RaisePropertyChanged("TipoRecurrente");
                }
            }
        }
        public DateTime? FechaYhoraRecordatorio
        {
            get { return _fechayhorarecordatorio; }
            set
            {
                if (_fechayhorarecordatorio != value)
                {
                    _fechayhorarecordatorio = value;
                    RaisePropertyChanged("FechaYhoraRecordatorio");
                }
            }
        }

        public string Descripcion
        {
            get { return _descripcion; }
            set
            {
                if (_descripcion != value)
                {
                    _descripcion = value;
                    RaisePropertyChanged("Descripcion");
                }
            }
        }

        public new Tareas Tarea
        {
            get
            {
                CheckValidationState("Tarea", _tarea);
                return _tarea;
            }
            set
            {
                if (_tarea != value)
                {
                    CheckValidationState("Tarea", _tarea);
                    _tarea = value;
                    RaisePropertyChanged("Tarea");
                }
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
                           
                            case "Contrato Cliente":
                                entity.IdFichero = db.ContratosClientes.Where(m => m.NombreCliente == _idficherovalue).FirstOrDefault().IdContratoCliente;
                                break;
                            case "Contrato Proveedor":
                                entity.IdFichero = db.ContratosProveedores.Where(m => m.ReferenciaContrato == _idficherovalue).FirstOrDefault().IdContratoProveedor;
                                break;
                            case "Empresa":
                                entity.IdFichero = db.Empresas.Where(m => m.Empresa == _idficherovalue).FirstOrDefault().IdEmpresa;
                                break;
                            case "Obra":
                                entity.IdFichero = db.HistorialObra.Where(m => m.Descripcion == _idficherovalue).FirstOrDefault().IdHistorialObra;
                                break;
                            case "Incidencia":
                                entity.IdFichero = db.Incidencias.Where(m => m.Incidencia == _idficherovalue).FirstOrDefault().IdIncidencia;
                                break;
                            case "Mantenimiento":
                                entity.IdFichero = db.Mantenimientos.Where(m => m.IdTipoMantenimientoNavigation.Valor == _idficherovalue).FirstOrDefault().IdMantenimiento;
                                break;
                        }
                    }

                    RaisePropertyChanged("IdFicheroValue");
                }
            }
        }

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
                       
                        case "Contrato Cliente":
                            IdFicheroValues = db.ContratosClientes.Where(m => m.FechaEliminacion == null).Select(m => m.NombreCliente).ToList();
                            break;
                        case "Contrato Proveedor":
                            IdFicheroValues = db.ContratosProveedores.Where(m => m.FechaEliminacion == null).Select(m => m.ReferenciaContrato).ToList();
                            break;
                        case "Empresa":
                            IdFicheroValues = db.Empresas.Where(m => m.FechaEliminacion == null).Select(m => m.Empresa).ToList();
                            break;  
                        case "Obra":
                            IdFicheroValues = db.HistorialObra.Where(m => m.FechaEliminacion == null).Select(m => m.Descripcion).ToList();
                            break;
                        case "Incidencia":
                            IdFicheroValues = db.Incidencias.Where(m => m.FechaEliminacion == null).Select(m => m.Incidencia).ToList();
                            break;
                        case "Mantenimiento":
                            IdFicheroValues = db.Mantenimientos.Where(m => m.FechaEliminacion == null).Select(m => m.IdTipoMantenimientoNavigation.Valor).ToList();
                            break;
                    }

                    RaisePropertyChanged("TipoFichero");
                }
            }
        }
        public Usuarios AsignadaA
        {
            get
            {
                return _asignadaa;
            }
            set
            {
                if (_asignadaa != value)
                {
                    _asignadaa = value;
                    RaisePropertyChanged("AsignadaA");
                }
            }
        }

        public string Estado
        {
            get
            {
                CheckValidationState("Estado", _estado);
                return _estado;
            }
            set
            {
                if (_estado != value)
                {
                    CheckValidationState("Estado", _estado);
                    _estado = value;
                    RaisePropertyChanged("Estado");
                }
            }
        }

        public new string Titulo
        {
            get
            {
                CheckValidationState("Titulo", _titulo);
                return _titulo;
            }
            set
            {
                if (_titulo != value)
                {
                    CheckValidationState("Titulo", _titulo);
                    _titulo = value;
                    RaisePropertyChanged("Titulo");
                }
            }
        }

        public Usuarios Responsable
        {
            get
            {
                CheckValidationState("Responsable", _responsable);
                return _responsable;
            }
            set
            {
                if (_responsable != value)
                {
                    CheckValidationState("Responsable", _responsable);
                    _responsable = value;
                    RaisePropertyChanged("Responsable");
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
                if (entity.IdTareaPeriodica  == 0)
                {
                    return Visibility.Collapsed;
                }
                return Visibility.Visible;
            }
        }


        protected override void LoadData()
        {
            base.LoadData();
            Tareas = db.Tareas.Where(m => m.FechaEliminacion == null).ToList();
            TipoFicheros = db.TipoFichero.ToList();
            Periodicidades = db.Periodicidad.ToList();
            Usuarios = db.Usuarios.ToList();
           
            
            if (entity.IdTareaPeriodica > 0)
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
                        IdFicheroValues = db.ContratosProveedores.Where(m => m.FechaEliminacion == null).Select(m => m.ReferenciaContrato).ToList();
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
                        IdFicheroValues = db.Mantenimientos.Where(m => m.FechaEliminacion == null).Select(m => m.IdTipoMantenimientoNavigation.Valor).ToList();
                        IdFicheroValue = db.Mantenimientos.Find(entity.IdFichero)?.IdTipoMantenimientoNavigation.Valor;
                        break;
                }

                Tarea = entity.IdTareaNavigation;
                TipoFichero = entity.IdTipoFicheroNavigation;          
                Propietario = entity.Propietario;
                AsignadaA = entity.IdUsuarioAsignadoNavigation;
                Responsable = entity.IdUsuarioResponsableNavigation;
                FechaInicio = entity.FechaInicio;
                FechaFin = entity.FechaFin;
                Estado = entity.Estado;
                Periodicidad = entity.IdPeriodicidadNavigation;
                Titulo = entity.Titulo;
                Descripcion = entity.Descripcion;
                Porcentaje = entity.Porcentaje?.ToString();
                Recurrente = entity.Recurrente;
                TipoRecurrente = entity.TipoRecurrente;
                FechaYhoraRecordatorio = entity.FechaYhoraRecordatorio;
                Trazabilidad("Maestros", "Tareas Periódicas", Descripcion, "Consulta", "Ficha Tarea Periódica");
            }
            else
            {
                FechaFin = DateTime.Now;
            }
        }

        protected override void ModifyData()
        {
            base.ModifyData();
            string accion = "Update";

            if (Errors.Count == 0)
            {

                var model = db.TareaPeriodica.Find(entity?.IdTareaPeriodica);

                if (model == null)
                {
                    model = new TareaPeriodica();
                    db.TareaPeriodica.Add(model);
                    accion = "Alta";
                }

                model.IdTareaNavigation = Tarea;
                model.IdTipoFicheroNavigation = TipoFichero;
                model.Propietario = Propietario;
                model.IdUsuarioAsignadoNavigation = AsignadaA;
                model.IdUsuarioResponsableNavigation = Responsable;
                model.FechaInicio = FechaInicio;

                if (FechaFin != null)
                    model.FechaFin = FechaFin;
                else
                    model.FechaFin = DateTime.Now;

                model.Estado = Estado;
                model.IdPeriodicidadNavigation = Periodicidad;
                model.Titulo = Titulo;
                model.Descripcion = Descripcion;
                model.IdFichero = entity.IdFichero;

                if (!String.IsNullOrEmpty(Porcentaje))
                    model.Porcentaje = double.Parse(Porcentaje);

                model.Recurrente = Recurrente;
                model.TipoRecurrente = TipoRecurrente;
                model.FechaYhoraRecordatorio = FechaYhoraRecordatorio;
                model.FechaSistema = DateTime.Now;
                model.IdUsuarioNavigation = UserId;
               
                db.SaveChanges();
                Trazabilidad("Maestros", "Tareas Periódicas", Descripcion, accion, "Ficha Tarea Periódica");
                baseVM.ChangePageCommand.Execute(baseVM.PageViewModels.Where(m => m.Name == "Mantenimiento Tarea Periódica").FirstOrDefault());
            }
        }

        protected override void DeleteData()
        {
            base.DeleteData();

          
            var model = db.TareaPeriodica.Find(entity.IdTareaPeriodica);

            var viewmodel = PageViewModels.Where(m => m.Name == "Eliminar Tarea Periódica").FirstOrDefault();
            viewmodel = new DeleteTareaPeriodicaVM(baseVM, entity);
            baseVM.CurrentPageViewModel = viewmodel;  
          
        }


        protected override void VolverListado()
        {
            base.VolverListado();
            baseVM.ChangePageCommand.Execute(baseVM.PageViewModels.Where(m => m.Name == "Mantenimiento Tarea Periódica").FirstOrDefault());
        }

        protected bool CheckValidationState<T>(string propertyName, T proposedValue)
        {
            if (propertyName == "Tarea")
            {
                if (Mensaje != null)
                    Mensaje = Mensaje.Replace("* El campo Tarea es obligatorio. ", "");

                if (proposedValue == null)
                {
                    Mensaje += "* El campo Tarea es obligatorio. ";
                    SetError(propertyName, "*");
                    return false;
                }

                else
                {
                    SetError(propertyName, String.Empty);
                    return true;
                }
            }

            if (propertyName == "TipoFichero")
            {
                if (Mensaje != null)
                    Mensaje = Mensaje.Replace("* El campo Tipo Fichero es obligatorio. ", "");

                if (proposedValue == null)
                {
                    Mensaje += "* El campo Tipo Fichero es obligatorio. ";
                    SetError(propertyName, "*");
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
                if (Mensaje != null)
                {
                    Mensaje = Mensaje.Replace("* El campo Fichero es obligatorio. ", "");
                }

                if (String.IsNullOrEmpty(proposedValue as String))
                {
                    SetError(propertyName, "*");
                    Mensaje += "* El campo Fichero es obligatorio. ";
                    return false;
                }

                else
                {
                    SetError(propertyName, String.Empty);
                    return true;
                }
            }

            if (propertyName == "Estado")
            {
                if (Mensaje != null)
                    Mensaje = Mensaje.Replace("* El campo Estado es obligatorio. ", "");

                if (String.IsNullOrEmpty(proposedValue as String))
                {
                    Mensaje += "* El campo Estado es obligatorio. ";
                    SetError(propertyName, "*");
                    return false;
                }

                else
                {
                    SetError(propertyName, String.Empty);
                    return true;
                }
            }

            if (propertyName == "Titulo")
            {
                if (Mensaje != null)
                    Mensaje = Mensaje.Replace("* El campo Título es obligatorio. ", "");

                if (String.IsNullOrEmpty(proposedValue as String))
                {
                    Mensaje += "* El campo Título es obligatorio. ";
                    SetError(propertyName, "*");
                    return false;
                }

                else
                {
                    SetError(propertyName, String.Empty);
                    return true;
                }
            }

            if (propertyName == "Responsable")
            {
                if (Mensaje != null)
                    Mensaje = Mensaje.Replace("* El campo Responsable es obligatorio. ", "");

                if (proposedValue == null)
                {
                    Mensaje += "* El campo Responsable es obligatorio. ";
                    SetError(propertyName, "*");
                    return false;
                }

                else
                {
                    SetError(propertyName, String.Empty);
                    return true;
                }
            }

            if (propertyName == "Porcentaje")
            {
                if (Mensaje != null)
                {
                    Mensaje = Mensaje.Replace("* El campo Porcentaje debe ser numérico. ", "");
                    Mensaje = Mensaje.Replace("* El campo Porcentaje no puede ser mayor de 100. ", "");
                }


                if (proposedValue != null && !double.TryParse(proposedValue as String, out double numValue))
                {
                    Mensaje += "* El campo Porcentaje debe ser numérico. ";
                    SetError(propertyName, "*");
                    return false;
                }

                else if (proposedValue != null && double.TryParse(proposedValue as String, out double numPor) && numPor > 100)
                {
                    Mensaje += "* El campo Porcentaje no puede ser mayor de 100. ";
                    SetError(propertyName, "*");
                    return false;
                }

                else
                {
                    SetError(propertyName, String.Empty);
                    return true;
                }
            }

            if (propertyName == "FechaFin")
            {
                if (Mensaje != null)
                    Mensaje = Mensaje.Replace("* El campo Fecha Fin es obligatorio. ", "");

                if (proposedValue == null)
                {
                    Mensaje += "* El campo Fecha Fin es obligatorio. ";
                    SetError(propertyName, "*");
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
