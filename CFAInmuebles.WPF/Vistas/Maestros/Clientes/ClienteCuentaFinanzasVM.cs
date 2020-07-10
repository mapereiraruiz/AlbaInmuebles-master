using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CFAInmuebles.WPF
{
    public class ClienteCuentaFinanzasVM : ObservableViewModel, IPageViewModel
    {
  
        public Terceros entity;

        private HomeClientesVM baseVM;
        public ClienteCuentaFinanzasVM(HomeClientesVM baseVM,  Terceros entity = null)
        {
            this.entity = entity;
            this.baseVM = baseVM;
        }
        public string Name
        {
            get
            {
                return "Cliente Cuenta Finanzas";
            }
        }
        protected override void LoadData()
        {
            base.LoadData();
           
            if (entity != null)
            {
                var cliente = db.ContratosClientes.Where(m => m.NombreCliente == entity.Nombre).FirstOrDefault();

                if (cliente != null)
                {
                    var context = dbsALTAI.Where(m => m.Schema == "CONT_" + cliente.IdEmpresaNavigation.EmpresaALTAI).FirstOrDefault();

                    if (context != null)
                    {
                        try
                        {
                            Apuntes = context.Apuntes.Where(m => m.Subcuenta == cliente.CuentaFianza || m.Contrapartida == cliente.CuentaFianza).ToList();
                        }

                        catch (Exception e)
                        {
                        }

                    }
                }

                Trazabilidad("Maestros", "Clientes", entity.Codigo.ToString(), "Consulta", "Mantenimiento Clientes Cuentas Fianzas");
            }

        }
    }
}
