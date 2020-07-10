using CFAInmuebles.Domain.Models;
using Microsoft.Win32;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.ComponentModel;
using System.Reflection;
using System.Collections.Generic;
using CFAInmuebles.Infrastructure.Data;

namespace CFAInmuebles.WPF
{
    public class AltaArticulosVM : ObservableViewModel, IPageViewModel, IDataErrorInfo
    {
        public Articulos entity;

        protected ICommand _openDialogCommand;

        private HomeArticulosVM baseVM;
        private string _articulo;
        private string _numunidad;
        private string _historialobra;
        private DateTime? _fechabaja;
        private DateTime? _fechaventa;
        private bool _alquilado;
        private string _planos;      
        private string _estado;
        private string _valorsuelo;
        private string _valoredificio;
        private Inmuebles _inmueble;
        private TipoArticulos _tipoarticulo;
        private TipoPlanta _planta;
        private bool _selectedItem;

        public AltaArticulosVM(HomeArticulosVM baseVM, Articulos entidad = null)
        {

            entity = new Articulos();

            if (entidad != null)
            {
                var properties = typeof(Articulos).GetProperties(BindingFlags.Instance | BindingFlags.Public)
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
                return "Alta Artículos";
            }
        }

        public string Descripcion
        {
            get {
                CheckValidationState("Descripcion", _articulo); 
                return _articulo; }
            set
            {
                if (_articulo != value)
                {
                    CheckValidationState("Descripcion", _articulo);
                    _articulo = value;
                    entity.Articulo = _articulo;
                    RaisePropertyChanged("Descripcion");
                }
            }
        }
  
        public string NumUnidad
        {
            get {
                CheckValidationState("NumUnidad", _numunidad); 
                return _numunidad; }
            set
            {
                if (_numunidad != value)
                {
                    CheckValidationState("NumUnidad", _numunidad);
                    _numunidad = value;

                    if (double.TryParse(NumUnidad, out double numValue))
                        entity.NumUnidad = double.Parse(NumUnidad);

                    RaisePropertyChanged("NumUnidad");
                }
            }
        }

