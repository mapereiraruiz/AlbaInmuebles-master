using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace CFAInmuebles.WPF
{
    public class AltaInmueblesJuridicoVM : ObservableViewModel, IPageViewModel, IDataErrorInfo
    {
        public Inmuebles entity;

        private HomeInmueblesVM baseVM;
        private DateTime? _fechacompra;
        private string _importecompra;
        private string _historialincidencia;
        private DateTime? _fechaventa;
        private string _importeventa;
        
        public AltaInmueblesJuridicoVM(HomeInmueblesVM baseVM, Inmuebles entity = null)
        {
            this.entity = entity;
            this.baseVM = baseVM;
        }
        public string Name
        {
            get
            {
                return "Alta Inmuebles Jurídico";
            }
        }

        public DateTime? FechaCompra
        {
            get
            {
                CheckValidationState("FechaCompra", _fechacompra);
                return _fechacompra;
            }
            set
            {
                if (_fechacompra != value)
                {
                    _fechacompra = value;
                    CheckValidationState("FechaCompra", _fechacompra);

                    if (FechaCompra != null)
                        entity.FechaCompra = _fechacompra ?? DateTime.Now;
                    RaisePropertyChanged("FechaCompra");
                }
            }
        }

        public string ImporteCompra
        {
            get
            {
                CheckValidationState("ImporteCompra", _importecompra);
                return _importecompra;
            }
            set
            {
                if (_importecompra != value)
                {
                    CheckValidationState("ImporteCompra", _importecompra);
                    _importecompra = value;

                    if (decimal.TryParse(ImporteCompra, out decimal numValue))
                        entity.ImporteCompra = decimal.Parse(ImporteCompra);
                    RaisePropertyChanged("ImporteCompra");
                }
            }
        }

        public string HistorialIncidencia
        {
            get { return _historialincidencia; }
            set
            {
                if (_historialincidencia != value)
                {
                    _historialincidencia = value;
                    entity.HistorialIncidencia = HistorialIncidencia;
                    RaisePropertyChanged("HistorialIncidencia");
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
                    entity.FechaVenta = FechaVenta;
                    RaisePropertyChanged("FechaVenta");
                }
            }
        }
        public string ImporteVenta
        {
            get
            {
                CheckValidationState("ImporteVenta", _importeventa);
                return _importeventa;
            }
            set
            {
                if (_importeventa != value)
                {
                    CheckValidationState("ImporteVenta", _importeventa);
                    _importeventa = value;

                    if (decimal.TryParse(ImporteVenta, out decimal numValue))
                        entity.ImporteVenta = decimal.Parse(ImporteVenta);
                    RaisePropertyChanged("ImporteVenta");
                }
            }
        }

        protected override void LoadData()
        {
            base.LoadData();

            if (entity != null)
            {
                FechaCompra = entity.FechaCompra;
                FechaVenta = entity.FechaVenta;
                HistorialIncidencia = entity.HistorialIncidencia;
                ImporteCompra = entity.ImporteCompra?.ToString();
                ImporteVenta = entity.ImporteVenta?.ToString();

                if (FechaCompra?.ToShortDateString() == "01/01/0001")
                    FechaCompra = DateTime.Now;
            }
        }

        protected bool CheckValidationState<T>(string propertyName, T proposedValue)
        {
            baseVM.entity = entity;
            baseVM.CheckValidationState(propertyName, proposedValue);

            if (propertyName == "FechaCompra")
            {
                if (proposedValue == null)
                {
                    SetError(propertyName, "El campo Fecha Compra es obligatorio");
                    return false;
                }

                else
                {
                    SetError(propertyName, String.Empty);
                    return true;
                }
            }

            if (propertyName == "ImporteCompra")
            {
                if (!String.IsNullOrEmpty(proposedValue as String) && !decimal.TryParse(proposedValue as String, out decimal numValue))
                {
                    SetError(propertyName, "El campo Importe Compra debe ser numérico.");                  
                    return false;
                }

                else
                {
                    SetError(propertyName, String.Empty);
                    return true;
                }
            }

            if (propertyName == "ImporteVenta")
            {

                if (!String.IsNullOrEmpty(proposedValue as String) && !decimal.TryParse(proposedValue as String, out decimal numValue))
                {
                    SetError(propertyName, "El campo Importe Venta debe ser numérico.");
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
