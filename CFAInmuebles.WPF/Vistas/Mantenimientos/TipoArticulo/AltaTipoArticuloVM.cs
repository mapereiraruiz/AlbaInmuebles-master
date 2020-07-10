using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace CFAInmuebles.WPF
{
    public class AltaTipoArticuloVM : ObservableViewModel, IPageViewModel, IDataErrorInfo
    {
        public TipoArticulos entity;

        private HomeTipoArticuloVM baseVM;
        private string _tipoarticulodes;
        private bool _alquilable;
        private string _coeficientehomogeneizacion;
        private TipoMedida _tipomedida;
        private TipoConceptoArticulo _tipoconceptoarticulo;
        private Clasificacion _clasificacion;
        private bool _selectedItem;
        
        public AltaTipoArticuloVM(HomeTipoArticuloVM baseVM, TipoArticulos entity = null)
        {
            this.entity = entity;
            this.baseVM = baseVM;
       }

        public string Name
        {
            get
            {
                return "Alta Tipo Artículo";
            }
        }

        public int MaxTipoArticulo
        {
            get
            {
                return 50;
            }
        }
        public string Tipoarticulo
        {
            get {
                CheckValidationState("Tipoarticulo", _tipoarticulodes); 
                return _tipoarticulodes; }
            set
            {
                if (_tipoarticulodes != value)
                {
                    CheckValidationState("Tipoarticulo", value);
                    _tipoarticulodes = value;
                    RaisePropertyChanged("Tipoarticulo");
                }
            }
        }
    
        public bool Alquilable
        {
            get { return _alquilable; }
            set
            {
                if (_alquilable != value)
                {
                    _alquilable = value;
                    RaisePropertyChanged("Alquilable");
                }
            }
        }

        public string CoeficienteHomogeneizacion
        {
            get {
                CheckValidationState("CoeficienteHomogeneizacion", _coeficientehomogeneizacion);
                return _coeficientehomogeneizacion; }
            set
            {
                if (_coeficientehomogeneizacion != value)
                {
                    CheckValidationState("CoeficienteHomogeneizacion", _coeficientehomogeneizacion);
                    _coeficientehomogeneizacion = value;
                    RaisePropertyChanged("CoeficienteHomogeneizacion");
                }
            }
        }

        public new TipoConceptoArticulo TipoConceptoArticulo
        {
            get
            {
                CheckValidationState("TipoConceptoArticulo", _tipoconceptoarticulo);
                return _tipoconceptoarticulo;
            }
            set
            {
                if (_tipoconceptoarticulo != value)
                {
                    CheckValidationState("TipoConceptoArticulo", _tipoconceptoarticulo);
                    _tipoconceptoarticulo = value;
                    RaisePropertyChanged("TipoConceptoArticulo");
                }
            }
        }

        public new Clasificacion Clasificacion
        {
            get
            {
                CheckValidationState("Clasificacion", _clasificacion);
                return _clasificacion;
            }
            set
            {
                if (_clasificacion != value)
                {
                    CheckValidationState("Clasificacion", _clasificacion);
                    _clasificacion = value;
                    RaisePropertyChanged("Clasificacion");
                }
            }
        }

        public new TipoMedida TipoMedida
        {
            get
            {
                CheckValidationState("TipoMedida", _tipomedida);
                return _tipomedida;
            }
            set
            {
                if (_tipomedida != value)
                {
                    CheckValidationState("TipoMedida", _tipomedida);
                    _tipomedida = value;
                    RaisePropertyChanged("TipoMedida");
                }
            }
        }
        public Visibility MostrarBotones
        {
            get
            {
                if (entity.IdTipoArticulo == 0)
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
            TipoMedidas = db.TipoMedida.ToList();
            TipoConceptoArticulos = db.TipoConceptoArticulo.ToList();
            Clasificaciones = db.Clasificacion.ToList();
           
            if (entity.IdTipoArticulo > 0)
            {
                Tipoarticulo = entity.Tipoarticulo;
                Alquilable = entity.Alquilable;
                CoeficienteHomogeneizacion = entity.CoeficienteHomogeneizacion.ToString();
                TipoMedida = entity.IdTipoMedidaNavigation;
                TipoConceptoArticulo = entity.IdTipoConceptoArticuloNavigation;
                Clasificacion = entity.IdClasificacionNavigation;
                Trazabilidad("Mantenimientos", "Tipo Artículo", Tipoarticulo, "Consulta", "Ficha Tipo Artículo");
            }
        }
        
       protected override void ModifyData()
       {
            base.ModifyData();
            string accion = "Update";

            if (Errors.Count == 0)
            {

                var model = db.TipoArticulos.Find(entity?.IdTipoArticulo);

                if (model == null)
                {
                    model = new TipoArticulos();
                    db.TipoArticulos.Add(model);
                    accion = "Alta";
                }

                model.Tipoarticulo = Tipoarticulo;
                model.Alquilable = Alquilable;
                model.CoeficienteHomogeneizacion = double.Parse(CoeficienteHomogeneizacion?.Replace('.', ','));
                model.IdTipoMedidaNavigation = TipoMedida;
                model.IdTipoConceptoArticuloNavigation = TipoConceptoArticulo;
                model.IdClasificacionNavigation = Clasificacion;
                model.FechaSistema = DateTime.Now;
                model.IdUsuarioNavigation = UserId;

                db.SaveChanges();
                Trazabilidad("Mantenimientos", "Tipo Artículo", Tipoarticulo, accion, "Ficha Tipo Artículo");
                baseVM.ChangePageCommand.Execute(baseVM.PageViewModels.Where(m => m.Name == "Mantenimiento Tipo Artículo").FirstOrDefault());
            }
       }

        protected override void DeleteData()
        {
            base.DeleteData();

            //Comprobamos que no tenga ninguna entidad relacionada vinculada
            var articulos = db.Articulos.Where(m => m.IdTipoArticulo == entity.IdTipoArticulo && m.FechaEliminacion == null).Any();
            var historicofacturacion = db.HistoricoFacturacionSuperficie.Where(m => m.IdTipoArticulo == entity.IdTipoArticulo).Any();
           
            if (!articulos && !historicofacturacion)
            {
                var viewmodel = PageViewModels.Where(m => m.Name == "Eliminar Tipo Artículo").FirstOrDefault();
                viewmodel = new DeleteTipoArticuloVM(baseVM, entity);
                baseVM.CurrentPageViewModel = viewmodel;
            }
            else
            {
                Mensaje = String.Empty;

                if (articulos)
                    Mensaje += "Desvincule los artículos vinculados a este Tipo Artículo para poder eliminarlo." + Environment.NewLine;
                if (historicofacturacion)
                    Mensaje += "Desvincule los históricos vinculados a este Tipo Artículo para poder eliminarlo.";

            }
        }

        protected override void VolverListado()
        {
            base.VolverListado();
            baseVM.ChangePageCommand.Execute(baseVM.PageViewModels.Where(m => m.Name == "Mantenimiento Tipo Artículo").FirstOrDefault());
        }

        protected bool CheckValidationState<T>(string propertyName, T proposedValue)
        {
            if (propertyName == "Tipoarticulo")
            {
                var existe = db.TipoArticulos.Where(m => m.Tipoarticulo == proposedValue as String && m.FechaEliminacion == null).FirstOrDefault();

                if (String.IsNullOrEmpty(proposedValue as String))
                {
                    SetError(propertyName, "El campo Tipo Artículo es obligatorio.");
                    return false;
                }

                else if (existe != null && existe.IdTipoArticulo != entity.IdTipoArticulo)
                {
                    SetError(propertyName, "Ya existe un Tipo Artículo con ese valor.");
                    return false;
                }

                else
                {
                    SetError(propertyName, String.Empty);
                    return true;
                }
            }

            if (propertyName == "CoeficienteHomogeneizacion")
            {
                if (String.IsNullOrEmpty(proposedValue as String))
                {
                    SetError(propertyName, "El campo Coeficiente Homogeneización es obligatorio.");
                    return false;
                }


                else if (!double.TryParse(proposedValue as String, out double numValue))
                {
                    SetError(propertyName, "El campo Coeficiente Homogeneización debe ser numérico.");
                    return false;
                }

                else
                {
                    SetError(propertyName, String.Empty);
                    return true;
                }
            }

            if (propertyName == "TipoMedida")
            {

                if (proposedValue == null)
                {
                    SetError(propertyName, "El campo Tipo Medida es obligatorio.");
                    return false;
                }

                else
                {
                    SetError(propertyName, String.Empty);
                    return true;
                }
            }

            if (propertyName == "TipoConceptoArticulo")
            {

                if (proposedValue == null)
                {
                    SetError(propertyName, "El campo Tipo Concepto Artículo es obligatorio.");
                    return false;
                }

                else
                {
                    SetError(propertyName, String.Empty);
                    return true;
                }
            }

            if (propertyName == "Clasificacion")
            {

                if (proposedValue == null)
                {
                    SetError(propertyName, "El campo Clasificación es obligatorio.");
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