        public string HistorialObra
        {
            get { return _historialobra; }
            set
            {
                if (_historialobra != value)
                {
                    _historialobra = value;
                    entity.HistorialObra = _historialobra;
                    RaisePropertyChanged("HistorialObra");
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

        public DateTime? FechaVenta
        {
            get { return _fechaventa; }
            set
            {
                if (_fechaventa != value)
                {
                    _fechaventa = value;

                    entity.FechaVenta = _fechaventa;
                    RaisePropertyChanged("FechaVenta");
                }
            }
        }

        public bool Alquilado
        {
            get { return _alquilado; }
            set
            {
                if (_alquilado != value)
                {
                    _alquilado = value;
                    entity.Alquilado = _alquilado;
                    RaisePropertyChanged("Alquilado");
                }
            }
        }

        public string Estado
        {
            get { return _estado; }
            set
            {
                if (_estado != value)
                {
                    _estado = value;
                    entity.Estado = _estado;
                    RaisePropertyChanged("Estado");
                }
            }
        }


        public string ValorSuelo
        {
            get {
                CheckValidationState("ValorSuelo", _valorsuelo); 
                return _valorsuelo; }
            set
            {
                if (_valorsuelo != value)
                {
                    CheckValidationState("ValorSuelo", _valorsuelo);
                    _valorsuelo = value;

                    if (decimal.TryParse(ValorSuelo, out decimal numValue))
                        entity.ValorSuelo = decimal.Parse(ValorSuelo);

                    RaisePropertyChanged("ValorSuelo");
                }
            }
        }


        public string ValorEdificio
        {
            get {
                CheckValidationState("ValorEdificio", _valoredificio); 
                return _valoredificio; }
            set
            {
                if (_valoredificio != value)
                {
                    CheckValidationState("ValorEdificio", _valoredificio);
                    _valoredificio = value;

                    if (decimal.TryParse(ValorEdificio, out decimal numValue))
                        entity.ValorEdificio = decimal.Parse(ValorEdificio);

                    RaisePropertyChanged("ValorEdificio");
                }
            }
        }


        public string Planos
        {
            get { return _planos; }
            set
            {
                if (_planos != value)
                {
                    _planos = value;
                    entity.Planos = _planos;
                    RaisePropertyChanged("Planos");
                }
            }
        }

        public new Inmuebles Inmueble
        {
            get
            {
                CheckValidationState("Inmueble", _inmueble);
                return _inmueble;
            }
            set
            {
                if (_inmueble != value)
                {
                    CheckValidationState("Inmueble", _inmueble);
                    _inmueble = value;

                    if (_inmueble != null)
                        entity.IdInmuebleNavigation = _inmueble;

                    RaisePropertyChanged("Inmueble");
                }
            }
        }

        public new TipoArticulos TipoArticulo
        {
            get
            {
                CheckValidationState("TipoArticulo", _tipoarticulo);
                return _tipoarticulo;
            }
            set
            {
                if (_tipoarticulo != value)
                {
                    CheckValidationState("TipoArticulo", _tipoarticulo);
                    _tipoarticulo = value;

                    if (_tipoarticulo != null)
                        entity.IdTipoArticuloNavigation = _tipoarticulo;

                    RaisePropertyChanged("TipoArticulo");
                }
            }
        }

        public new TipoPlanta Planta
        {
            get
            {
                CheckValidationState("Planta", _planta);
                return _planta;
            }
            set
            {
                if (_planta != value)
                {
                    CheckValidationState("Planta", _planta);
                    _planta = value;

                    if (_planta != null)
                        entity.IdPlantaNavigation = _planta;

                    RaisePropertyChanged("Planta");
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
                if (entity.IdArticulo == 0)
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


        protected override void LoadData()
        {
            base.LoadData();

            Inmuebles = db.Inmuebles.Where(m => m.FechaEliminacion == null).ToList();
            TipoArticulos = db.TipoArticulos.ToList();
            Plantas = db.TipoPlanta.ToList();

            if (entity.IdArticulo > 0)
            {
                Descripcion = entity.Articulo;
                Inmueble = entity.IdInmuebleNavigation;
                TipoArticulo  = entity.IdTipoArticuloNavigation;
                Planta = entity.IdPlantaNavigation;
                NumUnidad = entity.NumUnidad?.ToString();
                HistorialObra = entity.HistorialObra;
                FechaBaja = entity.FechaBaja;
                FechaVenta = entity.FechaVenta;
                Alquilado = entity.Alquilado;
                Planos = entity.Planos;
                Estado = entity.Estado;
                ValorSuelo = entity.ValorSuelo?.ToString();
                ValorEdificio = entity.ValorEdificio?.ToString();
                Trazabilidad("Maestros", "Artículos", Descripcion, "Consulta", "Ficha Artículos");
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

                var model = db.Articulos.Find(entity?.IdArticulo);

                if (model == null)
                {
                    entity.FechaAlta = DateTime.Now;
                    db.Articulos.Add(entity);
                    accion = "Alta";
                  
                }
                else
                {
                    var newEntity = db.Articulos.ApplyValues(entity, entity.IdArticulo);
                }

                db.SaveChanges();

                //Grabamos cambios
                if (accion == "Update")
                {
                    var historico = new HistoricoArticulos();
                    historico.Articulo = entity.Articulo;
                    historico.IdArticuloNavigation = model;
                    historico.Alquilado = entity.Alquilado;
                    historico.Estado = entity.Estado;
                    historico.FechaAlta = entity.FechaAlta;
                    historico.FechaBaja = entity.FechaBaja;
                    historico.FechaEliminacion = entity.FechaEliminacion;
                    historico.FechaSistema = DateTime.Now;
                    historico.HistorialObra = entity.HistorialObra;
                    historico.IdInmuebleNavigation = entity.IdInmuebleNavigation;
                    historico.IdPlantaNavigation = entity.IdPlantaNavigation;
                    historico.IdTipoArticuloNavigation = entity.IdTipoArticuloNavigation;
                    historico.IdUsuarioNavigation = UserId;
                    historico.NumUnidad = entity.NumUnidad;
                    historico.Planos = entity.Planos;                  
                    historico.ValorEdificio = entity.ValorEdificio;
                    historico.ValorSuelo = entity.ValorSuelo;
                    historico.FechaVenta = entity.FechaVenta;
                    db.HistoricoArticulos.Add(historico);
                    db.SaveChanges();
                }
                Trazabilidad("Maestros", "Artículos", Descripcion, accion, "Ficha Artículos");
                baseVM.ChangePageCommand.Execute(baseVM.PageViewModels.Where(m => m.Name == "Mantenimiento Artículos").FirstOrDefault());
            }
        }
        protected override void DeleteData()
        {
            base.DeleteData();

            var model = db.Articulos.Find(entity.IdArticulo);

            //Comprobamos que no tenga ninguna entidad relacionada

            var superficietasacion = db.SuperficieTasacion.Where(m => m.IdInmueble == entity.IdInmueble).Any();

            if (!superficietasacion)
            {
                var viewmodel = PageViewModels.Where(m => m.Name == "Eliminar Artículos").FirstOrDefault();
                viewmodel = new DeleteArticulosVM(baseVM, entity);
                baseVM.CurrentPageViewModel = viewmodel;
            }
            else
            {
                Mensaje = "Desvincule las Superficies de Tasación vinculadas con este Artículo para poder eliminarlo.";
            }
    
        }
        protected override void VolverListado()
        {
            base.VolverListado();
            baseVM.ChangePageCommand.Execute(baseVM.PageViewModels.Where(m => m.Name == "Mantenimiento Artículos").FirstOrDefault());
        }
        protected void OpenDialog()
        {
            var of = new OpenFileDialog();
            of.Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg|All files (*.*)|*.*";
            var res = of.ShowDialog();
            if (res.HasValue)
            {
                Planos = of.FileName;               
            }
        }

        protected bool CheckValidationState<T>(string propertyName, T proposedValue)
        {
            if (propertyName == "Descripcion")
            {
                var existe = db.Articulos.Where(m => m.Articulo == proposedValue as String && m.FechaEliminacion == null).FirstOrDefault();

                if (Mensaje != null)
                {
                    Mensaje = Mensaje.Replace("* El campo Descripción es obligatorio. ", "");
                    Mensaje = Mensaje.Replace("* Ya existe un Artículo para ese inmueble con ese valor. ", "");
                }

                if (String.IsNullOrEmpty(proposedValue as String))
                {
                    SetError(propertyName, "*");
                    Mensaje += "* El campo Descripción es obligatorio. ";
                    return false;
                }

                else if (existe != null && existe.IdArticulo != entity.IdArticulo && existe.IdInmueble == entity.IdInmueble)
                {
                    SetError(propertyName, "*");
                    Mensaje += "* Ya existe un Artículo para ese inmueble con ese valor. ";
                    return false;
                }

                else
                {
                    SetError(propertyName, String.Empty);                 
                    return true;
                }
            }

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

            if (propertyName == "TipoArticulo")
            {
                if (Mensaje != null)
                    Mensaje = Mensaje.Replace("* El campo Tipo Artículo es obligatorio. ", "");

                if (proposedValue == null)
                {
                    SetError(propertyName, "*");
                    Mensaje += "* El campo Tipo Artículo es obligatorio. ";
                    return false;
                }
                else
                {
                    SetError(propertyName, String.Empty);                  
                    return true;
                }
            }

            if (propertyName == "Planta")
            {
                if (Mensaje != null)
                    Mensaje = Mensaje.Replace("* El campo Planta es obligatorio. ", "");

                if (proposedValue == null)
                {
                    SetError(propertyName, "*");
                    Mensaje += "* El campo Planta es obligatorio. ";
                    return false;
                }
                else
                {
                    SetError(propertyName, String.Empty);
                    return true;
                }
            }

            if (propertyName == "NumUnidad")
            {
                if (Mensaje != null)
                    Mensaje = Mensaje.Replace("* El campo NumUnidad debe ser numérico. ", "");

                if (!String.IsNullOrEmpty(proposedValue as String) && !double.TryParse(proposedValue as String, out double numValue))
                {
                    SetError(propertyName, "*");                  
                    Mensaje += "* El campo NumUnidad debe ser numérico. ";
                    return false;
                }

                else
                {
                    SetError(propertyName, String.Empty);
                    return true;
                }
            }

            if (propertyName == "ValorSuelo")
            {
                if (Mensaje != null)
                    Mensaje = Mensaje.Replace("* El campo Valor Suelo debe ser numérico. ", "");

                if (!String.IsNullOrEmpty(proposedValue as String) && !double.TryParse(proposedValue as String, out double numValue))
                {
                    SetError(propertyName, "*");
                    Mensaje += "* El campo Valor Suelo debe ser numérico. ";
                    return false;
                }

                else
                {
                    SetError(propertyName, String.Empty);
                    return true;
                }
            }

            if (propertyName == "ValorEdificio")
            {
                if (Mensaje != null)
                    Mensaje = Mensaje.Replace("* El campo Valor Edificio debe ser numérico. ", "");

                if (!String.IsNullOrEmpty(proposedValue as String) && !double.TryParse(proposedValue as String, out double numValue))
                {
                    SetError(propertyName, "*");
                    Mensaje += "* El campo Valor Edificio debe ser numérico. ";
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

