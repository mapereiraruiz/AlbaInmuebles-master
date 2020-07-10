using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CFAInmuebles.WPF
{
    public class ContratoClienteAvalFianzaVM : ObservableViewModel, IPageViewModel
    {
        public ContratosClientes entity;

        private HomeContratoClienteVM baseVM;
        private string _importeaval;
        public ContratoClienteAvalFianzaVM(HomeContratoClienteVM baseVM, ContratosClientes entity = null)
        {
            this.entity = entity;
            this.baseVM = baseVM;
        }
        public string Name
        {
            get
            {
                return "Contratos Clientes Aval y Fianzas";
            }
        }


        public string ImporteAval
        {
            get { return _importeaval; }
            set
            {
                if (_importeaval != value)
                {
                    _importeaval = value;                  
                    RaisePropertyChanged("ImporteAval");
                }
            }
        }

        protected override void LoadData()
        {
            base.LoadData();

            ImporteAval = entity.ImporteAval?.ToString() ?? "Sin Aval";

            if (entity != null)
            {
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
    }
}
