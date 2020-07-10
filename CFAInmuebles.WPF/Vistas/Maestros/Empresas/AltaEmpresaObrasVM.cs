using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CFAInmuebles.Infrastructure.Data;
using System.Windows;

namespace CFAInmuebles.WPF
{
    public class AltaEmpresaObrasVM : ObservableViewModel, IPageViewModel
    {
        public HistorialObra entity;
        public Empresas entitybase;

        private FichaEmpresasVM baseVM;
        private string _descripcion;
        private DateTime? _fechaapertura;
        private DateTime? _fechacierre;
        private string _numeroexpediente;
        private string _presupuestoadjudicacion;
        private string _costefinal;
        private string _costeasumido;
        private bool _direccionfacultativasino;
        private string _direccionfacultativa;
       
        private bool _selectedItem;

        public AltaEmpresaObrasVM(FichaEmpresasVM baseVM, Empresas entitybase, HistorialObra entidad = null)
        {
            entity = new HistorialObra();

            if (entidad != null)
            {

                var properties = typeof(HistorialObra).GetProperties(BindingFlags.Instance | BindingFlags.Public)
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
                return "Alta Empresa Obras";
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
                    entity.Descripcion = _descripcion;
                    RaisePropertyChanged("Descripcion");
                }
            }
        }

        public DateTime? FechaApertura
        {
            get { return _fechaapertura; }
            set
            {
                if (_fechaapertura != value)
                {
                    _fechaapertura = value;
                    entity.FechaApertura = _fechaapertura;
                    RaisePropertyChanged("FechaApertura");
                }
            }
        }

        public DateTime? FechaCierre
        {
            get { return _fechacierre; }
            set
            {
                if (_fechacierre != value)
                {
                    _fechacierre = value;
                    entity.FechaCierre = _fechacierre;
                    RaisePropertyChanged("FechaCierre");
                }
            }
        }

        public string NumeroExpediente
        {
            get { return _numeroexpediente; }
            set
            {
                if (_numeroexpediente != value)
                {
                    _numeroexpediente = value;
                    entity.NumeroExpediente = _numeroexpediente;
                    RaisePropertyChanged("NumeroExpediente");
                }
            }
        }

        public string PresupuestoAdjudicacion
        {
            get
            {
                CheckValidationState("PresupuestoAdjudicacion", _presupuestoadjudicacion);
                return _presupuestoadjudicacion;
            }
            set
            {
                if (_presupuestoadjudicacion != value)
                {
                    CheckValidationState("PresupuestoAdjudicacion", _presupuestoadjudicacion);
                    _presupuestoadjudicacion = value;
                    if (decimal.TryParse(PresupuestoAdjudicacion, out decimal numValue))
                        entity.PresupuestoAdjudicacion = decimal.Parse(PresupuestoAdjudicacion);
                    RaisePropertyChanged("PresupuestoAdjudicacion");
                }
            }
        }

        public string CosteFinal
        {
            get
            {
                CheckValidationState("CosteFinal", _costefinal);
                return _costefinal;
            }
            set
            {
                if (_costefinal != value)
                {
                    CheckValidationState("CosteFinal", _costefinal);
                    _costefinal = value;
                    if (decimal.TryParse(CosteFinal, out decimal numValue))
                        entity.CosteFinal = decimal.Parse(CosteFinal);

                    RaisePropertyChanged("CosteFinal");
                }
            }
        }

        public string CosteAsumido
        {
            get
            {
                CheckValidationState("CosteAsumido", _costeasumido);
                return _costeasumido;
            }
            set
            {
                if (_costeasumido != value)
                {
                    CheckValidationState("CosteAsumido", _costeasumido);
                    _costeasumido = value;
                    if (decimal.TryParse(CosteAsumido, out decimal numValue))
                        entity.CosteAsumido = decimal.Parse(CosteAsumido);

                    RaisePropertyChanged("CosteAsumido");
                }
            }
        }

        public bool DireccionFacultativaSino
        {
            get { return _direccionfacultativasino; }
            set
            {
                if (_direccionfacultativasino != value)
                {
                    _direccionfacultativasino = value;
                    entity.DireccionFacultativaSino = _direccionfacultativasino;
                    RaisePropertyChanged("DireccionFacultativaSino");
                }
            }
        }

