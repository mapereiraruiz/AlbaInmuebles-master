using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Input;

namespace CFAInmuebles.WPF
{
    public class AltaTipoIPCVM : ObservableViewModel, IPageViewModel, IDataErrorInfo
    {
        public TipoIpc entity;

        protected ICommand _desvincularCommand;

        private HomeTipoIPCVM baseVM;
        private string _ipc;
        private DateTime? _fechaultimaactualizacion;
        private DateTime? _fechacomunicacionsubida;
        private DateTime? _fechafacturacion;
        private DateTime? _fechaproximarevision;
        private DateTime? _fechainiciofacturacion;
        private bool _cambiaripc;
        private int? _idtipoipcdestino;
        private string _incremento;
        private bool _selectedItem;

        public AltaTipoIPCVM(HomeTipoIPCVM baseVM, TipoIpc entidad = null)
        {
            entity = new TipoIpc();

            if (entidad != null)
            {

                var properties = typeof(TipoIpc).GetProperties(BindingFlags.Instance | BindingFlags.Public)
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
                 return "Alta Tipo IPC";
            }
        }

        public int MaxIpc 
        {
            get
            {
                return 100;
            }
        }


        public string Ipc
        {
            get {
                CheckValidationState("Ipc", _ipc); 
                return _ipc; }
            set
            {
                if (_ipc != value)
                {
                    CheckValidationState("Ipc", _ipc);
                    _ipc = value;
                    entity.Ipc = _ipc;
                    RaisePropertyChanged("Ipc");
                }
            }
        }

        public DateTime? FechaComunicacionSubida
        {
            get { return _fechaultimaactualizacion; }
            set
            {
                if (_fechaultimaactualizacion != value)
                {
                    _fechaultimaactualizacion = value;
                    entity.FechaUltimaActualizacion = _fechaultimaactualizacion;
                    RaisePropertyChanged("FechaUltimaActualizacion");
                }
            }
        }

        public DateTime? FechaUltimaActualizacion
        {
            get { return _fechacomunicacionsubida; }
            set
            {
                if (_fechacomunicacionsubida != value)
                {
                    _fechacomunicacionsubida = value;
                    entity.FechaComunicacionSubida = _fechacomunicacionsubida;
                    RaisePropertyChanged("FechaComunicacionSubida");
                }
            }
        }

        public DateTime? FechaFacturacion
        {
            get { return _fechafacturacion; }
            set
            {
                if (_fechafacturacion != value)
                {
                    _fechafacturacion = value;
                    entity.FechaFacturacion = _fechafacturacion;
                    RaisePropertyChanged("FechaFacturacion");
                }
            }
        }
        
        public DateTime? FechaProximaRevision
        {
            get { return _fechaproximarevision; }
            set
            {
                if (_fechaproximarevision != value)
                {
                    _fechaproximarevision = value;
                    entity.FechaProximaRevision = _fechaproximarevision;
                    RaisePropertyChanged("FechaProximaRevision");
                }
            }
        }
    
        public DateTime? FechaInicioFacturacion
        {
            get { return _fechainiciofacturacion; }
            set
            {
                if (_fechainiciofacturacion != value)
                {
                    _fechainiciofacturacion = value;
                    entity.FechaInicioFacturacion = _fechainiciofacturacion;
                    RaisePropertyChanged("FechaInicioFacturacion");
                }
            }
        }
    
        public bool Cambiaripc
        {
            get { return _cambiaripc; }
            set
            {
                if (_cambiaripc != value)
                {
                    _cambiaripc = value;
                    entity.Cambiaripc = _cambiaripc;
                    RaisePropertyChanged("Cambiaripc");
                }
            }
        }
    
        public int? IdTipoIpcDestino
        {
            get { return _idtipoipcdestino; }
            set
            {
                if (_idtipoipcdestino != value)
                {
                    _idtipoipcdestino = value;
                    entity.IdTipoIpcDestino = _idtipoipcdestino;
                    RaisePropertyChanged("IdTipoIpcDestino");
                }
            }
        }
   
