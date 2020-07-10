using CFAInmuebles.Domain.Models;
using CFAInmuebles.Infrastructure.Data;
using DevExpress.Xpf.Grid;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CFAInmuebles.WPF
{
    public class AltaLicenciaVM : ObservableViewModel, IPageViewModel, IDataErrorInfo
    {
        public Licencias entity;

        private HomeLicenciaVM baseVM;

        private TipoLicencia _tipolicencia;
        private string _numeroexpediente;
        private TipoFichero _tipofichero;
        private string _descripcion;
        private string _organismo;
        private DateTime? _fechaconcesion;
        private DateTime? _fechasolicitud;
        private string _archivodigital;
        private List<String> _idficherovalues;
        private String _idficherovalue;

        private bool _selectedItem;

        public AltaLicenciaVM(HomeLicenciaVM baseVM, Licencias entity = null)
        {
            this.entity = entity;
            this.baseVM = baseVM;
        }

        public string Name
        {
            get
            {
                 return "Alta Licencia";
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
                        }
                    }

                    RaisePropertyChanged("IdFicheroValue");
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
                    }

                    RaisePropertyChanged("TipoFichero");
                }
            }
        }

        public Visibility MostrarBotones
        {
            get
            {
                if (entity.IdLicencia == 0)
                {
                    return Visibility.Collapsed;
                }
                return Visibility.Visible;
            }
        }

        protected override void LoadData()
        {
            base.LoadData();

            TipoFicheros = db.TipoFichero.ToList();

            var search = db.TipoLicencia.Where(m => m.FechaEliminacion == null).AsQueryable();
            TiposLicencia = search.ToList();

            if (entity.IdLicencia > 0)
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
                }

                TipoFichero = entity.IdTipoFicheroNavigation;
                TipoLicencia = entity.IdTipoLicenciaNavigation;
                NumeroExpediente = entity.NumeroExpediente;
                Descripcion = entity.Descripcion;
                Organismo = entity.Organismo;
                FechaConcesion = entity.FechaConcesion;
                FechaSolicitud = entity.FechaSolicitud;
                ArchivoDigital= entity.ArchivoDigital;
                Trazabilidad("Mantenimientos", "Licencias", Descripcion, "Consulta", "Ficha Licencias");
            }
            else
            {
                FechaConcesion = DateTime.Now;
                FechaSolicitud = DateTime.Now;
            }
        }

        protected override void ModifyData()
        {
            base.ModifyData();
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
                model.IdTipoFicheroNavigation = TipoFichero;
                model.IdTipoLicenciaNavigation = TipoLicencia;
                model.NumeroExpediente = NumeroExpediente;

                model.Descripcion = Descripcion;
                model.Organismo = Organismo;
                model.FechaConcesion = FechaConcesion;
                model.FechaSolicitud = FechaSolicitud;
                model.ArchivoDigital = ArchivoDigital;
                model.IdUsuarioNavigation = UserId;
                model.FechaSistema = DateTime.Now;
                model.IdFichero = entity.IdFichero;

                db.SaveChanges();

                Trazabilidad("Mantenimientos", "Licencias", Descripcion, accion, "Ficha Licencias");
                baseVM.ChangePageCommand.Execute(baseVM.PageViewModels.Where(m => m.Name == "Mantenimiento Licencia").FirstOrDefault());
            }
        }

        protected override void DeleteData()
        {
            base.DeleteData();

            if (entity != null)
            {
                var viewmodel = PageViewModels.Where(m => m.Name == "Eliminar Licencia").FirstOrDefault();
                viewmodel = new DeleteLicenciaVM(baseVM, entity);
                baseVM.CurrentPageViewModel = viewmodel;
            }
        }


        protected override void VolverListado()
        {
            base.VolverListado();
            baseVM.ChangePageCommand.Execute(baseVM.PageViewModels.Where(m => m.Name == "Mantenimiento Licencia").FirstOrDefault());
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
                    SetError(propertyName, "El campo Numero Expediente es obligatorio.");
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