        public string DireccionFacultativa
        {
            get { return _direccionfacultativa; }
            set
            {
                if (_direccionfacultativa != value)
                {
                    _direccionfacultativa = value;
                    entity.DireccionFacultativa = _direccionfacultativa;
                    RaisePropertyChanged("DireccionFacultativa");
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
            TipoObras = db.TipoObra.ToList();
            TipoObras.Insert(0, new TipoObra { Valor = "Seleccione:" });

            ContratosProveedores = db.ContratosProveedores.Where(m => m.FechaEliminacion == null).DistinctBy(m => m.NombreProveedor).ToList();

            if (entity.IdHistorialObra > 0)
            {
                TipoFichero = entity.IdTipoFicheroNavigation;
                TipoObra = entity.IdTipoObraNavigation;
                ContratoProveedor = entity.IdContratoProveedorNavigation;
                Descripcion = entity.Descripcion;
                FechaApertura = entity.FechaApertura;
                FechaCierre = entity.FechaCierre;
                NumeroExpediente = entity.NumeroExpediente;
                PresupuestoAdjudicacion = entity.PresupuestoAdjudicacion.ToString();
                CosteFinal = entity.CosteFinal.ToString();
                CosteAsumido = entity.CosteAsumido.ToString();
                DireccionFacultativaSino = entity.DireccionFacultativaSino;
                DireccionFacultativa = entity.DireccionFacultativa;

                Trazabilidad("Mantenimientos", "Obras", Descripcion, "Consulta", "Ficha Obra");
            }
            else
            {
                FechaApertura = DateTime.Now;
                FechaCierre = DateTime.Now;
                DireccionFacultativaSino = false;
            }
        }

        protected override void ModifyData()
        {
            base.ModifyData();
            string accion = "Update";

            if (Errors.Count == 0)
            {

                var model = db.HistorialObra.Find(entity?.IdHistorialObra);

                if (model == null)
                {
                    model = new HistorialObra();
                    db.HistorialObra.Add(model);
                    accion = "Alta";
                }

                model.IdTipoFicheroNavigation = db.TipoFichero.Where(m => m.Valor == "Empresa").FirstOrDefault();
                model.IdContratoProveedorNavigation = ContratoProveedor;
                model.IdTipoObraNavigation = TipoObra;
                model.Descripcion = Descripcion;
                model.FechaApertura = FechaApertura;
                model.FechaCierre = FechaCierre;
                model.NumeroExpediente = NumeroExpediente;

                if (!String.IsNullOrEmpty(PresupuestoAdjudicacion))
                    model.PresupuestoAdjudicacion = decimal.Parse(PresupuestoAdjudicacion);

                if (!String.IsNullOrEmpty(CosteFinal))
                    model.CosteFinal = decimal.Parse(CosteFinal);

                if (!String.IsNullOrEmpty(CosteAsumido))
                    model.CosteAsumido = decimal.Parse(CosteAsumido);

                model.IdTipoObraNavigation = TipoObra;
                model.DireccionFacultativaSino = DireccionFacultativaSino;
                model.FechaSistema = DateTime.Now;
                model.DireccionFacultativa = DireccionFacultativa;
                model.IdFichero = entitybase.IdEmpresa;
                db.SaveChanges();

                if (accion == "Alta")
                {
                    var ficheroobra = new ObrasFichero();
                    ficheroobra.IdHistorialObra = model.IdHistorialObra;
                    ficheroobra.IdTipoFicheroNavigation = model.IdTipoFicheroNavigation;
                    ficheroobra.IdFichero = model.IdFichero;
                    db.ObrasFichero.Add(ficheroobra);
                }

                db.SaveChanges();
                Trazabilidad("Mantenimientos", "Historial Obras", accion, "Alta", "Ficha Obra");
                baseVM.ChangePageCommand.Execute(baseVM.PageViewModels.Where(m => m.Name == "Empresa Obras").FirstOrDefault());
            }
        }

        protected override void VolverListado()
        {
            base.VolverListado();

            var viewmodel = baseVM.PageViewModels.Where(m => m.Name == "Empresa Obras").FirstOrDefault();
            viewmodel = new EmpresaObrasVM(baseVM, entitybase);
            baseVM.CurrentPageViewModel = viewmodel;
       
        }

        protected bool CheckValidationState<T>(string propertyName, T proposedValue)
        {

            if (propertyName == "PresupuestoAdjudicacion")
            {

                if (!String.IsNullOrEmpty(proposedValue as String) && !decimal.TryParse(proposedValue as String, out decimal numValue))
                {
                    SetError(propertyName, "El campo Presupuesto Adjudicación debe ser numérico");
                    return false;
                }

                else
                {
                    SetError(propertyName, String.Empty);
                    return true;
                }
            }

            if (propertyName == "CosteFinal")
            {

                if (!String.IsNullOrEmpty(proposedValue as String) && !decimal.TryParse(proposedValue as String, out decimal numValue))
                {
                    SetError(propertyName, "El campo Coste Final debe ser numérico");
                    return false;
                }

                else
                {
                    SetError(propertyName, String.Empty);
                    return true;
                }
            }

            if (propertyName == "CosteAsumido")
            {

                if (!String.IsNullOrEmpty(proposedValue as String) && !decimal.TryParse(proposedValue as String, out decimal numValue))
                {
                    SetError(propertyName, "El campo Coste Asumido debe ser numérico");
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