        public string Incremento
        {
            get {
                CheckValidationState("Incremento", _incremento);
                return _incremento; }
            set
            {
                if (_incremento != value)
                {
                    CheckValidationState("Incremento", _incremento);
                    _incremento = value;

                    if (decimal.TryParse(Incremento, out decimal numValue))
                        entity.Incremento = decimal.Parse(Incremento);

                    RaisePropertyChanged("Incremento");
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
                if (entity.IdTipoIpc == 0)
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
            if (entity.IdTipoIpc > 0)
            {
                Ipc = entity.Ipc;
                FechaUltimaActualizacion = entity.FechaUltimaActualizacion;
                Cambiaripc = entity.Cambiaripc;
                Incremento = entity.Incremento?.ToString();
                FechaComunicacionSubida = entity.FechaComunicacionSubida;
                FechaFacturacion = entity.FechaFacturacion;
                FechaProximaRevision = entity.FechaProximaRevision;
                FechaInicioFacturacion = entity.FechaInicioFacturacion;               
                Trazabilidad("Mantenimientos", "Tipos IPC", Ipc, "Consulta", "Ficha Tipo IPC");
            }
            else
            {
                FechaUltimaActualizacion = DateTime.Now;
                FechaComunicacionSubida = DateTime.Now;
                FechaFacturacion = DateTime.Now;
                FechaProximaRevision = DateTime.Now;
                FechaInicioFacturacion = DateTime.Now;
            }
        }

        protected override void ModifyData()
        {
            base.ModifyData();
            string accion = "Update";

            if (Errors.Count == 0)
            {
                var model = db.TipoIpc.Find(entity?.IdTipoIpc);

                if (model == null)
                {
                    model = new TipoIpc();
                    db.TipoIpc.Add(model);
                    accion = "Alta";                 
                }

                model.Ipc = Ipc;
                model.FechaUltimaActualizacion = FechaUltimaActualizacion;
                model.Cambiaripc = Cambiaripc;

                if (!string.IsNullOrEmpty(Incremento))
                    model.Incremento = decimal.Parse(Incremento.Replace('.', ','));

                model.FechaComunicacionSubida = FechaComunicacionSubida;
                model.FechaFacturacion = FechaFacturacion;
                model.FechaProximaRevision = FechaProximaRevision;
                model.FechaInicioFacturacion = FechaInicioFacturacion;
                model.FechaSistema = DateTime.Now;
                model.IdUsuarioNavigation = UserId;

                db.SaveChanges();
                Trazabilidad("Mantenimientos", "Tipos IPC", Ipc, accion, "Ficha Tipo IPC");

                baseVM.ChangePageCommand.Execute(baseVM.PageViewModels.Where(m => m.Name == "Mantenimiento Tipo IPC").FirstOrDefault());
            }
        }

        protected override void DeleteData()
        {
            base.DeleteData();

            if (entity != null)
            {
                var model = db.TipoIpc.Find(entity.IdTipoIpc);

                //Comprobamos que no tenga ninguna entidad obligatoria vinculada
                var contratos = db.ContratosClientes.Where(m => m.IdTipoIpc == entity.IdTipoIpc && m.FechaEliminacion == null).Any();

                if (!contratos)
                {
                    var viewmodel = PageViewModels.Where(m => m.Name == "Eliminar Tipo IPC").FirstOrDefault();
                    viewmodel = new DeleteTipoIPCVM(baseVM, entity);
                    baseVM.CurrentPageViewModel = viewmodel;
                }
                else
                {
                    Mensaje = "Desvincule los contratos vinculados a este Tipo IPC para poder eliminarlo.";
                }
            }
        }

        protected void DesvincularContratos()
        {
            var viewmodel = PageViewModels.Where(m => m.Name == "Desvincular Contratos Tipo IPC").FirstOrDefault();
            viewmodel = new DesvincularContratosTipoIPCVM(baseVM, entity);
            baseVM.CurrentPageViewModel = viewmodel;
        }

        protected override void VolverListado()
        {
            base.VolverListado();
            baseVM.ChangePageCommand.Execute(baseVM.PageViewModels.Where(m => m.Name == "Mantenimiento Tipo IPC").FirstOrDefault());
        }

        protected bool CheckValidationState<T>(string propertyName, T proposedValue)
        {
            if (propertyName == "Ipc")
            {
                var existe = db.TipoIpc.Where(m => m.Ipc == proposedValue as String && m.FechaEliminacion == null).FirstOrDefault();

                if (String.IsNullOrEmpty(proposedValue as String))
                {
                    SetError(propertyName, "El campo Ipc es obligatorio.");                 
                    return false;
                }

                else if (existe != null && existe.IdTipoIpc != entity.IdTipoIpc )
                {
                    SetError(propertyName, "Ya existe un Ipc con ese valor.");
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
