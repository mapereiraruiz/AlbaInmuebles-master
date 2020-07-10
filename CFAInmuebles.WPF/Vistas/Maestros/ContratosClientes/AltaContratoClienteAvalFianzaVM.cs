using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace CFAInmuebles.WPF
{
    public class AltaContratoClienteAvalFianzaVM : ObservableViewModel, IPageViewModel, IDataErrorInfo
    {
        public ContratosClientes entity;

        private HomeContratoClienteVM baseVM;
        private DateTime? _fechavencimiento;
        private string _importeaval;
        private string _comentario;
        private string _cuentafianza;

        public AltaContratoClienteAvalFianzaVM(HomeContratoClienteVM baseVM, ContratosClientes entity = null)
        {
            this.entity = entity;
            this.baseVM = baseVM;
        }
        public string Name
        {
            get
            {
                return "Alta Contratos Clientes Aval Fianza";
            }
        }       
        public int MaxComentario
        {
            get { return 50; }
        }
        public int MaxCuentaFianza
        {
            get { return 50; }
        }
        public string CuentaFianza
        {
            get { return _cuentafianza; }
            set
            {
                if (_cuentafianza != value)
                {
                    _cuentafianza = value;
                    entity.CuentaFianza = CuentaFianza;
                    RaisePropertyChanged("CuentaFianza");
                }
            }
        }


        public string Comentario
        {
            get { return _comentario; }
            set
            {
                if (_comentario != value)
                {
                    _comentario = value;
                    entity.Comentario = Comentario;
                    RaisePropertyChanged("Comentario");
                }
            }
        }

        public string ImporteAval
        {
            get {
                CheckValidationState("ImporteAval", _importeaval); 
                return _importeaval; }
            set
            {
                if (_importeaval != value)
                {
                    CheckValidationState("ImporteAval", _importeaval);
                    _importeaval = value;

                    if (decimal.TryParse(ImporteAval, out decimal numValue))
                        entity.ImporteAval = decimal.Parse(ImporteAval);
                    RaisePropertyChanged("ImporteAval");
                }
            }
        }
        public DateTime? FechaVencimiento
        {
            get { return _fechavencimiento; }
            set
            {
                if (_fechavencimiento != value)
                {
                    _fechavencimiento = value;
                    entity.FechaVencimiento = FechaVencimiento;
                    RaisePropertyChanged("FechaVencimiento");
                }
            }
        }
        protected override void LoadData()
        {
            base.LoadData();
       
            if (entity != null)
            {
                FechaVencimiento = entity.FechaVencimiento;               
                ImporteAval  = entity.ImporteAval.ToString();
                Comentario = entity.Comentario;
                CuentaFianza = entity.CuentaFianza;

                var context = dbsALTAI.Where(m => m.Schema == "CONT_" + entity.IdEmpresaNavigation.EmpresaALTAI).FirstOrDefault();

                if (context != null)
                {
                    try
                    {
                        Apuntes = context.Apuntes.Where(m => m.Subcuenta == entity.CuentaFianza || m.Contrapartida == entity.CuentaFianza).ToList();
                    }

                    catch (Exception e)
                    {
                    }

                }

                Trazabilidad("Maestros", "Contratos Clientes", entity.NombreCliente, "Consulta", "Mantenimiento Clientes Cuentas Fianzas");
               
            }
        }
        protected bool CheckValidationState<T>(string propertyName, T proposedValue)
        {
            baseVM.CheckValidationState(propertyName, proposedValue);

            if (propertyName == "ImporteAval")
            {

                if (!String.IsNullOrEmpty(proposedValue as String) && !decimal.TryParse(proposedValue as String, out decimal numValue))
                {
                    SetError(propertyName, "El campo Importe Aval debe ser numérico.");
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
