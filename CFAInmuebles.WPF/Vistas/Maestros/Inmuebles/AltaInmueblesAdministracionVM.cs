using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace CFAInmuebles.WPF
{
    public class AltaInmueblesAdministracionVM : ObservableViewModel, IPageViewModel, IDataErrorInfo
    {
        public Inmuebles entity;

        private HomeInmueblesVM baseVM;
        private string _tasacapitalizacion;
        private string  _valorplazagaraje;

        public AltaInmueblesAdministracionVM(HomeInmueblesVM baseVM, Inmuebles entity)
        {
            this.entity = entity;
            this.baseVM = baseVM;           
        }
        public string Name
        {
            get
            {
                return "Alta Inmuebles Administración";
            }
        }

        private ConceptoFacturacion _concepto;

        public new ConceptoFacturacion ConceptoFacturacion
        {
            get { return _concepto; }
            set
            {
                if (_concepto != value)
                {
                    _concepto = value;
                    entity.IdConceptoFacturacionNavigation = ConceptoFacturacion;
                    RaisePropertyChanged("ConceptoFacturacion");
                }
            }
        }

        private TipoArticulos _tipoarticulo;

        public new TipoArticulos TipoArticulo
        {
            get { return _tipoarticulo; }
            set
            {
                if (_tipoarticulo != value)
                {
                    _tipoarticulo = value;
                    entity.IdTipoArticuloNavigation = TipoArticulo;
                    RaisePropertyChanged("TipoArticulo");
                }
            }
        }


        public string TasaCapitalizacion
        {
            get {
                CheckValidationState("TasaCapitalizacion", _tasacapitalizacion); 
                return _tasacapitalizacion; }
            set
            {
                if (_tasacapitalizacion != value)
                {
                    CheckValidationState("TasaCapitalizacion", _tasacapitalizacion);
                    _tasacapitalizacion = value;
                    if (double.TryParse(TasaCapitalizacion, out double numValue))
                        entity.TasaCapitalizacion = double.Parse(TasaCapitalizacion);
                    RaisePropertyChanged("TasaCapitalizacion");
                }
            }
        }

        public string ValorPlazaGaraje
        {
            get {
                CheckValidationState("ValorPlazaGaraje", _valorplazagaraje); 
                return _valorplazagaraje; }
            set
            {
                if (_valorplazagaraje != value)
                {
                    CheckValidationState("ValorPlazaGaraje", _valorplazagaraje);
                    _valorplazagaraje = value;

                    if (decimal.TryParse(ValorPlazaGaraje, out decimal numValue))
                        entity.ValorPlazaGaraje = decimal.Parse(ValorPlazaGaraje);
                    RaisePropertyChanged("ValorPlazaGaraje");
                }
            }
        }

        protected override void LoadData()
        {
            base.LoadData();

            ConceptosFacturacion = db.ConceptoFacturacion.Where(m => m.FechaEliminacion == null).ToList();
            TipoArticulos = db.TipoArticulos.Where(m => m.FechaEliminacion == null).ToList();

            if (entity != null)
            {
                TasaCapitalizacion = entity.TasaCapitalizacion?.ToString();
                ValorPlazaGaraje = entity.ValorPlazaGaraje?.ToString();
                ConceptoFacturacion = entity.IdConceptoFacturacionNavigation;
                TipoArticulo = entity.IdTipoArticuloNavigation;

            }
        }

        protected bool CheckValidationState<T>(string propertyName, T proposedValue)
        {
            baseVM.entity = entity;
            baseVM.CheckValidationState(propertyName, proposedValue);

            if (propertyName == "TasaCapitalizacion")
            {
                if (!String.IsNullOrEmpty(proposedValue as String) && !double.TryParse(proposedValue as String, out double numValue))
                {
                    SetError(propertyName, "El campo Tasa Capitalización debe ser numérico.");
                    return false;
                }

                else
                {
                    SetError(propertyName, String.Empty);
                    return true;
                }
            }

            if (propertyName == "ValorPlazaGaraje")
            {

                if (!String.IsNullOrEmpty(proposedValue as String) && !decimal.TryParse(proposedValue as String, out decimal numValue))
                {
                    SetError(propertyName, "El campo Valor Plaza Garaje debe ser numérico.");
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
