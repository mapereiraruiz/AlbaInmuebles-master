using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;

namespace CFAInmuebles.WPF
{
    public class AltaInmuebleLicenciasVM : ObservableViewModel, IPageViewModel, IDataErrorInfo
    {
        public Licencias entity;
        public Inmuebles entitybase;

        private FichaInmueblesVM baseVM;

        private TipoLicencia _tipolicencia;
        private string _numeroexpediente;
        private string _descripcion;
        private string _organismo;
        private DateTime? _fechaconcesion;
        private DateTime? _fechasolicitud;
        private string _archivodigital;


        private bool _selectedItem;

        public AltaInmuebleLicenciasVM(FichaInmueblesVM baseVM, Inmuebles entitybase, Licencias entidad = null)
        {
            entity = new Licencias();

            if (entidad != null)
            {

                var properties = typeof(Licencias).GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(p => !(p.PropertyType.IsInterface && p.PropertyType.IsGenericType &&
                              p.PropertyType.GetGenericTypeDefinition() == typeof(ICollection<>))
                             && p.CanWrite).ToList();
                properties.ForEach(p =>
                {
                    p.GetValue(entidad);
                    p.SetValue(entity, p.GetValue(entidad));
                });
            }
            this.entitybase = entitybase;
            this.baseVM = baseVM;
        }

        public string Name
        {
            get
            {
                return "Alta Inmueble Licencias";
            }
        }

        public new TipoLicencia TipoLicencia
        {
            get
            {
                CheckValidationState("TipoLicencia", _tipolicencia);
                return _tipolicencia;
            }
            set
            {
                if (_tipolicencia != value)
                {
                    CheckValidationState("TipoLicencia", _tipolicencia);
                    _tipolicencia = value;
                    entity.IdTipoLicenciaNavigation = _tipolicencia;
                    RaisePropertyChanged("TipoLicencia");
                }
            }
        }
        public string NumeroExpediente
        {
            get
            {
                CheckValidationState("NumeroExpediente", _numeroexpediente);
                return _numeroexpediente;
            }
            set
            {
                if (_numeroexpediente != value)
                {
                    CheckValidationState("NumeroExpediente", value);
                    _numeroexpediente = value;
                    RaisePropertyChanged("NumeroExpediente");
                }
            }
        }

        public string Descripcion
        {
            get
            {
                CheckValidationState("Descripcion", _descripcion);
                return _descripcion;
            }
            set
            {
                if (_descripcion != value)
                {
                    CheckValidationState("Descripcion", value);
                    _descripcion = value;
                    RaisePropertyChanged("Descripcion");
                }
            }
        }

        public string Organismo
        {
            get { return _organismo; }
            set
            {
                if (_organismo != value)
                {
                    _organismo = value;
                    RaisePropertyChanged("Organismo");
                }
            }
        }

        public DateTime? FechaConcesion
        {
            get { return _fechaconcesion; }
            set
            {
                if (_fechaconcesion != value)
                {
                    _fechaconcesion = value;
                    RaisePropertyChanged("FechaConcesion");
                }
            }
        }

        public DateTime? FechaSolicitud
        {
            get { return _fechasolicitud; }
            set
            {
                if (_fechasolicitud != value)
                {
                    _fechasolicitud = value;
                    RaisePropertyChanged("FechaSolicitud");
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
                    RaisePropertyChanged("ArchivoDigital");
                }
            }
        }

        public Visibility MostrarBotones
        {
            get
            {
                if (entity == null)
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
           
            TiposLicencia = db.TipoLicencia.Where(m => m.FechaEliminacion == null).ToList();

            if (entity.IdLicencia > 0)
            {
                TipoFichero = entity.IdTipoFicheroNavigation;
                TipoLicencia = entity.IdTipoLicenciaNavigation;
                NumeroExpediente = entity.NumeroExpediente;
                Descripcion = entity.Descripcion;
                Organismo = entity.Organismo;
                FechaConcesion = entity.FechaConcesion;
                FechaSolicitud = entity.FechaSolicitud;
                ArchivoDigital = entity.ArchivoDigital;
                Trazabilidad("Maestros", "Inmuebles", Descripcion, "Consulta", "Ficha Licencias");
            }
            else
            {
                FechaConcesion = DateTime.Now;
                FechaSolicitud = DateTime.Now;
            }
        }

        protected override void ModifyData()
        {
            string accion = "Update";

            if (Errors.Count == 0)
            {
                var model = db.Licencias.Find(entity?.IdLicencia);

                if (model == null)
                {
                    model = new Licencias();
                    db.Licencias.Add(model);
                    accion = "Alta";
                }

                model.IdTipoFicheroNavigation = db.TipoFichero.Where(m => m.Valor == "Inmueble").FirstOrDefault();
                model.IdTipoLicenciaNavigation = TipoLicencia;
                model.NumeroExpediente = NumeroExpediente;

                model.Descripcion = Descripcion;
                model.Organismo = Organismo;
                model.FechaConcesion = FechaConcesion;
                model.FechaSolicitud = FechaSolicitud;
                model.ArchivoDigital = ArchivoDigital;
                model.IdUsuarioNavigation = UserId;
                model.FechaSistema = DateTime.Now;
                model.IdFichero = entitybase.IdInmueble;

                db.SaveChanges();

                Trazabilidad("Maestros", "Inmuebles", Descripcion, accion, "Ficha Licencias");
                baseVM.ChangePageCommand.Execute(baseVM.PageViewModels.Where(m => m.Name == "Inmueble Licencias").FirstOrDefault());
            }
            
        }

        protected override void VolverListado()
        {
            base.VolverListado();

            var viewmodel = baseVM.PageViewModels.Where(m => m.Name == "Inmueble Licencias").FirstOrDefault();
            viewmodel = new InmuebleLicenciasVM(baseVM, entitybase);
            baseVM.CurrentPageViewModel = viewmodel;
       
        }

        protected bool CheckValidationState<T>(string propertyName, T proposedValue)
        {
            if (propertyName == "TipoLicencia")
            {
                if (proposedValue == null)
                {
                    SetError(propertyName, "El campo Tipo Licencia es obligatorio.");
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

            if (propertyName == "NumeroExpediente")
            {
                if (String.IsNullOrEmpty(proposedValue as String))
                {
                    SetError(propertyName, "El campo Número Expediente es obligatorio.");
                    return false;
                }
                else
                {
                    SetError(propertyName, String.Empty);
                    return true;
                }
            }

            if (propertyName == "Descripcion")
            {
                if (String.IsNullOrEmpty(proposedValue as String))
                {
                    SetError(propertyName, "El campo Descripción es obligatorio.");
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
