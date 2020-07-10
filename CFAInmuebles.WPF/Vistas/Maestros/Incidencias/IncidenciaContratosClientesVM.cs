using CFAInmuebles.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CFAInmuebles.WPF
{
    public class IncidenciaContratosClientesVM : ObservableViewModel, IPageViewModel
    {
        public Incidencias entity;

        private HomeIncidenciasVM baseVM;
        public IncidenciaContratosClientesVM(HomeIncidenciasVM baseVM, Incidencias entity = null)
        {
            this.entity = entity;
            this.baseVM = baseVM;
        }
        public string Name
        {
            get
            {
                return "Incidencia Contratos Clientes";
            }
        }
        protected override void LoadData()
        {
            base.LoadData();
            
            if (entity.IdIncidencia > 0)
            {
                ContratosClientes = db.ContratosClientes.Where(m => m.FechaEliminacion == null).ToList();

                //Comprobamos que contratos puede ver el usuario en función del inmueble
                if (!UserId.Administrador)
                {
                    var inmueble = db.UsuarioInmueble.Where(m => m.IdUsuario == UserId.IdUsuario).Select(m => m.IdInmueble).ToList();
                    ContratosClientes = ContratosClientes.Where(m => inmueble.Contains(m.IdInmueble)).ToList();
                    
                }
            }
        }
    }
}
